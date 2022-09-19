using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xD9 | mul.ovf.un | Multiply unsigned integer values. Unsigned result shall fit in same size | Base instruction

	public class IL_mul_ovf_un : Instruction 
	{
		public override string OpCode { get { return "D9"; } }

		public override string Mnemonic { get { return "mul.ovf.un"; } }

		public override string Description { get { return "Multiply unsigned integer values. Unsigned result shall fit in same size"; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_mul_ovf_un(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [mul.ovf.un] not done!");
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
