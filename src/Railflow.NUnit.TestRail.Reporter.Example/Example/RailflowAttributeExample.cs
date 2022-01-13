using NUnit.Framework;
using Railflow.NUnit.TestRail.Reporter;

namespace Example
{  
    [Railflow(
        Title = "class-title",
        JiraIds = new[] { "NC-1", "NC-2" },
        CasePriority ="High",
        CaseType ="Railflow",
        CaseFields = new[] { "Required text field = value from class", "estimate = 2s" },
        ResultFields = new[] { "Custom field  = hello from test class", "vesion=1.0" }, SmartAssignment = new []{"user1@yourcompany.com", "user2@yourcompany.com"})]
    public class RailflowAttributeExample
    {
        [SetUp]
        public void Setup()
        {
        }
        
       [Railflow(
        Title = "method-title",
        JiraIds = new[] { "NC-3", "NC-4" },
        CasePriority = "Critical",
        CaseType = "Performance",
        CaseFields = new[] { "Required text field = value from method","estimate=42s" },
        ResultFields = new[] { "Custom field  = test method rocks!", "version = 2.0" }, SmartAssignment = new[] { "user3@yourcompany.com", "user4@yourcompany.com"},
        TestRailIds = new[] {42,24})]
       [Test]
        public void MarkerExample1()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void MarkerExample2()
        {
            Assert.IsTrue(false);
        }
        
        [Test]
        public void MarkerExample3()
        {
            Assert.IsTrue(false);
        }
    }
}