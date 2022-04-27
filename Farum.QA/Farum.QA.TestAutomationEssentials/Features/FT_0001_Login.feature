Feature: FT_0001_Login
	As aspiring tester
	I want to verify if I'm able to login to Swag Labs

@Retry(2)
Scenario: User is able to login to Swag Labs
	Given User is on Swag Labs login page
		And User sets "standard_user" in Username
		And User sets "secret_sauce" in Password
	When User clicks Login button
	Then Products should be visible