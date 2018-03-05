Feature: Data Flow Tests
	verify that chart is displayed on browser;


Scenario: System has been started _on localhost:8080 _chart is displayed
	Given the output folder is empty
		And BlackBox program is executed 
		And the output file has been created
	When UI server is started
	Then UI server started successfully with string in log: "Server running at"
		And the chart with id "chart_div" has been displayed
