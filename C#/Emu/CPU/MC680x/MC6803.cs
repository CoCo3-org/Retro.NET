using System;
using System.Collections.Generic;
using System.Text;

namespace Emu.CPU.MC680x
{
    public class MC6803 : MC6800
    {
		//public MC10 mc10;

		public int idx = 0;
		// this.history = [];

		// Addressing modes
		public const int DIRECT = 1;
		public const int INDEX = 2;
		public const int EXTENDED = 3;
		public const int RELATIVE = 4;
		public const int IMMEDIATE = 5;
		public const int INHERENT = 6;

		public int INT_NONE = 0; // No interrupt required
		public int INT_IRQ = 1; // Standard IRQ interrupt
		public int INT_NMI = 2; // NMI interrupt
		public int WAI_ = 8; // set when WAI is waiting for an interrupt
		public int SLP = 0x10;
		public int IRQ_LINE = 0; // IRQ line number
		public int TIN_LINE = 1; // P20/Tin Input Capture line (eddge sense)
		public int CLEAR_LINE = 0; // clear (a fired, held or pulsed) line
		public int ASSERT_LINE = 1; // assert an interrupt immediately
		public int HOLD_LINE = 2; // hold interrupt line until enable is true
		public int PULSE_LINE = 3; // pulse interrupt line for one instruction

		public int TCSR_OLVL = 0x01;
		public int TCSR_IEDG = 0x02;
		public int TCSR_ETOI = 0x04;
		public int TCSR_EOCI = 0x08;
		public int TCSR_EICI = 0x10;
		public int TCSR_TOF = 0x20;
		public int TCSR_OCF = 0x40;
		public int TCSR_ICF = 0x80;

		public delegate int OpTableDelegate();
		public OpTableDelegate[] optable = new OpTableDelegate[256];

		public int cycleCount;

		// public MC6847 VDG;

		//// public var memoryBuffer:Int;
		public byte[] Memory; // = new byte[xxx];
	    //// public var port1Buffer:Int;
		public byte[] Port1;
		//// public var port2Buffer:Int;
		public byte[] Port2;

		public int memmode;
		public int printBuffer;
		public int[] irqState;
	    public int waiState;
		public int nmiState;
		public int extraCycles;
		public int irq2;
		public int pendingTCSR;

		public int F_CARRY;
		public int F_OVERFLOW;
		public int F_ZERO;
		public int F_SIGN;
		public int F_INTERRUPT;
		public int F_HALFCARRY;

		// public int REG_ACC; // Accumulator

		int _regD;
		public int REG_D 
		{
			get { return _regD; }
			set
			{
				_regD = value; // JAR_NOTE do I need the mask?
				this.REG_A = _regD & 0x00FF;
				this.REG_B = _regD >> 8; // JAR_NOTE do I need the mask?
			}
		}


		public byte[] ROM;

		public MC6803() 
		{
			// this.mc10 = mc;
			// this.ROM = Bytes.alloc(8 * 1024);
			// this.Init_Rom();
			// this.Init();
		}

		public void Print() 
		{
			string flags = "";
			if (this.F_CARRY != 0) flags += "C"; else flags += ".";
			if (this.F_OVERFLOW != 0) flags += "O"; else flags += ".";
			if (this.F_ZERO != 0) flags += "Z"; else flags += ".";
			if (this.F_SIGN != 0) flags += "S"; else flags += ".";
			if (this.F_INTERRUPT != 0) flags += "I"; else flags += ".";
			if (this.F_HALFCARRY != 0) flags += "H"; else flags += ".";

			Console.WriteLine("B={0} A={1} D={2} X={3} SP={4} PC={5} {6} OP={7} MNEM=?",
				this.REG_B.ToString("X2"),
				this.REG_A.ToString("X2"),
				this.REG_D.ToString("X4"),
				this.REG_IP.ToString("X4"),
				this.REG_SP.ToString("X4"),
				this.REG_PC.ToString("X4"),
				flags,
				this.FetchMemory(this.REG_PC).ToString("X2")
				//this.REG_A.ToString("X"),
				);
		}

		public void Init() 
		{
			//// this.memoryBuffer = new ArrayBuffer(0xC000);
			this.Memory = new byte[0xC000]; // new Uint8Array(this.memoryBuffer);

			//// this.port1Buffer = new ArrayBuffer(8);
			this.Port1 = new byte[8]; // new Uint8Array(this.port1Buffer);

			////  this.port2Buffer = new ArrayBuffer(8);
			this.Port2 = new byte[8]; // new Uint8Array(this.port2Buffer);

			this.cycleCount = 0;

			//this.printBuffer = new Array();
			this.irqState = new int[10]; // Array<Int>();  //JAR_NOTE; HOW LARGE??

			this.waiState = 0;
			this.nmiState = 0;
			this.extraCycles = 0;
			this.irq2 = 0;
			this.pendingTCSR = 0;

			// this.REG_ACC = new Uint8Array(this.accumBuffer);
			// this.REG_D = new Uint16Array(this.accumBuffer);
			// this.REG_B = this.REG_ACC.subarray(0, 1);
			// this.REG_A = this.REG_ACC.subarray(1, 2);

			for (int i = 0; i < 0xC000; i++)
				this.Memory[i] = 0x00;

			for (int i = 0; i < 8; i++)
			{
				this.Port1[i] = 0xFF;
				this.Port2[i] = 0xFF;
			}

			this.irqState[this.IRQ_LINE] = 0;
			this.irqState[this.TIN_LINE] = 0;

			this.F_CARRY = 0;
			this.F_OVERFLOW = 0;
			this.F_ZERO = 0;
			this.F_SIGN = 0;
			this.F_INTERRUPT = 0;
			this.F_HALFCARRY = 0;

			this.REG_D = 0;
			this.REG_SP = 0;
			this.REG_IP = 0;

			this.InitTable();

			this.Reset();
		}

		public void Reset() 
		{
			this.REG_PC = (this.FetchMemory(0xFFFE) << 8) + this.FetchMemory(0xFFFF);

			this.SEI(); // IRQ disabled

			this.Memory[0x0b] = 0xFF; // output compare register defaults
			this.Memory[0x0c] = 0xFF;
			this.Memory[0x11] = 0x20; // transmit control status registers

			this.waiState = 0;
			this.nmiState = 0;
			this.irqState[this.IRQ_LINE] = 0;
			this.irqState[this.TIN_LINE] = 0;
			this.irq2 = 0;
			this.pendingTCSR = 0;
			this.cycleCount = 0;
		}

		public int Emulate() 
		{
			var lastpc = this.REG_PC;

			if ((this.waiState & this.WAI_) != 0)
			{
				Console.WriteLine("IN WAI");
				this.cycleCount = (this.cycleCount + 1) & 0xFFFF;
				this.CheckTimer();
				return 1;
			}

			var opcode = this.FetchOpCode();

			if (opcode < this.optable.Length)
			{
				var cycles = this.optable[opcode]();

				for (var i = 0; i < cycles; i++) 
				{
					this.cycleCount = (this.cycleCount + 1) & 0xFFFF;
					this.CheckTimer();
				}

				//  var cc = this.flagsToVariable();
				//  this.history.push({ OP: opcode.toString(16), last_PC: lastpc, PC: this.REG_PC, D: this.REG_D, X: this.REG_IP, S: this.REG_SP, CC: cc });
				//  this.idx++;

				this.cycleCount += this.extraCycles;
				this.extraCycles = 0;

				return cycles;
			}

			//  this.checkIRQLines();

			Console.WriteLine("unknown opcode: " + opcode);
			return 0;
		}

		public void CheckTimer() 
		{
			// TODO: input capture interrupt

			/*
			 * Output compare
			 */
			if (this.cycleCount == (this.Memory[0x0b] << 8) + (this.Memory[0x0c] & 0xFF))
			{
				this.Memory[0x08] |= (byte)this.TCSR_OCF; // set OCF (output compare flag)
				this.pendingTCSR |= this.TCSR_OCF;
				this.ModifiedTCSR();
				if (this.F_INTERRUPT == 0 && ((this.Memory[0x08] & this.TCSR_EOCI) != 0))
					this.TakeOCI();

			}

			/*
			 * Timer overflow
			*/
			if (this.cycleCount == 0xFFFF)
			{
				this.Memory[0x08] |= (byte)this.TCSR_TOF; // set TOF (timer overflow flag)
				this.pendingTCSR |= this.TCSR_TOF;
				this.ModifiedTCSR();
				if (this.F_INTERRUPT == 0 && ((this.Memory[0x08] & this.TCSR_ETOI) != 0))
					this.TakeTOI();
			}
		}

		public void Interrupt(int vector) 
		{
			if ((this.waiState & (this.WAI_ | this.SLP)) != 0)
			{
				if ((this.waiState & this.WAI_) != 0)
				{
					this.extraCycles += 4;
				}
				this.waiState &= ~(this.WAI_ | this.SLP);
			}
			else
			{
				this.PushStack16(this.REG_PC);
				this.PushStack16(this.REG_IP);
				this.PushStack(this.REG_A);
				this.PushStack(this.REG_B);
				this.PushStack(this.FlagsToVariable());
				this.extraCycles += 12;
			}
			this.SEI();

			this.REG_PC = vector;
			this.REG_PC &= 0xFFFF;
		}

		////checkIRQLines: function () [[
		////    if (this.F_INTERRUPT == 0) [[
		////        if (this.irqState[this.IRQ_LINE] != this.CLEAR_LINE) [[
		////            this.interrupt(0xFFf8);
		////        ]] else [[
		////            this.checkIRQ2();
		////        ]]
		////    ]]
		////]],

