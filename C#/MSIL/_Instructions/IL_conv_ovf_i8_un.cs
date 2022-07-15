﻿using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x85 | conv.ovf.i8.un | Convert unsigned to an int64 (on the stack as int64) and throw an exception on overflow. | Base instruction

	public class IL_conv_ovf_i8_un : Instruction 
	{
		public override string OpCode { get { return "85"; } }

		public override string Mnemonic { get { return "conv.ovf.i8.un"; } }

		public override string Description { get { return "Convert unsigned to an int64 (on the stack as int64) and throw an exception on overflow."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_conv_ovf_i8_un(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [conv.ovf.i8.un] not done!");
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
			throw new Exception("M6x09_Simulate [conv.ovf.i8.un] not done!");
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