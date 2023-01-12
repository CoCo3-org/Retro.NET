//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace CPU
{
	public class Instruction : CodeLine
	{
		public virtual byte? PreByte { get { return null; } }
		public virtual byte OpCode { get { throw new NotImplementedException("Type: " + this.GetType()); } }

		public virtual string Desc { get { return null; } }
		public virtual string Category { get { return null; } }

		public Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}
	}
}
