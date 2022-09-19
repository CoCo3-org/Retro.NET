using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x2C | brfalse.s | Branch to target if value is zero (false), short form. | Base instruction

	public class IL_brfalse_s : Instruction 
	{
		public override string OpCode { get { return "2C"; } }

		public override string Mnemonic { get { return "brfalse.s"; } }

		public override string Description { get { return "Branch to target if value is zero (false), short form."; } }
		public override string Category { get { return "Base instruction"; } }

		public int BranchMethodOffset { get; private set; }

		public IL_brfalse_s(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
			Cecil.Cil.Instruction instruction = (Cecil.Cil.Instruction)cecilInstruction.Operand;
			this.BranchMethodOffset = instruction.Offset;
		}

		public override void Execute() 
		{
			object obj = this.ParentMethod.ParentType.ParentModule.MethodStack.Pop();
			if ((int)obj == 1)
			{
				Instruction instruction = this.ParentMethod.InstructionDict[this.BranchMethodOffset];
				this.ParentMethod.CurrentInstructionIndex = instruction.Index;
			}
			else
				this.ParentMethod.CurrentInstructionIndex++;
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
