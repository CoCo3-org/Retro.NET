﻿using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x1C | ldc.i4.6 | Push 6 onto the stack as int32. | Base instruction

	public class IL_ldc_i4_6 : Instruction 
	{
		public override string OpCode { get { return "1C"; } }

		public override string Mnemonic { get { return "ldc.i4.6"; } }

		public override string Description { get { return "Push 6 onto the stack as int32."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_ldc_i4_6(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldc.i4.6] not done!");
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
