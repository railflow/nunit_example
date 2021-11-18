# Railflow.NUnit.TestRail.Reporter Examples

Examples on how to use [Railflow.NUnit.TestRail.Reporter](https://www.nuget.org/packages/Railflow.NUnit.TestRail.Reporter/) package for TestRail integration.

Installing (NuGet)
============

Use this command to add the package to your test project.

```powershell
Install-Package Railflow.NUnit.TestRail.Reporter
```

Writing test
=============

The addin provides custom **RailflowAttribute** which can be applied to test methods/classes to mark them with TestRail metadata. See [railflow-nunit](https://github.com/railflow/railflow-nunit/blob/master/README.md) for more info.

Here is an example test with comments explaining attributes propagation logic:

```c#
    /// <summary>
    /// NOTE: 'JiraIds' isn't class-level marker. So will be ignored
    /// <properties>
    ///     <property name = "railflow-title" value="class-title" />
    ///     <property name = "railflow-case-fields" value="class-case-field-1 class-case-field-2" />
    /// </properties>
    /// </summary>
    [Railflow(
        Title = "class-title",
        JiraIds = new[] { "class-jira-id-1", "class-jira-id-2" },
        CaseFields = new[] { "class-case-field-1", "class-case-field-2" })]
    public class ExampleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Markers:
        /// <properties>
        ///     <property name = "railflow-title" value="func-title" />
        ///     <property name = "railflow-case-fields" value="class-case-field-1 class-case-field-2" /> (inherited from class-level)
        ///     <property name = "railflow-test-rail-ids" value="func-test-rail-id-1 func-test-rail-id-2" />
        ///     <property name = "railflow-case-priority" value="func-case-priority" />
        /// </properties>
        /// </summary>
        [Railflow(
            Title = "func-title",
            CasePriority = "func-case-priority",
            TestRailIds = new[] { "func-test-rail-id-1", "func-test-rail-id-2" })]
        [Test]
        public void Test1()
        {
        }

        /// <summary>
        /// Markers:
        /// <properties>
        ///     <property name = "railflow-title" value="class-title" /> (inherited)
        ///     <property name = "railflow-case-fields" value="class-case-field-1 class-case-field-2" /> (inherited)
        ///     <property name = "railflow-jira-ids" value="func-jira-id-1 func-jira-id-2" />
        /// </properties>
        /// </summary>
        [Railflow(JiraIds = new[] { "func-jira-id-1", "func-jira-id-2" })]
        [Test]
        public void Test2()
        {
        }

        /// <summary>
        /// Markers:
        /// <properties>
        ///     <property name = "railflow-title" value="class-title" /> (inherited)
        ///     <property name = "railflow-case-fields" value="class-case-field-1 class-case-field-2" /> (inherited)
        /// </properties>
        /// </summary>
        [Test]
        public void Test3()
        {
        }

        /// <summary>
        /// Markers:
        /// <properties>
        ///     <property name = "railflow-title" value="class-title" /> (inherited)
        ///     <property name = "railflow-case-fields" value="class-case-field-1 class-case-field-2" /> (inherited)
        /// </properties>
        /// </summary>
        [Railflow]
        [Test]
        public void Test4()
        {
        }
```

Running tests
============

Use [NUnit.ConsoleRunner](https://www.nuget.org/packages/NUnit.ConsoleRunner/) to run NUnit tests and generate output. NOTE: Visual Studio adapter doesn't generate NUnit output.

E.g.

```powershell
nunit3-console.exe /myTestProject/bin/debug/myTestProject.dll
```

XML output
===========

Here is the output of test (<u>non-relevant pieces are skipped</u>).

```xml
<test-suite type="TestFixture">
	<properties>
		<property name="railflow-title" value="class-title"/>
		<property name="railflow-case-fields" value="class-case-field-1 class-case-field-2"/>
	</properties>
	<test-case id="1-1001" name="Test1">
		<properties>
			<property name="railflow-title" value="func-title"/>
			<property name="railflow-case-fields" value="class-case-field-1 class-case-field-2"/>
			<property name="railflow-test-rail-ids" value="func-test-rail-id-1 func-test-rail-id-2"/>
			<property name="railflow-case-priority" value="func-case-priority"/>
		</properties>
	</test-case>
	<test-case id="1-1003" name="Test2">
		<properties>
			<property name="railflow-title" value="class-title"/>
			<property name="railflow-case-fields" value="class-case-field-1 class-case-field-2"/>
			<property name="railflow-jira-ids" value="func-jira-id-1 func-jira-id-2"/>
		</properties>
	</test-case>
	<test-case id="1-1004" name="Test3">
		<properties>
			<property name="railflow-title" value="class-title"/>
			<property name="railflow-case-fields" value="class-case-field-1 class-case-field-2"/>
		</properties>
	</test-case>
	<test-case id="1-1005" name="Test4">
		<properties>
			<property name="railflow-title" value="class-title"/>
			<property name="railflow-case-fields" value="class-case-field-1 class-case-field-2"/>
		</properties>
	</test-case>
</test-suite>
    
```
