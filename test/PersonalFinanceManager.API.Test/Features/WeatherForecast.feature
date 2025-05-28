Feature: Weather Forecast

As a UI developer
I want an API to get weather forecasts
So that I can display weather information in the application

Background: Ensure the API is running
	Given the API is running
	When the weather forecast is requested
	
Scenario: Get weather forecast successfully
	Then the response should contain a list of weather forecasts
	And each weather forecast should have a date, temperature, and summary