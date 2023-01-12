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
	public class ModuleDefinition_Tests
	{
		[Test]
		public void Constructor_Defaults() 
		{
			var module = new global::MSIL.ModuleDefinition();

			Assert.IsNull(module.AssemblyFilePath);
			Assert.IsNull(module.MC680xPath);
			Assert.IsNull(module.MC6x09Path);
			Assert.IsNull(module.Z80Path);
			Assert.IsNull(module.MOS6502Path);
			Assert.IsNull(module.MC68000Path);
			Assert.IsNull(module.CecilModule);

			Assert.That(module.TypeDefinitions.Count, Is.EqualTo(0));
			Assert.That(module.TypeDefinitionDictionary.Count, Is.EqualTo(0));
			Assert.That(module.SysTypeDefinitionDictionary.Count, Is.EqualTo(0));

			Assert.That(module.StringConstantCount, Is.EqualTo(0));
			Assert.That(module.StringConstantDictionary.Count, Is.EqualTo(0));
			Assert.IsNotNull(module.MethodStack);
			Assert.IsNull(module.MainMethod);
		}

		[Test]
		public void Test_Do_Nothing_App()
		{
			// 1000-Do_Nothing_App.cs

			// .assembly '1000-Do_Nothing_App'
			var module = new global::MSIL.ModuleDefinition();
			module.AssemblyName = "1000-Do_Nothing_App";

			// class Sample.Program
			var type = new global::MSIL.TypeDefinition(module);
			type.TypeName = "Sample_Program";

			// static void Sample.Program_Main()
			var method = new global::MSIL.MethodDefinition(type);
			//

			// void Sample.Program.ctor()


		}
	}
}
