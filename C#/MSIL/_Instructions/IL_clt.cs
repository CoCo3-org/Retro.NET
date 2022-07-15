using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0xFE04 | clt | Push 1 (of type int32) if value1 < value2, else push 0. | Base instruction

	public class IL_clt : Instruction 
	{
		public override string OpCode { get { return "FE04"; } }

		public override string Mnemonic { get { return "clt"; } }

		public override string Description { get { return "Push 1 (of type int32) if value1 < value2, else push 0."; } }
		public override string Category { get { return "Base instruction"; } }

		public IL_clt(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

		public override void Execute() 
		{
			// value1 is pushed onto the stack.
			// value2 is pushed onto the stack.
			// value2 and value1 are popped from the stack; clt tests if value1 is less than value2.
			// If value1 is less than value2, 1 is pushed onto the stack; otherwise 0 is pushed onto the stack.

			object value2 = this.ParentMethod.ParentType.ParentModule.MethodStack.Pop();
			object value1 = this.ParentMethod.ParentType.ParentModule.MethodStack.Pop();

			// Console.WriteLine($"{value1} < {value2}");
			// Console.WriteLine($"{value1.GetType()} < {value2.GetType()}");

			if (value1.GetType() == typeof(int))
			{
				if ((int)value1 < (int)value2)				
					this.ParentMethod.ParentType.ParentModule.MethodStack.Push(1);
				else
					this.ParentMethod.ParentType.ParentModule.MethodStack.Push(0);
			}

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

		public override void MC6x09_Simulate() 
		{
			throw new Exception("M6x09_Simulate [clt] not done!");
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
