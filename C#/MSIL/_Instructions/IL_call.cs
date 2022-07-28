using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x28 | call | Call method described by method. | Base instruction

	public class IL_call : Instruction 
	{
		public override string OpCode { get { return "28"; } }

		public override string Mnemonic { get { return "call"; } }

		public override string Description { get { return "Call method described by method."; } }
		public override string Category { get { return "Base instruction"; } }

		public Cecil.MethodReference MethodReference { get; private set; }
		public string DeclaringType { get; private set; }
		public string CallMethodFullName { get; private set; }

		public IL_call(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
			this.MethodReference = (Cecil.MethodReference)cecilInstruction.Operand;
			this.DeclaringType = this.MethodReference.DeclaringType.ToString();
			this.CallMethodFullName = this.MethodReference.FullName;
		}

		public override void Execute() 
		{
			// Console.WriteLine("call Name: " + this.MethodReference.Name);
			// Console.WriteLine("call FullName: " + this.MethodReference.FullName);
			// Console.WriteLine("call Module: " + this.MethodReference.Module.ToString());
			// Console.WriteLine("call DeclaringType: " + this.MethodReference.DeclaringType.ToString());
			
			// see if "Module" contains DeclaringType -- if it does Call DeclaringType's Method .... 
						// if NOT .. then search for DeclaringType in globals and call Method there ... 

			//if(this.CallMethodFullName == "System.Void Samples.Program::MethodOverload(System.String)")
			//	Console.WriteLine("**** HERE!!!! System.Void Samples.Program::MethodOverload(System.String)");
			
			if (this.ParentMethod.ParentType.ParentModule.TypeDefinitionDictionary.ContainsKey(this.DeclaringType))
			{
				TypeDefinition typeDefinition = this.ParentMethod.ParentType.ParentModule.TypeDefinitionDictionary[this.DeclaringType];
				if (typeDefinition.MethodDefinitionDict.ContainsKey(this.MethodReference.FullName))
				{
					MethodDefinition methodDefinition = typeDefinition.MethodDefinitionDict[this.MethodReference.FullName];
					methodDefinition.Execute();
				}
				else
					throw new Exception($"MethodReference [{this.MethodReference.FullName}] in [{this.DeclaringType}] not found!");
			}
			else if(this.ParentMethod.ParentType.ParentModule.SysTypeDefinitionDictionary.ContainsKey(this.DeclaringType))
			{
				Sys.TypeDefinition typeDefinition = this.ParentMethod.ParentType.ParentModule.SysTypeDefinitionDictionary[this.DeclaringType];
				if(typeDefinition.MethodDefinitionDict.ContainsKey(this.CallMethodFullName))
				{
					Sys.MethodDefinition methodDefinition = typeDefinition.MethodDefinitionDict[this.MethodReference.FullName];
					methodDefinition.Execute(this);
				}
				else
					throw new Exception($"MethodDefinition [{this.CallMethodFullName}] in [{this.DeclaringType}] not found!");
			}
			else
				throw new Exception($"DeclaringType [{this.DeclaringType}] not found!");

			this.ParentMethod.CurrentInstructionIndex++;
		}

		public override void MC680x_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC680x_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC6x09_UnOptimized_Code(StringBuilder sb) 
		{
			// this.OutputDescCategoryLine(sb);

			sb.AppendLine("\tjsr ... --> " + this.CecilInstruction.Operand.ToString());

		}

		public override void MC6x09_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}
	}
}
