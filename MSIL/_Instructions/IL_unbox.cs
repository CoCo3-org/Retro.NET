using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x79 | unbox | Extract a value-type from obj, its boxed representation, and push a managed pointer to it to the top of the stack | Object model instruction

	public class IL_unbox : Instruction 
	{
		public override string OpCode { get { return "79"; } }

		public override string Mnemonic { get { return "unbox"; } }

		public override string Description { get { return "Extract a value-type from obj, its boxed representation, and push a managed pointer to it to the top of the stack"; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_unbox(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [unbox] not done!");
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
			throw new Exception("M6x09_Simulate [unbox] not done!");
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
