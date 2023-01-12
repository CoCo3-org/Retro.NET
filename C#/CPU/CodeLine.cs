//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace CPU
{
	public class CodeLine : XmlNode
	{
		public Label Label { get; set; }
		public int Address { get; set; }

		public virtual string Mnemonic { get { return null; } }
		
		public Module Module { get; set; }
		public CodeBlock CodeBlock { get; set; }

		public override string XmlTag => this.Mnemonic;

		public CodeLine(CodeBlock codeBlock) 
			: base(codeBlock)
		{
			this.CodeBlock = codeBlock;
			if (CodeBlock != null)
			{
				if (codeBlock.GetType() == typeof(Module))
					this.Module = (Module)codeBlock;
				else
					this.Module = codeBlock.Module;
			}
		}
	}
}
