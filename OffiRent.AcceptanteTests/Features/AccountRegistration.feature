Feature: AccountRegistration
	As a user
	I need to create an account in the application
	To be able to use its functionalities 

Background:
Given you are in RegisterPage

Scenario: User successfully registers account
	Given you fill the following mandatory data
	| firstName | surname  | email                   | password | confirmPassword | phoneNumber | birthDay				   |
	| Pedro     | Castillo | peter.castle@gmail.com  | 12345     | 12345            |987654321	| 1970-05-29T16:19:58.253Z |
	When you click register button
	Then the system returns a message saying that your account has been created successfully 
	And redirects you to login page

Scenario: User already has an account associated with that email

	Given you fill the following mandatory data
	| firstName | surname  | email                   | password | confirmPassword | phoneNumber | birthDay				   |
	| Pedro     | Castillo | pedro.castillo@gmail.com | 12345    | 12345            |987654321	| 1970-05-29T16:19:58.253Z |
	And the user already has an account in the application registered with that same email
	When you click register button
	Then the system shows a message saying that this email already exists

Scenario: User specifies a future date of birth

	Given you fill the following mandatory data with future birth day
	| firstName | surname  | email                   | password | confirmPassword | phoneNumber | birthDay				   |
	| Pedro     | Castillo | pedro.castillo@gmail.com | 12345     | 12345            |987654321	| 2022-05-29T16:19:58.253Z |
	When you click register button
	Then the system shows a message indicating that the date of birth cannot be in the future 