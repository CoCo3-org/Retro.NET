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
	// 0x72 | ldstr | Push a string object for the literal string. | Object model instruction

	public class IL_ldstr : Instruction 
	{
		public override string OpCode { get { return "72"; } }

		public override string Mnemonic { get { return "ldstr"; } }

		public override string Description { get { return "Push a string object for the literal string."; } }
		public override string Category { get { return "Object model instruction"; } }

		public string LiteralKey { get; private set; }
		public string LiteralString { get; private set; }

		public IL_ldstr(MethodDefinition parentMethod, Cecil.Cil.Instruction cecilInstruction = null) 
			: base(parentMethod, cecilInstruction)
		{
			this.LiteralString = (string)cecilInstruction.Operand;

			if (!parentMethod.ParentType.ParentModule.StringConstantDictionary.ContainsKey(this.LiteralString))
			{
				this.LiteralKey = "G_STR_" + parentMethod.ParentType.ParentModule.StringConstantCount++;
				parentMethod.ParentType.ParentModule.StringConstantDictionary.Add(this.LiteralString, this.LiteralKey);
			}
			else
				this.LiteralKey = parentMethod.ParentType.ParentModule.StringConstantDictionary[this.LiteralString];
		}

		public override void Execute() 
		{
			this.ParentMethod.ParentType.ParentModule.MethodStack.Push(this.LiteralString);
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

			sb.AppendLine("\tpshs ... --> " + this.LiteralKey);
		}

		public override void MC6x09_Optimized_Code(StringBuilder sb) 
		{
			this.OutputDescCategoryLine(sb);
		}
	}
}
