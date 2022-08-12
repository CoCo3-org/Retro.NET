using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class COM_Extended : COM_Instruction, IExtended
	{
		public override byte OpCode { get { return 0x73; } }
	}
}
