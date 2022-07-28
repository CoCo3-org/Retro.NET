using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xC6 | mkrefany | Push a typed reference to ptr of type class onto the stack. | Object model instruction

	public class IL_mkrefany : Instruction 
	{
		public override string OpCode { get { return "C6"; } }

		public override string Mnemonic { get { return "mkrefany"; } }

		public override string Description { get { return "Push a typed reference to ptr of type class onto the stack."; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_mkrefany(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [mkrefany] not done!");
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
