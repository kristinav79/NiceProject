using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using TestProject.Data;

namespace TestProject.TestHelpers
{
    public static class OutPutFileContent
    {
        public static int FindCount(string symbol)
        {

            var file = DirectorySetup.GetPath(ConfigurationManager.AppSettings[InputData.OutputFilePath]);
           // OutPutFileContent content = new OutPutFileContent();
            var lines = File.ReadAllLines(file);
            string[] oneLineData;
            var barNumber = 0;
            for (int i = 1; i < lines.Length; i++)
            {
                oneLineData = lines[i].Split(new[] { ',' });
                var resultString = Regex.Replace(oneLineData[0], "\"", "");
                if (resultString == symbol)
                {
                   
                    ScenarioContext.Current["count"] = oneLineData[1];
                    barNumber= i;
                    break;
                }

            }
            return barNumber;
        }
    }
}

