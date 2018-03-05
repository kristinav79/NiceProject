using NUnit.Framework;
using System.Configuration;
using System.IO;
using TechTalk.SpecFlow;
using TestProject.Data;
using TestProject.TestHelpers;

namespace TestProject.StepDefinitions
{
    [Binding]
    public sealed class BlackBoxStepTestDefinitions
    {

        [Given(@"the output folder is empty")]
        public void GivenTheOutputFolderIsEmpty()
        {
            var outputZone = DirectorySetup.GetPath(ConfigurationManager.AppSettings[InputData.OutputZone]);
            DirectoryInfo directoryInfo = new DirectoryInfo(outputZone);
            directoryInfo.Empty();
        }

        [Given(@"BlackBox program is executed")]
        [When(@"BlackBox program is executed")]
        public void WhenBlackBoxProgramIsExecuted()
        {
            var dropZone = DirectorySetup.GetPath(ConfigurationManager.AppSettings[InputData.DropZone]);
            var outputZone = DirectorySetup.GetPath(ConfigurationManager.AppSettings[InputData.OutputZone]);
            var program = DirectorySetup.GetPath(ConfigurationManager.AppSettings[InputData.BlackBoxProgram]);
            var command = $"\" java -jar  \"{program}\"   \"{dropZone}\"  \"{outputZone}\"\"";
            var execution = new CommandExecution();
            ScenarioContext.Current[ContextKey.Log] = execution.Execute(command, true);

        }
        [Then(@"output file is not empty")]
        public void ThenOutputFileIsNotEmpty()
        {
            string outputFile = DirectorySetup.GetPath(ConfigurationManager.AppSettings[InputData.OutputFilePath]);
            Assert.AreNotEqual(0, new FileInfo(outputFile).Length, "The file exist, but it is empty");
        }
    }
}
