﻿using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace Retro.NET
{
	class Program
	{
		static void Main(string[] args)
		{
			StreamWriter sw = new StreamWriter(@"..\..\..\_countLog.txt", true);
			sw.WriteLine("91919\t" + "Samples" + "\t" + System.DateTime.Now + "\t" + "Manual Testing");
			sw.Close();

			MSIL.ModuleDefinition mod = new MSIL.ModuleDefinition(@"..\..\..\Samples\00-NopApp.exe");
			// MSIL.ModuleDefinition mod = new MSIL.ModuleDefinition(@"..\..\..\Samples\01-HelloWorld.exe");
			// MSIL.ModuleDefinition mod = new MSIL.ModuleDefinition(@"..\..\..\Samples\02-HelloWorld-In-Method.exe");
			// MSIL.ModuleDefinition mod = new MSIL.ModuleDefinition(@"..\..\..\Samples\03-HelloWorld-In-Class.exe");
			// MSIL.ModuleDefinition mod = new MSIL.ModuleDefinition(@"..\..\..\Samples\05-HelloWorld-In-Classes2.exe");

			mod.Initialize();
			mod.CilListing();
			mod.MC680x_AssemblyLanguage(@"..\..\..\Samples\00-NopApp.a01");
			mod.MC6x09_AssemblyLanguage(@"..\..\..\Samples\00-NopApp.a09");
			
			// -------------------------------------------------- move to function


			// --------------------------------------------------

			mod.Execute();

			Console.WriteLine("Done!");
            Console.ReadKey();
		}
	}
}
