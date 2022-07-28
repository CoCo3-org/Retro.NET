using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xD5 | conv.ovf.u | Convert to a native unsigned int (on the stack as native int) and throw an exception on overflow. | Base instruction

	public class IL_conv_ovf_u : Instruction 
	{
		public override string OpCode { get { return "D5"; } }

		public override string Mnemonic { get { return "conv.ovf.u"; } }

		public override string Description { get { return "Convert to a native unsigned int (on the stack as native int) and throw an exception on overflow."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_conv_ovf_u(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [conv.ovf.u] not done!");
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
