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
	// 0x93 | ldelem.u2 | Load the element with type unsigned int16 at index onto the top of the stack as an int32. | Object model instruction

	public class IL_ldelem_u2 : Instruction 
	{
		public override string OpCode { get { return "93"; } }

		public override string Mnemonic { get { return "ldelem.u2"; } }

		public override string Description { get { return "Load the element with type unsigned int16 at index onto the top of the stack as an int32."; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_ldelem_u2(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ldelem.u2] not done!");
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
