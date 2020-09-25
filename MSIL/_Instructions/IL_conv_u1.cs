﻿using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xD2 | conv.u1 | Convert to unsigned int8, pushing int32 on stack. | Base instruction

	public class IL_conv_u1 : Instruction 
	{
		public override string OpCode { get { return "D2"; } }

		public override string Mnemonic { get { return "conv.u1"; } }

		public override string Description { get { return "Convert to unsigned int8, pushing int32 on stack."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_conv_u1(Cecil.Cil.Instruction cecilInstruction, MethodDefinition parentMethod) 
			: base(cecilInstruction, parentMethod)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [conv.u1] not done!");
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
			throw new Exception("M6x09_Simulate [conv.u1] not done!");
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
