using System;

namespace Sample
{
	class HelloWorld
	{
		public void SayHello(string hello)
		{
			Console.WriteLine(hello);
		}
	}

	class Program
	{
		static void Main()
		{
			var hello = new HelloWorld();
			hello.SayHello("Hello, World!");
		}
	}
}
