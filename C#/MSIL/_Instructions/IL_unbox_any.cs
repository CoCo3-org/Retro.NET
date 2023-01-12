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
	// 0xA5 | unbox.any | Extract a value-type from obj, its boxed representation, and copy to the top of the stack | Object model instruction

	public class IL_unbox_any : Instruction 
	{
		public override string OpCode { get { return "A5"; } }

		public override string Mnemonic { get { return "unbox.any"; } }

		public override string Description { get { return "Extract a value-type from obj, its boxed representation, and copy to the top of the stack"; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_unbox_any(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
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
