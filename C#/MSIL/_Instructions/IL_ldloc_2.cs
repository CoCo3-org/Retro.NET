using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x08 | ldloc.2 | Load local variable 2 onto stack. | Base instruction

	public class IL_ldloc_2 : Instruction 
	{
		public override string OpCode { get { return "08"; } }

		public override string Mnemonic { get { return "ldloc.2"; } }

		public override string Description { get { return "Load local variable 2 onto stack."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_ldloc_2(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
		}

		public override void Execute() 
		{
			object obj = this.ParentMethod.LocalVariables[2];
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
