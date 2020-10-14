using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	public class Instruction
	{
		public Cecil.Cil.Instruction CecilInstruction { get; private set; }
		public MethodDefinition ParentMethod { get; private set; }

		public virtual string OpCode { get { return null; } }

		public virtual string Mnemonic { get { return null; } }
		public virtual string Operand { get { return ""; } }

		public virtual string LineRepr { get { return null; } }

		public virtual string Description { get { return null; } }
		public virtual string Category { get { return null; } }

		public int Index { get; set; }
		public int Offset { get; private set; }

		public Instruction(Cecil.Cil.Instruction instruction, MethodDefinition parentMethod)
		{
			this.CecilInstruction = instruction;
			this.ParentMethod = parentMethod;

			this.Offset = instruction.Offset;
		}

		public virtual void OutputDescCategoryLine(StringBuilder sb) 
		{
			sb.AppendLine("* [" + this.OpCode + "] " + this.Mnemonic + " : " + this.Description + " [" + this.Category + "]");
		}

		public virtual void CilListing() 
		{
			Console.WriteLine($"{this.Offset:X4}: {this.OpCode}\t{this.Mnemonic}\t\t// {this.Description}");
 		}

		public virtual void Execute() 
		{
			throw new Exception("Instruction [] not done!");
		}

		// -----

		public virtual void MC6800_Simulate() 
		{
			throw new Exception("M6x09_Simulate [] not done!");
		}

		public virtual void MC6800_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public virtual void MC6800_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		// -----

		public virtual void MC6801_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public virtual void MC6801_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		// -----

		public virtual void MC6809_Simulate() 
		{
			throw new Exception("M6x09_Simulate [] not done!");
		}

		public virtual void MC6809_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public virtual void MC6809_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		// -----

		public virtual void MC68000_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public virtual void MC68000_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		// -----

		public virtual void Z80_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public virtual void Z80_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		// -----

		public virtual void MOS6502_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public virtual void MOS6502_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		// -----

		public virtual void X86_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public virtual void X86_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		// -----

		public virtual void ARM_UnOptimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}

		public virtual void ARM_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}
	}
}
