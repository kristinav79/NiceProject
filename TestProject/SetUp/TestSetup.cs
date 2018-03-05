using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TestProject.Data;
using TestProject.TestHelpers;

namespace TestProject.SetUp
{
    [Binding]
    public sealed class TestSetup
    {

        [AfterTestRun]
        public static void AfterTestRun()
        {
            CommandExecution execution = new CommandExecution();
            var command = "taskkill /im node.exe /F";
            var processExecutionResult = execution.Execute(command, false);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.ContainsKey(ContextKey.Driver))
            {
                var driver = (ChromeDriver)ScenarioContext.Current[ContextKey.Driver];
                driver.Close();
            }

            CommandExecution execution = new CommandExecution();
            var command = "taskkill /im node.exe /F";
            var processExecutionResult = execution.Execute(command, false);
        }
    }
}
