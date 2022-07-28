using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x6E | conv.u8 | Convert to unsigned int64, pushing int64 on stack. | Base instruction

	public class IL_conv_u8 : Instruction 
	{
		public override string OpCode { get { return "6E"; } }

		public override string Mnemonic { get { return "conv.u8"; } }

		public override string Description { get { return "Convert to unsigned int64, pushing int64 on stack."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_conv_u8(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [conv.u8] not done!");
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
