using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xFE0E | stloc | Pop a value from stack into local variable indx. | Base instruction

	public class IL_stloc : Instruction 
	{
		public override string OpCode { get { return "FE0E"; } }

		public override string Mnemonic { get { return "stloc"; } }

		public override string Description { get { return "Pop a value from stack into local variable indx."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_stloc(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [stloc] not done!");
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
