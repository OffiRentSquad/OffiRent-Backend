Feature: CreateBookingIntent
	As a user
	I want to make a reservation of an office in the application 
	So this could be at my disposition for a determined range of time  

Background:
	Given you are a registered user
	And you are in the Posts page of office with id 4

@mytag
Scenario: User makes the intention of reservation successfully
	Given the user wants to make a reservation
	And introduces the following data
	| startDate                  | endDate                    |
	| "2021-06-01T16:19:58.253Z" | "2021-06-15T16:19:58.253Z" |
	When the user click Book button
	Then the system shows Booking intent saved and sent to owner

Scenario:  User makes the intention of reservation of a closed office post
	Given the user wants to make a reservation
	And introduces the following data
	| startDate                  | endDate                    |
	| "2021-06-01T16:19:58.253Z" | "2021-06-15T16:19:58.253Z" |
    And the post finished 
	When the user click Book button
	Then  the system returns an error message saying that the office is no longer available 

Scenario: User tries to make a intention of reservation over its own office
	Given the user wants to make a reservation
	And introduces the following data
	| startDate                  | endDate                    |
	| "2021-06-01T16:19:58.253Z" | "2021-06-15T16:19:58.253Z" |
    And the post or office belongs to him 
	When the user click Book button
	Then the system shows an error message saying that he can't rent his own office 
	