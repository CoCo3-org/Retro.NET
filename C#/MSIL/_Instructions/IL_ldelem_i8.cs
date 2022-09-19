using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x96 | ldelem.i8 | Load the element with type int64 at index onto the top of the stack as an int64. | Object model instruction

	public class IL_ldelem_i8 : Instruction 
	{
		public override string OpCode { get { return "96"; } }

		public override string Mnemonic { get { return "ldelem.i8"; } }

		public override string Description { get { return "Load the element with type int64 at index onto the top of the stack as an int64."; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_ldelem_i8(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldelem.i8] not done!");
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
