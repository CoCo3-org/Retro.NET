using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xFE1E | readonly. | Specify that the subsequent array address operation performs no type check at runtime, and that it returns a controlled-mutability managed pointer | Prefix to instruction

	public class IL_readonly_ : Instruction 
	{
		public override string OpCode { get { return "FE1E"; } }

		public override string Mnemonic { get { return "readonly."; } }

		public override string Description { get { return "Specify that the subsequent array address operation performs no type check at runtime, and that it returns a controlled-mutability managed pointer"; } }
		public override string Category { get { return "Prefix to instruction"; } }

		public IL_readonly_(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [readonly.] not done!");
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
