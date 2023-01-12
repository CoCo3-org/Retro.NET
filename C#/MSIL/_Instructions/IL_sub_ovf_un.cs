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
	// 0xDB | sub.ovf.un | Subtract native unsigned int from a native unsigned int. Unsigned result shall fit in same size. | Base instruction

	public class IL_sub_ovf_un : Instruction 
	{
		public override string OpCode { get { return "DB"; } }

		public override string Mnemonic { get { return "sub.ovf.un"; } }

		public override string Description { get { return "Subtract native unsigned int from a native unsigned int. Unsigned result shall fit in same size."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_sub_ovf_un(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [sub.ovf.un] not done!");
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
