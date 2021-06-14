Feature: SearchPost
	As a user
	I want to search an office post in the app  
	So i can see the details and decide if i want to rent it or not 

Background:
	Given you are a reegistered user
	| email						| password		|
	| keiko.fujimori@gmail.com	| FuerzaPopular100	|

@mytag
Scenario: User searches an office by price range
	Given the user searches an office post by price range
	|minPrice|maxPrice|district|
	|"200"|"600"||
	When the user clicks Search button
	Then the system returns a list of offices in the price range wanted 

Scenario:  User searches an office without price range
	When the user searches an office post only by district
	|minPrice|maxPrice|district|
	|||Jesus Maria|
	Then the system wont let you click the button 