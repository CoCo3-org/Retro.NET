using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BMI_Relative : Instruction, IRelative
	{
		public override byte OpCode { get { return 0x2B; } }

		public override string Mnemonic { get { return "BMI"; } }
	}
}
