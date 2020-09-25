using System;

namespace Sample
{
	class HelloWorld
	{
		public void SayHello()
		{
			Console.WriteLine("Hello, World!");
		}
	}


    class Program
    {
		static void Main()
        {
			var hello = new HelloWorld();
			hello.SayHello();
        }
    }
}
