Feature: CreateOffice
	As a user provider
	I want to create an office
	So that I can make posts about it

Background:
	Given you are a registeered user
	| email						| password		|
	| keiko.fujimori@gmail.com	| FuerzaPopular100	|
	And you are in the Create Office page

Scenario: Successful office creation
	Given you fill the following details
	|title		|district	|description				 |
	|Lorem Ipsum|Jesus Maria|Lorem Ipsum dolor sit amet..|
	When you click the create button
	Then the system creates the office and saves it in the database

Scenario: Non premium user tries to create more than one office
	Given you are not premium user
	When you click the create button
	Then the system returns an error message saying non premium users cannot have more than one office

Scenario: Creating office without description
	Given there is no office description
	| title    | district   | description |
	| Oficina2 | San Miguel |             |
	When you click the create button
	Then the system returns an error message saying that every office needs a description
