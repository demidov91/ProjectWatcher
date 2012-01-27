using NMock2.Actions;
using ProjectWatcher.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using ProjectWatcher.Models.Projects;
using System.Collections.Generic;
using DAL;
using System.Security.Principal;
using NMock2;
using System.Web;
using DAL.Interface;

namespace TestProject
{
    
    
    [TestClass()]
    public class ProjectsHelperTest
    {


        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            /*MockRepository repository = new MockRepository();
            HttpCookieCollection cookies = repository.CreateMock<HttpCookieCollection>();
            Expect.Call(cookies["culture"]).Return(new HttpCookie("culture", "en-US"));
            HttpContextBase contextMock = repository.CreateMock<HttpContextBase>();
            Expect.Call(contextMock.Request.Cookies).Return(cookies);
            context = contextMock;*/
            ProjectWatcher.Helpers.ResourcesHelper.LoadResourses();            
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        private String exampleTableDefinition = @"Header1,     **[%some%](%link%)**,   String,  18
Header2,    $if(%ut_coverage% > 50\, %ut_coverage%, %false%), Enumeration, 20";
        private String exampleFilter = @"%ut_coverage%<90 || %platform%=='.NET'";
        
        private String engCulture = "en-US";

        private static HttpContextBase context = null;

        #endregion


        

        [TestMethod()]
        public void StringToColumnExceptionTest()
        {
            string comaSeparatedProperties = @"Header,     *[some](formula)*,   String,  18, 15";
            ArgumentException expected = new ArgumentException();
            Object actual;
            try
            {                
                actual = ProjectsHelper_Accessor.StringToColumn(comaSeparatedProperties, null);
            }
            catch(ArgumentException e)
            {
                actual = e; 
            }
            Assert.IsTrue(actual.GetType().Equals(expected.GetType()));
        }

        [TestMethod()]
        public void GetFuncAndVarTest()
        {
            string tableDefinition = exampleTableDefinition;
            List<String> functions = new List<String>();
            List<String> variables = new List<String>();
            ProjectsHelper_Accessor.GetFunctionsAndVariables(tableDefinition, functions, variables);
            Assert.IsTrue(functions.Count == 1 && variables.Count == 2);
        }

        [TestMethod()]
        public void RequestBuilderTest()
        {
            RequestBuilder builder = new RequestBuilder();
            builder.Filter = @"%ut_coverage%<90 || %platform%=='.NET'";
            builder.SqlFunctions = new string[] { @"if(%prop1%>1\, %prop2%\, 42)", @"min(%prop1%\,%prop2%)"};
            builder.Variables = new string[] { "ut_coverage", "prop1"};
            Evaluation[] values = new Evaluation[0];
            try
            {
                values = builder.GetValues();
            }
            catch (Exception e)
            {
                if (!(e is ArgumentException))
                {
                    Assert.Fail();
                }
            }
            foreach (Evaluation oneProjectVars in values)
            {
                Assert.IsNotNull(oneProjectVars.Formulas[@"if(%prop1%>1\, %prop2%\, 42)"]);
                Assert.IsNotNull(oneProjectVars.Formulas[@"min(%prop1%\,%prop2%)"]);
                Assert.IsNotNull(oneProjectVars.Values["prop1"]);
                Assert.IsNotNull(oneProjectVars.Values["ut_coverage"]);
                int coverage = 0;
                Assert.IsTrue(Int32.TryParse(oneProjectVars.Values["ut_coverage"], out coverage));
                Assert.IsTrue(coverage < 90);
            }

        }


        [TestMethod()]
        public void CreateFilterModelTest()
        {
            string filter = "%ut_coverage%>80";
            string tableDefinition = exampleTableDefinition;
            string culture = "en-US";
            Object result = ProjectsHelper.CreateFilterModel(filter, tableDefinition, culture);
            Assert.IsNotNull(result as FilterModel);
        }

