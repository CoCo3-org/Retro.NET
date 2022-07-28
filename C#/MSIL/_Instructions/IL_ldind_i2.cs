using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x48 | ldind.i2 | Indirect load value of type int16 as int32 on the stack. | Base instruction

	public class IL_ldind_i2 : Instruction 
	{
		public override string OpCode { get { return "48"; } }

		public override string Mnemonic { get { return "ldind.i2"; } }

		public override string Description { get { return "Indirect load value of type int16 as int32 on the stack."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_ldind_i2(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldind.i2] not done!");
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
