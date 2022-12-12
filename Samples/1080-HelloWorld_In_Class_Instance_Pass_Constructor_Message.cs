using System;

namespace Sample
{
	class HelloWorld
	{
		public HelloWorld(string hello)
		{
			Console.WriteLine(hello);
		}
	}

	class Program
	{
		static void Main()
		{
			var hello = new HelloWorld("Hello, World!");
			var bye = new HelloWorld("Goodbye, World!");
		}
	}
}
