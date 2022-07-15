using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xFE03 | cgt.un | Push 1 (of type int32) if value1 > value2, unsigned or unordered, else push 0. | Base instruction

	public class IL_cgt_un : Instruction 
	{
		public override string OpCode { get { return "FE03"; } }

		public override string Mnemonic { get { return "cgt.un"; } }

		public override string Description { get { return "Push 1 (of type int32) if value1 > value2, unsigned or unordered, else push 0."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_cgt_un(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [cgt.un] not done!");
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
			throw new Exception("M6x09_Simulate [cgt.un] not done!");
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
