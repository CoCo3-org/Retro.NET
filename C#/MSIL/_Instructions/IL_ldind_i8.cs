using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x4C | ldind.i8 | Indirect load value of type int64 as int64 on the stack. | Base instruction

	public class IL_ldind_i8 : Instruction 
	{
		public override string OpCode { get { return "4C"; } }

		public override string Mnemonic { get { return "ldind.i8"; } }

		public override string Description { get { return "Indirect load value of type int64 as int64 on the stack."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_ldind_i8(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldind.i8] not done!");
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
