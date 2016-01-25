using NUnit.Framework;
using NunitGo;
using Sandbox;

namespace Tests
{
    public class NUnitTests
    {
        [TestFixture]
        public class Sandbox
        {
            [Test, NunitGoAction("ab396a20-b3a0-4920-8bc4-26545e9f3803")]
            public void Test()
            {
                EmailSender.Test();
            }
        }
    }
}
