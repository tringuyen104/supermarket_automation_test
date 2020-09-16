@FeatureRegister
Feature: Register

@RegisterWithWrongFormatEmail
Scenario: Register with wrong format email
	Given Register with wrong format email value is "abc"
	Then Show error message validate input is "Please include an '@' in the email address. 'abc' is missing an '@'."	with Id is "Email"

@RegisterWithPasswordLengthLessThenSeven
Scenario: Register with password length less then seven
	Given Register with password length less then seven
	Then Show error message is "Password length minimum: 7. Non-alphanumeric characters required: 1."
	
@RegisterWithPasswordAndConfirmPasswordNotMatch
Scenario: Register with password and confirm password not match
	Given Register with password and confirm password not match
	Then Show error message password not match is "The password and confirmation password must match."	

@RegisterNoneTypingSecurityQuestion
Scenario: Register none typing security question
	Given Register none typing security question
	Then Show error message validate input is "Please fill out this field."	with Id is "Answer"

@RegisterNoneTypingFullname
Scenario: Register none typing fullname
	Given Register none typing fullname
	Then Show error message validate input is "Please fill out this field."	with Id is "FullName"

@RegisterNoneTypingEmail
Scenario: Register none typing Email
	Given Register none typing Email
	Then Show error message validate input is "Please fill out this field."	with Id is "Email"

@RegisterNoneTypingPassword
Scenario: Register none typing Password
	Given Register none typing Password
	Then  Show error message validate input is "Please fill out this field."	with Id is "Password"

@RegisterNoneCheckTermAndConditions
Scenario: Register none check term and conditions
	Given Register none check term and conditions
	Then Show error message with alert is "Please accept our terms and conditions!"

@RegisterWithUserExist
Scenario: Register with user exist
	Given Register with user exist
	Then Show error message is "Please enter a different user name."

@RegisterWithEmailExist
Scenario: Register with email exist
	Given Register with email exist
	Then Show error message is "The e-mail address that you entered is already in use. Please enter a different e-mail address."
	
@RegisterWithCorrectData
Scenario: Register success with correct data
	Given Register success with correct data
	Then Show success message is "Your account has been created."
