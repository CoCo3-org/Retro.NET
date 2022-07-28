using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xB3 | conv.ovf.i1 | Convert to an int8 (on the stack as int32) and throw an exception on overflow. | Base instruction

	public class IL_conv_ovf_i1 : Instruction 
	{
		public override string OpCode { get { return "B3"; } }

		public override string Mnemonic { get { return "conv.ovf.i1"; } }

		public override string Description { get { return "Convert to an int8 (on the stack as int32) and throw an exception on overflow."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_conv_ovf_i1(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [conv.ovf.i1] not done!");
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
