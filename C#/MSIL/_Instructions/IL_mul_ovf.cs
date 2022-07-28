using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xD8 | mul.ovf | Multiply signed integer values. Signed result shall fit in same size | Base instruction

	public class IL_mul_ovf : Instruction 
	{
		public override string OpCode { get { return "D8"; } }

		public override string Mnemonic { get { return "mul.ovf"; } }

		public override string Description { get { return "Multiply signed integer values. Signed result shall fit in same size"; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_mul_ovf(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [mul.ovf] not done!");
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
