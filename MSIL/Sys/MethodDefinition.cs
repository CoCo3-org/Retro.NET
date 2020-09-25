using System;
using System.Collections.Generic;
using System.Text;

namespace MSIL.Sys
{
	public class MethodDefinition
	{
		public virtual string Name { get { throw new Exception("Must Override!"); } }
		public virtual string FullName { get { throw new Exception("Must Override!"); } }

		public virtual void Execute(IL_call call)
		{
			throw new Exception("Must Override!");
		}
	}
}
