Feature: LogIn
	As a user
	I need to log in 
	So I can use the app

@mytag
Scenario: User logs in successfully
	Given you introduce your credentials
	| email						| password		|
	| pedro.castillo@gmail.com	| PeruLibre100	|
	When you click login button
	Then the system redirects you to home page

Scenario: User introduces incorrect password
	Given you introduce your credentials
	| email						| password		|
	| pedro.castillo@gmail.com	| BLAA			|
	When you click login button
	Then the system shows a message indicating credentials are not correct