        [TestMethod()]
        public void CreateFooterModelTest()
        {
            Mockery mockery = new NMock2.Mockery();
            HttpContextWarker mockContext = mockery.NewMock<HttpContextWarker>();
            IPrincipal mockUser = mockery.NewMock<IPrincipal>();
            NMock2.Expect.Once.On(mockUser).Method(new NMock2.Matchers.MethodNameMatcher("IsInRole")).With(new String[] { "administrator" }).Will(NMock2.Return.Value(true));
            NMock2.Expect.AtLeast(0).On(mockContext).GetProperty("User").Will(new ReturnAction(mockUser));
            NMock2.Expect.AtLeast(0).On(mockContext).Method("GetCulture").Will(new ReturnAction("en-EN"));
            string culture = engCulture;
            String filter = "";
            String tableDefinition = "";
            FooterModel actual = ProjectsHelper.CreateFooterModel(filter, tableDefinition, null, mockContext);
            Assert.IsNotNull(actual);
        }

        
        [TestMethod()]
        public void DeleteFunctionsTest()
        {
            string input = @"[$max(%prop1%\,%prop2%)](%link%)";
            string expected = @"[](%link%)";
            string actual;
            actual = ProjectsHelper_Accessor.DeleteFunctions(input);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void GetFunctionsTest()
        {
            string tableDefinition = exampleTableDefinition;
            List<string> functions = new List<string>();
            ProjectsHelper_Accessor.GetFunctions(tableDefinition, functions);
            Assert.IsTrue(functions.Count == 1 && functions[0] == @"if(%ut_coverage% > 50\, %ut_coverage%, %false%)");            
        }

        [TestMethod()]
        public void GetFunctionsAndVariablesTest()
        {
            string tableDefinition = @"[$max(%prop1%\,%prop2%)](%link%)";
            List<string> functions = new List<string>();
            List<string> variables = new List<string>();
            ProjectsHelper_Accessor.GetFunctionsAndVariables(tableDefinition, functions, variables);
            Assert.IsTrue(functions.Count == 1 && variables.Count == 1);
            Assert.IsTrue(functions[0] == @"max(%prop1%\,%prop2%)");
            Assert.IsTrue(variables[0] == @"link");
        }

        

        [TestMethod()]
        public void GetVariablesTest()
        {
            string tableWithNoFunctions = @"Name, [%property%](%link_value%), type, 18";
            List<string> variables = new List<string>();
            ProjectsHelper_Accessor.GetVariables(tableWithNoFunctions, variables);
            Assert.IsTrue(variables.Count == 2);
            Assert.IsTrue(variables[0] == "property" && variables[1] == "link_value");
        }

        
        [TestMethod()]
        public void SetValuesIntoFormulasTest()
        {
            Evaluation[] valuesForEach = new Evaluation[2];
            valuesForEach[0] = new Evaluation();
            valuesForEach[0].Formulas.Add(@"avg(\)", "42");
            valuesForEach[0].Values.Add(@"var", "2");
            valuesForEach[1] = new Evaluation();
            valuesForEach[1].Formulas.Add(@"avg(%prop_1%\,%prop_2%)", "5");
            valuesForEach[1].Values.Add(@"var", "7");
            ColumnDefinition[] columns = new ColumnDefinition[2];
            columns[0] = new ColumnDefinition { Header = "first", Formula = @"$avg(\)", Type = "Percentage", Width = 5 };
            columns[1] = new ColumnDefinition { Header = "second", Formula = @"%var%", Type = "Number", Width = 10 };
            ProjectModel[] result = ProjectsHelper.SetValuesIntoFormulas(valuesForEach, columns);
            Assert.IsTrue(result.Length == 2);
            Assert.IsTrue(result[0].Properties[0] == "42%");
            Assert.IsTrue(result[1].Properties[1] == "7");            
        }

        [TestMethod()]
        public void StringToColumnTest()
        {
            string comaSeparatedProperties = @"Header,     *[some](formula)*,   String,  18";
            ColumnDefinition expected = new ColumnDefinition { Header = "Header", Formula = "*[some](formula)*", Type = "String", Width = 18 };
            ColumnDefinition actual;
            try
            {
                actual = ProjectsHelper_Accessor.StringToColumn(comaSeparatedProperties, context);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentException);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateExportDataTest()
        {
            TableModel model = new TableModel();
            String[] headers = { "Header1", "Header2", "Header3" };
            model.Headers = headers;
            model.Projects =new ProjectModel[] { new ProjectModel {Properties= new String[]{ "1", "2", "3"} }, new ProjectModel {Properties=new String[]{ "4", "5", "6"} } };
            string expected = "Header1;Header2;Header3;\n1;2;3;\n4;5;6;\n";
            string actual;
            actual = ProjectsHelper.CreateExportData(model);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void FormCSVFileTest()
        {
            string[] headers = { "Header1", "Header2", "Header3" };
            string[][] projects = new String[][] { new String[] { "1", "2", "3" }, new String[] { "4", "5", "6" } };
            string expected = "Header1;Header2;Header3;\n1;2;3;\n4;5;6;\n";
            string actual;
            actual = ProjectsHelper_Accessor.FormCSVFile(headers, projects);
            Assert.AreEqual(expected, actual);
        }
    }
}
