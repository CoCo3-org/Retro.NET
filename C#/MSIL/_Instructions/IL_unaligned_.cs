using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xFE12 | unaligned. | Subsequent pointer instruction might be unaligned. | Prefix to instruction

	public class IL_unaligned_ : Instruction 
	{
		public override string OpCode { get { return "FE12"; } }

		public override string Mnemonic { get { return "unaligned."; } }

		public override string Description { get { return "Subsequent pointer instruction might be unaligned."; } }
		public override string Category { get { return "Prefix to instruction"; } }

		public IL_unaligned_(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [unaligned.] not done!");
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
