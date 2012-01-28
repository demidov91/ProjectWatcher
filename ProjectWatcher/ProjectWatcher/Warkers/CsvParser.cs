using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using DAL.Interface;
using SystemSettings;

namespace ProjectWatcher.Warkers
{
    public class CsvParser
    {
        protected StreamReader stream;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        internal CsvParser(Stream stream)
        {
            this.stream = new StreamReader(stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Null in case of bad csv format.</returns>
        internal Dictionary<int, Evaluation> GetValuesForProjects()
        {
            Dictionary<int, Evaluation> toReturn = new Dictionary<int, Evaluation>();
            IEnumerable<String> headers = ParseForHeaders(stream.ReadLine());
            if (headers == null || headers.Count() == 0 || headers.ElementAt(0) != "ProjectId")
            {
                return null;
            }
            while (!stream.EndOfStream)
            {
                String projectFromFile = stream.ReadLine();
                KeyValuePair<int, Evaluation> projectEvaluation = ReadDictionaryOfEvaluations(projectFromFile, headers.ToArray());
                if (projectEvaluation.Value != null)
                {
                    toReturn.Add(projectEvaluation.Key, projectEvaluation.Value);
                }
            }
            return toReturn;
        }

        private IEnumerable<string> ParseForHeaders(string line)
        {
            return line.Split(';', ' ').Where(x => x.Length > 0);
        }

        private KeyValuePair<int, Evaluation> ReadDictionaryOfEvaluations(string projectFromFile, String[] headers)
        {
            String[] valuesFromFile = projectFromFile.Split(';').ToArray();
            if(valuesFromFile.Length != headers.Length)
            {
                return new KeyValuePair<int,Evaluation>();
            }
            for (int i = 0; i < valuesFromFile.Length; i++)
            {
                valuesFromFile[i] = valuesFromFile[i].FileFormatvalueIntoProgramFormat();
            }
            Int32 projectId = 0;
            if(!Int32.TryParse(valuesFromFile[0], out projectId))
            {
                return new KeyValuePair<int, Evaluation>();
            }
            Evaluation evaluation = new Evaluation();
            for (int i = 1; i < headers.Length; i++ )
            {
                evaluation.Values.Add(headers[i], valuesFromFile[i]);                
            }
            return new KeyValuePair<int, Evaluation>(projectId, evaluation);
        }

        
    }
    
}