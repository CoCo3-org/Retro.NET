//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

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
