using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests.CPU
{
	[TestFixture]
	public class Module_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var module = new global::CPU.Module();

			Assert.That(module.XmlTag, Is.EqualTo("Module"));
		}
	}
}
