using System;
using System.Collections.Generic;
using System.Text;

namespace MSIL.Sys
{
	public class TypeDefinition
	{
		public virtual string Name { get { throw new Exception("Must Override!"); } }
		public virtual string FullName { get { throw new Exception("Must Override!"); } }

		public Dictionary<string, MethodDefinition> MethodDefinitionDict { get; } = new Dictionary<string, MethodDefinition>();
	}
}
