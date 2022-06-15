using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x9A | ldelem.ref | Load the element at index onto the top of the stack as an O. The type of the O is the same as the element type of the array pushed on the CIL stack. | Object model instruction

	public class IL_ldelem_ref : Instruction 
	{
		public override string OpCode { get { return "9A"; } }

		public override string Mnemonic { get { return "ldelem.ref"; } }

		public override string Description { get { return "Load the element at index onto the top of the stack as an O. The type of the O is the same as the element type of the array pushed on the CIL stack."; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_ldelem_ref(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldelem.ref] not done!");
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
			throw new Exception("M6x09_Simulate [ldelem.ref] not done!");
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
