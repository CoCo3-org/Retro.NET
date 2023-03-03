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
			var method = new global::MSIL.MethodDefinition("TestMethod", type);
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

		[Test]
		public void IL_nop() 
		{
            global::MSIL.ModuleDefinition mod = new();
            global::MSIL.TypeDefinition type = new(mod);
            global::MSIL.MethodDefinition method = new("TestMethod", type);
            global::MSIL.IL_nop instruction = new(method);

			Assert.That(instruction.OpCode, Is.EqualTo("00"));
            Assert.That(instruction.Mnemonic, Is.EqualTo("nop"));
            Assert.That(instruction.Description, Is.EqualTo("Do nothing (No operation)."));
            Assert.That(instruction.Category, Is.EqualTo("Base instruction"));
        }
    
		[Test]
		public void IL_ret() 
		{
            global::MSIL.ModuleDefinition mod = new();
            global::MSIL.TypeDefinition type = new(mod);
            global::MSIL.MethodDefinition method =new("TestMethod", type);
            global::MSIL.IL_ret instruction = new(method);

            Assert.That(instruction.OpCode, Is.EqualTo("2A"));
            Assert.That(instruction.Mnemonic, Is.EqualTo("ret"));
            Assert.That(instruction.Description, Is.EqualTo("Return from method, possibly with a value."));
            Assert.That(instruction.Category, Is.EqualTo("Base instruction"));
        }
    }
}
