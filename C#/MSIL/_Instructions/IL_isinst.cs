﻿using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x75 | isinst | Test if obj is an instance of class, returning null or an instance of that class or interface. | Object model instruction

	public class IL_isinst : Instruction 
	{
		public override string OpCode { get { return "75"; } }

		public override string Mnemonic { get { return "isinst"; } }

		public override string Description { get { return "Test if obj is an instance of class, returning null or an instance of that class or interface."; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_isinst(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [isinst] not done!");
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
