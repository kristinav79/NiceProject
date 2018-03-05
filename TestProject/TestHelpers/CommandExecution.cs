using System.Diagnostics;
using TechTalk.SpecFlow;


namespace TestProject.TestHelpers
{
    class CommandExecution
    {
        public string Execute(string command, bool streamRead)
        {
            string errors = string.Empty;
            string result = string.Empty;
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            startInfo.Arguments = "/C " + command;
            process.StartInfo = startInfo;

            try
            {
                process.Start();
                if (streamRead)
                {
                    result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                }
                else
                {
                    result = process.StandardOutput.ReadLine();
                }

            }
            catch
            {
                errors = process.StandardError.ReadToEnd();
            }
            ScenarioContext.Current[TestProject.Data.ContextKey.Process] = process;
            return errors == string.Empty ? result : errors;
        }
    }
        
}
