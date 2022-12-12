using System;
using System.Collections.Generic;
using System.Text;

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
