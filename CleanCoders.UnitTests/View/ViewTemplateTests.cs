using NUnit.Framework;

namespace CleanCoders.View.UnitTest
{
    public class ViewTemplateTests
    {
        [Test]
        public void NoReplacement()
        {
            ViewTemplate template = new ViewTemplate("Some static content");
            Assert.AreEqual("Some static content", template.GetContent());
        }

        [Test]
        public  void SimpleReplacement()
        {
            ViewTemplate template = new ViewTemplate("replace ${this}.");
            template.Replace("this", "replacement");

            Assert.AreEqual("replace replacement.", template.GetContent());
        }
    }
}