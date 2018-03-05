
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using TechTalk.SpecFlow;
using TestProject.TestHelpers;
using TestProject.Data;

namespace TestProject.StepDefinitions
{
    [Binding]
    public sealed class UIServerTestStepDefinition
    {
 
        [When(@"UI server is started")]
        public void WhenNodeServerIsStarted()
        {
          
            var nodeServer = ConfigurationManager.AppSettings[InputData.UIServer];
            var command = $"cd {DirectorySetup.GetPath(ConfigurationManager.AppSettings[InputData.UIServerFolder])} & node {nodeServer}";
            var execution = new CommandExecution();
            var portCorrect = int.TryParse(ConfigurationManager.AppSettings[InputData.Port], out int port );
            var host = ConfigurationManager.AppSettings[InputData.Host];
            if (!ServiceCheck.IsListening(host, port))
            {
              ScenarioContext.Current[ContextKey.Log] = execution.Execute(command,false);
            }
            else Assert.Fail("The server is already running");
        }


        [Given(@"server is NOT running on host ""(.*)"" with port ""(.*)""")]
        public void GivenServerIsNOTRunningOnHostWithPort(string host, int port)
        {
          
            var isServerUp = ServiceCheck.IsListening(host, port);
            Assert.IsFalse(isServerUp, $"Service on host {host} and port {port} is running");

        }

        [Then(@"server is running on host ""(.*)"" with port ""(.*)""")]
        [Given(@"server is running on host ""(.*)"" with port ""(.*)""")]
        public void GivenServerIsRunningOnHostWithPort(string host, int port)
        {
            var isServerUp = ServiceCheck.IsListening(host, port);
            Assert.IsTrue(isServerUp, $"Service on host {host} and port {port} is not running");
        }

        [Given(@"the output file has been created")]
        [Then(@"the output file has been created")]
        public void ThenTheOutputFileHasBeenCreated()
        {
            var outputFile = DirectorySetup.GetPath(ConfigurationManager.AppSettings[InputData.OutputFilePath]);
            Assert.IsTrue(File.Exists(outputFile), "Something wrong, the output file haven't been created");
        }

        [Then(@"program successfully finished to run with log: ""(.*)""")]
        public void ThenProgramSuccessfullyFinishedToRun(string successIndicator)
        {
            string logText = (string)ScenarioContext.Current[ContextKey.Log];
            var found = logText.Contains(successIndicator);
            Assert.IsTrue(found, "Something wrong with the system, please check");
        }

        [Then(@"the page title  ""(.*)"" has been displayed")]
        public void ThenPageTitleHasBeenDisplayed(string title)
        {
            var url = ConfigurationManager.AppSettings[InputData.Url];
            var driver = new ChromeDriver(DirectorySetup.GetPath(ConfigurationManager.AppSettings[InputData.Drivers]));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl(url);
            Thread.Sleep(200);

            Assert.AreEqual(title,driver.FindElement(By.XPath("/html/body/h1")).Text, $"The page {url} wasn't loaded correctly, the title is incorrect , please check");
            ScenarioContext.Current[ContextKey.Driver] = driver;
        }


        [Then(@"the chart with id ""(.*)"" has been displayed")]
        public void ThenChartHasBeenDisplayed(string elementId)
        {
            var url = ConfigurationManager.AppSettings[InputData.Url];
            var driver = new ChromeDriver(DirectorySetup.GetPath(ConfigurationManager.AppSettings[InputData.Drivers]));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(url);
            Assert.IsTrue(driver.FindElement(By.Id(elementId)).Displayed, $"The page {url} wasn't loaded correctly, please check");
            ScenarioContext.Current[ContextKey.Driver] = driver;
        }

        [Then(@"UI server started successfully with string in log: ""(.*)""")]
        public void ThenUIServerStartedSuccessfullyWithStringInLog(string successIndicator)
        {
            string logText = (string)ScenarioContext.Current["Log"];
            var found = logText.Contains(successIndicator);
            Assert.IsTrue(found, $"There is errors in system:{logText}");
        }
       

        [Then(@"chart bar with letter ""(.*)"" value is ""(.*)""")]
        public void ThenChartBarWithLetterValueIs(string barLetter, string value)
        {

            var barNumber = OutPutFileContent.FindCount(barLetter);
            var driver = (ChromeDriver)ScenarioContext.Current[ContextKey.Driver];

            try
            {
               Thread.Sleep(300);
                var chartBar = driver.FindElement(By.XPath($"//*[name()='svg']/svg:g[2]/svg:g[1]/svg:g[2]/svg:rect[{barNumber}]"));
                Actions act = new Actions(driver);
                act.MoveToElement(chartBar).Build().Perform();
                Thread.Sleep(300);
                var toolTipElement = driver.FindElement(By.XPath($"//*[name()='svg']/svg:g[3]")).Text;
                Thread.Sleep(300);
                var toolTipText = Regex.Split(toolTipElement, ("\r\n"));
                var result = toolTipText[2];
                Assert.AreEqual(value, result, $"the letter {barLetter} count is incorrect");

            }
            catch (Exception e)
            {
                Assert.IsNull(e, "The chart element wasn't found");
                
            }

        }
    }
}
