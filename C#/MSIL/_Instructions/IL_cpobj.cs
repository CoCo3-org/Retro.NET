﻿using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x70 | cpobj | Copy a value type from src to dest. | Object model instruction

	public class IL_cpobj : Instruction 
	{
		public override string OpCode { get { return "70"; } }

		public override string Mnemonic { get { return "cpobj"; } }

		public override string Description { get { return "Copy a value type from src to dest."; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_cpobj(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [cpobj] not done!");
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
