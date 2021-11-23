using NUnit.Framework;
using Railflow.NUnit.TestRail.Reporter;

namespace Example
{
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
    public class RailflowAttributeExample
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
        ///     <property name = "railflow-case-priority" value="func-case-priority" />
        ///     <property name = "railflow-test-rail-ids" value="func-test-rail-id-1 func-test-rail-id-2" />
        ///     <property name = "railflow-jira-ids" value="func-jira-id-1 func-jira-id-2" />
        /// </properties>
        /// </summary>
        [Railflow(
            Title = "func-title",
            CasePriority = "func-case-priority",
            TestRailIds = new[] { "func-test-rail-id-1", "func-test-rail-id-2" },
            JiraIds = new[] { "func-jira-id-1", "func-jira-id-2" })]
        [Test]
        public void MarkerExample1()
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
        public void MarkerExample2()
        {
        }
    }
}