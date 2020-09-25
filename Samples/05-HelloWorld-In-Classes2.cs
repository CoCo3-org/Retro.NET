using System;

namespace Sample
{
	class HelloWorld
	{
		public void SayHello(int x)
		{
			Console.Write("Hello, World ");
			Console.Write(x);
			Console.WriteLine("!");
		}
	}

	class Program
	{
		static void Main()
		{
			var hello = new HelloWorld();
			var hello2 = new HelloWorld();
			var hello3 = new HelloWorld();
			hello.SayHello(1);
			hello2.SayHello(2);
			hello3.SayHello(3);
		}
	}
}
