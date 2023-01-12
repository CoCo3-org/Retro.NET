//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace Emu.CPU.MC680x
{
	public class MC6800 : Cpu
	{
		int _regA, _regB;
		public int REG_A 
		{
			get { return _regA; }
			set
			{
				_regA = value; // JAR_NOTE do I need the mask?
				//_regD = (_regD & 0xFF00) | _regA;
			}
		}
		public int REG_B 
		{
			get { return _regB; }
			set
			{
				_regB = value; // JAR_NOTE do I need the mask?
				//_regD = (_regD & 0x00FF) | _regB << 8;
			}
		}

		public int REG_SP; // Stack pointer
		public int REG_IP; // INDEX pointer
		public int REG_PC; // Program counter

		// ------------------------------------------------------------------
		// Set and Fetch Memory
		// ------------------------------------------------------------------
		public void SetMemory(int address, int value) 
		{
			address &= 0xFFFF;
			value &= 0xFF;
		}

		public void SetMemory16(int address, int value) 
		{
			address &= 0xFFFF;
			value &= 0xFFFF;
			this.SetMemory(address, value >> 8);
			this.SetMemory(address + 1, value & 0xFF);
		}

		public int FetchMemory(int address) 
		{
			address &= 0xFFFF;

			return this.BusManager.ReadByte(address);
		}

		// ------------------------------------------------------------------
		//
		// ------------------------------------------------------------------
		public void PushStack(int value) 
		{
			this.SetMemory(this.REG_SP, value);
			this.REG_SP--;
			this.REG_SP &= 0xFFFF;
		}

		public void PushStack16(int value) 
		{
			this.SetMemory16(this.REG_SP - 1, value);
			this.REG_SP -= 2;
			this.REG_SP &= 0xFFFF;
		}

		public int PopStack() 
		{
			this.REG_SP++;
			this.REG_SP &= 0xFFFF;
			return this.FetchMemory(this.REG_SP);
		}

		public int PopStack16() 
		{
			this.REG_SP += 2;
			this.REG_SP &= 0xFFFF;
			return this.FetchMemory(this.REG_SP) + (this.FetchMemory(this.REG_SP - 1) << 8);
		}

		// ------------------------------------------------------------------
		//
		// ------------------------------------------------------------------
		public void NOP() 
		{
			this.REG_PC++;
			this.REG_PC &= 0xFFFF;
		}

		public void RTS() 
		{
			this.REG_PC = this.PopStack16();
		}
	}
}
