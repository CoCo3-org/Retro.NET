using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retro.NET.CPUGen
{
	public class MOS6502 : CPUClass
	{
		public override string XmlTag => "MOS6502";

		public MOS6502()
		{
			OpCode op = null;

			op = new(); op.Op = 0x00; op.Mnem = "BRK"; op.Mode = "Implied"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x01; op.Mnem = "ORA"; op.Mode = "IndirectX"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x02; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x03; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x04; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x05; op.Mnem = "ORA"; op.Mode = "ZeroPage"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x06; op.Mnem = "ASL"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x07; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x08; op.Mnem = "PHP"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x09; op.Mnem = "ORA"; op.Mode = "Immediate"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x0A; op.Mnem = "ASL"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x0B; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x0C; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x0D; op.Mnem = "ORA"; op.Mode = "Absolute"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x0E; op.Mnem = "ASL"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x0F; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);

			op = new(); op.Op = 0x10; op.Mnem = "BPL"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x11; op.Mnem = "ORA"; op.Mode = "IndirectY"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x12; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x13; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x14; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x15; op.Mnem = "ORA"; op.Mode = "ZeroPageX"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x16; op.Mnem = "ASL"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x17; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x18; op.Mnem = "CLC"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x19; op.Mnem = "ORA"; op.Mode = "AbsoluteY"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x1A; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x1B; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x1C; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x1D; op.Mnem = "ORA"; op.Mode = "AbsoluteX"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x1E; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x1F; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);

			op = new(); op.Op = 0x20; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x21; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x22; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x23; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x24; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x25; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x26; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x27; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x28; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x29; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x2A; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x2B; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x2C; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x2D; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x2E; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x2F; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);

			op = new(); op.Op = 0x30; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x31; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x32; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x33; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x34; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x35; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x36; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x37; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x38; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x39; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x3A; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x3B; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x3C; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x3D; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x3E; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
			op = new(); op.Op = 0x3F; op.Mnem = "---"; op.Mode = "---"; this.OpCodes.Add(op);
		}

		public void ReadUntitled1() 
		{
			StreamReader sr = new StreamReader(@"C:\Users\jross\Desktop\Untitled1.txt");

			while (!sr.EndOfStream)
			{
				string ln = sr.ReadLine();
				string[] array = ln.Split('\t');
				array[array.Length - 1] = array[array.Length - 1].TrimEnd();

				Console.WriteLine(string.Join("][", array) + " - " + array.Length);
				if (array[2] != "")
				{
					int op_code = Convert.ToInt16(array[2], 16);
					this.OpCodes[op_code].Mode = "Implied";
				}

				if (array[3] != "")
				{
					int op_code = Convert.ToInt16(array[3], 16);
					this.OpCodes[op_code].Mode = "IndirectX";
				}

				if (array[4] != "")
				{
					int op_code = Convert.ToInt16(array[4], 16);
					this.OpCodes[op_code].Mode = "IndirectY";
				}

				if (array[5] != "")
				{
					int op_code = Convert.ToInt16(array[5], 16);
					this.OpCodes[op_code].Mode = "Absolute";
				}

				if (array[6] != "")
				{
					int op_code = Convert.ToInt16(array[6], 16);
					this.OpCodes[op_code].Mode = "AbsoluteX";
				}

				if (array[7] != "")
				{
					int op_code = Convert.ToInt16(array[7], 16);
					this.OpCodes[op_code].Mode = "Immediate";
				}

				if (array[8] != "")
				{
					int op_code = Convert.ToInt16(array[8], 16);
					this.OpCodes[op_code].Mode = "AbsoluteY";
				}

				if (array[9] != "")
				{
					int op_code = Convert.ToInt16(array[9], 16);
					this.OpCodes[op_code].Mode = "ZeroPage";
				}

				if (array[10] != "")
				{
					int op_code = Convert.ToInt16(array[10], 16);
					this.OpCodes[op_code].Mode = "ZeroPageX";
				}
			}

			sr.Close();
		}

		public void ReadUntitled2()
		{
			StreamReader sr = new StreamReader(@"C:\Users\jross\Desktop\Untitled2.txt");

			int x = 0;
			while (!sr.EndOfStream)
			{
				string ln = sr.ReadLine().Trim();

				if (x <= this.OpCodes.Count - 1)
				{
					if (this.OpCodes[x].Mnem != ln)
						this.OpCodes[x].Mnem = ln;
				}
				else
				{
					OpCode op = new OpCode();
					op.Op = x;
					op.Mnem = ln;
					op.Mode = "---";
					this.OpCodes.Add(op);
				}
				x++;
			}

			sr.Close();
		}

		public void ReadUntitled3()
		{
			Console.WriteLine("=====================================================");

			StreamReader sr = new StreamReader(@"C:\Users\jross\Desktop\Untitled3.txt");

			while (!sr.EndOfStream)
			{
				string ln = sr.ReadLine().Trim();

				string mode = ln.Substring(0, 14).TrimEnd().Replace(" ","").Replace(",","");
				string mnem = ln.Substring(14, 3);
				string sample = ln.Substring(18, 10).TrimEnd();
				string opstr = ln.Substring(29, 2);
				string bytes = ln.Substring(33, 1);
				string cycles = ln.Substring(37).TrimEnd();

				Console.WriteLine($"[{mode}][{mnem}][{sample}][{opstr}][{bytes}][{cycles}]");

				int op_code = Convert.ToInt16(opstr, 16);

				if (this.OpCodes[op_code].Mnem != mnem)
				{
					Console.WriteLine($"{this.OpCodes[op_code].Mnem} != {mnem}");
					this.OpCodes[op_code].Mnem = mnem;
				}

				if (this.OpCodes[op_code].Mode != mode)
				{
					Console.WriteLine($"{this.OpCodes[op_code].Mode} != {mode}");
					this.OpCodes[op_code].Mode = mode;
				}

				this.OpCodes[op_code].Sample = sample;

				this.OpCodes[op_code].NBytes = int.Parse(bytes);
				this.OpCodes[op_code].NCycles = int.Parse(cycles.Substring(0,1));
				this.OpCodes[op_code].PlusCycles = cycles.EndsWith('+');
			}

			sr.Close();

		}
	}
}
