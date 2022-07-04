using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xA5 | unbox.any | Extract a value-type from obj, its boxed representation, and copy to the top of the stack | Object model instruction

	public class IL_unbox_any : Instruction 
	{
		public override string OpCode { get { return "A5"; } }

		public override string Mnemonic { get { return "unbox.any"; } }

		public override string Description { get { return "Extract a value-type from obj, its boxed representation, and copy to the top of the stack"; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_unbox_any(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [unbox.any] not done!");
		}

		public override void MC680x_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC680x_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC6x09_Simulate() 
		{
			throw new Exception("M6x09_Simulate [unbox.any] not done!");
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
