using NUnit.Framework;

namespace TestSandbox9
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test1()
        {
            var someClass1 = new SomeClass()
            {
                Name = "some name 1"
            };
            someClass1.ConsoleWrite();
        }

        [Test]
        public void Test2()
        {
            var someClass2 = new SomeClass();
            someClass2.ConsoleWrite();
        }
    }
}
