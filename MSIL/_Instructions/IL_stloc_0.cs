using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x0A | stloc.0 | Pop a value from stack into local variable 0. | Base instruction

	public class IL_stloc_0 : Instruction 
	{
		public override string OpCode { get { return "0A"; } }

		public override string Mnemonic { get { return "stloc.0"; } }

		public override string Description { get { return "Pop a value from stack into local variable 0."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_stloc_0(Cecil.Cil.Instruction cecilInstruction, MethodDefinition parentMethod) 
			: base(cecilInstruction, parentMethod)
		{
		}

		public override void Execute() 
		{
			object obj = this.ParentMethod.ParentType.ParentModule.MethodStack.Pop();
			this.ParentMethod.LocalVariables[0] = obj;
			this.ParentMethod.CurrentInstructionIndex++;
		}

		public override void MC6801_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC6801_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC6809_Simulate() 
		{
			throw new Exception("M6x09_Simulate [stloc.0] not done!");
		}

		public override void MC6809_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC6809_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}
	}
}
