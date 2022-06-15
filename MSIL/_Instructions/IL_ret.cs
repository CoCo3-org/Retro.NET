using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x2A | ret | Return from method, possibly with a value. | Base instruction

	public class IL_ret : Instruction 
	{
		public override string OpCode { get { return "2A"; } }

		public override string Mnemonic { get { return "ret"; } }

		public override string Description { get { return "Return from method, possibly with a value."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_ret(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			// https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ret

			this.ParentMethod.CurrentInstructionIndex++;
			throw new Exception("Instruction [ret] not done!");
		}

		public override void MC6801_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC6801_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);

			sb.Append("ret");
		}

		public override void MC6809_Simulate() 
		{
			throw new Exception("M6x09_Simulate [ret] not done!");
		}

        //public override void M6x09_UnOptimized_Code(StringBuilder sb) 
        //{
        //	this.OutputDescCategoryLine(sb);
        //}

        public override void MC6809_UnOptimized_Code(StringBuilder sb)
        {
            // base.M6x09_UnOptimized_Code(sb);

			sb.AppendLine("\tret");
        }

        public override void MC6809_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}
	}
}
