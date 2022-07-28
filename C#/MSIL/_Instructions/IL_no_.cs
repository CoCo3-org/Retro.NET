using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xFE19 | no. | The specified fault check(s) normally performed as part of the execution of the subsequent instruction can/shall be skipped. | Prefix to instruction

	public class IL_no_ : Instruction 
	{
		public override string OpCode { get { return "FE19"; } }

		public override string Mnemonic { get { return "no."; } }

		public override string Description { get { return "The specified fault check(s) normally performed as part of the execution of the subsequent instruction can/shall be skipped."; } }
		public override string Category { get { return "Prefix to instruction"; } }

		public IL_no_(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [no.] not done!");
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
