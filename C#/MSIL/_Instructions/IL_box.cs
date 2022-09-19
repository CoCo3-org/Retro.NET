using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x8C | box | Convert a boxable value to its boxed form | Object model instruction

	public class IL_box : Instruction 
	{
		public override string OpCode { get { return "8C"; } }

		public override string Mnemonic { get { return "box"; } }

		public override string Description { get { return "Convert a boxable value to its boxed form"; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_box(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
			// Console.WriteLine("IL_box: " + cecilInstruction.Operand.GetType());
			// Cecil.TypeReference typeReference = (Cecil.TypeReference)cecilInstruction.Operand;
			// Console.WriteLine("IL_box: " + typeReference.FullName);
			// Console.WriteLine("IL_box: " + typeReference.ToString());
		}

		public override void Execute() 
		{
			object obj = this.ParentMethod.ParentType.ParentModule.MethodStack.Pop();
			// do conversion -- but since our stack is ALWAYS object then we do nothing
			this.ParentMethod.ParentType.ParentModule.MethodStack.Push(obj);
			this.ParentMethod.CurrentInstructionIndex++;
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
