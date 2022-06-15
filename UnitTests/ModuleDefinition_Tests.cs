using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	using NUnit.Framework;

	[TestFixture]
	public class ModuleDefinition_Tests
	{
		[Test]
		public void Constructor()
		{
			MSIL.ModuleDefinition mod = new MSIL.ModuleDefinition(@"..\..\..\Samples\05-HelloWorld-In-Classes2.exe");
			Assert.IsTrue(false);
		}
	}
}
