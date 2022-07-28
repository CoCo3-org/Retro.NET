using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x46 | ldind.i1 | Indirect load value of type int8 as int32 on the stack. | Base instruction

	public class IL_ldind_i1 : Instruction 
	{
		public override string OpCode { get { return "46"; } }

		public override string Mnemonic { get { return "ldind.i1"; } }

		public override string Description { get { return "Indirect load value of type int8 as int32 on the stack."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_ldind_i1(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldind.i1] not done!");
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
