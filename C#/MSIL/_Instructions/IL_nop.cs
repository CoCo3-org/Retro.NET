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
	// 0x00 | nop | Do nothing (No operation). | Base instruction

	public class IL_nop : Instruction 
	{
		public override string OpCode { get { return "00"; } }

		public override string Mnemonic { get { return "nop"; } }

		public override string Description { get { return "Do nothing (No operation)."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_nop(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
		}

		public override void Execute() 
		{
			// We literally do nothing here!
			this.ParentMethod.CurrentInstructionIndex++;
		}

		public override void MC680x_UnOptimized_Code(StringBuilder sb) 
		{
			//this.OutputDescCategoryLine(sb);
			sb.AppendLine("\tnop");
		}

		public override void MC680x_Optimized_Code(StringBuilder sb) 
		{
			//this.OutputDescCategoryLine(sb);
			sb.AppendLine("\tnop");
		}

        //public override void M6x09_UnOptimized_Code(StringBuilder sb) 
        //{
        //	this.OutputDescCategoryLine(sb);
        //}

        public override void MC6x09_UnOptimized_Code(StringBuilder sb)
        {
            // base.M6x09_UnOptimized_Code(sb);

			sb.AppendLine("\tnop");
		}

		public override void MC6x09_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}
	}
}
