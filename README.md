# Railflow.NUnit.TestRail.Reporter Examples

Examples on how to use [Railflow.NUnit.TestRail.Reporter](https://www.nuget.org/packages/Railflow.NUnit.TestRail.Reporter/) package with NUnit tests for TestRail integration.



Installing (NuGet)
============

Use this command to add the package to your test project.

```powershell
Install-Package Railflow.NUnit.TestRail.Reporter
```

Also recommended to add [WebDriverManager](https://www.nuget.org/packages/WebDriverManager/) package for automatic Selenium driver resolution (is must have for CI)

```powershell
Install-Package WebDriverManager
```



Writing tests
=============



## 1) Using RailflowAttribute

Apply custom **RailflowAttribute** attribute to test classes/methods to mark them with TestRail metadata. See [railflow-nunit](https://github.com/railflow/railflow-nunit/blob/master/README.md) for more info.

Here is an example test:

```c#
[Railflow(Title = "class-title", CaseFields = new[] { "class-case-field-1", "class-case-field-2" })]
public class RailflowAttributeExample
{
	[Railflow(Title = "func-title", TestRailIds = new[] { 1, 2 })]
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
			<property name="railflow-title" value="class-title"/>
			<property name="railflow-case-fields" value="class-case-field-1"/>
			<property name="railflow-case-fields" value="class-case-field-2"/>
		</properties>
		<test-case name="MarkerExample1">
			<properties>
				<property name="railflow-title" value="func-title"/>
				<property name="railflow-test-rail-ids" value="1"/>
				<property name="railflow-test-rail-ids" value="2"/>
			</properties>
		</test-case>
		<test-case name="MarkerExample2">
	</test-suite>
	<test-suite name="RailflowScreenshotsExample">
		<test-case name="ScreenshotExample1">
			<attachments>
				<attachment>
					<filePath>D:\a\nunit_example\nunit_example\src\Railflow.NUnit.TestRail.Reporter.Example\Example\bin\Debug\net472\railflow-screenshots\test-run 2021-12-15-03-11-38\ScreenshotExample1-0.png</filePath>
				</attachment>
				<attachment>
					<filePath>D:\a\nunit_example\nunit_example\src\Railflow.NUnit.TestRail.Reporter.Example\Example\bin\Debug\net472\railflow-screenshots\test-run 2021-12-15-03-11-38\ScreenshotExample1-1.png</filePath>
				</attachment>
				<attachment>
					<filePath>D:\a\nunit_example\nunit_example\src\Railflow.NUnit.TestRail.Reporter.Example\Example\bin\Debug\net472\railflow-screenshots\test-run 2021-12-15-03-11-38\ScreenshotExample1-2.png</filePath>
				</attachment>
			</attachments>
		</test-case>
		<test-case name="ScreenshotExample2">
			<attachments>
				<attachment>
					<filePath>D:\a\nunit_example\nunit_example\src\Railflow.NUnit.TestRail.Reporter.Example\Example\bin\Debug\net472\railflow-screenshots\test-run 2021-12-15-03-11-38\ScreenshotExample2-3.png</filePath>
				</attachment>
			</attachments>
		</test-case>
	</test-suite>
</test-suite>
```
