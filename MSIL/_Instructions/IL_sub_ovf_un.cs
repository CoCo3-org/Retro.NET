using System;
using System.Collections.Generic;
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

		public IL_sub_ovf_un(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [sub.ovf.un] not done!");
		}

		public override void MC6801_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC6801_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC6809_Simulate() 
		{
			throw new Exception("M6x09_Simulate [sub.ovf.un] not done!");
		}

		public override void MC6809_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC6809_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}
	}
}
