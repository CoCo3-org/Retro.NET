using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x98 | ldelem.r4 | Load the element with type float32 at index onto the top of the stack as an F | Object model instruction

	public class IL_ldelem_r4 : Instruction 
	{
		public override string OpCode { get { return "98"; } }

		public override string Mnemonic { get { return "ldelem.r4"; } }

		public override string Description { get { return "Load the element with type float32 at index onto the top of the stack as an F"; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_ldelem_r4(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldelem.r4] not done!");
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
			throw new Exception("M6x09_Simulate [ldelem.r4] not done!");
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
