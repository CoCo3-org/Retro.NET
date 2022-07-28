using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x91 | ldelem.u1 | Load the element with type unsigned int8 at index onto the top of the stack as an int32. | Object model instruction

	public class IL_ldelem_u1 : Instruction 
	{
		public override string OpCode { get { return "91"; } }

		public override string Mnemonic { get { return "ldelem.u1"; } }

		public override string Description { get { return "Load the element with type unsigned int8 at index onto the top of the stack as an int32."; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_ldelem_u1(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldelem.u1] not done!");
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
