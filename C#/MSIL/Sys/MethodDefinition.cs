//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

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
