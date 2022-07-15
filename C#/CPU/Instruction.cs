using System;
using System.Collections.Generic;
using System.Text;

namespace CPU
{
	public class Instruction : CodeLine
	{
		public virtual int PreOp { get { return -1; } }
		public virtual int OpCode { get { return -1; } } 

		public virtual string Desc { get { return null; } }
		public virtual string Category { get { return null; } }
	}
}
