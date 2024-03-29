﻿//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace MSIL.Sys.Console
{
	public class WriteLine_String : MethodDefinition
	{
		public override string FullName => "System.Void System.Console::WriteLine(System.String)";
		//                                System.String System.Console::ReadLine()

		public override void Execute(IL_call call)
		{
			string str = (string)call.ParentMethod.ParentType.ParentModule.MethodStack.Pop();
			System.Console.WriteLine(str);
		}
	}

	public class ReadLine : MethodDefinition
	{
		public override string FullName => "System.String System.Console::ReadLine()";

		public override void Execute(IL_call call)
		{
			string str = System.Console.ReadLine();
			call.ParentMethod.ParentType.ParentModule.MethodStack.Push(str);
		}
	}

	public class TypeDefinition : Sys.TypeDefinition
	{
		public override string Name => "Console";
		public override string FullName => "System.Console";

		public TypeDefinition()
		{
			MethodDefinition methDef = new WriteLine_String();
			this.MethodDefinitionDict.Add(methDef.FullName, methDef);

			methDef = new ReadLine();
			this.MethodDefinitionDict.Add(methDef.FullName, methDef);
		}
	}
}
