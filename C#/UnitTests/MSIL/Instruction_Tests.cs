//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

using NUnit.Framework;

namespace UnitTests.MSIL
{
	[TestFixture]
	public class Instruction_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var module = new global::MSIL.ModuleDefinition();
			var type = new global::MSIL.TypeDefinition(module);
			var method = new global::MSIL.MethodDefinition(type);
			var instruction = new global::MSIL.Instruction(method);

			Assert.IsNull(instruction.CecilInstruction);
			Assert.IsNotNull(instruction.ParentMethod);
			Assert.That(instruction.ParentMethod, Is.EqualTo(method));

			Assert.IsNull(instruction.OpCode);
			Assert.IsNull(instruction.Mnemonic);
			Assert.That(instruction.Operand, Is.EqualTo(""));
			Assert.IsNull(instruction.LineRepr);
			Assert.IsNull(instruction.Description);
			Assert.IsNull(instruction.Category);

			Assert.That(instruction.Index, Is.EqualTo(0));
			Assert.That(instruction.Offset, Is.EqualTo(0));
		}
	}
}
