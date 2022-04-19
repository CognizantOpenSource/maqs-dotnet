Feature: AppiumFeature
	Appium Tests

@MAQS_Appium
Scenario: Driver in BaseAppiumTestSteps
	Given class BaseAppiumTestSteps
	Then BaseAppiumTestSteps TestObject is not null
	And TestObject is type AppiumTestObject
	And BaseAppiumTestSteps ScenarioContext is not null
	And BaseAppiumTestSteps ScenarioContext is type ScenarioContext