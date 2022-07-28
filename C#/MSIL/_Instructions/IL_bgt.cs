using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x3D | bgt | Branch to target if greater than. | Base instruction

	public class IL_bgt : Instruction 
	{
		public override string OpCode { get { return "3D"; } }

		public override string Mnemonic { get { return "bgt"; } }

		public override string Description { get { return "Branch to target if greater than."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_bgt(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [bgt] not done!");
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
