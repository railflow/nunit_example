using NUnit.Framework;
using Railflow.NUnit.TestRail.Reporter;

namespace Example
{
    /// <summary>
    /// NOTE: 'JiraIds' isn't class-level marker. So will be ignored
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
        
        [Railflow(
            Title = "func-title",
            CasePriority = "func-case-priority",
            TestRailIds = new[] { 1, 2 },
            JiraIds = new[] { "func-jira-id-1", "func-jira-id-2" })]
        [Test]
        public void MarkerExample1()
        {
        }

        [Test]
        public void MarkerExample2()
        {
        }
    }
}