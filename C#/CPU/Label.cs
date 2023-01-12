//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace CPU
{
	public class Label : XmlNode
	{
		public string Text { get; set; }
		public CodeLine CodeLine { get; set; }

		public Label(CodeLine codeLine)  
			: base(codeLine)
		{
			this.CodeLine = codeLine;
		}
	}
}
