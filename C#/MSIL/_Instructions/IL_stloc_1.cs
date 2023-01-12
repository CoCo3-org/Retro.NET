//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x0B | stloc.1 | Pop a value from stack into local variable 1. | Base instruction

	public class IL_stloc_1 : Instruction 
	{
		public override string OpCode { get { return "0B"; } }

		public override string Mnemonic { get { return "stloc.1"; } }

		public override string Description { get { return "Pop a value from stack into local variable 1."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_stloc_1(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
		}

		public override void Execute() 
		{
			object obj = this.ParentMethod.ParentType.ParentModule.MethodStack.Pop();
			this.ParentMethod.LocalVariables[0] = obj;
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
