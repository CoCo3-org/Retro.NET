using System;

namespace Sample
{
	class ClassWithMembers
	{
		bool BoolVar { get; set; }
		byte ByteVar { get; set; }
		char CharVar { get; set; }
		int IntVar { get; set; }
		string StrVar { get; set; }
	}

    class Program
    {
		static void Main()
        {
			var classWithMembers = new ClassWithMembers();
        }
    }
}
