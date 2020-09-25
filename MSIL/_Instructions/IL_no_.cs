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

		public IL_no_(Cecil.Cil.Instruction cecilInstruction, MethodDefinition parentMethod) 
			: base(cecilInstruction, parentMethod)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [no.] not done!");
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
			throw new Exception("M6x09_Simulate [no.] not done!");
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
