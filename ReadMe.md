# QA Taks

## Description
This project is for "QA Task" automated testing demonstration. All component that needed for this demonstration is under the solution Folder:
* The program and server code are under the Solution Folder : __/Task__
* The chrome driver is in folder __/Drivers__
* Test Results and Report is in __/TestReport__

## Prerequisites

To run test solution you need to install:
* Visual Studio 2017 ( Community 2017 will be ok);

Note: Please install Specflow too on Visual Studio ( Tools->Extension&Updates)

To run QA tasks and tests you will need to install:
* Node 
* Java

## Test Configuration

The program and server configuration parameters are in App.Config. 
If there are any changes, please update App.Config.
An Example:
<appSettings>

    <add key="DropZone" value="Task\DropZone\Input.txt" />
    <add key="OutputFolder" value="Task\UI_Server\CSVs" />
</appSettings>

If new appSettings key is added, please update /Data/InputData, this will keep your solution tidy and clean.
Note: if your solution won't be under the solution, please update step definitions to remove relative path;

## Running Tests

Open solution  and build it; 
__Note:_ Missing packages will be installed automatically;
You can run tests:
 * From Visual Studio - Select Test Explorer and Run All tests.
 * Execute __NiceSolution/TestReport/TestReporting.bat__ 

## Test steps

There are test steps where the data is set up form App.config,
but also there are steps where you can pass parameters.
This is only for demonstration of parameters set up possibilities ;
```specflow
Scenario: UI Server is Started - on localhost:8080 - successfully
	Given server is NOT running on host "localhost" with port "8080"
	When Node server is started
```

## Test results report

To get test results report you need execute __NiceSolution/TestReport/TestReporting.bat__ :
* double click on .bat file and the tests will be executed and test report __/TestReport/QATaskResultFinalReport.html__ will be generated.
* The TestReporting.bat file could be executed on pipeline, scheduled in Jenkins and etc.

There is an example of test report : 
![Report Eample](ReadmeHelper/ReportEample.png)



