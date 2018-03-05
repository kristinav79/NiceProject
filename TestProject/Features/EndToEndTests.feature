Feature: System EndToEnd Tests
	In order to avoid mistakes
	the End To End process needs to be tested: 
	* BlackBox program executed;
	* output file created,
	* UI server started
	* chart is visible and data is correct;


Scenario: Data on chart is displayed correctly
	Given the output folder is empty
		And BlackBox program is executed 
		And the output file has been created
	When UI server is started
	Then the chart with id "chart_div" has been displayed
		And chart bar with letter "o" value is "18"
		And chart bar with letter "a" value is "25"
		

