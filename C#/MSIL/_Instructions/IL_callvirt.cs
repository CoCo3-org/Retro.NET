using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x6F | callvirt | Call a method associated with an object. | Object model instruction

	public class IL_callvirt : Instruction 
	{
		public override string OpCode { get { return "6F"; } }

		public override string Mnemonic { get { return "callvirt"; } }

		public override string Description { get { return "Call a method associated with an object."; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_callvirt(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [callvirt] not done!");
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

			sb.AppendLine($"\tjsr ... --> [{this.Operand}][{this.Offset}]");
		}

		public override void MC6x09_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}
	}
}
