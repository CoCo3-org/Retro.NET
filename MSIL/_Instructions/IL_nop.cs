using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x00 | nop | Do nothing (No operation). | Base instruction

	public class IL_nop : Instruction 
	{
		public override string OpCode { get { return "00"; } }

		public override string Mnemonic { get { return "nop"; } }

		public override string Description { get { return "Do nothing (No operation)."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_nop(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			// We literally do nothing here!
			this.ParentMethod.CurrentInstructionIndex++;
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
			throw new Exception("M6x09_Simulate [nop] not done!");
		}

        //public override void M6x09_UnOptimized_Code(StringBuilder sb) 
        //{
        //	this.OutputDescCategoryLine(sb);
        //}

        public override void MC6809_UnOptimized_Code(StringBuilder sb)
        {
            // base.M6x09_UnOptimized_Code(sb);

			sb.AppendLine("\tnop");
		}

		public override void MC6809_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}
	}
}
