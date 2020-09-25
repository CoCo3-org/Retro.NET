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
			var hello2 = new HelloWorld();
			var hello3 = new HelloWorld();
			hello.SayHello();
			hello2.SayHello();
			hello3.SayHello();
		}
	}
}
