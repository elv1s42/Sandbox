using NUnit.Framework;
using NunitGo;

namespace Sandbox
{
    [TestFixture]
    public class Sandbox
    {
        [Test, NunitGoAction("ab396a20-b3a0-4920-8bc4-26545e9f3801")]
        public void Test1()
        {
            Assert.AreEqual(1, 1);
        }

        [Test, NunitGoAction("ab396a20-b3a0-4920-8bc4-26545e9f3800")]
        public void Test2()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
