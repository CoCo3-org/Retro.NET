using System;

namespace Sample
{
	class ClassWithMembers
	{
		bool BoolVar;
		byte ByteVar;
		char CharVar;
		int IntVar;
		string StrVar;
	}

    class Program
    {
		static void Main()
        {
			var classWithMembers = new ClassWithMembers();
        }
    }
}
