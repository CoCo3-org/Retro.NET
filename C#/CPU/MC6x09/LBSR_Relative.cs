//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace CPU.MC6x09
{
	public class LBSR_Relative : Instruction, IRelative 
	{
		public LBSR_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x17; } }

		public override string Mnemonic { get { return "LBSR"; } }
	}
}
