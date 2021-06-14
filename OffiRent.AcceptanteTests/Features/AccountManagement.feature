Feature: AccountManagement
	As a user 
	I need to be able to edit my account data 
	so that my personal information is up to date 

Background:
	Given you are a registered user
	| email						| password		|
	| keiko.fujimori@gmail.com	| FuerzaPopular100	|
	And you are in the Profile page

Scenario: User successfully edits their personal information
	Given you click Edit profile button
	And you update fullName
	| fullName	     |
	| Keiko Fuji	 |
	When you click save changes button
	Then the system updates and records your data

Scenario: User cancels updating process
	Given you update fullName
	| fullName	     |
	| Keiko Fujo	 |
	When you click cancel button
	Then the system does not change your data