using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ROL_Extended : ROL_Instruction, IExtended 
	{
		public override byte OpCode { get { return 0x79; } }
	}
}
