using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x09 | ldloc.3 | Load local variable 3 onto stack. | Base instruction

	public class IL_ldloc_3 : Instruction 
	{
		public override string OpCode { get { return "09"; } }

		public override string Mnemonic { get { return "ldloc.3"; } }

		public override string Description { get { return "Load local variable 3 onto stack."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_ldloc_3(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldloc.3] not done!");
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
