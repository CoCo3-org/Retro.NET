using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x94 | ldelem.i4 | Load the element with type int32 at index onto the top of the stack as an int32. | Object model instruction

	public class IL_ldelem_i4 : Instruction 
	{
		public override string OpCode { get { return "94"; } }

		public override string Mnemonic { get { return "ldelem.i4"; } }

		public override string Description { get { return "Load the element with type int32 at index onto the top of the stack as an int32."; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_ldelem_i4(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldelem.i4] not done!");
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
			throw new Exception("M6x09_Simulate [ldelem.i4] not done!");
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
