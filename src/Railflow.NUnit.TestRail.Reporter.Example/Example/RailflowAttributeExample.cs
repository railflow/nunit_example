using NUnit.Framework;
using Railflow.NUnit.TestRail.Reporter;

namespace Example
{
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
}