		////checkIRQ2: function () [[
		////    //if ((this.irq2 & this.TCSR_ICF) != 0) [[
		////    //    this.takeICI();
		////    //]] else if ((this.irq2 & this.TCSR_OCF) != 0) [[
		////    //    this.takeOCI();
		////    //]] else if ((this.irq2 & this.TCSR_TOF) != 0) [[
		////    //    this.takeTOI();
		////    //]]
		////]],

		public void TakeICI() 
		{
			this.Interrupt(0x4209);
		}

		public void TakeOCI() 
		{
			this.Interrupt(0x4206);
		}

		public void TakeTOI() 
		{
			this.Interrupt(0x4203);
		}

		public void TakeSCI() 
		{
			this.Interrupt(0x4200);
		}

		public void TakeTRAP() 
		{
			this.Interrupt(0xF72E);
		}

		public void ModifiedTCSR() 
		{
			//this.irq2 = (this.memory[0x08] & (this.memory[0x08] << 3)) & (this.TCSR_ICF | this.TCSR_OCF | this.TCSR_TOF);
			this.irq2 = (this.Memory[0x08] & (this.TCSR_ICF | this.TCSR_OCF | this.TCSR_TOF));
		}

		public void InitTable() 
		{
			var self = this;

			//ABA
			this.optable[0x1b] = delegate () { self.memmode = MC6803.INHERENT; self.ABA(); return 2; };

			//ABX
			this.optable[0x3a] = delegate () { self.memmode = MC6803.INHERENT; self.ABX(); return 3; };

			//ADCA
			this.optable[0x89] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ADCA(); return 2; };
			this.optable[0x99] = delegate () { self.memmode = MC6803.DIRECT; self.ADCA(); return 3; };
			this.optable[0xa9] = delegate () { self.memmode = MC6803.INDEX; self.ADCA(); return 4; };
			this.optable[0xb9] = delegate () { self.memmode = MC6803.EXTENDED; self.ADCA(); return 4; };

			//ADCB 
			this.optable[0xc9] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ADCB(); return 2; };
			this.optable[0xd9] = delegate () { self.memmode = MC6803.DIRECT; self.ADCB(); return 3; };
			this.optable[0xe9] = delegate () { self.memmode = MC6803.INDEX; self.ADCB(); return 4; };
			this.optable[0xf9] = delegate () { self.memmode = MC6803.EXTENDED; self.ADCB(); return 4; };

