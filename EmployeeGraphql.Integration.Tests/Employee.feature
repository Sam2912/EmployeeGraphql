Feature: Employee

A short summary of the feature

@tag1
Scenario: Add Employee
	Given I have database setup
	And I have employee to add
	When I call the graphql endpoint
	Then I shoud see employee is saved

