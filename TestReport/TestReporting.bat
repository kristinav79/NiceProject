..\packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe --labels=on --out=TestResult.txt "--result=TestResult.xml;format=nunit2" ..\TestProject\bin\Debug\TestProject.dll
..\packages\SpecFlow.2.2.1\tools\specflow.exe nunitexecutionreport ..\TestProject\TestProject.csproj /out:QATaskResultFinalReport.html
