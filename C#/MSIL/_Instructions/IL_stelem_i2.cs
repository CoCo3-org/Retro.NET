﻿using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x9D | stelem.i2 | Replace array element at index with the int16 value on the stack. | Object model instruction

	public class IL_stelem_i2 : Instruction 
	{
		public override string OpCode { get { return "9D"; } }

		public override string Mnemonic { get { return "stelem.i2"; } }

		public override string Description { get { return "Replace array element at index with the int16 value on the stack."; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_stelem_i2(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [stelem.i2] not done!");
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
			throw new Exception("M6x09_Simulate [stelem.i2] not done!");
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