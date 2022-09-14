using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class BMI_Relative : Instruction, IRelative 
	{
		public BMI_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x2B; } }

		public override string Mnemonic { get { return "BMI"; } }
	}
}
