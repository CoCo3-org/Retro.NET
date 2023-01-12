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
	// 0x9A | ldelem.ref | Load the element at index onto the top of the stack as an O. The type of the O is the same as the element type of the array pushed on the CIL stack. | Object model instruction

	public class IL_ldelem_ref : Instruction 
	{
		public override string OpCode { get { return "9A"; } }

		public override string Mnemonic { get { return "ldelem.ref"; } }

		public override string Description { get { return "Load the element at index onto the top of the stack as an O. The type of the O is the same as the element type of the array pushed on the CIL stack."; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_ldelem_ref(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldelem.ref] not done!");
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
