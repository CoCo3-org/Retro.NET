﻿using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x87 | conv.ovf.u2.un | Convert unsigned to an unsigned int16 (on the stack as int32) and throw an exception on overflow. | Base instruction

	public class IL_conv_ovf_u2_un : Instruction 
	{
		public override string OpCode { get { return "87"; } }

		public override string Mnemonic { get { return "conv.ovf.u2.un"; } }

		public override string Description { get { return "Convert unsigned to an unsigned int16 (on the stack as int32) and throw an exception on overflow."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_conv_ovf_u2_un(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [conv.ovf.u2.un] not done!");
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
			throw new Exception("M6x09_Simulate [conv.ovf.u2.un] not done!");
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