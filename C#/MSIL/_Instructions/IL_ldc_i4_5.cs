using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x1B | ldc.i4.5 | Push 5 onto the stack as int32. | Base instruction

	public class IL_ldc_i4_5 : Instruction 
	{
		public override string OpCode { get { return "1B"; } }

		public override string Mnemonic { get { return "ldc.i4.5"; } }

		public override string Description { get { return "Push 5 onto the stack as int32."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_ldc_i4_5(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldc.i4.5] not done!");
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
