using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x02 | ldarg.0 | Load argument 0 onto the stack. | Base instruction

	public class IL_ldarg_0 : Instruction 
	{
		public override string OpCode { get { return "02"; } }

		public override string Mnemonic { get { return "ldarg.0"; } }

		public override string Description { get { return "Load argument 0 onto the stack."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_ldarg_0(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			object obj = this.ParentMethod.ParentType.ParentModule.MethodStack[0];
			this.ParentMethod.ParentType.ParentModule.MethodStack.Push(obj);
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
			this.OutputDescCategoryLine(sb);
		}

		public override void MC6x09_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}
	}
}
