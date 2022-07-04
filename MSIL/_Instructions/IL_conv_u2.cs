﻿using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xD1 | conv.u2 | Convert to unsigned int16, pushing int32 on stack. | Base instruction

	public class IL_conv_u2 : Instruction 
	{
		public override string OpCode { get { return "D1"; } }

		public override string Mnemonic { get { return "conv.u2"; } }

		public override string Description { get { return "Convert to unsigned int16, pushing int32 on stack."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_conv_u2(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [conv.u2] not done!");
		}

		public override void MC680x_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC680x_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC6x09_Simulate() 
		{
			throw new Exception("M6x09_Simulate [conv.u2] not done!");
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
