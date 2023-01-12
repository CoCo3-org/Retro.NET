//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace MSIL.Sys.String
{
	public class Concat : MethodDefinition
	{
		public override string FullName => "System.String System.String::Concat(System.Object,System.Object)";

		public override void Execute(IL_call call)
		{
			object obj1 = call.ParentMethod.ParentType.ParentModule.MethodStack.Pop();
			object obj2 = call.ParentMethod.ParentType.ParentModule.MethodStack.Pop();

			string str = obj2.ToString() + obj1.ToString();
			call.ParentMethod.ParentType.ParentModule.MethodStack.Push(str);
		}
	}

	public class TypeDefinition : Sys.TypeDefinition
	{
		public override string Name => "String";
		public override string FullName => "System.String";

		public TypeDefinition()
		{
			MethodDefinition methDef = new Concat();
			this.MethodDefinitionDict.Add(methDef.FullName, methDef);

			//methDef = new ReadLine();
			//this.MethodDefinitionDict.Add(methDef.FullName, methDef);

		}
	}
}
