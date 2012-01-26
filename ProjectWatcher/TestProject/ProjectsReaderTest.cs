using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;


namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for ProjectsReaderTest and is intended
    ///to contain all ProjectsReaderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProjectsReaderTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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
            DAL.Helpers.ConnectionHelper.LoadORM();
            Validation.TypeValidationHelper.LoadTypes();
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
        #endregion


        [TestMethod()]
        public void GetPropertiesDefinitionsTest()
        {
            ProjectsReader target = new ProjectsReader(); // TODO: Initialize to an appropriate value
            int projectId = 0; // TODO: Initialize to an appropriate value
            Property[] actual;
            actual = target.GetPropertiesDefinitions(projectId);
            Assert.IsNotNull(actual);
            foreach (Property property in actual)
            {
                Assert.IsTrue(property.Name.Length > 0);
                Assert.IsTrue(property.SystemName != null);
                Assert.IsTrue(Regex.IsMatch(property.SystemName, @"\A\w{1,}\z"));
                Assert.IsTrue(property.Type == "String" || property.Type == "Select" || property.Type == "Multyselect" || property.Type == "Number" || property.Type == "Percentage");
            }
        }

        [TestMethod()]
        public void CreateNewPropertyTest1()
        {
            ProjectsReader target = new ProjectsReader(); // TODO: Initialize to an appropriate value
            string name = "Можно даже кирилическое название";
            string systemName = "latin_only";
            string type = "Percentage";
            target.CreateNewProperty(name, systemName, type, null);
        }

        [TestMethod()]
        public void CreateNewPropertyTest2()
        {
            ProjectsReader target = new ProjectsReader(); // TODO: Initialize to an appropriate value
            string type = "Percentage";
            string name = "Name";
            string systemName = "it should be single word";
            Object forException = null;
            try
            {
                target.CreateNewProperty(name, systemName, type, null);
            }
            catch (Exception e)
            {
                forException = e;
            }
            Assert.IsTrue(forException is BadSystemNameException);
        }

        [TestMethod()]
        public void CreateNewPropertyTest3()
        {
            ProjectsReader target = new ProjectsReader(); // TODO: Initialize to an appropriate value
            string name = "Можно даже кирилическое название";
            string type = "Percentage";
            string systemName = "hello,world";
            Object forException = null;
            try
            {
                target.CreateNewProperty(name, systemName, type, null);
            }
            catch (Exception e)
            {
                forException = e;
            }
            Assert.IsTrue(forException is BadSystemNameException);
        }


        [TestMethod()]
        public void GetProjectTest()
        {
            ProjectsReader target = new ProjectsReader(); 
            int projectId = 5; 
            Project actual;
            actual = target.GetProject(projectId);
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Owner);            
        }
    }
}
