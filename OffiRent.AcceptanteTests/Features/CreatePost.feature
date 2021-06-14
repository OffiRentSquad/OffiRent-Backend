Feature: CreatePost
	As a provider user
	I want to create a post
	So that people who are interested on booking it can contact me

Background:
	Given you are a registered user
	And you are in the Posts page of office with id 4

@mytag

Scenario: Creation of publication with duration greater than 1 month from a non-premium user
    Given you want to create a publication with the following details
    |startDate|endDate|title|monthlyFee|
    |"2021-06-01T16:19:58.253Z"|"2021-07-31T16:19:58.253Z"|"Pretty office!!"|"500.00"|
    And duration greater than 1 month
    And you are not premium user
    When Create button is clicked
    Then the system returns a message saying that non-premium users cannot create publications with a duration greater than 1 month

Scenario: Creation of publication with duration greater than 1 month from a premium user
    Given you want to create a publication with the following details
    |startDate|endDate|title|monthlyFee|
    |"2021-06-01T16:19:58.253Z"|"2021-07-31T16:19:58.253Z"|"Pretty office!!"|"500.00"|
    And duration greater than 1 month
    And you are premium user
    When Create button is clicked
    Then the system returns a message saying post created successfully