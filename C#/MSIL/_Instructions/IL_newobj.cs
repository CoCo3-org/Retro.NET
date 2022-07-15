using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	// 0x73 | newobj | Allocate an uninitialized object or value type and call ctor. | Object model instruction

	public class IL_newobj : Instruction 
	{
		public override string OpCode { get { return "73"; } }

		public override string Mnemonic { get { return "newobj"; } }

		public override string Description { get { return "Allocate an uninitialized object or value type and call ctor."; } }
		public override string Category { get { return "Object model instruction"; } }

		public IL_newobj(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction) 
			: base(parentMethod ,cecilInstruction)
		{
		}

        public override void CilListing() 
        {
			Console.WriteLine($"{this.Offset:X4}: {this.OpCode}\t{this.Mnemonic}\t{this.Operand.GetType()}\t\t// {this.Description}");
			// Console.WriteLine($"{this.Offset:X4}: {this.OpCode}\t{this.Mnemonic}\t\t// {this.Description}");
		}

		public override void Execute() 
		{
			// Creates a new object or a new instance of a value type, 
			// pushing an object reference(type O) onto the evaluation stack.

			// https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.newobj
			
			//- Arguments arg1 through argn are pushed on the stack in sequence.
			//- Arguments argn through arg1 are popped from the stack and passed to ctor for object creation.
			//- A reference to the new object is pushed onto the stack.

			Cecil.MethodDefinition method_to_call = (Cecil.MethodDefinition)this.CecilInstruction.Operand;
			Console.WriteLine("newobj [" + method_to_call.Name + "][" + method_to_call.FullName + "]");

			foreach(var td_key in this.ParentMethod.ParentType.ParentModule.TypeDefinitionDictionary.Keys)
            {
				if (td_key == "<Module>")
					continue;

				TypeDefinition td = this.ParentMethod.ParentType.ParentModule.TypeDefinitionDictionary[td_key];
				Console.WriteLine($"{td_key} {td.CecilType.Name} -> {td.CecilType.FullName}");
            }

			this.ParentMethod.CurrentInstructionIndex++;

			throw new Exception("Instruction [newobj] not done!");
		}

		public override void MC680x_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);

			sb.AppendLine("\tJSR obj_alloc");
			sb.AppendLine("\tPSHX");
		}

		public override void MC680x_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public override void MC6x09_Simulate() 
		{
			throw new Exception("M6x09_Simulate [newobj] not done!");
		}

		public override void MC6x09_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);

			sb.AppendLine($"\tjsr ... --> allocate_obj");
			sb.AppendLine($"\tjsr ... --> [{this.Operand}][{this.Offset}]");
		}

		public override void MC6x09_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}
	}
}
