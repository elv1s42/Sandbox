using System;

namespace TestSandbox9
{
    public class SomeClass
    {
        public string Name;

        public SomeClass()
        {
            Console.WriteLine("Name inside constuctor: " + Name);
        }

        public void ConsoleWrite()
        {
            Console.WriteLine("Name : " + Name);
        }
    }
}
