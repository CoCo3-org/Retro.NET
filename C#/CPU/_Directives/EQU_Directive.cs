//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace CPU
{
	public class EQU_Directive : Directive
	{
		public override string Mnemonic { get { return "EQU"; } }

		public EQU_Directive(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}
	}
}
