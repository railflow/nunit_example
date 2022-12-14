# Railflow.NUnit.TestRail.Reporter Examples

Examples on how to use [Railflow.NUnit.TestRail.Reporter](https://www.nuget.org/packages/Railflow.NUnit.TestRail.Reporter/) package with NUnit tests for TestRail integration.


Installing (NuGet)
============

Use this command to add the package to your test project.

```powershell
Install-Package Railflow.NUnit.TestRail.Reporter
```

Also it is recommended to add [WebDriverManager](https://www.nuget.org/packages/WebDriverManager/) package for automatic Selenium driver resolution (is a must-have for CI)

```powershell
Install-Package WebDriverManager
```



Writing tests
=============



## 1) Using RailflowAttribute

Apply custom **Railflow** attribute to test classes/methods to mark them with TestRail metadata. See [railflow-nunit](https://www.nuget.org/packages/Railflow.NUnit.TestRail.Reporter) for more info.

Here is an example test:

```c#
[Railflow(
        Title = "class-title",
        JiraIds = new[] { "NC-1", "NC-2" },
        CasePriority ="High",
        CaseType ="Railflow",
        CaseFields = new[] { "Required text field = value from class", "estimate = 2s" },
        ResultFields = new[] { "Custom field  = hello from test class", "vesion=1.0" }, 
        SmartAssignment = new []{"user1@yourcompany.com", "user2@yourcompany.com"})]
public class RailflowAttributeExample
{
	 [Railflow(
            Title = "method-title",
            JiraIds = new[] { "NC-3", "NC-4" },
            CasePriority = "Critical",
            CaseType = "Performance",
            CaseFields = new[] { "Required text field = value from method","estimate=42s" },
            ResultFields = new[] { "Custom field  = test method rocks!", "version = 2.0" }, 
            SmartAssignment = new[] { "user3@yourcompany.com", "user4@yourcompany.com"},
            TestRailIds = new[] {42,24})]
	[Test]
	public void MarkerExample1()
	{
	}

	[Test]
	public void MarkerExample2()
	{
	}
}
```



## 2) Using CurrentTest class

Here is an example on how to take screenshots and associate with tests:

```c#
public class RailflowScreenshotsExample
{
	private ChromeDriver driver;

	[OneTimeSetUp]
	public void OneTimeSetUp()
	{
		// Will run driver in headless-mode (without UI)
		var options = new ChromeOptions();
		options.AddArguments("--headless");

		// Setup chrome driver matching browser on current machine
		// NOTE: This is a must for CI tests (where browser version isn't known upfront)
		new DriverManager().SetUpDriver(new ChromeConfig(), "MatchingBrowser");

		// Instantiate driver and navigate to specific URL
		driver = new ChromeDriver(options)
		{
			Url = "https://duckduckgo.com"
		};

		// Wait the page to load
		Thread.Sleep(2000);
	}

	[Test]
	public void ScreenshotExample1()
	{
		// Take multiple screenshots and associate with current test
		CurrentTest.TakeScreenshot(driver);
		CurrentTest.TakeScreenshot(driver);
		CurrentTest.TakeScreenshot(driver);
	}

	[Test]
	public void ScreenshotExample2()
	{
		var screenshot = driver.GetScreenshot();

		// Associate existing screenshot with current test
		CurrentTest.AddExistingScreenshot(screenshot);
	}

	[TearDown]
	public void TearDown()
	{
		// Take screenshot only if the last test failed
		CurrentTest.TakeScreenshotIfLastTestFailed(driver);
	}

	[OneTimeTearDown]
	public void OneTimeTearDown()
	{
		// Quit and dispose
		driver.Quit();
	}
}
```



Running tests
============

Use [NUnit.ConsoleRunner](https://www.nuget.org/packages/NUnit.ConsoleRunner/) or [dotnet CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test) to run your NUnit tests and generate output XML.

NOTE: Visual Studio TestAdapter doesn't generate NUnit output.

E.g.

```powershell
nunit3-console.exe /myTestProject/bin/debug/myTestProject.dll
```

OR

```powershell
dotnet test MyTestProject.csproj -- NUnit.TestOutputXml=../../../TestResults
```



XML output
===========

Here is the output of tests from examples above (<u>non-relevant pieces are skipped</u>).

```xml
<test-suite name="Example">      
	<test-suite name="RailflowAttributeExample">
		<properties>
			<property name="railflow-title" value="class-title" />
			<property name="railflow-case-type" value="Railflow" />
			<property name="railflow-case-priority" value="High" />
			<property name="railflow-result-fields" value="Custom field  = hello from test class" />
			<property name="railflow-result-fields" value="vesion=1.0" />
			<property name="railflow-case-fields" value="Required text field = value from class" />
			<property name="railflow-case-fields" value="estimate = 2s" />
			<property name="railflow-jira-ids" value="NC-1" />
			<property name="railflow-jira-ids" value="NC-2" />
			<property name="railflow-smart-assignment" value="user1@yourcompany.com" />
			<property name="railflow-smart-assignment" value="user2@yourcompany.com" />
		</properties>       
		<test-case id="0-1001" name="MarkerExample1">
			<properties>
				<property name="railflow-title" value="method-title" />
				<property name="railflow-case-type" value="Performance" />
				<property name="railflow-case-priority" value="" />
				<property name="railflow-result-fields" value="Custom field  = test method rocks!" />
				<property name="railflow-result-fields" value="version = 2.0" />
				<property name="railflow-case-fields" value="Required text field = value from method" />
				<property name="railflow-case-fields" value="estimate=42s" />
				<property name="railflow-jira-ids" value="NC-3" />
				<property name="railflow-jira-ids" value="NC-4" />
				<property name="railflow-smart-assignment" value="user3@yourcompany.com" />
				<property name="railflow-smart-assignment" value="user4@yourcompany.com" />
			</properties>       
		</test-case>
		<test-case name="MarkerExample2">          
		</test-case>        
	</test-suite>
	<test-suite name="RailflowScreenshotsExample" >
		<test-case name="ScreenshotExample1">
			<attachments>
				<attachment>
					<filePath>C:\Users\sergi\source\repos\railflow_nunit_example\src\Railflow.NUnit.TestRail.Reporter.Example\Example\railflow-screenshots\test-run 2022-01-11-05-42-18\ScreenshotExample1-0.png</filePath>
				</attachment>
				<attachment>
					<filePath>C:\Users\sergi\source\repos\railflow_nunit_example\src\Railflow.NUnit.TestRail.Reporter.Example\Example\railflow-screenshots\test-run 2022-01-11-05-42-18\ScreenshotExample1-1.png</filePath>
				</attachment>
				<attachment>
					<filePath>C:\Users\sergi\source\repos\railflow_nunit_example\src\Railflow.NUnit.TestRail.Reporter.Example\Example\railflow-screenshots\test-run 2022-01-11-05-42-18\ScreenshotExample1-2.png</filePath>
				</attachment>
			</attachments>
		</test-case>       
	</test-suite>
</test-suite>
```
