﻿using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x84 | conv.ovf.i4.un | Convert unsigned to an int32 (on the stack as int32) and throw an exception on overflow. | Base instruction

	public class IL_conv_ovf_i4_un : Instruction 
	{
		public override string OpCode { get { return "84"; } }

		public override string Mnemonic { get { return "conv.ovf.i4.un"; } }

		public override string Description { get { return "Convert unsigned to an int32 (on the stack as int32) and throw an exception on overflow."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_conv_ovf_i4_un(Cecil.Cil.Instruction cecilInstruction, MethodDefinition parentMethod) 
			: base(cecilInstruction, parentMethod)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [conv.ovf.i4.un] not done!");
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
			throw new Exception("M6x09_Simulate [conv.ovf.i4.un] not done!");
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
