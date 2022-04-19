Feature: PlaywrightFeature
	Playwright Tests

@MAQS_Playwright
Scenario: Driver in BasePlaywrightTestSteps
	Given class BasePlaywrightTestSteps
	Then BasePlaywrightTestSteps TestObject is not null
	And TestObject is type PlaywrightTestObject
	And BasePlaywrightTestSteps ScenarioContext is not null
	And BasePlaywrightTestSteps ScenarioContext is type ScenarioContext
	And BasePlaywrightTestSteps Null driver is null