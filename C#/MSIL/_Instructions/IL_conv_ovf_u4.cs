using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xB8 | conv.ovf.u4 | Convert to an unsigned int32 (on the stack as int32) and throw an exception on overflow. | Base instruction

	public class IL_conv_ovf_u4 : Instruction 
	{
		public override string OpCode { get { return "B8"; } }

		public override string Mnemonic { get { return "conv.ovf.u4"; } }

		public override string Description { get { return "Convert to an unsigned int32 (on the stack as int32) and throw an exception on overflow."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_conv_ovf_u4(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [conv.ovf.u4] not done!");
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
