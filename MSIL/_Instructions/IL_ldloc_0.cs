using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x06 | ldloc.0 | Load local variable 0 onto stack. | Base instruction

	public class IL_ldloc_0 : Instruction 
	{
		public override string OpCode { get { return "06"; } }

		public override string Mnemonic { get { return "ldloc.0"; } }

		public override string Description { get { return "Load local variable 0 onto stack."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_ldloc_0(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			object obj = this.ParentMethod.LocalVariables[0];
			this.ParentMethod.ParentType.ParentModule.MethodStack.Push(obj);
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
			throw new Exception("M6x09_Simulate [ldloc.0] not done!");
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
