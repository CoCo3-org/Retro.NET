﻿using System;
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

		public IL_stloc_0(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			object obj = this.ParentMethod.ParentType.ParentModule.MethodStack.Pop();
			this.ParentMethod.LocalVariables[0] = obj;
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
