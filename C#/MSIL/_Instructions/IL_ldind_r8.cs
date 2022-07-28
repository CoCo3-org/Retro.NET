using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x4F | ldind.r8 | Indirect load value of type float64 as F on the stack. | Base instruction

	public class IL_ldind_r8 : Instruction 
	{
		public override string OpCode { get { return "4F"; } }

		public override string Mnemonic { get { return "ldind.r8"; } }

		public override string Description { get { return "Indirect load value of type float64 as F on the stack."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_ldind_r8(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldind.r8] not done!");
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
