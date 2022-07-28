using System;

namespace Sample
{
	class HelloWorld
	{
		public static void SayHello(string hello)
		{
			Console.WriteLine(hello);
		}
	}

    class Program
    {
		static void Main()
        {
			HelloWorld.SayHello("Hello, World!");
		}
    }
}
