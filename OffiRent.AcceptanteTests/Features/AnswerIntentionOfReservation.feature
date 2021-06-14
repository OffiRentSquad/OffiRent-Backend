Feature: AnswerIntentionOfReservation
	As a provider user
	I want to accpet the reservation intentions of my office  
	So i formalized the deal made with the user that has reserved the office also to make the management easier 


@mytag
Scenario: Provider User accepts the reservation intention
	Given the  provider user has already coordinate previously with the user that wants to rent  
	And have agreed a deal and want to formalize the start of rent 
	When the provider user accepts the reservation intention,
	Then the system will delete the reservation intention and will create a reservation done by the user that wants the office 

Scenario: Provider User declines the reservation intention
	Given the provider user hasn't make a deal with the rent user  
	When the provider user declines the reservation intention,
	Then  the system will delete the reservation intention