			//ADDA 
			this.optable[0x8b] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ADDA(); return 2; };
			this.optable[0x9b] = delegate () { self.memmode = MC6803.DIRECT; self.ADDA(); return 3; };
			this.optable[0xab] = delegate () { self.memmode = MC6803.INDEX; self.ADDA(); return 4; };
			this.optable[0xbb] = delegate () { self.memmode = MC6803.EXTENDED; self.ADDA(); return 4; };

			//ADDB 
			this.optable[0xcb] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ADDB(); return 2; };
			this.optable[0xdb] = delegate () { self.memmode = MC6803.DIRECT; self.ADDB(); return 3; };
			this.optable[0xeb] = delegate () { self.memmode = MC6803.INDEX; self.ADDB(); return 4; };
			this.optable[0xfb] = delegate () { self.memmode = MC6803.EXTENDED; self.ADDB(); return 4; };

			//ADDD 
			this.optable[0xc3] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ADDD(); return 4; };
			this.optable[0xd3] = delegate () { self.memmode = MC6803.DIRECT; self.ADDD(); return 5; };
			this.optable[0xe3] = delegate () { self.memmode = MC6803.INDEX; self.ADDD(); return 6; };
			this.optable[0xf3] = delegate () { self.memmode = MC6803.EXTENDED; self.ADDD(); return 6; };

			//ANDA 
			this.optable[0x84] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ANDA(); return 2; };
			this.optable[0x94] = delegate () { self.memmode = MC6803.DIRECT; self.ANDA(); return 3; };
			this.optable[0xa4] = delegate () { self.memmode = MC6803.INDEX; self.ANDA(); return 4; };
			this.optable[0xb4] = delegate () { self.memmode = MC6803.EXTENDED; self.ANDA(); return 4; };

			//ANDB 
			this.optable[0xc4] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ANDB(); return 2; };
			this.optable[0xd4] = delegate () { self.memmode = MC6803.DIRECT; self.ANDB(); return 3; };
			this.optable[0xe4] = delegate () { self.memmode = MC6803.INDEX; self.ANDB(); return 4; };
			this.optable[0xf4] = delegate () { self.memmode = MC6803.EXTENDED; self.ANDB(); return 4; };

			//ASL 
			this.optable[0x68] = delegate () { self.memmode = MC6803.INDEX; self.ASL(); return 6; };
			this.optable[0x78] = delegate () { self.memmode = MC6803.EXTENDED; self.ASL(); return 6; };

			//ASLA 
			this.optable[0x48] = delegate () { self.memmode = MC6803.INHERENT; self.ASLA(); return 2; };

			//ASLB 
			this.optable[0x58] = delegate () { self.memmode = MC6803.INHERENT; self.ASLB(); return 2; };

			//ASLD 
			this.optable[0x05] = delegate () { self.memmode = MC6803.INHERENT; self.ASLD(); return 3; };

			//ASR 
			this.optable[0x67] = delegate () { self.memmode = MC6803.INDEX; self.ASR(); return 6; };
			this.optable[0x77] = delegate () { self.memmode = MC6803.EXTENDED; self.ASR(); return 6; };

			//ASRA 
			this.optable[0x47] = delegate () { self.memmode = MC6803.INHERENT; self.ASRA(); return 2; };

			//ASRB 
			this.optable[0x57] = delegate () { self.memmode = MC6803.INHERENT; self.ASRB(); return 2; };

			//BRA 
			this.optable[0x20] = delegate () { self.memmode = MC6803.RELATIVE; self.BRA(); return 3; };

			//BRN 
			this.optable[0x21] = delegate () { self.memmode = MC6803.RELATIVE; self.BRN(); return 3; };

			//BCC 
			this.optable[0x24] = delegate () { self.memmode = MC6803.RELATIVE; self.BCC(); return 3; };

			//BCS 
			this.optable[0x25] = delegate () { self.memmode = MC6803.RELATIVE; self.BCS(); return 3; };

			//BEQ	
			this.optable[0x27] = delegate () { self.memmode = MC6803.RELATIVE; self.BEQ(); return 3; };

			//BGE	
			this.optable[0x2c] = delegate () { self.memmode = MC6803.RELATIVE; self.BGE(); return 3; };

			//BGT	
			this.optable[0x2e] = delegate () { self.memmode = MC6803.RELATIVE; self.BGT(); return 3; };

			//BHI	
			this.optable[0x22] = delegate () { self.memmode = MC6803.RELATIVE; self.BHI(); return 3; };

			//BLE	
			this.optable[0x2f] = delegate () { self.memmode = MC6803.RELATIVE; self.BLE(); return 3; };

			//BLS	
			this.optable[0x23] = delegate () { self.memmode = MC6803.RELATIVE; self.BLS(); return 3; };

			//BLT	
			this.optable[0x2d] = delegate () { self.memmode = MC6803.RELATIVE; self.BLT(); return 3; };

			//BMI	
			this.optable[0x2b] = delegate () { self.memmode = MC6803.RELATIVE; self.BMI(); return 3; };

			//BNE	
			this.optable[0x26] = delegate () { self.memmode = MC6803.RELATIVE; self.BNE(); return 3; };

			//BVC	
			this.optable[0x28] = delegate () { self.memmode = MC6803.RELATIVE; self.BVC(); return 3; };

			//BVS	
			this.optable[0x29] = delegate () { self.memmode = MC6803.RELATIVE; self.BVS(); return 3; };

			//BPL	
			this.optable[0x2a] = delegate () { self.memmode = MC6803.RELATIVE; self.BPL(); return 3; };

			//BSR	
			this.optable[0x8d] = delegate () { self.memmode = MC6803.RELATIVE; self.BSR(); return 6; };

			//BITA	
			this.optable[0x85] = delegate () { self.memmode = MC6803.IMMEDIATE; self.BITA(); return 2; };
			this.optable[0x95] = delegate () { self.memmode = MC6803.DIRECT; self.BITA(); return 3; };
			this.optable[0xa5] = delegate () { self.memmode = MC6803.INDEX; self.BITA(); return 4; };
			this.optable[0xb5] = delegate () { self.memmode = MC6803.EXTENDED; self.BITA(); return 4; };

			//BITB	
			this.optable[0xc5] = delegate () { self.memmode = MC6803.IMMEDIATE; self.BITB(); return 2; };
			this.optable[0xd5] = delegate () { self.memmode = MC6803.DIRECT; self.BITB(); return 3; };
			this.optable[0xe5] = delegate () { self.memmode = MC6803.INDEX; self.BITB(); return 4; };
			this.optable[0xf5] = delegate () { self.memmode = MC6803.EXTENDED; self.BITB(); return 4; };

			//CBA	
			this.optable[0x11] = delegate () { self.memmode = MC6803.INHERENT; self.CBA(); return 2; };

			//CLC	
			this.optable[0x0c] = delegate () { self.memmode = MC6803.INHERENT; self.CLC(); return 2; };

			//CLI	
			this.optable[0x0e] = delegate () { self.memmode = MC6803.INHERENT; self.CLI(); return 2; };

			//CLR	
			this.optable[0x6f] = delegate () { self.memmode = MC6803.INDEX; self.CLR(); return 6; };
			this.optable[0x7f] = delegate () { self.memmode = MC6803.EXTENDED; self.CLR(); return 6; };

			//CLRA	
			this.optable[0x4f] = delegate () { self.memmode = MC6803.INHERENT; self.CLRA(); return 2; };

			//CLRB	
			this.optable[0x5f] = delegate () { self.memmode = MC6803.INHERENT; self.CLRB(); return 2; };

			//CLV	
			this.optable[0x0a] = delegate () { self.memmode = MC6803.INHERENT; self.CLV(); return 2; };

			//CMPA	
			this.optable[0x81] = delegate () { self.memmode = MC6803.IMMEDIATE; self.CMPA(); return 2; };
			this.optable[0x91] = delegate () { self.memmode = MC6803.DIRECT; self.CMPA(); return 3; };
			this.optable[0xa1] = delegate () { self.memmode = MC6803.INDEX; self.CMPA(); return 4; };
			this.optable[0xb1] = delegate () { self.memmode = MC6803.EXTENDED; self.CMPA(); return 4; };

			//CMPB	
			this.optable[0xc1] = delegate () { self.memmode = MC6803.IMMEDIATE; self.CMPB(); return 2; };
			this.optable[0xd1] = delegate () { self.memmode = MC6803.DIRECT; self.CMPB(); return 3; };
			this.optable[0xe1] = delegate () { self.memmode = MC6803.INDEX; self.CMPB(); return 4; };
			this.optable[0xf1] = delegate () { self.memmode = MC6803.EXTENDED; self.CMPB(); return 4; };

			//COM	
			this.optable[0x63] = delegate () { self.memmode = MC6803.INDEX; self.COM(); return 6; };
			this.optable[0x73] = delegate () { self.memmode = MC6803.EXTENDED; self.COM(); return 6; };

			//COMA	
			this.optable[0x43] = delegate () { self.memmode = MC6803.INHERENT; self.COMA(); return 2; };

			//COMB	
			this.optable[0x53] = delegate () { self.memmode = MC6803.INHERENT; self.COMB(); return 2; };

			//CPX	
			this.optable[0x8c] = delegate () { self.memmode = MC6803.IMMEDIATE; self.CPX(); return 4; };
			this.optable[0x9c] = delegate () { self.memmode = MC6803.DIRECT; self.CPX(); return 5; };
			this.optable[0xac] = delegate () { self.memmode = MC6803.INDEX; self.CPX(); return 6; };
			this.optable[0xbc] = delegate () { self.memmode = MC6803.EXTENDED; self.CPX(); return 6; };

			//DAA	
			this.optable[0x19] = delegate () { self.memmode = MC6803.INHERENT; self.DAA(); return 2; };

			//DEC	
			this.optable[0x6a] = delegate () { self.memmode = MC6803.INDEX; self.DEC(); return 6; };
			this.optable[0x7a] = delegate () { self.memmode = MC6803.EXTENDED; self.DEC(); return 6; };

			//DECA	
			this.optable[0x4a] = delegate () { self.memmode = MC6803.INHERENT; self.DECA(); return 2; };

			//DECB	
			this.optable[0x5a] = delegate () { self.memmode = MC6803.INHERENT; self.DECB(); return 2; };

			//DES	
			this.optable[0x34] = delegate () { self.memmode = MC6803.INHERENT; self.DES(); return 3; };

			//DEX	
			this.optable[0x09] = delegate () { self.memmode = MC6803.INHERENT; self.DEX(); return 3; };

			//EORA	
			this.optable[0x88] = delegate () { self.memmode = MC6803.IMMEDIATE; self.EORA(); return 2; };
			this.optable[0x98] = delegate () { self.memmode = MC6803.DIRECT; self.EORA(); return 3; };
			this.optable[0xa8] = delegate () { self.memmode = MC6803.INDEX; self.EORA(); return 4; };
			this.optable[0xb8] = delegate () { self.memmode = MC6803.EXTENDED; self.EORA(); return 4; };

			//EORB	
			this.optable[0xc8] = delegate () { self.memmode = MC6803.IMMEDIATE; self.EORB(); return 2; };
			this.optable[0xd8] = delegate () { self.memmode = MC6803.DIRECT; self.EORB(); return 3; };
			this.optable[0xe8] = delegate () { self.memmode = MC6803.INDEX; self.EORB(); return 4; };
			this.optable[0xf8] = delegate () { self.memmode = MC6803.EXTENDED; self.EORB(); return 4; };

			//INC	
			this.optable[0x6c] = delegate () { self.memmode = MC6803.INDEX; self.INC(); return 6; };
			this.optable[0x7c] = delegate () { self.memmode = MC6803.EXTENDED; self.INC(); return 6; };

			//INCA	
			this.optable[0x4c] = delegate () { self.memmode = MC6803.INHERENT; self.INCA(); return 2; };

			//INCB	
			this.optable[0x5c] = delegate () { self.memmode = MC6803.INHERENT; self.INCB(); return 2; };

			//INS	
			this.optable[0x31] = delegate () { self.memmode = MC6803.INHERENT; self.INS(); return 3; };

			//INX	
			this.optable[0x08] = delegate () { self.memmode = MC6803.INHERENT; self.INX(); return 3; };

			//JMP	
			this.optable[0x6e] = delegate () { self.memmode = MC6803.INDEX; self.JMP(); return 3; };
			this.optable[0x7e] = delegate () { self.memmode = MC6803.EXTENDED; self.JMP(); return 3; };

			//JSR	
			this.optable[0x9d] = delegate () { self.memmode = MC6803.DIRECT; self.JSR(); return 5; };
			this.optable[0xad] = delegate () { self.memmode = MC6803.INDEX; self.JSR(); return 6; };
			this.optable[0xbd] = delegate () { self.memmode = MC6803.EXTENDED; self.JSR(); return 6; };

			//LDAA	
			this.optable[0x86] = delegate () { self.memmode = MC6803.IMMEDIATE; self.LDAA(); return 2; };
			this.optable[0x96] = delegate () { self.memmode = MC6803.DIRECT; self.LDAA(); return 3; };
			this.optable[0xa6] = delegate () { self.memmode = MC6803.INDEX; self.LDAA(); return 4; };
			this.optable[0xb6] = delegate () { self.memmode = MC6803.EXTENDED; self.LDAA(); return 4; };

			//LDAB	
			this.optable[0xc6] = delegate () { self.memmode = MC6803.IMMEDIATE; self.LDAB(); return 2; };
			this.optable[0xd6] = delegate () { self.memmode = MC6803.DIRECT; self.LDAB(); return 3; };
			this.optable[0xe6] = delegate () { self.memmode = MC6803.INDEX; self.LDAB(); return 4; };
			this.optable[0xf6] = delegate () { self.memmode = MC6803.EXTENDED; self.LDAB(); return 4; };

			//LDD	
			this.optable[0xcc] = delegate () { self.memmode = MC6803.IMMEDIATE; self.LDD(); return 3; };
			this.optable[0xdc] = delegate () { self.memmode = MC6803.DIRECT; self.LDD(); return 4; };
			this.optable[0xec] = delegate () { self.memmode = MC6803.INDEX; self.LDD(); return 5; };
			this.optable[0xfc] = delegate () { self.memmode = MC6803.EXTENDED; self.LDD(); return 5; };

			//LDS	
			this.optable[0x8e] = delegate () { self.memmode = MC6803.IMMEDIATE; self.LDS(); return 3; };
			this.optable[0x9e] = delegate () { self.memmode = MC6803.DIRECT; self.LDS(); return 4; };
			this.optable[0xae] = delegate () { self.memmode = MC6803.INDEX; self.LDS(); return 5; };
			this.optable[0xbe] = delegate () { self.memmode = MC6803.EXTENDED; self.LDS(); return 5; };

			//LDX	
			this.optable[0xce] = delegate () { self.memmode = MC6803.IMMEDIATE; self.LDX(); return 3; };
			this.optable[0xde] = delegate () { self.memmode = MC6803.DIRECT; self.LDX(); return 4; };
			this.optable[0xee] = delegate () { self.memmode = MC6803.INDEX; self.LDX(); return 5; };
			this.optable[0xfe] = delegate () { self.memmode = MC6803.EXTENDED; self.LDX(); return 5; };

			//LSR	
			this.optable[0x64] = delegate () { self.memmode = MC6803.INDEX; self.LSR(); return 6; };
			this.optable[0x74] = delegate () { self.memmode = MC6803.EXTENDED; self.LSR(); return 6; };

			//LSRA	
			this.optable[0x44] = delegate () { self.memmode = MC6803.INHERENT; self.LSRA(); return 2; };

			//LSRB	
			this.optable[0x54] = delegate () { self.memmode = MC6803.INHERENT; self.LSRB(); return 2; };

			//LSRD	
			this.optable[0x04] = delegate () { self.memmode = MC6803.INHERENT; self.LSRD(); return 3; };

			//MUL	
			this.optable[0x3d] = delegate () { self.memmode = MC6803.INHERENT; self.MUL(); return 10; };

			//NEG	
			this.optable[0x60] = delegate () { self.memmode = MC6803.INDEX; self.NEG(); return 6; };
			this.optable[0x70] = delegate () { self.memmode = MC6803.EXTENDED; self.NEG(); return 6; };

			//NEGA	
			this.optable[0x40] = delegate () { self.memmode = MC6803.INHERENT; self.NEGA(); return 2; };

			//NEGB	
			this.optable[0x50] = delegate () { self.memmode = MC6803.INHERENT; self.NEGB(); return 2; };

			//NOP	
			this.optable[0x01] = delegate () { self.memmode = MC6803.INHERENT; self.NOP(); return 2; };

			//NGC	
			this.optable[0x62] = delegate () { self.memmode = MC6803.INDEX; self.NGC(); return 6; };
			this.optable[0x72] = delegate () { self.memmode = MC6803.EXTENDED; self.NGC(); return 6; };

			//NGCA	
			this.optable[0x42] = delegate () { self.memmode = MC6803.INHERENT; self.NGCA(); return 2; };

			//NGCB	
			this.optable[0x52] = delegate () { self.memmode = MC6803.INHERENT; self.NGCB(); return 2; };

			//ORAA	
			this.optable[0x8a] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ORAA(); return 2; };
			this.optable[0x9a] = delegate () { self.memmode = MC6803.DIRECT; self.ORAA(); return 3; };
			this.optable[0xaa] = delegate () { self.memmode = MC6803.INDEX; self.ORAA(); return 4; };
			this.optable[0xba] = delegate () { self.memmode = MC6803.EXTENDED; self.ORAA(); return 4; };

			//ORAB	
			this.optable[0xca] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ORAB(); return 2; };
			this.optable[0xda] = delegate () { self.memmode = MC6803.DIRECT; self.ORAB(); return 3; };
			this.optable[0xea] = delegate () { self.memmode = MC6803.INDEX; self.ORAB(); return 4; };
			this.optable[0xfa] = delegate () { self.memmode = MC6803.EXTENDED; self.ORAB(); return 4; };

			//PSHA	
			this.optable[0x36] = delegate () { self.memmode = MC6803.INHERENT; self.PSHA(); return 3; };

			//PSHB	
			this.optable[0x37] = delegate () { self.memmode = MC6803.INHERENT; self.PSHB(); return 3; };

			//PSHX	
			this.optable[0x3c] = delegate () { self.memmode = MC6803.INHERENT; self.PSHX(); return 4; };

			//PULA	
			this.optable[0x32] = delegate () { self.memmode = MC6803.INHERENT; self.PULA(); return 4; };

			//PULB	
			this.optable[0x33] = delegate () { self.memmode = MC6803.INHERENT; self.PULB(); return 4; };

			//PULX	
			this.optable[0x38] = delegate () { self.memmode = MC6803.INHERENT; self.PULX(); return 5; };

			//ROL	
			this.optable[0x69] = delegate () { self.memmode = MC6803.INDEX; self.ROL(); return 6; };
			this.optable[0x79] = delegate () { self.memmode = MC6803.EXTENDED; self.ROL(); return 6; };

			//ROLA	
			this.optable[0x49] = delegate () { self.memmode = MC6803.INHERENT; self.ROLA(); return 2; };

			//ROLB	
			this.optable[0x59] = delegate () { self.memmode = MC6803.INHERENT; self.ROLB(); return 2; };

			//ROR	
			this.optable[0x66] = delegate () { self.memmode = MC6803.INDEX; self.ROR(); return 6; };
			this.optable[0x76] = delegate () { self.memmode = MC6803.EXTENDED; self.ROR(); return 6; };

			//RORA	
			this.optable[0x46] = delegate () { self.memmode = MC6803.INHERENT; self.RORA(); return 2; };

			//RORB	
			this.optable[0x56] = delegate () { self.memmode = MC6803.INHERENT; self.RORB(); return 2; };

			//RTI	
			this.optable[0x3b] = delegate () { self.memmode = MC6803.INHERENT; self.RTI(); return 10; };

			//RTS
			this.optable[0x39] = delegate () { self.memmode = MC6803.INHERENT; self.RTS(); return 5; };

			//SBA
			this.optable[0x10] = delegate () { self.memmode = MC6803.INHERENT; self.SBA(); return 2; };

			//SBCA
			this.optable[0x82] = delegate () { self.memmode = MC6803.IMMEDIATE; self.SBCA(); return 2; };
			this.optable[0x92] = delegate () { self.memmode = MC6803.DIRECT; self.SBCA(); return 3; };
			this.optable[0xa2] = delegate () { self.memmode = MC6803.INDEX; self.SBCA(); return 4; };
			this.optable[0xb2] = delegate () { self.memmode = MC6803.EXTENDED; self.SBCA(); return 4; };

			//SBCB
			this.optable[0xc2] = delegate () { self.memmode = MC6803.IMMEDIATE; self.SBCB(); return 2; };
			this.optable[0xd2] = delegate () { self.memmode = MC6803.DIRECT; self.SBCB(); return 3; };
			this.optable[0xe2] = delegate () { self.memmode = MC6803.INDEX; self.SBCB(); return 4; };
			this.optable[0xf2] = delegate () { self.memmode = MC6803.EXTENDED; self.SBCB(); return 4; };

			//SEC
			this.optable[0x0d] = delegate () { self.memmode = MC6803.INHERENT; self.SCC(); return 2; };

			//SEI	
			this.optable[0x0f] = delegate () { self.memmode = MC6803.INHERENT; self.SEI(); return 2; };

			//SEV	
			this.optable[0x0b] = delegate () { self.memmode = MC6803.INHERENT; self.SEV(); return 2; };

			//STAA	
			this.optable[0x87] = delegate () { self.memmode = MC6803.IMMEDIATE; self.STAA(); return 3; }; // TEST
			this.optable[0x97] = delegate () { self.memmode = MC6803.DIRECT; self.STAA(); return 3; };
			this.optable[0xa7] = delegate () { self.memmode = MC6803.INDEX; self.STAA(); return 4; };
			this.optable[0xb7] = delegate () { self.memmode = MC6803.EXTENDED; self.STAA(); return 4; };

			//STAB	
			//this.optable[0xc7] = delegate() { self.memmode = MC6803.IMMEDIATE; self.STAB(); return 3; }; // TEST
			this.optable[0xd7] = delegate () { self.memmode = MC6803.DIRECT; self.STAB(); return 3; };
			this.optable[0xe7] = delegate () { self.memmode = MC6803.INDEX; self.STAB(); return 4; };
			this.optable[0xf7] = delegate () { self.memmode = MC6803.EXTENDED; self.STAB(); return 4; };

			//STD	
			//this.optable[0xcd] = delegate() { self.memmode = MC6803.IMMEDIATE; self.STD(); return 5; };
			this.optable[0xdd] = delegate () { self.memmode = MC6803.DIRECT; self.STD(); return 4; };
			this.optable[0xed] = delegate () { self.memmode = MC6803.INDEX; self.STD(); return 5; };
			this.optable[0xfd] = delegate () { self.memmode = MC6803.EXTENDED; self.STD(); return 5; };

			//STS	
			//this.optable[0x8f] = delegate() { self.memmode = MC6803.IMMEDIATE; self.STS(); return 4; }; // TEST
			this.optable[0x9f] = delegate () { self.memmode = MC6803.DIRECT; self.STS(); return 4; };
			this.optable[0xaf] = delegate () { self.memmode = MC6803.INDEX; self.STS(); return 5; };
			this.optable[0xbf] = delegate () { self.memmode = MC6803.EXTENDED; self.STS(); return 5; };

			//STX	
			//this.optable[0xcf] = delegate() { self.memmode = MC6803.IMMEDIATE; self.STX(); return 4; };
			this.optable[0xdf] = delegate () { self.memmode = MC6803.DIRECT; self.STX(); return 4; };
			this.optable[0xef] = delegate () { self.memmode = MC6803.INDEX; self.STX(); return 5; };
			this.optable[0xFF] = delegate () { self.memmode = MC6803.EXTENDED; self.STX(); return 5; };

			//SUBA	
			this.optable[0x80] = delegate () { self.memmode = MC6803.IMMEDIATE; self.SUBA(); return 2; };
			this.optable[0x90] = delegate () { self.memmode = MC6803.DIRECT; self.SUBA(); return 3; };
			this.optable[0xa0] = delegate () { self.memmode = MC6803.INDEX; self.SUBA(); return 4; };
			this.optable[0xb0] = delegate () { self.memmode = MC6803.EXTENDED; self.SUBA(); return 4; };

			//SUBB	
			this.optable[0xc0] = delegate () { self.memmode = MC6803.IMMEDIATE; self.SUBB(); return 2; };
			this.optable[0xd0] = delegate () { self.memmode = MC6803.DIRECT; self.SUBB(); return 3; };
			this.optable[0xe0] = delegate () { self.memmode = MC6803.INDEX; self.SUBB(); return 4; };
			this.optable[0xf0] = delegate () { self.memmode = MC6803.EXTENDED; self.SUBB(); return 4; };

			//SUBD	
			this.optable[0x83] = delegate () { self.memmode = MC6803.IMMEDIATE; self.SUBD(); return 4; };
			this.optable[0x93] = delegate () { self.memmode = MC6803.DIRECT; self.SUBD(); return 5; };
			this.optable[0xa3] = delegate () { self.memmode = MC6803.INDEX; self.SUBD(); return 6; };
			this.optable[0xb3] = delegate () { self.memmode = MC6803.EXTENDED; self.SUBD(); return 6; };

			//SWI	
			this.optable[0x3f] = delegate () { self.memmode = MC6803.INHERENT; self.SWI(); return 12; };

			//TAB	
			this.optable[0x16] = delegate () { self.memmode = MC6803.INHERENT; self.TAB(); return 2; };

			//TAP	
			this.optable[0x06] = delegate () { self.memmode = MC6803.INHERENT; self.TAP(); return 2; };

			//TBA	
			this.optable[0x17] = delegate () { self.memmode = MC6803.INHERENT; self.TBA(); return 2; };

			//TPA	
			this.optable[0x07] = delegate () { self.memmode = MC6803.INHERENT; self.TPA(); return 2; };

			//TST	
			this.optable[0x6d] = delegate () { self.memmode = MC6803.INDEX; self.TST(); return 6; };
			this.optable[0x7d] = delegate () { self.memmode = MC6803.EXTENDED; self.TST(); return 6; };

			//TSTA	
			this.optable[0x4d] = delegate () { self.memmode = MC6803.INHERENT; self.TSTA(); return 2; };

			//TSTB	
			this.optable[0x5d] = delegate () { self.memmode = MC6803.INHERENT; self.TSTB(); return 2; };

			//TSX	
			this.optable[0x30] = delegate () { self.memmode = MC6803.INHERENT; self.TSX(); return 3; };

			//TXS 
			this.optable[0x35] = delegate () { self.memmode = MC6803.INHERENT; self.TXS(); return 3; };

			//WAI 
			this.optable[0x3e] = delegate () { self.memmode = MC6803.INHERENT; self.WAI(); return 9; };


			this.optable[0x00] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x00); return 2; };
			this.optable[0x02] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x02); return 2; };
			this.optable[0x03] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x03); return 2; };
			this.optable[0x12] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x12); return 2; };
			this.optable[0x13] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x13); return 2; };
			this.optable[0x14] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x14); return 2; };
			this.optable[0x15] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x15); return 2; };
			this.optable[0x18] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x18); return 2; };
			this.optable[0x1a] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x1a); return 2; };
			this.optable[0x1c] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x1c); return 2; };
			this.optable[0x1d] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x1d); return 2; };
			this.optable[0x1e] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x1e); return 2; };
			this.optable[0x1f] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x1f); return 2; };
			this.optable[0x41] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x41); return 2; };
			this.optable[0x45] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x45); return 2; };
			this.optable[0x4b] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x4b); return 2; };
			this.optable[0x4e] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x4e); return 2; };
			this.optable[0x51] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x51); return 2; };
			this.optable[0x55] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x55); return 2; };
			this.optable[0x5b] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x5b); return 2; };
			this.optable[0x5e] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x5e); return 2; };
			this.optable[0x61] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x61); return 2; };
			this.optable[0x65] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x65); return 2; };
			this.optable[0x6b] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x6b); return 2; };
			this.optable[0x71] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x71); return 2; };
			this.optable[0x75] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x75); return 2; };
			this.optable[0x7b] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x7b); return 2; };
			this.optable[0x87] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x87); return 2; };
			this.optable[0x8f] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0x8f); return 2; };
			this.optable[0xc7] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0xc7); return 2; };
			this.optable[0xcd] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0xcd); return 2; };
			this.optable[0xcf] = delegate () { self.memmode = MC6803.IMMEDIATE; self.ERROR(0xcf); return 2; };
		}

		public void ERROR(int op) 
		{
			Console.WriteLine("ERROR: unknown opcode: " + op);
		}

		public void ABA() 
		{
			this.REG_A = this.Add(this.REG_A, this.REG_B);
		}
		public void ABX() 
		{
			this.REG_IP += this.REG_B;
			this.REG_IP &= 0xFFFF;
		}
		public void ADCA() 
		{
			this.REG_A = this.AddCarry(this.REG_A, this.FetchData());
		}
		public void ADCB() 
		{
			this.REG_B = this.AddCarry(this.REG_B, this.FetchData());
		}
		public void ADDA() 
		{
			this.REG_A = this.Add(this.REG_A, this.FetchData());
		}
		public void ADDB() 
		{
			this.REG_B = this.Add(this.REG_B, this.FetchData());
		}
		public void ADDD() 
		{
			this.REG_D = this.Add16(this.REG_D, this.FetchData16());
		}
		public void ANDA() 
		{
			this.REG_A = this.And(this.REG_A, this.FetchData());
		}
		public void ANDB() 
		{
			this.REG_B = this.And(this.REG_B, this.FetchData());
		}
		public void ASL() 
		{
			var scratch = this.ArithmeticShiftLeft(this.FetchData());
			this.SetLastRead(scratch);
		} 
		public void ASLA() 
		{
			this.REG_A = this.ArithmeticShiftLeft(this.REG_A);
		}
		public void ASLB() 
		{
			this.REG_B = this.ArithmeticShiftLeft(this.REG_B);
		}
		public void ASLD() 
		{
			this.REG_D = this.ShiftLeft16(this.REG_D);
		}
		public void ASR() 
		{
			var scratch = this.ArithmeticShiftRight(this.FetchData());
			this.SetLastRead(scratch);
		}
		public void ASRA() 
		{
			this.REG_A = this.ArithmeticShiftRight(this.REG_A);
		}
		public void ASRB() 
		{
			this.REG_B = this.ArithmeticShiftRight(this.REG_B);
		}
		public void BRA() 
		{
			var pos = this.SignExtend(this.FetchData());
			this.REG_PC += pos;
			this.REG_PC &= 0xFFFF;
		} 
		public void BRN() 
		{
			this.FetchData();
		}
		public void BCC() 
		{
			var pos = this.SignExtend(this.FetchData());
			if (this.F_CARRY == 0)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BCS() 
		{
			var pos = this.SignExtend(this.FetchData());
			if (this.F_CARRY == 1)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BEQ() 
		{
			var pos = this.SignExtend(this.FetchData());
			if (this.F_ZERO == 1)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BGE() 
		{
			var pos = this.SignExtend(this.FetchData());
			if ((this.F_SIGN ^ this.F_OVERFLOW) == 0)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BGT() 
		{
			var pos = this.SignExtend(this.FetchData());
			if ((this.F_ZERO | (this.F_SIGN ^ this.F_OVERFLOW)) == 0)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BHI() 
		{
			var pos = this.SignExtend(this.FetchData());
			if ((this.F_CARRY | this.F_ZERO) == 0)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BLE() 
		{
			var pos = this.SignExtend(this.FetchData());
			if ((this.F_ZERO | (this.F_SIGN ^ this.F_OVERFLOW)) == 1)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BLS() 
		{
			var pos = this.SignExtend(this.FetchData());
			if ((this.F_CARRY | this.F_ZERO) == 1)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BLT() 
		{
			var pos = this.SignExtend(this.FetchData());
			if ((this.F_SIGN ^ this.F_OVERFLOW) == 1)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BMI() 
		{
			var pos = this.SignExtend(this.FetchData());
			if (this.F_SIGN == 1)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BNE() 
		{
			var pos = this.SignExtend(this.FetchData());
			if (this.F_ZERO == 0)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BVC() 
		{
			var pos = this.SignExtend(this.FetchData());
			if (this.F_OVERFLOW == 0)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BVS() 
		{
			var pos = this.SignExtend(this.FetchData());
			if (this.F_OVERFLOW == 1)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BPL() 
		{
			var pos = this.SignExtend(this.FetchData());
			if (this.F_SIGN == 0)
			{
				this.REG_PC += pos;
				this.REG_PC &= 0xFFFF;
			}
		}
		public void BSR() 
		{
			var pos = this.SignExtend(this.Fetch());
			this.PushStack16(this.REG_PC);
			this.REG_PC += pos;
			this.REG_PC &= 0xFFFF;
		}
		public void BITA() 
		{
			this.And(this.REG_A, this.FetchData());
		}
		public void BITB() 
		{
			this.And(this.REG_B, this.FetchData());
		}
		public void CBA() 
		{
			this.Subtract(this.REG_A, this.REG_B);
		}
		public void CLC() 
		{
			this.F_CARRY = 0;
		}
		public void CLI() 
		{
			this.F_INTERRUPT = 0;
			//this.emulate();
			//this.checkIRQLines();
		}
		public void CLR() 
		{
			this.SetMemory(this.FetchAddress(), 0);
			this.F_ZERO = 1;
			this.F_SIGN = 0;
			this.F_OVERFLOW = 0;
			this.F_CARRY = 0;
		}
		public void CLRA() 
		{
			this.REG_A = 0;
			this.F_SIGN = 0;
			this.F_OVERFLOW = 0;
			this.F_CARRY = 0;
			this.F_ZERO = 1;
		}
		public void CLRB() 
		{
			this.REG_B = 0;
			this.F_SIGN = 0;
			this.F_OVERFLOW = 0;
			this.F_CARRY = 0;
			this.F_ZERO = 1;
		}
		public void CLV() 
		{
			this.F_OVERFLOW = 0;
		}
		public void CMPA() 
		{
			this.Subtract(this.REG_A, this.FetchData());
		}
		public void CMPB() 
		{
			this.Subtract(this.REG_B, this.FetchData());
		}
		public void COM() 
		{
			var scratch = this.Complement(this.FetchData());
			this.SetLastRead(scratch);
		}
		public void COMA() 
		{
			this.REG_A = this.Complement(this.REG_A);
		}
		public void COMB() 
		{
			this.REG_B = this.Complement(this.REG_B);
		}
		public void CPX() 
		{
			this.Subtract16(this.REG_IP, this.FetchData16());
		}
		public void DAA() 
		{
			int msn, lsn, t, cf = 0;

			msn = this.REG_A & 0xf0;
			lsn = this.REG_A & 0x0f;

			if (lsn > 0x09 || this.F_HALFCARRY != 0) cf |= 0x06;
			if (msn > 0x80 && lsn > 0x09) cf |= 0x60;
			if (msn > 0x90 || this.F_CARRY != 0) cf |= 0x60;

			t = cf + this.REG_A;

			this.F_ZERO = 0;
			this.F_SIGN = 0;
			this.F_OVERFLOW = 0;

			this.Set8NZ(t & 0xFFFF);
			this.Set8C(t & 0xFFFF);

			this.REG_A = t;
		}
		public void DEC() 
		{
			var scratch = this.Decrement(this.FetchData());
			this.SetLastRead(scratch);
		}
		public void DECA() 
		{
			this.REG_A = this.Decrement(this.REG_A);
		}
		public void DECB() 
		{
			this.REG_B = this.Decrement(this.REG_B);
		}
		public void DES() 
		{
			this.REG_SP--;
			this.REG_SP &= 0xFFFF;
		}
		public void DEX() 
		{
			this.REG_IP--;
			this.REG_IP &= 0xFFFF;
			this.Set16Z(this.REG_IP);
		}
		public void EORA() 
		{
			this.REG_A = this.Eor(this.REG_A, this.FetchData());
		}
		public void EORB() 
		{
			this.REG_B = this.Eor(this.REG_B, this.FetchData());
		}
		public void INC() 
		{
			var scratch = this.Increment(this.FetchData());
			this.SetLastRead(scratch);
		}
		public void INCA() 
		{
			this.REG_A = this.Increment(this.REG_A);
		}
		public void INCB() 
		{
			this.REG_B = this.Increment(this.REG_B);
		}
		public void INS() 
		{
			this.REG_SP++;
			this.REG_SP &= 0xFFFF;
		}
		public void INX() 
		{
			this.REG_IP++;
			this.REG_IP &= 0xFFFF;
			this.Set16Z(this.REG_IP);
		}
		public void JMP() 
		{
			int pos;
			if (this.memmode == MC6803.INDEX)
			{
				pos = this.REG_IP + this.Fetch();
			}
			else
			{
				pos = this.FetchAddress();
			}
			this.REG_PC = pos;
		}
		public void JSR() 
		{
			var pos = this.FetchAddress();
			this.PushStack16(this.REG_PC);
			this.REG_PC = pos;
		}
		public void LDAA() 
		{
			this.REG_A = this.FetchData();
			this.Set8NZ(this.REG_A);
			this.F_OVERFLOW = 0;
		}
		public void LDAB() 
		{
			this.REG_B = this.FetchData();
			this.Set8NZ(this.REG_B);
			this.F_OVERFLOW = 0;
		}
		public void LDD() 
		{
			this.REG_D = this.FetchData16();
			this.Set16NZ(this.REG_D);
			this.F_OVERFLOW = 0;
		}
		public void LDS() 
		{
			this.REG_SP = this.FetchData16();
			this.Set16NZ(this.REG_SP);
			this.F_OVERFLOW = 0;
		}
		public void LDX() 
		{
			var scratch = this.FetchData16();
			this.REG_IP = scratch;
			this.Set16NZ(this.REG_IP);
			this.F_OVERFLOW = 0;
		}
		public void LSR() 
		{
			var scratch = this.LogicalShiftRight(this.FetchData());
			this.SetLastRead(scratch);
		}
		public void LSRA() 
		{
			this.REG_A = this.LogicalShiftRight(this.REG_A);
		}
		public void LSRB() 
		{
			this.REG_B = this.LogicalShiftRight(this.REG_B);
		}
		public void LSRD() 
		{
			this.REG_D = this.ShiftRight16(this.REG_D);
		}
		public void MUL() 
		{
			this.REG_D = (this.REG_A * this.REG_B) & 0xFFFF;
			if ((this.REG_A * this.REG_B) > 0xFFFF)
			{
				this.F_CARRY = 1;
			}
			else
			{
				this.F_CARRY = 0;
			}
		}
		public void NEG() 
		{
			var scratch = this.Negate(this.FetchData());
			this.SetLastRead(scratch);
		}
		public void NEGA() 
		{
			this.REG_A = this.Negate(this.REG_A);
		}
		public void NEGB() 
		{
			this.REG_B = this.Negate(this.REG_B);
		}
		public void NGC() 
		{
			var scratch = this.NegateCarry(this.FetchData());
			this.SetLastRead(scratch);
		}
		public void NGCA() 
		{
			this.REG_A = this.NegateCarry(this.REG_A);
		}
		public void NGCB() 
		{
			this.REG_B = this.NegateCarry(this.REG_B);
		}
		public void ORAA() 
		{
			this.REG_A = this.Or(this.REG_A, this.FetchData());
		}
		public void ORAB() 
		{
			this.REG_B = this.Or(this.REG_B, this.FetchData());
		}
		public void PSHA() 
		{
			this.PushStack(this.REG_A);
		}
		public void PSHB() 
		{
			this.PushStack(this.REG_B);
		}
		public void PSHX() 
		{
			this.PushStack16(this.REG_IP);
		}
		public void PULA() 
		{
			this.REG_A = this.PopStack();
		}
		public void PULB() 
		{
			this.REG_B = this.PopStack();
		}
		public void PULX() 
		{
			this.REG_IP = this.PopStack16();
		}
		public void ROL() 
		{
			var scratch = this.RotateLeft(this.FetchData());
			this.SetLastRead(scratch);
		}
		public void ROLA() 
		{
			this.REG_A = this.RotateLeft(this.REG_A);
		}
		public void ROLB() 
		{
			this.REG_B = this.RotateLeft(this.REG_B);
		}
		public void ROR() 
		{
			var scratch = this.RotateRight(this.FetchData());
			this.SetLastRead(scratch);
		}
		public void RORA() 
		{
			this.REG_A = this.RotateRight(this.REG_A);
		}
		public void RORB() 
		{
			this.REG_B = this.RotateRight(this.REG_B);
		}
		public void RTI() 
		{
			this.VariableToFlags(this.PopStack());
			this.REG_B = this.PopStack();
			this.REG_A = this.PopStack();
			this.REG_IP = this.PopStack16();
			this.REG_PC = this.PopStack16();
			//this.checkIRQLines();
		}
		public void SBA() 
		{
			this.REG_A = this.Subtract(this.REG_A, this.REG_B);
		}
		public void SBCA() 
		{
			this.REG_A = this.SubtractCarry(this.REG_A, this.FetchData());
		}
		public void SBCB() 
		{
			this.REG_B = this.SubtractCarry(this.REG_B, this.FetchData());
		}
		public void SCC() 
		{
			this.F_CARRY = 1;
		} 
		public void SEI() 
		{
			this.F_INTERRUPT = 1;
			//this.emulate();
			//this.history = [];
			//this.checkIRQLines();
		} 
		public void SEV() 
		{
			this.F_OVERFLOW = 1;
		} 
		public void STAA() 
		{
			this.SetMemory(this.FetchAddress(), this.REG_A);
			this.F_OVERFLOW = 0;
			this.Set8NZ(this.REG_A);
		}
		public void STAB( )
		{
			this.SetMemory(this.FetchAddress(), this.REG_B);
			this.F_OVERFLOW = 0;
			this.Set8NZ(this.REG_B);
		}
		public void STD( )
		{
			var scratch = this.FetchAddress();
			this.SetMemory16(scratch, this.REG_D);
			this.F_OVERFLOW = 0;
			this.Set16NZ(this.REG_D);
		}
		public void STS() 
		{
			var scratch = this.FetchAddress();
			this.SetMemory16(scratch, this.REG_SP);
			this.F_OVERFLOW = 0;
			this.Set16NZ(this.REG_SP);
		}
		public void STX() 
		{
			var scratch = this.FetchAddress();
			this.SetMemory16(scratch, this.REG_IP);
			this.F_OVERFLOW = 0;
			this.Set16NZ(this.REG_IP);
		}
		public void SUBA() 
		{
			this.REG_A = this.Subtract(this.REG_A, this.FetchData());
		}
		public void SUBB() 
		{
			this.REG_B = this.Subtract(this.REG_B, this.FetchData());
		}
		public void SUBD() 
		{
			this.REG_D = this.Subtract16(this.REG_D, this.FetchData16());
		}
		public void SWI() 
		{
			Console.WriteLine("SWI Called - Exit");

			this.PushStack16(this.REG_PC);
			this.PushStack16(this.REG_IP);
			this.PushStack(this.REG_A);
			this.PushStack(this.REG_B);
			this.PushStack(this.FlagsToVariable());

			this.SEI();

			this.REG_PC = this.FetchMemory(0XFFFA) & 0xFFFF;
			this.REG_PC &= 0xFFFF;

			//                    return;
			//                    this.F_INTERRUPT = 1;
			//                    this.pushStack16(this.REG_PC);
			//                    this.pushStack16(this.REG_IP);
			//                    this.pushStack16(this.REG_A);
			//                    this.pushStack16(this.REG_B);
			//                    this.pushStack16(this.flagsToVariable());
			//                    this.REG_PC = (this.fetchMemory(0xFFfa) << 8) + this.fetchMemory(0xFFfb);
		}
		public void TAB() 
		{
			this.REG_B = this.REG_A;
			this.Set8NZ(this.REG_B);
			this.F_OVERFLOW = 0;
		}
		public void TAP() 
		{
			this.VariableToFlags(this.REG_A);
			//this.emulate();
			//this.checkIRQLines();
		}
		public void TBA() 
		{
			this.REG_A = this.REG_B;
			this.Set8NZ(this.REG_A);
			this.F_OVERFLOW = 0;
		}
		public void TPA() 
		{
			this.REG_A = this.FlagsToVariable();
		}
		public void TST() 
		{
			this.F_OVERFLOW = 0;
			this.F_CARRY = 0;
			this.Set8NZ(this.FetchData());
		}
		public void TSTA() 
		{
			this.F_OVERFLOW = 0;
			this.F_CARRY = 0;
			this.Set8NZ(this.REG_A);
		}
		public void TSTB() 
		{
			this.F_OVERFLOW = 0;
			this.F_CARRY = 0;
			this.Set8NZ(this.REG_B);
		}
		public void TSX() 
		{
			this.REG_IP = this.REG_SP + 1;
		}
		public void TXS() 
		{
			this.REG_SP = this.REG_IP - 1;
		}
		public void WAI() 
		{
			this.waiState |= this.WAI_;
			this.PushStack16(this.REG_PC);
			this.PushStack16(this.REG_IP);
			this.PushStack(this.REG_A);
			this.PushStack(this.REG_B);
			this.PushStack(this.FlagsToVariable());
			//this.checkIRQLines();

			if ((this.waiState & this.WAI_) != 0)
			{
				Console.WriteLine("eat cycles");
				this.cycleCount = (this.cycleCount + 1) & 0xFFFF;
				this.CheckTimer();
			}
			Console.WriteLine("WAI called");
		}

		public int FlagsToVariable() 
		{
			var ret =
				this.F_CARRY * 0x01 +
				this.F_OVERFLOW * 0x02 +
				this.F_ZERO * 0x04 +
				this.F_SIGN * 0x08 +
				this.F_INTERRUPT * 0x10 +
				this.F_HALFCARRY * 0x20;
			return ret;
		}

		public void VariableToFlags(int CCR) 
		{
			this.F_CARRY = (CCR & 0x01);
			this.F_OVERFLOW = (CCR & 0x02) >> 1;
			this.F_ZERO = (CCR & 0x04) >> 2;
			this.F_SIGN = (CCR & 0x08) >> 3;
			this.F_INTERRUPT = (CCR & 0x10) >> 4;
			this.F_HALFCARRY = (CCR & 0x20) >> 5;
		}

		public int Fetch() 
		{
			var scratch = this.FetchMemory(this.REG_PC);
			this.REG_PC++;
			this.REG_PC &= 0xFFFF;
			return scratch;
		}

		public int FetchOpCode() 
		{
			return this.Fetch();
		}


		public int FetchAddress() 
		{
			switch (this.memmode)
			{
				case MC6803.DIRECT:
					return this.Fetch();
				case MC6803.INDEX:
					return this.Fetch() + this.REG_IP;
				case MC6803.EXTENDED:
					return (this.Fetch() << 8) + this.Fetch();
				default:
					Console.WriteLine("Tried FetchAddress with mode" + this.memmode);
					return 0;
			}
		}

		public int FetchData() 
		{
			switch (this.memmode)
			{
				case MC6803.IMMEDIATE:
					return this.Fetch();
				case MC6803.RELATIVE:
					return this.Fetch();
				case MC6803.INDEX:
					return this.FetchMemory(this.Fetch() + this.REG_IP);
				case MC6803.DIRECT:
					return this.FetchMemory(this.Fetch());
				case MC6803.EXTENDED:
					return this.FetchMemory((this.Fetch() << 8) + this.Fetch());
				default:
					Console.WriteLine("Invalid mode in fetchData");
					return 0;
			}
		}

		public int FetchData16() 
		{
			int scratch;
			switch (this.memmode)
			{
				case MC6803.IMMEDIATE:
					return (this.Fetch() << 8) + this.Fetch();
				case MC6803.INDEX:
					scratch = this.Fetch() + this.REG_IP;
					 break;
				case MC6803.DIRECT:
					scratch = this.Fetch();
					 break;
				case MC6803.EXTENDED:
					scratch = (this.Fetch() << 8) + this.Fetch();
					 break;
				default:
					Console.WriteLine("Invalid mode in fetchData16");
					return 0;
			}
			//return ((this.fetchMemory(scratch) << 8) + this.fetchMemory(scratch + 1)) & 0xFFFF;
			return ((this.FetchMemory(scratch) << 8) + this.FetchMemory(scratch + 1));
		}


		public void SetLastRead(int value) 
		{
			value &= 0xFF;
			int position = 0;
			switch (this.memmode)
			{
				case MC6803.DIRECT:
					position = this.FetchMemory(this.REG_PC - 1);
					break;
				case MC6803.INDEX:
					position = this.FetchMemory(this.REG_PC - 1) + this.REG_IP;
					break;
				case MC6803.EXTENDED:
					position = (this.FetchMemory(this.REG_PC - 2) << 8) + this.FetchMemory(this.REG_PC - 1);
					break;
				default:
					Console.WriteLine("Tried to set last read with mode " + this.memmode);
					position = 0;
					break;
			}
			this.SetMemory(position, value);
		}

		public int Add(int first, int second) 
		{
			first &= 0xFF;
			second &= 0xFF;
			var scratch = first + second;
			this.Set8HNZVC(first, second, (scratch & 0xFFFF));
			return (scratch & 0xFF);
		}

		public int AddCarry(int first, int second) 
		{
			first &= 0xFF;
			second &= 0xFF;
			var scratch = first + second + this.F_CARRY;
			this.Set8HNZVC(first, second, (scratch & 0xFFFF));
			return (scratch & 0xFF);
		}

		public int Add16(int first, int second) 
		{
			first &= 0xFFFF;
			second &= 0xFFFF;
			var scratch = first + second;
			this.Set16NZVC(first, second, (scratch & -1)); // 0xFFFFffff
			return (scratch & 0xFFFF);
		}

		public int Subtract(int first, int second) 
		{
			first &= 0xFF;
			second &= 0xFF;
			var scratch = first - second;
			this.Set8NZVC(first, second, (scratch & 0xFFFF));
			return (scratch & 0xFF);
		}

		public int Subtract16(int first, int second) 
		{
			first &= 0xFFFF;
			second &= 0xFFFF;
			var scratch = first - second;
			this.Set16NZVC(first, second, (scratch & -1)); // 0XFFFFFFFF
			return (scratch & 0xFFFF);
		}

		public int SubtractCarry(int first, int second) 
		{
			first &= 0xFF;
			second &= 0xFF;
			var scratch = first - second - this.F_CARRY;
			this.Set8NZVC(first, second, (scratch & 0xFFFF));
			return (scratch & 0xFF);
		}

		public int And(int first, int second) 
		{
			first &= 0xFF;
			second &= 0xFF;
			var result = first & second;
			this.Set8NZ(result & 0xFF);
			this.F_OVERFLOW = 0;
			//this.F_CARRY = 0; // Why do you carry with and? surely 0xFF & 0xFF is just 0xFF.... (no carry?)
			return (result & 0xFF);
		}

		public int Complement(int first) 
		{
			first &= 0xFF;
			var result = first ^ 0xFF;
			this.Set8NZ(result & 0xFF);
			this.F_OVERFLOW = 0;
			this.F_CARRY = 1;
			return (result & 0xFF);
		}

		public int Eor(int first, int second) 
		{
			first &= 0xFF;
			second &= 0xFF;
			var result = first ^ second;
			this.Set8NZ(result & 0xFF);
			this.F_OVERFLOW = 0;
			return (result & 0xFF);
		}

		public int Negate(int first) 
		{
			return this.Subtract(0, first);
		}

		public int NegateCarry(int first) 
		{
			return this.SubtractCarry(0, first);
		}

		public int Or(int first, int second) 
		{
			first &= 0xFF;
			second &= 0xFF;
			var result = first | second;
			this.Set8NZ(result & 0xFF);
			this.F_OVERFLOW = 0;
			return (result & 0xFF);
		}

		public int Decrement(int first) 
		{
			first &= 0xFF;
			this.F_OVERFLOW = (first == 0x80) ? 1 : 0;
			var result = (first - 1) & 0xFF;
			this.Set8NZ(result);
			return (result);
		}

		public int Increment(int first) 
		{
			first &= 0xFF;
			this.F_OVERFLOW = (first == 0x7f) ? 1 : 0;
			var result = (first + 1) & 0xFF;
			this.Set8NZ(result);
			return (result);
		}

		public int ShiftLeft(int first, int mode) 
		{
			first &= 0xFF;
			var result = (first << 1) & 0xFF;
			if (mode == 3)
			{
				result += this.F_CARRY;
			}
			this.F_CARRY = ((first & 0x80) >> 7);
			this.Set8NZ(result);
			this.F_OVERFLOW = (this.F_SIGN ^ this.F_CARRY);
			return (result & 0xFF);
		}

		public int ArithmeticShiftLeft(int first) 
		{
			return this.ShiftLeft(first, 1);
		}

		public int RotateLeft(int first) 
		{
			return this.ShiftLeft(first, 3);
		}

		public int ShiftLeft16(int first) 
		{
			first &= 0xFFFF;
			var result = (first << 1) & 0xFFFF;
			this.F_CARRY = ((first & 0x8000) >> 15);
			this.Set16NZ(result);
			this.F_OVERFLOW = (this.F_SIGN ^ this.F_CARRY);
			return (result & 0xFFFF);
		}

		public int ShiftRight16(int first) 
		{
			first &= 0xFFFF;
			var result = (first >> 1) & 0xFFFF;
			this.F_CARRY = (first & 0x01);
			this.Set16NZ(result);
			this.F_OVERFLOW = (this.F_SIGN ^ this.F_CARRY);
			return (result & 0xFFFF);
		}

		public int ShiftRight(int first, int mode) 
		{
			first &= 0xFF;
			var result = (first >> 1) & 0xFF;
			switch (mode)
			{
				case 1:
					result |= (first & 0x80);
					break;
				case 3:
					result |= (this.F_CARRY << 7);
					break;
				case 2:
					break;
				default:
					Console.WriteLine("Bad shift");
					break;
			}
			this.F_CARRY = (first & 0x01);
			this.Set8NZ(result);
			this.F_OVERFLOW = (this.F_SIGN ^ this.F_CARRY);
			return (result & 0xFF);
		}

		public int ArithmeticShiftRight(int first) 
		{
			return this.ShiftRight(first, 1);
		}

		public int LogicalShiftRight(int first) 
		{
			return this.ShiftRight(first, 2);
		}

		public int RotateRight(int first) 
		{
			return this.ShiftRight(first, 3);
		}

		public void Set8N(int value) 
		{
			this.F_SIGN = (value & 0x80) >> 7;
		}

		public void Set16N(int value) 
		{
			this.F_SIGN = (value & 0x8000) >> 15;
		}

		public void Set8Z(int value) 
		{
			this.F_ZERO = (value & 0xFF) == 0 ? 1 : 0;
		}

		public void Set16Z(int value) 
		{
			this.F_ZERO = (value & 0xFFFF) == 0 ? 1 : 0;
		}

		public void Set8H(int first, int second, int result) 
		{
			this.F_HALFCARRY = ((first ^ second ^ result) & 0x10) >> 4;
		}

		public void Set8C(int value) 
		{
			this.F_CARRY = (value & 0x100) >> 8;
		}

		public void Set16C(int value) 
		{
			this.F_CARRY = (value & 0x10000) >> 16;
		}

		public void Set8V(int first, int second, int result) 
		{
			this.F_OVERFLOW = ((first ^ second ^ result ^ (result >> 1)) & 0x80) >> 7;
		}

		public void Set16V(int first, int second, int result) 
		{
			this.F_OVERFLOW = ((first ^ second ^ result ^ (result >> 1)) & 0x8000) >> 15;
		}

		public void Set8NZ(int value) 
		{
			this.Set8N(value);
			this.Set8Z(value);
		}

		public void Set16NZ(int value) 
		{
			this.Set16N(value);
			this.Set16Z(value);
		}

		public void Set8HNZVC(int first, int second, int result) 
		{
			this.Set8NZVC(first, second, result);
			this.Set8H(first, second, result);
		}

		public void Set8NZVC(int first, int second, int result) 
		{
			this.Set8NZ(result & 0xFF);
			this.Set8V(first, second, result);
			this.Set8C(result);
		}

		public void Set16NZVC(int first, int second, int result) 
		{
			this.Set16NZ(result & 0xFFFF);
			this.Set16V(first, second, result);
			this.Set16C(result);
		}

		public int SignExtend(int value) 
		{
			return value < 128 ? value : value - 256;
		}

		public string[] Mnemonics = new string[] { 
			".CLB", "NOP ", ".CEA", ".SEA", "LSRD", "ASLD", "TAP ", "TPA ", "INX ", "DEX ", "CLV ", "SEV ", "CLC ", "SEC ", "CLI ", "SEI ",
			"SBA ", "CBA ", ".12 ", ".13 ", ".14 ", ".15 ", "TAB ", "TBA ", ".18 ", "DAA ", ".1A ", "ABA ", ".1C ", ".1D ", ".1E ", ".1F ",
			"BRA ", "BRN ", "BHI ", "BLS ", "BCC ", "BCS ", "BNE ", "BEQ ", "BVC ", "BVS ", "BPL ", "BMI ", "BGE ", "BLT ", "BGT ", "BLE ",
			"TSX ", "INS ", "PULA", "PULB", "DES ", "TXS ", "PHSA", "PSHB", "PULX", "RTS ", "ABX ", "RTI ", "PSHX", "MUL ", "WAI ", "SWI ",
			"NEGA", ".41 ", ".42 ", "COMA", "LSRA", ".45 ", "RORA", "ASRA", "ASLA", "ROLA", "DECA", ".4B ", "INCA", "TSTA", "T4E ", "CLRA",
			"NEGB", ".51 ", ".52 ", "COMB", "LSRB", ".55 ", "RORB", "ASRB", "ASLB", "ROLB", "DECB", ".5B ", "INCB", "TSTB", "T5E ", "CLRB",
			"NEG ", ".61 ", ".62 ", "COM ", "LSR ", ".65 ", "ROR ", "ASR ", "ASL ", "ROL ", "DEC ", ".6B ", "INC ", "TST ", "JMP ", "CLR ",
			"NEG ", ".71 ", ".72 ", "COM ", "LSR ", ".75 ", "ROR ", "ASR ", "ASL ", "ROL ", "DEC ", ".7B ", "INC ", "TST ", "JMP ", "CLR ",
			"SUBA", "CMPA", "SBCA", "SUBD", "ANDA", "BITA", "LDAA", ".87 ", "EORA", "ADCA", "ORAA", "ADDA", "CMPX", "BSR ", "LDS ", ".8F ",
			"SUBA", "CMPA", "SBCA", "SUBD", "ANDA", "BITA", "LDAA", "STAA", "EORA", "ADCA", "ORAA", "ADDA", "CMPX", "JSR ", "LDS ", "STS ",
			"SUBA", "CMPA", "SBCA", "SUBD", "ANDA", "BITA", "LDAA", "STAA", "EORA", "ADCA", "ORAA", "ADDA", "CMPX", "JSR ", "LDS ", "STS ",
			"SUBA", "CMPA", "SBCA", "SUBD", "ANDA", "BITA", "LDAA", "STAA", "EORA", "ADCA", "ORAA", "ADDA", "CMPX", "JSR ", "LDS ", "STS ",
			"SUBB", "CMPB", "SBCB", "ADDD", "ANDB", "BITB", "LDAB", ".C7 ", "EORB", "ADCB", "ORAB", "ADDB", "LDD ", ".CD ", "LDX ", ".CF ",
			"SUBB", "CMPB", "SBCB", "ADDD", "ANDB", "BITB", "LDAB", "STAB", "EORB", "ADCB", "ORAB", "ADDB", "LDD ", "STD ", "LDX ", "STX ",
			"SUBB", "CMPB", "SBCB", "ADDD", "ANDB", "BITB", "LDAB", "STAB", "EORB", "ADCB", "ORAB", "ADDB", "LDD ", "STD ", "LDX ", "STX ",
			"SUBB", "CMPB", "SBCB", "ADDD", "ANDB", "BITB", "LDAB", "STAB", "EORB", "ADCB", "ORAB", "ADDB", "LDD ", "STD ", "LDX ", "STX ",
		};

		public void Disassemble(int address) 
		{
			var op = this.FetchMemory(address);
			var opstr = this.Mnemonics[op];

			if ((op & 0xf0) == 0x20 || op == 0x8d) // relative
			{ 
				var dest = this.FetchMemory((address + 1) & 0xFFFF);
				dest = (dest & 0x80) != 0 ? dest | 0xFF00 : dest;
				dest = (address + 2 + dest) & 0xFFFF;
				//Console.WriteLine(StringTools.hex(address) + " " + opstr + "  " + StringTools.hex(dest));

			}
			else if ((op & 0xf0) == 0x60 || (op & 0xf0) == 0xa0 || (op & 0xf0) == 0xe0) // indexed
			{ 
				var offset = this.FetchMemory((address + 1) & 0xFFFF);
				//Console.WriteLine(StringTools.hex(address) + " " + opstr + "  " + StringTools.hex(offset) + ",X");

			}
			else if ((op & 0xf0) == 0x70 || (op & 0xf0) == 0xb0 || (op & 0xf0) == 0xf0)  // extended
			{
				var mem16 = (this.FetchMemory((address + 1) & 0xFFFF) << 8) + this.FetchMemory((address + 2) & 0xFFFF);
				//Console.WriteLine(StringTools.hex(address) + " " + opstr + "  " + StringTools.hex(mem16));

			}
			else if ((op & 0xf0) == 0x90 || (op & 0xf0) == 0xd0) // direct
			{ 
				var mem8 = this.FetchMemory((address + 1) & 0xFFFF);
				//Console.WriteLine(StringTools.hex(address) + " " + opstr + "  " + StringTools.hex(mem8));

			}
			else if ((op & 0xf0) == 0x80 || (op & 0xf0) == 0xc0)
			{
				var imm = this.FetchMemory((address + 1) & 0xFFFF);
				if ((op & 0xf) == 0x3 || (op & 0xf) > 0xb)
				{
					imm = (imm << 8) + this.FetchMemory((address + 2) & 0xFFFF);
				}
				//Console.WriteLine(StringTools.hex(address) + " " + opstr + " #" + StringTools.hex(imm));

			}
			else
			{
				//Console.WriteLine(StringTools.hex(address) + " " + opstr);

			}
		}
	}
}
