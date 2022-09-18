using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests.CPU
{
	[TestFixture]
	public class CodeBlock_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var module = new global::CPU.Module();
			var codeBlock = new global::CPU.CodeBlock(module);

			Assert.That(codeBlock.Lines.Count, Is.EqualTo(0));
		}
	}
}
