Feature: Healthcheck

This feature tests whether your application
is properly integrated with all external services

Scenario: A user can check wheather the application is healthy or not
	Given the base url is 'https://localhost:5001/'
	When send a Http Get request
	Then the http status code should be 200
