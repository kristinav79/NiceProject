Feature: System Component Test  
	to avoid mistakes
	each component of system need to be verified : if it is can be started and run;
	No data validation will be done here;

Background:
Given the output folder is empty


@Black Box
Scenario: Black Box program is executed - output file is created
	When BlackBox program is executed 
	Then program successfully finished to run with log: "Finished writing out results"
		And the output file has been created
		And output file is not empty

@UI Server
Scenario: UI Server is Started - on localhost - successfully
	Given server is NOT running on host "localhost" with port "8080"
	When UI server is started
	Then UI server started successfully with string in log: "Server running at"
		And server is running on host "localhost" with port "8080"
		And the page title  "QA TASK" has been displayed
	



	
