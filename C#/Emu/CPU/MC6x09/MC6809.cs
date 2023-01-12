//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace Emu.CPU.MC6x09
{
    public class MC6809 : Cpu
    {
        enum F
        {
            CARRY = 1,
            OVERFLOW = 2,
            ZERO = 4,
            NEGATIVE = 8,
            IRQMASK = 16,
            HALFCARRY = 32,
            FIRQMASK = 64,
            ENTIRE = 128
        }

        private int regX;
        private int regY;
        private int regU;
        private int regS;
        private int regPC;
        private int regA; // byte
        private int regB; // byte

        private int getRegD() 
        {
            return 0xFFFF & (this.regA << 8 | this.regB & 0xFF);
        }

        private void setRegD(int value) 
        {
            this.regB = value & 0xFF;
            this.regA = (value >> 8) & 0xFF;
        }

        private int regDP; // byte
        private int regCC; // byte

        private int stackAddress; // address to set regS to on reset.
        private int iClocks;

        // private byte[] mem;

        private int pcCount;

        /* Pending interrupt bits */
        int INT_NMI = 1;
        int INT_FIRQ = 2;
        int INT_IRQ = 4;

        public MC6809() 
        {
            this.Init_Instructions();
            this.Init_Instructions10();
            this.Init_Instructionst11();
        }

        private int MakeSignedByte(int x) 
        {
            return x << 24 >> 24;
        }

        private int MakeSignedWord(int x) 
        {
            return x << 16 >> 16;
        }

        private int SET_V8(int a, int b, int r) 
        {
            // TODO: might need to mask & 0xFF each param.
            return (((a ^ b ^ r ^ (r >> 1)) & 0x80) >> 6);
        }

        private int SET_V16(int a, int b, int r) 
        {
            // TODO: might need to mask & 0xFFFF each param.
            return (((a ^ b ^ r ^ (r >> 1)) & 0x8000) >> 14);
        }

        private bool _debug = false;
        private string Hex(int v, int width) 
        {
            // JAR_NOTE: check ... 
            var s = v.ToString("X4");
            //if (!width) width = 2;
            //while (s.length < width)
            //{
            //	s = "0" + s;
            //}
            return s;
        }

        private void Dumpmem(int addr, int count) 
        {
            for (var a = addr; a < addr + count; a++)
            {
                Console.WriteLine(a.ToString("X4") + " " + this.Hex(this.M6809ReadByte(a), 2));
            }
        }

        private void Dumpstack(int count) 
        {
            var addr = this.regS;
            for (var i = 0; i < count; i++)
            {
                Console.WriteLine(this.Hex(this.M6809ReadWord(addr), 4));
                addr += 2;
            }
        }

        private string StateToString() 
        {
            return "pc:" + this.Hex(this.regPC, 4) +
                " s:" + this.Hex(this.regS, 4) +
                " u:" + this.Hex(this.regU, 4) +
                " x:" + this.Hex(this.regX, 4) +
                " y:" + this.Hex(this.regY, 4) +
                " a:" + this.Hex(this.regA, 2) +
                " b:" + this.Hex(this.regB, 2) +
                " d:" + this.Hex(this.getRegD(), 4) +
                " dp:" + this.Hex(this.regDP, 2) +
                " cc:" + this.FlagsToString();
        }

        private string NextOp() 
        {
            var pc = this.regPC;

            var nextop = this.M6809ReadByte(pc);
            var mn = this._mnemonics;
            if (nextop == 0x10)
            {
                mn = this._mnemonics10;
                nextop = this.M6809ReadByte(++pc);
            }
            else if (nextop == 0x11)
            {
                // JAR_NOTE: fix .... 
                //mn = this.mnemonics11;
                nextop = this.M6809ReadByte(++pc);
            }
            return mn[nextop];
        }

        private string State() 
        {
            var pc = this.regPC;

            var nextop = this.M6809ReadByte(pc);
            var mn = this._mnemonics;
            if (nextop == 0x10)
            {
                mn = this._mnemonics10;
                nextop = this.M6809ReadByte(++pc);
            }
            else if (nextop == 0x11)
            {
                // JAR_NOTE: fix .... 
                // mn = this.mnemonics11;
                nextop = this.M6809ReadByte(++pc);
            }

            var ret = this.Hex(pc, 4) + " " +
                mn[nextop] + " " +
                this.Hex(this.ReadByteROM(pc + 1), 2) + " " +
                this.Hex(this.ReadByteROM(pc + 2), 2) + " ";

            ret +=
            " s:" + this.Hex(this.regS, 4) +
            " u:" + this.Hex(this.regU, 4) +
            " x:" + this.Hex(this.regX, 4) +
            " y:" + this.Hex(this.regY, 4) +
            " a:" + this.Hex(this.regA, 2) +
            " b:" + this.Hex(this.regB, 2) +
            " d:" + this.Hex(this.getRegD(), 4) +
            " dp:" + this.Hex(this.regDP, 2) +
            " cc:" + this.FlagsToString() +
            "  [" + this.pcCount + "]";

            return ret;
        }

        private string FlagsToString() 
        {
            return (((this.regCC & (int)F.NEGATIVE) != 0) ? "N" : "-") +
                (((this.regCC & (int)F.ZERO) != 0) ? "Z" : "-") +
                (((this.regCC & (int)F.CARRY) != 0) ? "C" : "-") +
                (((this.regCC & (int)F.IRQMASK) != 0) ? "I" : "-") +
                (((this.regCC & (int)F.HALFCARRY) != 0) ? "H" : "-") +
                (((this.regCC & (int)F.OVERFLOW) != 0) ? "V" : "-") +
                (((this.regCC & (int)F.FIRQMASK) != 0) ? "C" : "-") +
                (((this.regCC & (int)F.ENTIRE) != 0) ? "E" : "-");
        }

        private void Execute(int iClocks, int interruptRequest, int breakpoint) 
        {
            this.iClocks = iClocks;
            if ((breakpoint != 0))
            {
                Console.WriteLine("breakpoint set: " + breakpoint.ToString("X4"));
            }

            while (this.iClocks > 0) /* Execute for the amount of time alloted */
            {

                if ((breakpoint != 0) && this.regPC == breakpoint)
                {
                    Console.WriteLine("hit breakpoint at " + breakpoint.ToString("X4"));
                    this.Halt();
                    break;
                }

                interruptRequest = this.HandleIRQ(interruptRequest);

                var mn = this.NextOp();
                // JAR_NOTE: fix 
                //if (this.counts.hasOwnProperty(mn))
                //{
                //	this.counts[mn]++;
                //}
                //else
                //{
                //	this.inorder.push(mn);
                //	this.counts[mn] = 1;
                //}

                var ucOpcode = this.NextPCByte();
                this.iClocks -= _mc6809Cycles[ucOpcode]; /* Subtract execution time */
                if (this._debug)
                    Console.WriteLine((this.regPC - 1).ToString("X4") + ": " + this._mnemonics[ucOpcode]);

                var instruction = this._instructions[ucOpcode];
                if (instruction == null)
                {
                    Console.WriteLine("*** illegal opcode: " + ucOpcode.ToString("X4") + " at " + (this.regPC - 1).ToString("X4"));
                    this.iClocks = 0;
                    this.Halt();
                }
                else
                {
                    instruction();
                }
            }
        }

        private int ReadByteROM(int addr) 
        {
            // JAR_NOTE: Finish!! 
            // var ucByte = this.mem[MEM_ROM + addr];
            // var ucByte = this.mem[addr];
            // Console.WriteLine("Read ROM: " + addr.ToString("X4") + " -> " + ucByte.ToString("X4"));
            // return ucByte;

            return 0;
        }

        private void Reset()
        {
            this.regX = 0;
            this.regY = 0;
            this.regU = 0;
            this.regS = this.stackAddress;
            this.regA = 0;
            this.regB = 0;
            this.regDP = 0;
            this.regCC = (int)(F.FIRQMASK | F.IRQMASK);
            this.regPC = 0;
            this._goto((this.ReadByteROM(0xFFFF) << 8) | this.ReadByteROM(0xFFFF));
        }

        private void SetStackAddress(int addr)
        {
            this.stackAddress = addr;
        }

        private void LoadMemory(byte[] bytes, int addr)
        {
            // JAR_NOTE: Finish!! 
        }

        private void SetMemoryMap(byte[] map)
        {
            // JAR_NOTE: Finish!! 
        }

        private bool _halted = false;
        private void Halt()
        {
            this._halted = true;
            this.iClocks = 0;
            Console.WriteLine("halted.");
        }

        private int NextPCByte() 
        {
            this.pcCount++;
            return this.M6809ReadByte(this.regPC++);
        }

        private int NextPCWord() 
        {
            var word = this.M6809ReadWord(this.regPC);
            this.regPC += 2;
            this.pcCount += 2;
            return word;
        }

        private int M6809ReadByte(int addr) 
        {
            // JAR_NOTE: Finish!! 
            return -1;
        }

        private void M6809WriteByte(int addr, int ucByte) 
        {
            // JAR_NOTE: Finish!! 

            //var c = this.mem[addr + MEM_FLAGS]; /* If special flag (ROM or hardware) */
            //switch (c)
            //{
            //	case 0: /* Normal RAM */
            //			// Console.WriteLine("Write RAM: " + addr.toString(16) + " = " + (ucByte & 0xFF).toString(16));
            //		this.mem[addr + MEM_RAM] = (byte)(ucByte & 0xFF);
            //		break;
            //	case 1: /* Normal ROM - nothing to do */

            //		Console.WriteLine("******** Write ROM: from PC: " + this.regPC.ToString("X4") + "   " + addr.ToString("X4") + " = " + (ucByte & 0xFF).ToString("X4"));
            //		this.mem[addr + MEM_ROM] = (byte)(ucByte & 0xFF); // write it to ROM anyway...
            //		break;
            //	default: /* Call special handler routine for this address */
            //		var handler = this.memHandler[c - 2];
            //		if (handler == undefined)
            //		{
            //			Console.WriteLine("need write handler at " + (c - 2));
            //		}
            //		else
            //			handler.write(addr, ucByte & 0xFF);
            //		break;
            //}
        }

        private int M6809ReadWord(int addr) 
        {
            var hi = this.M6809ReadByte(addr);
            var lo = this.M6809ReadByte(addr + 1);
            return hi << 8 | lo;
        }

        private void M6809WriteWord(int addr, int usWord) 
        {
            this.M6809WriteByte(addr, usWord >> 8);
            this.M6809WriteByte(addr + 1, usWord);
        }

        private void PushByte(int ucByte, bool user) 
        {
            var addr = user ? --this.regU : --this.regS;
            this.M6809WriteByte(addr, ucByte);
        }

        private void M6809PUSHBU(int ucByte) 
        {
            this.PushByte(ucByte, true);
        }

        private void M6809PUSHB(int ucByte) 
        {
            this.PushByte(ucByte, false);
        }

        private void M6809PUSHW(int usWord) 
        {
            // push lo byte first.
            this.M6809PUSHB(usWord);
            this.M6809PUSHB(usWord >> 8);
        }

        private void M6809PUSHWU(int usWord) 
        {
            // push lo byte first.
            this.M6809PUSHBU(usWord);
            this.M6809PUSHBU(usWord >> 8);
        }

        private int PullByte(bool user) 
        {
            var addr = user ? this.regU : this.regS;
            var val = this.M6809ReadByte(addr);
            if (user) ++this.regU;
            else ++this.regS;
            return val;
        }

        private int M6809PULLB() 
        {
            return this.PullByte(false);
        }

        private int M6809PULLBU() 
        {
            return this.PullByte(true);
        }

        private int M6809PULLW() 
        {
            var hi = this.M6809PULLB();
            var lo = this.M6809PULLB();
            return hi << 8 | lo;
        }

        private int M6809PULLWU() 
        {
            var hi = this.M6809PULLBU();
            var lo = this.M6809PULLBU();
            return hi << 8 | lo;
        }

        private int Get_pReg(string pReg) 
        {
            // JAR_NOTE: FINISH!!!
            return -1;
        }

        private void Set_pReg(string pReg, int val) 
        {
            // JAR_NOTE: FINISH!!!
            // return -1;
        }

        private int M6809PostByte() 
        {
            string pReg = "";
            int usAddr = 0, sTemp;
            var ucPostByte = this.NextPCByte();
            switch (ucPostByte & 0x60)
            {
                case 0:
                    pReg = "X";
                    break;
                case 0x20:
                    pReg = "Y";
                    break;
                case 0x40:
                    pReg = "U";
                    break;
                case 0x60:
                    pReg = "S";
                    break;
            }
            pReg = "reg" + pReg;

            if ((ucPostByte & 0x80) == 0)
            {
                /* Just a 5 bit signed offset + register */
                var sByte = ucPostByte & 0x1f;
                if (sByte > 15) /* Two's complement 5-bit value */
                    sByte -= 32;
                this.iClocks -= 1;
                return Get_pReg(pReg) + sByte;
            }

            switch (ucPostByte & 0xf)
            {
                case 0: /* EA = ,reg+ */
                    usAddr = Get_pReg(pReg);
                    Set_pReg(pReg, Get_pReg(pReg) + 1);
                    this.iClocks -= 2;
                    break;
                case 1: /* EA = ,reg++ */
                    usAddr = Get_pReg(pReg);
                    Set_pReg(pReg, Get_pReg(pReg) + 2);
                    this.iClocks -= 3;
                    break;
                case 2: /* EA = ,-reg */
                    Set_pReg(pReg, Get_pReg(pReg) - 1);
                    usAddr = Get_pReg(pReg);
                    this.iClocks -= 2;
                    break;
                case 3: /* EA = ,--reg */
                    Set_pReg(pReg, Get_pReg(pReg) - 2);
                    usAddr = Get_pReg(pReg);
                    this.iClocks -= 3;
                    break;
                case 4: /* EA = ,reg */
                    usAddr = Get_pReg(pReg);
                    break;
                case 5: /* EA = ,reg + B */
                    usAddr = Get_pReg(pReg) + MakeSignedByte(this.regB);
                    this.iClocks -= 1;
                    break;
                case 6: /* EA = ,reg + A */
                    usAddr = Get_pReg(pReg) + MakeSignedByte(this.regA);
                    this.iClocks -= 1;
                    break;
                case 7: /* illegal */
                    Console.WriteLine("illegal postbyte pattern 7 at " + (this.regPC - 1).ToString("X4"));
                    this.Halt();
                    usAddr = 0;
                    break;
                case 8: /* EA = ,reg + 8-bit offset */
                    usAddr = Get_pReg(pReg) + MakeSignedByte(this.NextPCByte());
                    this.iClocks -= 1;
                    break;
                case 9: /* EA = ,reg + 16-bit offset */
                    usAddr = Get_pReg(pReg) + MakeSignedWord(this.NextPCWord());
                    this.iClocks -= 4;
                    break;
                case 0xA: /* illegal */
                    Console.WriteLine("illegal postbyte pattern 0xA" + (this.regPC - 1).ToString("X4"));
                    this.Halt();
                    usAddr = 0;
                    break;
                case 0xB: /* EA = ,reg + D */
                    this.iClocks -= 4;
                    usAddr = Get_pReg(pReg) + this.getRegD();
                    break;
                case 0xC: /* EA = PC + 8-bit offset */
                    sTemp = MakeSignedByte(this.NextPCByte());
                    usAddr = this.regPC + sTemp;
                    this.iClocks -= 1;
                    break;
                case 0xD: /* EA = PC + 16-bit offset */
                    sTemp = MakeSignedWord(this.NextPCWord());
                    usAddr = this.regPC + sTemp;
                    this.iClocks -= 5;
                    break;
                case 0xE: /* Illegal */
                    Console.WriteLine("illegal postbyte pattern 0xE" + (this.regPC - 1).ToString("X4"));
                    this.Halt();
                    usAddr = 0;
                    break;
                case 0xF: /* EA = [,address] */
                    this.iClocks -= 5;
                    usAddr = this.NextPCWord();
                    break;
            } /* switch */

            if ((ucPostByte & 0x10) != 0) /* Indirect addressing */
            {
                usAddr = this.M6809ReadWord(usAddr & 0xFFFF);
                this.iClocks -= 3;
            }
            return usAddr & 0xFFFF; /* Return the effective address */
        }

        private void M6809PSHS(int ucTemp) 
        {
            var i = 0;

            if ((ucTemp & 0x80) != 0)
            {
                this.M6809PUSHW(this.regPC);
                i += 2;
            }
            if ((ucTemp & 0x40) != 0)
            {
                this.M6809PUSHW(this.regU);
                i += 2;
            }
            if ((ucTemp & 0x20) != 0)
            {
                this.M6809PUSHW(this.regY);
                i += 2;
            }
            if ((ucTemp & 0x10) != 0)
            {
                this.M6809PUSHW(this.regX);
                i += 2;
            }
            if ((ucTemp & 0x8) != 0)
            {
                this.M6809PUSHB(this.regDP);
                i++;
            }
            if ((ucTemp & 0x4) != 0)
            {
                this.M6809PUSHB(this.regB);
                i++;
            }
            if ((ucTemp & 0x2) != 0)
            {
                this.M6809PUSHB(this.regA);
                i++;
            }
            if ((ucTemp & 0x1) != 0)
            {
                this.M6809PUSHB(this.regCC);
                i++;
            }
            this.iClocks -= i; /* Add extra clock cycles (1 per byte) */
        }

        private void M6809PSHU(int ucTemp) 
        {
            var i = 0;

            if ((ucTemp & 0x80) != 0)
            {
                this.M6809PUSHWU(this.regPC);
                i += 2;
            }
            if ((ucTemp & 0x40) != 0)
            {
                this.M6809PUSHWU(this.regU);
                i += 2;
            }
            if ((ucTemp & 0x20) != 0)
            {
                this.M6809PUSHWU(this.regY);
                i += 2;
            }
            if ((ucTemp & 0x10) != 0)
            {
                this.M6809PUSHWU(this.regX);
                i += 2;
            }
            if ((ucTemp & 0x8) != 0)
            {
                this.M6809PUSHBU(this.regDP);
                i++;
            }
            if ((ucTemp & 0x4) != 0)
            {
                this.M6809PUSHBU(this.regB);
                i++;
            }
            if ((ucTemp & 0x2) != 0)
            {
                this.M6809PUSHBU(this.regA);
                i++;
            }
            if ((ucTemp & 0x1) != 0)
            {
                this.M6809PUSHBU(this.regCC);
                i++;
            }
            this.iClocks -= i; /* Add extra clock cycles (1 per byte) */
        }

        private void M6809PULS(int ucTemp) 
        {
            var i = 0;
            if ((ucTemp & 0x1) != 0)
            {
                this.regCC = this.M6809PULLB();
                i++;
            }
            if ((ucTemp & 0x2) != 0)
            {
                this.regA = this.M6809PULLB();
                i++;
            }
            if ((ucTemp & 0x4) != 0)
            {
                this.regB = this.M6809PULLB();
                i++;
            }
            if ((ucTemp & 0x8) != 0)
            {
                this.regDP = this.M6809PULLB();
                i++;
            }
            if ((ucTemp & 0x10) != 0)
            {
                this.regX = this.M6809PULLW();
                i += 2;
            }
            if ((ucTemp & 0x20) != 0)
            {
                this.regY = this.M6809PULLW();
                i += 2;
            }
            if ((ucTemp & 0x40) != 0)
            {
                this.regU = this.M6809PULLW();
                i += 2;
            }
            if ((ucTemp & 0x80) != 0)
            {
                this._goto(this.M6809PULLW());
                i += 2;
            }
            this.iClocks -= i; /* Add extra clock cycles (1 per byte) */
        }

        private void M6809PULU(int ucTemp) 
        {
            var i = 0;
            if ((ucTemp & 0x1) != 0)
            {
                this.regCC = this.M6809PULLBU();
                i++;
            }
            if ((ucTemp & 0x2) != 0)
            {
                this.regA = this.M6809PULLBU();
                i++;
            }
            if ((ucTemp & 0x4) != 0)
            {
                this.regB = this.M6809PULLBU();
                i++;
            }
            if ((ucTemp & 0x8) != 0)
            {
                this.regDP = this.M6809PULLBU();
                i++;
            }
            if ((ucTemp & 0x10) != 0)
            {
                this.regX = this.M6809PULLWU();
                i += 2;
            }
            if ((ucTemp & 0x20) != 0)
            {
                this.regY = this.M6809PULLWU();
                i += 2;
            }
            if ((ucTemp & 0x40) != 0)
            {
                this.regU = this.M6809PULLWU();
                i += 2;
            }
            if ((ucTemp & 0x80) != 0)
            {
                this._goto(this.M6809PULLWU());
                i += 2;
            }
            this.iClocks -= i; /* Add extra clock cycles (1 per byte) */
        }

        private int HandleIRQ(int interruptRequest) 
        {
            /* NMI is highest priority */
            if ((interruptRequest & INT_NMI) != 0)
            {
                Console.WriteLine("taking NMI!!!!");
                this.M6809PUSHW(this.regPC);
                this.M6809PUSHW(this.regU);
                this.M6809PUSHW(this.regY);
                this.M6809PUSHW(this.regX);
                this.M6809PUSHB(this.regDP);
                this.M6809PUSHB(this.regB);
                this.M6809PUSHB(this.regA);
                this.regCC |= 0x80; /* Set bit indicating machine state on stack */
                this.M6809PUSHB(this.regCC);
                this.regCC |= (int)(F.FIRQMASK | F.IRQMASK); /* Mask interrupts during service routine */
                this.iClocks -= 19;
                this._goto(this.M6809ReadWord(0xFFFC));
                interruptRequest &= ~INT_NMI; /* clear this bit */
                Console.WriteLine(this.State());
                return interruptRequest;
            }
            /* Fast IRQ is next priority */
            if (((interruptRequest & INT_FIRQ) != 0) && (this.regCC & (int)F.FIRQMASK) == 0)
            {
                Console.WriteLine("taking FIRQ!!!!");
                this.M6809PUSHW(this.regPC);
                this.regCC &= 0x7f; /* Clear bit indicating machine state on stack */
                this.M6809PUSHB(this.regCC);
                interruptRequest &= ~INT_FIRQ; /* clear this bit */
                this.regCC |= (int)(F.FIRQMASK | F.IRQMASK); /* Mask interrupts during service routine */
                this.iClocks -= 10;
                this._goto(this.M6809ReadWord(0xFFF6));
                Console.WriteLine(this.State());
                return interruptRequest;
            }
            /* IRQ is lowest priority */
            if ((interruptRequest & INT_IRQ) != 0 && (this.regCC & (int)F.IRQMASK) == 0)
            {
                Console.WriteLine("taking IRQ!!!!");
                this.M6809PUSHW(this.regPC);
                this.M6809PUSHW(this.regU);
                this.M6809PUSHW(this.regY);
                this.M6809PUSHW(this.regX);
                this.M6809PUSHB(this.regDP);
                this.M6809PUSHB(this.regB);
                this.M6809PUSHB(this.regA);
                this.regCC |= 0x80; /* Set bit indicating machine state on stack */
                this.M6809PUSHB(this.regCC);
                this.regCC |= (int)F.IRQMASK; /* Mask interrupts during service routine */
                this._goto(this.M6809ReadWord(0xFFF8));
                interruptRequest &= ~INT_IRQ; /* clear this bit */
                this.iClocks -= 19;
                Console.WriteLine(this.State());
                return interruptRequest;
            }
            return interruptRequest;
        }

        private void ToggleDebug() 
        {
            this._debug = !this._debug;
            Console.WriteLine("debug " + this._debug);
        }

        private void _goto(int usAddr) 
        {
            if (usAddr == 0xFFB3)
            {
                Console.WriteLine("PC from " + this.regPC.ToString("X4") + " -> " + usAddr.ToString("X4"));
                if (this.getRegD() > 0x9800)
                {
                    Console.WriteLine("off screen??? " + this.getRegD().ToString("X4"));
                }
            }
            this.regPC = usAddr;
        }

        private void _flagnz(int val) 
        {
            if ((val & 0xFF) == 0)
                this.regCC |= (int)F.ZERO;
            else if ((val & 0x80) != 0)
                this.regCC |= (int)F.NEGATIVE;
        }

        private void _flagnz16(int val) 
        {
            if ((val & 0xFFFF) == 0)
                this.regCC |= (int)F.ZERO;
            else if ((val & 0x8000) != 0)
                this.regCC |= (int)F.NEGATIVE;
        }

        private int _neg(int val) 
        {
            this.regCC &= ~(int)(F.CARRY | F.ZERO | F.OVERFLOW | F.NEGATIVE);
            if (val == 0x80)
                this.regCC |= (int)F.OVERFLOW;
            val = ~val + 1;
            val &= 0xFF;
            this._flagnz(val);
            if ((this.regCC & (int)F.NEGATIVE) != 0)
                this.regCC |= (int)F.CARRY;
            return val;
        }

        private int _com(int val) 
        {
            this.regCC &= ~(int)(F.ZERO | F.OVERFLOW | F.NEGATIVE);
            this.regCC |= (int)F.CARRY;
            val = ~val;
            val &= 0xFF;
            this._flagnz(val);
            return val;
        }

        private int _lsr(int val) 
        {
            this.regCC &= ~(int)(F.ZERO | F.CARRY | F.NEGATIVE);
            if ((val & 1) != 0)
                this.regCC |= (int)F.CARRY;
            val >>= 1;
            val &= 0xFF;
            if (val == 0)
                this.regCC |= (int)F.ZERO;
            return val;
        }

        private int _ror(int val) 
        {
            var oldc = this.regCC & (int)F.CARRY;
            this.regCC &= ~(int)(F.ZERO | F.CARRY | F.NEGATIVE);
            if ((val & 1) != 0)
                this.regCC |= (int)F.CARRY;
            val = val >> 1 | oldc << 7;
            val &= 0xFF;
            this._flagnz(val);
            return val;
        }

        private int _asr(int val) 
        {
            this.regCC &= ~(int)(F.ZERO | F.CARRY | F.NEGATIVE);
            if ((val & 1) != 0)
                this.regCC |= (int)F.CARRY;
            val = val & 0x80 | val >> 1;
            val &= 0xFF;
            this._flagnz(val);
            return val;
        }

        private int _asl(int val) 
        {
            var oldval = val;
            this.regCC &= ~(int)(F.ZERO | F.CARRY | F.OVERFLOW | F.NEGATIVE);
            if ((val & 0x80) != 0)
                this.regCC |= (int)F.CARRY;
            val <<= 1;
            val &= 0xFF;
            this._flagnz(val);
            if (((oldval ^ val) & 0x80) != 0)
                this.regCC |= (int)F.OVERFLOW;
            return val;
        }

        private int _rol(int val) 
        {
            var oldval = val;
            var oldc = this.regCC & (int)F.CARRY; /* Preserve old carry flag */
            this.regCC &= ~(int)(F.ZERO | F.CARRY | F.OVERFLOW | F.NEGATIVE);
            if ((val & 0x80) != 0)
                this.regCC |= (int)F.CARRY;
            val = val << 1 | oldc;
            val &= 0xFF;
            this._flagnz(val);
            if (((oldval ^ val) & 0x80) != 0)
                this.regCC |= (int)F.OVERFLOW;
            return val;
        }

        private int _dec(int val) 
        {
            val--;
            val &= 0xFF;
            this.regCC &= ~(int)(F.ZERO | F.OVERFLOW | F.NEGATIVE);
            this._flagnz(val);
            if (val == 0x7f || val == 0xFF)
                this.regCC |= (int)F.OVERFLOW;
            return val;
        }

        private int _inc(int val) 
        {
            val++;
            val &= 0xFF;
            this.regCC &= ~(int)(F.ZERO | F.OVERFLOW | F.NEGATIVE);
            this._flagnz(val);
            if (val == 0x80 || val == 0)
                this.regCC |= (int)F.OVERFLOW;
            return val;
        }

        private int _tst(int val) 
        {
            this.regCC &= ~(int)(F.ZERO | F.OVERFLOW | F.NEGATIVE);
            this._flagnz(val);
            return val;
        }

        private void _clr(int addr) 
        {
            this.M6809WriteByte(addr, 0);
            /* clear N,V,C, set Z */
            this.regCC &= ~(int)(F.CARRY | F.OVERFLOW | F.NEGATIVE);
            this.regCC |= (int)F.ZERO;
        }

        private int _or(int ucByte1, int ucByte2) 
        {
            this.regCC &= ~(int)(F.ZERO | F.OVERFLOW | F.NEGATIVE);
            var ucTemp = ucByte1 | ucByte2;
            this._flagnz(ucTemp);
            return ucTemp;
        }

        private int _eor(int ucByte1, int ucByte2) 
        {
            this.regCC &= ~(int)(F.ZERO | F.OVERFLOW | F.NEGATIVE);
            var ucTemp = ucByte1 ^ ucByte2;
            this._flagnz(ucTemp);
            return ucTemp;
        }

        private int _and(int ucByte1, int ucByte2) 
        {
            this.regCC &= ~(int)(F.ZERO | F.OVERFLOW | F.NEGATIVE);
            var ucTemp = ucByte1 & ucByte2;
            this._flagnz(ucTemp);
            return ucTemp;
        }

        private void _cmp(int ucByte1, int ucByte2) 
        {
            var sTemp = (ucByte1 & 0xFF) - (ucByte2 & 0xFF);
            this.regCC &= ~(int)(F.ZERO | F.CARRY | F.OVERFLOW | F.NEGATIVE);
            this._flagnz(sTemp);
            if ((sTemp & 0x100) != 0)
                this.regCC |= (int)F.CARRY;
            this.regCC |= SET_V8(ucByte1, ucByte2, sTemp);
        }

        private void _setcc16(int usWord1, int usWord2, int lTemp) 
        {
            this.regCC &= ~(int)(F.ZERO | F.CARRY | F.OVERFLOW | F.NEGATIVE);
            this._flagnz16(lTemp);
            if ((lTemp & 0x10000) != 0)
                this.regCC |= (int)F.CARRY;
            this.regCC |= SET_V16(usWord1 & 0xFFFF, usWord2 & 0xFFFF, lTemp & 0x1ffff);
        }

        private void _cmp16(int usWord1, int usWord2) 
        {
            var lTemp = (usWord1 & 0xFFFF) - (usWord2 & 0xFFFF);
            this._setcc16(usWord1, usWord2, lTemp);
        }

        private int _sub(int ucByte1, int ucByte2) 
        {
            var sTemp = (ucByte1 & 0xFF) - (ucByte2 & 0xFF);
            this.regCC &= ~(int)(F.ZERO | F.CARRY | F.OVERFLOW | F.NEGATIVE);
            this._flagnz(sTemp);
            if ((sTemp & 0x100) != 0)
                this.regCC |= (int)F.CARRY;
            this.regCC |= SET_V8(ucByte1, ucByte2, sTemp);
            return sTemp & 0xFF;
        }

        private int _sub16(int usWord1, int usWord2) 
        {
            var lTemp = (usWord1 & 0xFFFF) - (usWord2 & 0xFFFF);
            this._setcc16(usWord1, usWord2, lTemp);
            return lTemp & 0xFFFF;
        }

        private int _sbc(int ucByte1, int ucByte2)
        {
            var sTemp = (ucByte1 & 0xFF) - (ucByte2 & 0xFF) - (this.regCC & 1);
            this.regCC &= ~(int)(F.ZERO | F.CARRY | F.OVERFLOW | F.NEGATIVE);
            this._flagnz(sTemp);
            if ((sTemp & 0x100) != 0)
                this.regCC |= (int)F.CARRY;
            this.regCC |= SET_V8(ucByte1, ucByte2, sTemp);
            return sTemp & 0xFF;
        }

        private int _add(int ucByte1, int ucByte2)
        {
            var sTemp = (ucByte1 & 0xFF) + (ucByte2 & 0xFF);
            this.regCC &= ~(int)(F.HALFCARRY | F.ZERO | F.CARRY | F.OVERFLOW | F.NEGATIVE);
            this._flagnz(sTemp);
            if ((sTemp & 0x100) != 0)
                this.regCC |= (int)F.CARRY;
            this.regCC |= SET_V8(ucByte1, ucByte2, sTemp);
            if (((sTemp ^ ucByte1 ^ ucByte2) & 0x10) != 0)
                this.regCC |= (int)F.HALFCARRY;
            return sTemp & 0xFF;
        }

        private int _add16(int usWord1, int usWord2)
        {
            var lTemp = (usWord1 & 0xFFFF) + (usWord2 & 0xFFFF);
            this._setcc16(usWord1, usWord2, lTemp);
            return lTemp & 0xFFFF;
        }

        private int _adc(int ucByte1, int ucByte2)
        {
            var sTemp = (ucByte1 & 0xFF) + (ucByte2 & 0xFF) + (this.regCC & 1);
            this.regCC &= ~(int)(F.HALFCARRY | F.ZERO | F.CARRY | F.OVERFLOW | F.NEGATIVE);
            this._flagnz(sTemp);
            if ((sTemp & 0x100) != 0)
                this.regCC |= (int)F.CARRY;
            this.regCC |= SET_V8(ucByte1, ucByte2, sTemp);
            if (((sTemp ^ ucByte1 ^ ucByte2) & 0x10) != 0)
                this.regCC |= (int)F.HALFCARRY;
            return sTemp & 0xFF;
        }

        private int DpAddr()
        {
            return (this.regDP << 8) + this.NextPCByte();
        }

        private void DpOp() // func: (val: int) => int
        {
        }

        /* direct page addressing */
        private void NEG_Direct() // 0x00: /* NEG - direct */
        {
            var addr = this.DpAddr();
            var val = this.M6809ReadByte(addr);
            this.M6809WriteByte(addr, this._neg(val));
        }

        private void COM_Direct() // 0x03: /* COM - direct */
        {
            var addr = this.DpAddr();
            var val = this.M6809ReadByte(addr);
            this.M6809WriteByte(addr, this._com(val));
        }

        private void LSR_Direct() // 0x04: /* LSR - direct */
        {
            var addr = this.DpAddr();
            var val = this.M6809ReadByte(addr);
            this.M6809WriteByte(addr, this._lsr(val));
        }

        private void ROR_Direct() // 0x06: /* ROR - direct */
        {
            var addr = this.DpAddr();
            var val = this.M6809ReadByte(addr);
            this.M6809WriteByte(addr, this._ror(val));
        }

        private void ASR_Direct() // 0x07: /* ASR - direct */
        {
            var addr = this.DpAddr();
            var val = this.M6809ReadByte(addr);
            this.M6809WriteByte(addr, this._asr(val));
        }

        private void ASL_Direct() // 0x08: /* ASL/LSL - direct */
        {
            var addr = this.DpAddr();
            var val = this.M6809ReadByte(addr);
            this.M6809WriteByte(addr, this._asl(val));
        }

        private void ROL_Direct() // 0x09: /* ROL - direct */
        {
            var addr = this.DpAddr();
            var val = this.M6809ReadByte(addr);
            this.M6809WriteByte(addr, this._rol(val));
        }

        private void DEC_Direct() // 0x0A: /* DEC - direct */
        {
            var addr = this.DpAddr();
            var val = this.M6809ReadByte(addr);
            this.M6809WriteByte(addr, this._dec(val));
        }

        private void INC_Direct() // 0x0C: /* INC - direct */
        {
            var addr = this.DpAddr();
            var val = this.M6809ReadByte(addr);
            this.M6809WriteByte(addr, this._inc(val));
        }

        private void TST_Direct() // 0x0D: /* TST - direct */
        {
            var addr = this.DpAddr();
            var val = this.M6809ReadByte(addr);
            this.M6809WriteByte(addr, this._tst(val));
        }

        private void JMP_Direct() // 0x0E: /* JMP - direct */
        {
            this._goto(this.DpAddr());
        }

        private void CLR_Direct() // 0x0F: /* CLR - direct */
        {
            this._clr(this.DpAddr());
        }

        /* P10  extended Op codes */

        private void LBRN_Relative() // 0x1021: /* LBRN - relative */
        {
            this.regPC += 2;
        }

        private void LBHI_Relative() // 0x1022: /* LBHI - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if (!((this.regCC & (int)(F.CARRY | F.ZERO)) != 0))
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBLS_Relative() // 0x1023: /* LBLS - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if ((this.regCC & (int)(F.CARRY | F.ZERO)) != 0)
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBCC_Relative() // 0x1024: /* LBHS/LBCC - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if (!((this.regCC & (int)F.CARRY) != 0))
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBCS_Relative() // 0x1025: /* LBLO/LBCS - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if ((this.regCC & (int)F.CARRY) != 0)
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBNE_Relative() // 0x1026: /* LBNE - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if (!((this.regCC & (int)F.ZERO) != 0))
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBEQ_Relative() // 0x1027: /* LBEQ - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if ((this.regCC & (int)F.ZERO) != 0)
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBVC_Relative() // 0x1028: /* LBVC - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if (!((this.regCC & (int)F.OVERFLOW) != 0))
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBVS_Relative() // 0x1029: /* LBVS - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if ((this.regCC & (int)F.OVERFLOW) != 0)
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBPL_Relative() // 0x102A: /* LBPL - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if (!((this.regCC & (int)F.NEGATIVE) != 0))
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBMI_Relative() // 0x102B: /* LBMI - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if ((this.regCC & (int)F.NEGATIVE) != 0)
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBGE_Relative() // 0x102C: /* LBGE - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if (!(((this.regCC & (int)F.NEGATIVE) ^ (this.regCC & (int)F.OVERFLOW) << 2) != 0))
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBLT_Relative() // 0x102D: /* LBLT - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if (((this.regCC & (int)F.NEGATIVE) ^ (this.regCC & (int)F.OVERFLOW) << 2) != 0)
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBGT_Relative() // 0x102E: /* LBGT - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if (!(((this.regCC & (int)F.NEGATIVE) ^ (this.regCC & (int)F.OVERFLOW) << 2) != 0 || (this.regCC & (int)F.ZERO) != 0))
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void LBLE_Relative() // 0x102F: /* LBLE - relative */
        {
            var sTemp = MakeSignedWord(this.NextPCWord());
            if (((this.regCC & (int)F.NEGATIVE) ^ (this.regCC & (int)F.OVERFLOW) << 2) != 0 || (this.regCC & (int)F.ZERO) != 0)
            {
                this.iClocks -= 1; /* Extra clock if branch taken */
                this.regPC += sTemp;
            }
        }

        private void SWI2_Inherent() // 0x103F: /* SWI2 - inherent */
        {
            this.regCC |= 0x80; /* Entire machine state stacked */
            this.M6809PUSHW(this.regPC);
            this.M6809PUSHW(this.regU);
            this.M6809PUSHW(this.regY);
            this.M6809PUSHW(this.regX);
            this.M6809PUSHB(this.regDP);
            this.M6809PUSHB(this.regA);
            this.M6809PUSHB(this.regB);
            this.M6809PUSHB(this.regCC);
            this._goto(this.M6809ReadWord(0xFFF4));
        }

        private void Cmpd() // 0x1083: /* CMPD - immediate*/
        {
            var usTemp = this.NextPCWord();
            this._cmp16(this.getRegD(), usTemp);
        }

        private void cmpy() // 0x108C: /* CMPY - immediate */
        {
            var usTemp = this.NextPCWord();
            this._cmp16(this.regY, usTemp);
        }

        private void ldy() // 0x108E: /* LDY - immediate */
        {
            this.regY = this.NextPCWord();
            this._flagnz16(this.regY);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void cmpdd() // 0x1093: /* CMPD - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.getRegD(), usTemp);
        }

        private void cmpyd() // 0x109c: /* CMPY - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.regY, usTemp);
        }

        private void ldyd() // 0x109E: /* LDY - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.regY = this.M6809ReadWord(usAddr);
            this._flagnz16(this.regY);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void sty() // 0x109F: /* STY - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.M6809WriteWord(usAddr, this.regY);
            this._flagnz16(this.regY);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void cmpdi() // 0x10A3: /* CMPD - indexed */
        {
            var usAddr = this.M6809PostByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.getRegD(), usTemp);
        }

        private void cmpyi() // 0x10AC: /* CMPY - indexed */
        {
            var usAddr = this.M6809PostByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.regY, usTemp);
        }

        private void ldyi() // 0x10AE: /* LDY - indexed */
        {
            var usAddr = this.M6809PostByte();
            this.regY = this.M6809ReadWord(usAddr);
            this._flagnz16(this.regY);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void styi() // 0x10AF: /* STY - indexed */
        {
            var usAddr = this.M6809PostByte();
            this.M6809WriteWord(usAddr, this.regY);
            this._flagnz16(this.regY);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void cmpde() // 0x10B3: /* CMPD - extended */
        {
            var usAddr = this.NextPCWord();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.getRegD(), usTemp);
        }

        private void cmpye() // 0x10BC: /* CMPY - extended */
        {
            var usAddr = this.NextPCWord();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.regY, usTemp);
        }

        private void ldye() // 0x10BE: /* LDY - extended */
        {
            var usAddr = this.NextPCWord();
            this.regY = this.M6809ReadWord(usAddr);
            this._flagnz16(this.regY);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void stye() // 0x10BF: /* STY - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteWord(usAddr, this.regY);
            this._flagnz16(this.regY);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void lds() // 0x10CE: /* LDS - immediate */
        {
            this.regS = this.NextPCWord();
            this._flagnz16(this.regS);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void ldsd() // 0x10DE: /* LDS - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.regS = this.M6809ReadWord(usAddr);
            this._flagnz16(this.regS);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void stsd() // 0x10DF: /* STS - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.M6809WriteWord(usAddr, this.regS);
            this._flagnz16(this.regS);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void ldsi() // 0x10EE: /* LDS - indexed */
        {
            var usAddr = this.M6809PostByte();
            this.regS = this.M6809ReadWord(usAddr);
            this._flagnz16(this.regS);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void stsi() // 0x10EF: /* STS - indexed */
        {
            var usAddr = this.M6809PostByte();
            this.M6809WriteWord(usAddr, this.regS);
            this._flagnz16(this.regS);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void ldse() // 0x10FE: /* LDS - extended */
        {
            var usAddr = this.NextPCWord();
            this.regS = this.M6809ReadWord(usAddr);
            this._flagnz16(this.regS);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void stse() // 0x10FF: /* STS - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteWord(usAddr, this.regS);
            this._flagnz16(this.regS);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void SWI3_Inherent() // 0x113F: /* SWI3 - inherent */
        {
            this.regCC |= 0x80; /* Set entire flag to indicate whole machine state on stack */
            this.M6809PUSHW(this.regPC);
            this.M6809PUSHW(this.regU);
            this.M6809PUSHW(this.regY);
            this.M6809PUSHW(this.regX);
            this.M6809PUSHB(this.regDP);
            this.M6809PUSHB(this.regA);
            this.M6809PUSHB(this.regB);
            this.M6809PUSHB(this.regCC);
            this._goto(this.M6809ReadWord(0xFFF2));
        }

        private void CMPU_Immediate() // 0x1183: /* CMPU - immediate */
        {
            var usTemp = this.NextPCWord();
            this._cmp16(this.regU, usTemp);
        }

        private void CMPS_Immediate() // 0x118C: /* CMPS - immediate */
        {
            var usTemp = this.NextPCWord();
            this._cmp16(this.regS, usTemp);
        }

        private void CMPU_Direct() // 0x1193: /* CMPU - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.regU, usTemp);
        }

        private void CMPS_Direct() // 0x119C: /* CMPS - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.regS, usTemp);
        }

        private void CMPU_Indexed() // 0x11A3: /* CMPU - indexed */
        {
            var usAddr = this.M6809PostByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.regU, usTemp);
        }

        private void CMPS_Indexed() // 0x11AC: /* CMPS - indexed */
        {
            var usAddr = this.M6809PostByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.regS, usTemp);
        }

        private void CMPU_Extended() // 0x11B3: /* CMPU - extended */
        {
            var usAddr = this.NextPCWord();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.regU, usTemp);
        }

        private void CMPS_Extended() // 0x11BC: /* CMPS - extended */
        {
            var usAddr = this.NextPCWord();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.regS, usTemp);
        }

        private void nop() { } // 0x12: /* NOP - inherent */

        private void sync() { } // 0x13: /* SYNC - inherent */

        private void lbra() // 0x16: /* LBRA - relative */
        {
            /* LBRA - relative jump */
            var sTemp = MakeSignedWord(this.NextPCWord());
            this.regPC += sTemp;
        }

        private void lbsr() // 0x17: /* LBSR - relative */
        {
            /* LBSR - relative call */
            var sTemp = MakeSignedWord(this.NextPCWord());
            this.M6809PUSHW(this.regPC);
            this.regPC += sTemp;
        }

        private void daa() // 0x19: /* DAA - inherent */
        {
            int cf = 0;
            int msn = this.regA & 0xf0;
            int lsn = this.regA & 0x0f;
            if (lsn > 0x09 || (this.regCC & 0x20) != 0) cf |= 0x06;
            if (msn > 0x80 && lsn > 0x09) cf |= 0x60;
            if (msn > 0x90 || (this.regCC & 0x01) != 0) cf |= 0x60;
            var usTemp = cf + this.regA;
            this.regCC &= ~(int)(F.CARRY | F.NEGATIVE | F.ZERO | F.OVERFLOW);
            if ((usTemp & 0x100) != 0)
                this.regCC |= (int)F.CARRY;
            this.regA = usTemp & 0xFF;
            this._flagnz(this.regA);
        }

        private void orcc() // 0x1A: /* ORCC - immediate */
        {
            this.regCC |= this.NextPCByte();
        }

        private void andcc() // 0x1C: /* ANDCC - immediate */
        {
            this.regCC &= this.NextPCByte();
        }

        private void sex() // 0x1D: /* SEX - inherent */
        {
            this.regA = ((this.regB & 0x80) != 0) ? 0xFF : 0x00;
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE);
            var d = this.getRegD();
            this._flagnz16(d);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void _setreg(string name, int value)
        {
            // console.log(name + '=' + value.toString(16));
            if (name == "D")
            {
                this.setRegD(value);
            }
            else
            {
                // JAR_NOTE: revisit why it's doing it this way ... 

                // this["reg" + name] = value;
            }
        }

        private void M6809TFREXG(int ucPostByte, bool bExchange)
        {
            var ucTemp = ucPostByte & 0x88;
            if (ucTemp == 0x80 || ucTemp == 0x08)
            {
                Console.WriteLine("**** M6809TFREXG problem...");
                ucTemp = 0; /* PROBLEM! */
            }
            string srname = "";
            int srcval = 0;
            switch (ucPostByte & 0xf0) /* Get source register */ {
                case 0x00: /* D */
                    srname = "D";
                    srcval = this.getRegD();
                    break;
                case 0x10: /* X */
                    srname = "X";
                    srcval = this.regX;
                    break;
                case 0x20: /* Y */
                    srname = "Y";
                    srcval = this.regY;
                    break;
                case 0x30: /* U */
                    srname = "U";
                    srcval = this.regU;
                    break;
                case 0x40: /* S */
                    srname = "S";
                    srcval = this.regS;
                    break;
                case 0x50: /* PC */
                    srname = "PC";
                    srcval = this.regPC;
                    break;
                case 0x80: /* A */
                    srname = "A";
                    srcval = this.regA;
                    break;
                case 0x90: /* B */
                    srname = "B";
                    srcval = this.regB;
                    break;
                case 0xA0: /* CC */
                    srname = "CC";
                    srcval = this.regCC;
                    break;
                case 0xB0: /* DP */
                    srname = "DP";
                    srcval = this.regDP;
                    break;
                default: /* Illegal */
                    Console.WriteLine("illegal src register in M6809TFREXG");
                    this.Halt();
                    break;
            }
            // console.log('EXG src: ' + srname + '=' + srcval.toString(16));
            switch (ucPostByte & 0xf) /* Get destination register */ {
                case 0x00: /* D */
                    // console.log('EXG dst: D=' + this.getRegD().toString(16));
                    if (bExchange)
                    {
                        this._setreg(srname, this.getRegD());
                    }
                    this.setRegD(srcval);
                    break;
                case 0x1: /* X */
                    // console.log('EXG dst: X=' + this.regX.toString(16));
                    if (bExchange)
                    {
                        this._setreg(srname, this.regX);
                    }
                    this.regX = srcval;
                    break;
                case 0x2: /* Y */
                    // console.log('EXG dst: Y=' + this.regY.toString(16));
                    if (bExchange)
                    {
                        this._setreg(srname, this.regY);
                    }
                    this.regY = srcval;
                    break;
                case 0x3: /* U */
                    // console.log('EXG dst: U=' + this.regU.toString(16));
                    if (bExchange)
                    {
                        this._setreg(srname, this.regU);
                    }
                    this.regU = srcval;
                    break;
                case 0x4: /* S */
                    // console.log('EXG dst: S=' + this.regS.toString(16));
                    if (bExchange)
                    {
                        this._setreg(srname, this.regS);
                    }
                    this.regS = srcval;
                    break;
                case 0x5: /* PC */
                    // console.log('EXG dst: PC=' + this.regPC.toString(16));
                    if (bExchange)
                    {
                        this._setreg(srname, this.regPC);
                    }
                    this._goto(srcval);
                    break;
                case 0x8: /* A */
                    // console.log('EXG dst: A=' + this.regA.toString(16));
                    if (bExchange)
                    {
                        this._setreg(srname, this.regA);
                    }
                    this.regA = 0xFF & srcval;
                    break;
                case 0x9: /* B */
                    // console.log('EXG dst: B=' + this.regB.toString(16));
                    if (bExchange)
                    {
                        this._setreg(srname, this.regB);
                    }
                    this.regB = 0xFF & srcval;
                    break;
                case 0xA: /* CC */
                    // console.log('EXG dst: CC=' + this.regCC.toString(16));
                    if (bExchange)
                    {
                        this._setreg(srname, this.regCC);
                    }
                    this.regCC = 0xFF & srcval;
                    break;
                case 0xB: /* DP */
                    // console.log('EXG dst: DP=' + this.regDP.toString(16));
                    if (bExchange)
                    {
                        this._setreg(srname, this.regDP);
                    }
                    this.regDP = srcval;
                    break;
                default: /* Illegal */
                    Console.WriteLine("illegal dst register in M6809TFREXG");
                    this.Halt();
                    break;
            }
        }

        private void exg() // 0x1E: /* EXG - immediate */
        {
            var ucTemp = this.NextPCByte(); /* Get postbyte */
            this.M6809TFREXG(ucTemp, true);
        }

        private void tfr() // 0x1F: /* TFR - immediate */
        {
            var ucTemp = this.NextPCByte(); /* Get postbyte */
            this.M6809TFREXG(ucTemp, false);
        }

        private void bra() // 0x20: /* BRA - relative */
        {
            var offset = MakeSignedByte(this.NextPCByte());
            this.regPC += offset;
        }

        private void brn() // 0x21: /* BRN - relative */
        {
            this.regPC++; // never.
        }

        private void bhi() // 0x22: /* BHI - relative */
        {
            var offset = MakeSignedByte(this.NextPCByte());
            if (!((this.regCC & (int)(F.CARRY | F.ZERO)) != 0))
                this.regPC += offset;
        }

        private void bls() // 0x23: /* BLS - relative */
        {
            var offset = MakeSignedByte(this.NextPCByte());
            if ((this.regCC & (int)(F.CARRY | F.ZERO)) != 0)
                this.regPC += offset;
        }

        private void branchIf(bool go)
        {
            var offset = MakeSignedByte(this.NextPCByte());
            if (go)
                this.regPC += offset;
        }

        private void branch(int flag, bool ifSet)
        {
            this.branchIf((this.regCC & flag) == (ifSet ? flag : 0));
        }

        private void bcc() // 0x24: /* BCC - relative */
        {
            this.branch((int)F.CARRY, false);
        }

        private void bcs() // 0x25: /* BCS - relative */
        {
            this.branch((int)F.CARRY, true);
        }

        private void bne() // 0x26: /* BNE - relative */
        {
            this.branch((int)F.ZERO, false);
        }

        private void beq() // 0x27: /* BEQ - relative */
        {
            this.branch((int)F.ZERO, true);
        }

        private void bvc() // 0x28: /* BVC - relative */
        {
            this.branch((int)F.OVERFLOW, false);
        }

        private void bvs() // 0x29: /* BVS - relative */
        {
            this.branch((int)F.OVERFLOW, true);
        }

        private void bpl() // 0x2A: /* BPL - relative */
        {
            this.branch((int)F.NEGATIVE, false);
        }

        private void bmi() // 0x2B: /* BMI - relative */
        {
            this.branch((int)F.NEGATIVE, true);
        }

        private void bge() // 0x2C: /* BGE - relative */
        {
            var go = !(((this.regCC & (int)F.NEGATIVE) ^ (this.regCC & (int)F.OVERFLOW) << 2) == 0);
            this.branchIf(go);
        }

        private void blt() // 0x2D: /* BLT - relative */
        {
            var go = (this.regCC & (int)F.NEGATIVE) ^ (this.regCC & (int)F.OVERFLOW) << 2;
            this.branchIf(go != 0);
        }

        private void bgt() // 0x2E: /* BGT - relative */
        {
            var bit = (this.regCC & (int)F.NEGATIVE) ^ (this.regCC & (int)F.OVERFLOW) << 2;
            var go = bit == 0 || (this.regCC & (int)F.ZERO) != 0;
            this.branchIf(go);
        }

        private void ble() // 0x2F: /* BLE - relative */
        {
            var bit = (this.regCC & (int)F.NEGATIVE) ^ (this.regCC & (int)F.OVERFLOW) << 2;
            var go = bit != 0 || (this.regCC & (int)F.ZERO) != 0;
            this.branchIf(go);
        }

        private void leax() // 0x30: /* LEAX - indexed */
        {
            this.regX = this.M6809PostByte();
            this.regCC &= ~(int)F.ZERO;
            if (this.regX == 0)
                this.regCC |= (int)F.ZERO;
        }

        private void leay() // 0x31: /* LEAY - indexed */
        {
            this.regY = this.M6809PostByte();
            this.regCC &= ~(int)F.ZERO;
            if (this.regY == 0)
                this.regCC |= (int)F.ZERO;
        }

        private void leas() // 0x32: /* LEAS - indexed */
        {
            this.regS = this.M6809PostByte();
        }

        private void leau() // 0x33: /* LEAU - indexed */
        {
            this.regU = this.M6809PostByte();
        }

        private void pshs() // 0x34: /* PSHS - immediate */
        {
            var ucTemp = this.NextPCByte(); /* Get the flags byte */
            this.M6809PSHS(ucTemp);
        }

        private void puls() // 0x35: /* PULS - immediate */
        {
            var ucTemp = this.NextPCByte(); /* Get the flags byte */
            this.M6809PULS(ucTemp);
        }

        private void pshu() // 0x36: /* PSHU - immediate */
        {
            var ucTemp = this.NextPCByte(); /* Get the flags byte */
            this.M6809PSHU(ucTemp);
        }

        private void pulu() // 0x37: /* PULU - immediate */
        {
            var ucTemp = this.NextPCByte(); /* Get the flags byte */
            this.M6809PULU(ucTemp);
        }

        private void rts() // 0x39: /* RTS - inherent */
        {
            this._goto(this.M6809PULLW());
        }

        private void abx() // 0x3A: /* ABX - inherent */
        {
            this.regX += this.regB;
        }

        private void rti() // 0x3B: /* RTI - inherent */
        {
            this.regCC = this.M6809PULLB();
            if ((this.regCC & 0x80) != 0) /* Entire machine state stacked? */
            {
                this.iClocks -= 9;
                this.regA = this.M6809PULLB();
                this.regB = this.M6809PULLB();
                this.regDP = this.M6809PULLB();
                this.regX = this.M6809PULLW();
                this.regY = this.M6809PULLW();
                this.regU = this.M6809PULLW();
            }
            this._goto(this.M6809PULLW());
        }

        private void cwai() // 0x3C: /* CWAI - immediate */
        {
            this.regCC &= this.NextPCByte();
        }

        private void mul() // 0x3D: /* MUL - inherent */
        {
            var usTemp = this.regA * this.regB;
            if (usTemp != 0)
                this.regCC &= ~(int)F.ZERO;
            else
                this.regCC |= (int)F.ZERO;
            if ((usTemp & 0x80) != 0)
                this.regCC |= (int)F.CARRY;
            else
                this.regCC &= ~(int)F.CARRY;
            this.setRegD(usTemp);
        }

        private void swi() // 0x3F: /* SWI - inherent */
        {
            this.regCC |= 0x80; /* Indicate whole machine state is stacked */
            this.M6809PUSHW(this.regPC);
            this.M6809PUSHW(this.regU);
            this.M6809PUSHW(this.regY);
            this.M6809PUSHW(this.regX);
            this.M6809PUSHB(this.regDP);
            this.M6809PUSHB(this.regB);
            this.M6809PUSHB(this.regA);
            this.M6809PUSHB(this.regCC);
            this.regCC |= 0x50; /* Disable further interrupts */
            this._goto(this.M6809ReadWord(0xFFFA));
        }

        private void nega() // 0x40: /* NEGA - inherent */
        {
            this.regA = this._neg(this.regA);
        }

        private void coma() // 0x43: /* COMA - inherent */
        {
            this.regA = this._com(this.regA);
        }

        private void lsra() // 0x44: /* LSRA - inherent */
        {
            this.regA = this._lsr(this.regA);
        }

        private void rora() // 0x46: /* RORA - inherent */
        {
            this.regA = this._ror(this.regA);
        }

        private void asra() // 0x47: /* ASRA - inherent */
        {
            this.regA = this._asr(this.regA);
        }

        private void asla() // 0x48: /* ASLA - inherent */
        {
            this.regA = this._asl(this.regA);
        }

        private void rola() // 0x49: /* ROLA - inherent */
        {
            this.regA = this._rol(this.regA);
        }

        private void deca() // 0x4A: /* DECA - inherent */
        {
            this.regA = this._dec(this.regA);
        }

        private void inca() // 0x4C: /* INCA - inherent */
        {
            this.regA = this._inc(this.regA);
        }

        private void tsta() // 0x4D: /* TSTA - inherent */
        {
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regA);
        }

        private void clra() // 0x4F: /* CLRA - inherent */
        {
            this.regA = 0;
            this.regCC &= ~(int)(F.NEGATIVE | F.OVERFLOW | F.CARRY);
            this.regCC |= (int)F.ZERO;
        }

        private void negb() // 0x50: /* NEGA - inherent */
        {
            this.regB = this._neg(this.regB);
        }

        private void comb() // 0x53: /* COMB - inherent */
        {
            this.regB = this._com(this.regB);
        }

        private void lsrb() // 0x54: /* LSRB - inherent */
        {
            this.regB = this._lsr(this.regB);
        }

        private void rorb() // 0x56: /* RORB - inherent */
        {
            this.regB = this._ror(this.regB);
        }

        private void asrb() // 0x57: /* ASRB - inherent */
        {
            this.regB = this._asr(this.regB);
        }

        private void aslb() // 0x58: /* ASLB - inherent */
        {
            this.regB = this._asl(this.regB);
        }

        private void rolb() // 0x59: /* ROLB - inherent */
        {
            this.regB = this._rol(this.regB);
        }

        private void decb() // 0x5A: /* DECB - inherent */
        {
            this.regB = this._dec(this.regB);
        }

        private void incb() // 0x5C: /* INCB - inherent */
        {
            this.regB = this._inc(this.regB);
        }

        private void tstb() // 0x5D: /* TSTB - inherent */
        {
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regB);
        }

        private void clrb() // 0x5F: /* CLRB - inherent */
        {
            this.regB = 0;
            this.regCC &= ~(int)(F.NEGATIVE | F.OVERFLOW | F.CARRY);
            this.regCC |= (int)F.ZERO;
        }

        private void NEG_Indexed() // 0x60: /* NEG - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.M6809WriteByte(usAddr, this._neg(ucTemp));
        }

        private void COM_Indexed() // 0x63: /* COM - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.M6809WriteByte(usAddr, this._com(ucTemp));
        }

        private void LSR_Indexed() // 0x64: /* LSR - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.M6809WriteByte(usAddr, this._lsr(ucTemp));
        }

        private void ROR_Indexed() // 0x66: /* ROR - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.M6809WriteByte(usAddr, this._ror(ucTemp));
        }

        private void ASR_Indexed() // 0x67: /* ASR - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.M6809WriteByte(usAddr, this._asr(ucTemp));
        }

        private void ASL_Indexed() // 0x68: /* ASL - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.M6809WriteByte(usAddr, this._asl(ucTemp));
        }

        private void ROL_Indexed() // 0x69: /* ROL - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.M6809WriteByte(usAddr, this._rol(ucTemp));
        }

        private void DEC_Indexed() // 0x6A: /* DEC - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.M6809WriteByte(usAddr, this._dec(ucTemp));
        }

        private void INC_Indexed() // 0x6C: /* INC - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.M6809WriteByte(usAddr, this._inc(ucTemp));
        }

        private void TST_Indexed() // 0x6D: /* TST - indexed */
        {
            var usAddr = this.M6809PostByte();
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            var val = this.M6809ReadByte(usAddr);
            this._flagnz(val);
        }

        private void JMP_Indexed() // 0x6E: /* JMP - indexed */
        {
            this._goto(this.M6809PostByte());
        }

        private void CLR_Indexed() // 0x6F: /* CLR - indexed */
        {
            var usAddr = this.M6809PostByte();
            this.M6809WriteByte(usAddr, 0);
            this.regCC &= ~(int)(F.OVERFLOW | F.CARRY | F.NEGATIVE);
            this.regCC |= (int)F.ZERO;
        }

        private void NEG_Extended() // 0x70: /* NEG - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteByte(usAddr, this._neg(this.M6809ReadByte(usAddr)));
        }

        private void COM_Extended() // 0x73: /* COM - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteByte(usAddr, this._com(this.M6809ReadByte(usAddr)));
        }

        private void LSR_Extended() // 0x74: /* LSR - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteByte(usAddr, this._lsr(this.M6809ReadByte(usAddr)));
        }

        private void ROR_Extended() // 0x76: /* ROR - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteByte(usAddr, this._ror(this.M6809ReadByte(usAddr)));
        }

        private void ASR_Extended() // 0x77: /* ASR - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteByte(usAddr, this._asr(this.M6809ReadByte(usAddr)));
        }

        private void asle() // 0x78: /* ASL - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteByte(usAddr, this._asl(this.M6809ReadByte(usAddr)));
        }

        private void role() // 0x79: /* ROL - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteByte(usAddr, this._rol(this.M6809ReadByte(usAddr)));
        }

        private void dece() // 0x7A: /* DEC - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteByte(usAddr, this._dec(this.M6809ReadByte(usAddr)));
        }

        private void ince() // 0x7C: /* INC - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteByte(usAddr, this._inc(this.M6809ReadByte(usAddr)));
        }

        private void tste() // 0x7D: /* TST - extended */
        {
            var usAddr = this.NextPCWord();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(ucTemp);
        }

        private void jmpe() // 0x7E: /* JMP - extended */
        {
            this._goto(this.M6809ReadWord(this.regPC));
        }

        private void clre() // 0x7F: /* CLR - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteByte(usAddr, 0);
            this.regCC &= ~(int)(F.CARRY | F.OVERFLOW | F.NEGATIVE);
            this.regCC |= (int)F.ZERO;
        }

        private void SUBA_Immediate() // 0x80: /* SUBA - immediate */
        {
            this.regA = this._sub(this.regA, this.NextPCByte());
        }

        private void CMPA_Immediate() // 0x81: /* CMPA - immediate */
        {
            var ucTemp = this.NextPCByte();
            this._cmp(this.regA, ucTemp);
        }

        private void SBCA_Immediate() // 0x82: /* SBCA - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regA = this._sbc(this.regA, ucTemp);
        }

        private void SUBD_Immediate() // 0x83: /* SUBD - immediate */
        {
            var usTemp = this.NextPCWord();
            this.setRegD(this._sub16(this.getRegD(), usTemp));
        }

        private void ANDA_Immediate() // 0x84: /* ANDA - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regA = this._and(this.regA, ucTemp);
        }

        private void BITA_Immediate() // 0x85: /* BITA - immediate */
        {
            var ucTemp = this.NextPCByte();
            this._and(this.regA, ucTemp);
        }

        private void LDA_Immediate() // 0x86: /* LDA - immediate */
        {
            this.regA = this.NextPCByte();
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regA);
        }

        private void EORA_Immediate() // 0x88: /* EORA - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regA = this._eor(this.regA, ucTemp);
        }

        private void ADCA_Immediate() // 0x89: /* ADCA - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regA = this._adc(this.regA, ucTemp);
        }

        private void ORA_Immediate() // 0x8A: /* ORA - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regA = this._or(this.regA, ucTemp);
        }

        private void ADDA_Immediate() // 0x8B: /* ADDA - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regA = this._add(this.regA, ucTemp);
        }

        private void CMPX_Immediate() // 0x8C: /* CMPX - immediate */
        {
            var usTemp = this.NextPCWord();
            this._cmp16(this.regX, usTemp);
        }

        private void bsr() // 0x8D: /* BSR */
        {
            var sTemp = MakeSignedByte(this.NextPCByte());
            this.M6809PUSHW(this.regPC);
            this.regPC += sTemp;
        }

        private void LDX_Immediate() // 0x8E: /* LDX - immediate */
        {
            var usTemp = this.NextPCWord();
            this.regX = usTemp;
            this._flagnz16(usTemp);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void SUBA_Direct() // 0x90: /* SUBA - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._sub(this.regA, ucTemp);
        }

        private void CMPA_Direct() // 0x91: /* CMPA - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this._cmp(this.regA, ucTemp);
        }

        private void SBCA_Direct() // 0x92: /* SBCA - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._sbc(this.regA, ucTemp);
        }

        private void SUBD_Direct() // 0x93: /* SUBD - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this.setRegD(this._sub16(this.getRegD(), usTemp));
        }

        private void ANDA_Direct() // 0x94: /* ANDA - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._and(this.regA, ucTemp);
        }

        private void BITA_Direct() // 0x95: /* BITA - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this._and(this.regA, ucTemp);
        }

        private void LDA_Direct() // 0x96: /* LDA - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.regA = this.M6809ReadByte(usAddr);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regA);
        }

        private void STA_Direct() // 0x97: /* STA - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.M6809WriteByte(usAddr, this.regA);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regA);
        }

        private void EORA_Direct() // 0x98: /* EORA - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._eor(this.regA, ucTemp);
        }

        private void ADCA_Direct() // 0x99: /* ADCA - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._or(this.regA, ucTemp);
        }

        private void ORA_Direct() // 0x9A: /* ORA - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._or(this.regA, ucTemp);
        }

        private void ADDA_Direct() // 0x9B: /* ADDA - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._add(this.regA, ucTemp);
        }

        private void CMPX_Direct() // 0x9C: /* CMPX - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.regX, usTemp);
        }

        private void JSR_Direct() // 0x9D: /* JSR - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.M6809PUSHW(this.regPC);
            this._goto(usAddr);
        }

        private void LDX_Direct() // 0x9E: /* LDX - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.regX = this.M6809ReadWord(usAddr);
            this._flagnz16(this.regX);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void STX_Direct() // 0x9F: /* STX - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.M6809WriteWord(usAddr, this.regX);
            this._flagnz16(this.regX);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void SUBA_Indexed() // 0xA0: /* SUBA - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._sub(this.regA, ucTemp);
        }

        private void CMPA_Indexed() // 0xA1: /* CMPA - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this._cmp(this.regA, ucTemp);
        }

        private void SBCA_Indexed() // 0xA2: /* SBCA - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._sbc(this.regA, ucTemp);
        }

        private void SUBD_Indexed() // 0xA3: /* SUBD - indexed */
        {
            var usAddr = this.M6809PostByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this.setRegD(this._sub16(this.getRegD(), usTemp));
        }

        private void ANDA_Indexed() // 0xA4: /* ANDA - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._and(this.regA, ucTemp);
        }

        private void BITA_Indexed() // 0xA5: /* BITA - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this._and(this.regA, ucTemp);
        }

        private void LDA_Indexed() // 0xA6: /* LDA - indexed */
        {
            var usAddr = this.M6809PostByte();
            this.regA = this.M6809ReadByte(usAddr);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regA);
        }

        private void STA_Indexed() // 0xA7: /* STA - indexed */
        {
            var usAddr = this.M6809PostByte();
            this.M6809WriteByte(usAddr, this.regA);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regA);
        }

        private void EORA_Indexed() // 0xA8: /* EORA - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._eor(this.regA, ucTemp);
        }

        private void ADCA_Indexed() // 0xA9: /* ADCA - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._adc(this.regA, ucTemp);
        }

        private void ORA_Indexed() // 0xAA: /* ORA - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._or(this.regA, ucTemp);
        }

        private void ADDA_Indexed() // 0xAB: /* ADDA - indexed */
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regA = this._add(this.regA, ucTemp);
        }

        private void CMPX_Indexed() // 0xAC: /* CMPX - indexed */
        {
            var usAddr = this.M6809PostByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this._cmp16(this.regX, usTemp);
        }

        private void JSR_Indexed() // 0xAD: /* JSR - indexed */
        {
            var usAddr = this.M6809PostByte();
            this.M6809PUSHW(this.regPC);
            this._goto(usAddr);
        }

        private void LDX_Indexed() // 0xAE: /* LDX - indexed */
        {
            var usAddr = this.M6809PostByte();
            this.regX = this.M6809ReadWord(usAddr);
            this._flagnz16(this.regX);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void STX_Indexed() // 0xAF: /* STX - indexed */
        {
            var usAddr = this.M6809PostByte();
            this.M6809WriteWord(usAddr, this.regX);
            this._flagnz16(this.regX);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void SUBA_Extended() // 0xB0: /* SUBA - extended */
        {
            var usAddr = this.NextPCWord();
            this.regA = this._sub(this.regA, this.M6809ReadByte(usAddr));
        }

        private void CMPA_Extended() // 0xB1: /* CMPA - extended */
        {
            var usAddr = this.NextPCWord();
            this._cmp(this.regA, this.M6809ReadByte(usAddr));
        }

        private void SBCA_Extended() // 0xB2: /* SBCA - extended */
        {
            var usAddr = this.NextPCWord();
            this.regA = this._sbc(this.regA, this.M6809ReadByte(usAddr));
        }

        private void SUBD_Extended() // 0xB3: /* SUBD - extended */
        {
            var usAddr = this.NextPCWord();
            this.setRegD(this._sub16(this.getRegD(), this.M6809ReadWord(usAddr)));
        }

        private void ANDA_Extended() // 0xB4: /* ANDA - extended */
        {
            var usAddr = this.NextPCWord();
            this.regA = this._and(this.regA, this.M6809ReadByte(usAddr));
        }

        private void BITA_Extended() // 0xB5: /* BITA - extended */
        {
            var usAddr = this.NextPCWord();
            this._and(this.regA, this.M6809ReadByte(usAddr));
        }

        private void LDA_Extended() // 0xB6: /* LDA - extended */
        {
            var usAddr = this.NextPCWord();
            this.regA = this.M6809ReadByte(usAddr);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regA);
        }

        private void STA_Extended() // 0xB7: /* STA - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteByte(usAddr, this.regA);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regA);
        }

        private void EORA_Extended() // 0xB8: /* EORA - extended */
        {
            var usAddr = this.NextPCWord();
            this.regA = this._eor(this.regA, this.M6809ReadByte(usAddr));
        }

        private void ADCA_Extended() // 0xB9: /* ADCA - extended */
        {
            var usAddr = this.NextPCWord();
            this.regA = this._adc(this.regA, this.M6809ReadByte(usAddr));
        }

        private void ORA_Extended() // 0xBA: /* ORA - extended */
        {
            var usAddr = this.NextPCWord();
            this.regA = this._or(this.regA, this.M6809ReadByte(usAddr));
        }

        private void ADDA_Extended() // 0xBB: /* ADDA - extended */
        {
            var usAddr = this.NextPCWord();
            this.regA = this._add(this.regA, this.M6809ReadByte(usAddr));
        }

        private void CMPX_Extended() // 0xBC: /* CMPX - extended */
        {
            var usAddr = this.NextPCWord();
            this._cmp16(this.regX, this.M6809ReadWord(usAddr));
        }

        private void JSR_Extended() // 0xBD: /* JSR - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809PUSHW(this.regPC);
            this._goto(usAddr);
        }

        private void LDX_Extended() // 0xBE: /* LDX - extended */
        {
            var usAddr = this.NextPCWord();
            this.regX = this.M6809ReadWord(usAddr);
            this._flagnz16(this.regX);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void STX_Extended() // 0xBF: /* STX - extended */
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteWord(usAddr, this.regX);
            this._flagnz16(this.regX);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void SUBB_Immediate() // 0xC0: /* SUBB - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regB = this._sub(this.regB, ucTemp);
        }

        private void CMPB_Immediate() // 0xC1: /* CMPB - immediate */
        {
            var ucTemp = this.NextPCByte();
            this._cmp(this.regB, ucTemp);
        }

        private void SBCB_Immediate() // 0xC2: /* SBCB - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regB = this._sbc(this.regB, ucTemp);
        }

        private void ADDD_Immediate() // 0xC3: /* ADDD - immediate */
        {
            var usTemp = this.NextPCWord();
            this.setRegD(this._add16(this.getRegD(), usTemp));
        }

        private void ANDB_Immediate() // 0xC4: /* ANDB - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regB = this._and(this.regB, ucTemp);
        }

        private void BITB_Immediate() // 0xC5: /* BITB - immediate */
        {
            var ucTemp = this.NextPCByte();
            this._and(this.regB, ucTemp);
        }

        private void LDB_Immediate() // 0xC6: /* LDB - immediate */
        {
            this.regB = this.NextPCByte();
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regB);
        }

        private void EORB_Immediate() // 0xC8: /* EORB - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regB = this._eor(this.regB, ucTemp);
        }

        private void ADCB_Immediate() // 0xC9: /* ADCB - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regB = this._adc(this.regB, ucTemp);
        }

        private void ORB_Immediate() // 0xCA: /* ORB - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regB = this._or(this.regB, ucTemp);
        }

        private void ADDB_Immediate() // 0xCB: /* ADDB - immediate */
        {
            var ucTemp = this.NextPCByte();
            this.regB = this._add(this.regB, ucTemp);
        }

        private void LDD_Immediate() // 0xCC: /* LDD - immediate */
        {
            var d = this.NextPCWord();
            this.setRegD(d);
            this._flagnz16(d);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void LDU_Immediate() // 0xCE: /* LDU - immediate */
        {
            this.regU = this.NextPCWord();
            this._flagnz16(this.regU);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void SUBB_Direct() // 0xD0: /* SUBB - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._sub(this.regB, ucTemp);
        }

        private void CMPB_Direct() // 0xD1: /* CMPB - direct */
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this._cmp(this.regB, ucTemp);
        }

        private void SBCB_Direct() // 0xD2: /* SBCB - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._sbc(this.regB, ucTemp);
        }

        private void ADDD_Direct() // 0xD3: /* ADDD - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this.setRegD(this._add16(this.getRegD(), usTemp));
        }

        private void ANDB_Direct() // 0xD4: /* ANDB - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._and(this.regB, ucTemp);
        }

        private void BITB_Direct() // 0xD5: /* BITB - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this._and(this.regB, ucTemp);
        }

        private void LDB_Direct() // 0xD6: /* LDB - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.regB = this.M6809ReadByte(usAddr);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regB);
        }

        private void STB_Direct() // 0xD7: /* STB - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.M6809WriteByte(usAddr, this.regB);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regB);
        }

        private void EORB_Direct() // 0xD8: /* EORB - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._eor(this.regB, ucTemp);
        }

        private void ADCB_Direct() // 0xD9: /* ADCB - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._adc(this.regB, ucTemp);
        }

        private void ORB_Direct() // 0xDA: /* ORB - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._or(this.regB, ucTemp);
        }

        private void ADDB_Direct() // 0xDB: /* ADDB - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._add(this.regB, ucTemp);
        }

        private void LDD_Direct() // 0xDC: /* LDD - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var d = this.M6809ReadWord(usAddr);
            this.setRegD(d);
            this._flagnz16(d);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void STD_Direct() // 0xDD: /* STD - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            var d = this.getRegD();
            this.M6809WriteWord(usAddr, d);
            this._flagnz16(d);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void LDU_Direct() // 0xDE: /* LDU - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.regU = this.M6809ReadWord(usAddr);
            this._flagnz16(this.regU);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void STU_Direct() // 0xDF: /* STU - direct */ 
        {
            var usAddr = this.regDP * 256 + this.NextPCByte();
            this.M6809WriteWord(usAddr, this.regU);
            this._flagnz16(this.regU);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void SUBB_Indexed() // 0xE0: /* SUBB - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._sub(this.regB, ucTemp);
        }

        private void CMPB_Indexed() // 0xE1: /* CMPB - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this._cmp(this.regB, ucTemp);
        }

        private void SBCD_Indexed() // 0xE2: /* SBCB - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._sbc(this.regB, ucTemp);
        }

        private void ADDD_Indexed() // 0xE3: /* ADDD - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            var usTemp = this.M6809ReadWord(usAddr);
            this.setRegD(this._add16(this.getRegD(), usTemp));
        }

        private void ANDB_Indexed() // 0xE4: /* ANDB - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._and(this.regB, ucTemp);
        }

        private void BITB_Indexed() // 0xE5: /* BITB - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this._and(this.regB, ucTemp);
        }

        private void LDB_Indexed() // 0xE6: /* LDB - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            this.regB = this.M6809ReadByte(usAddr);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regB);
        }

        private void STB_Indexed() // 0xE7: /* STB - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            this.M6809WriteByte(usAddr, this.regB);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regB);
        } 

        private void EORB_Indexed() // 0xE8: /* EORB - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._eor(this.regB, ucTemp);
        }

        private void ADCB_Indexed() // 0xE9: /* ADCB - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._adc(this.regB, ucTemp);
        }

        private void ORB_Indexed() // 0xEA: /* ORB - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._or(this.regB, ucTemp);
        }

        private void ADDB_Indexed() // 0xEB: /* ADDB - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._add(this.regB, ucTemp);
        }

        private void LDD_Indexed() // 0xEC: /* LDD - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            var d = this.M6809ReadWord(usAddr);
            this.setRegD(d);
            this._flagnz16(d);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void STD_Indexed() // 0xED: /* STD - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            var d = this.getRegD();
            this.M6809WriteWord(usAddr, d);
            this._flagnz16(d);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void LDU_Indexed() // 0xEE: /* LDU - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            this.regU = this.M6809ReadWord(usAddr);
            this._flagnz16(this.regU);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void STU_Indexed() // 0xEF: /* STU - indexed */ 
        {
            var usAddr = this.M6809PostByte();
            this.M6809WriteWord(usAddr, this.regU);
            this._flagnz16(this.regU);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void SUBB_Extended() // 0xF0: /* SUBB - extended */ 
        {
            var usAddr = this.NextPCWord();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._sub(this.regB, ucTemp);
        }

        private void CMPB_Extended() // 0xF1: /* CMPB - extended */ 
        {
            var usAddr = this.NextPCWord();
            var ucTemp = this.M6809ReadByte(usAddr);
            this._cmp(this.regB, ucTemp);
        }

        private void SBCB_Extended() // 0xF2: /* SBCB - extended */ 
        {
            var usAddr = this.NextPCWord();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._sbc(this.regB, ucTemp);
        }

        private void ADDD_Extended() // 0xF3: /* ADDD - extended */ 
        {
            var usAddr = this.NextPCWord();
            var usTemp = this.M6809ReadWord(usAddr);
            this.setRegD(this._add16(this.getRegD(), usTemp));
        }

        private void ANDB_Extended() // 0xF4: /* ANDB - extended */ 
        {
            var usAddr = this.NextPCWord();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._and(this.regB, ucTemp);
        }

        private void BITB_Extended() // 0xF5: /* BITB - extended */ 
        {
            var usAddr = this.NextPCWord();
            var ucTemp = this.M6809ReadByte(usAddr);
            this._and(this.regB, ucTemp);
        }

        private void LDB_Extended() // 0xF6: /* LDB - extended */ 
        {
            var usAddr = this.NextPCWord();
            this.regB = this.M6809ReadByte(usAddr);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regB);
        }

        private void STB_Extended() // 0xF7: /* STB - extended */ 
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteByte(usAddr, this.regB);
            this.regCC &= ~(int)(F.ZERO | F.NEGATIVE | F.OVERFLOW);
            this._flagnz(this.regB);
        }

        private void EORB_Extended() // 0xF8: /* EORB - extended */ 
        {
            var usAddr = this.NextPCWord();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._eor(this.regB, ucTemp);
        }

        private void ADCB_Extended() // 0xF9: /* ADCB - extended */ 
        {
            var usAddr = this.NextPCWord();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._adc(this.regB, ucTemp);
        }

        private void ORB_Extended() // 0xFA: /* ORB - extended */ 
        {
            var usAddr = this.NextPCWord();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._or(this.regB, ucTemp);
        }

        private void ADDB_Extended() // 0xFB: /* ADDB - extended */ 
        {
            var usAddr = this.NextPCWord();
            var ucTemp = this.M6809ReadByte(usAddr);
            this.regB = this._add(this.regB, ucTemp);
        }

        private void LDD_Extended() // 0xFC: /* LDD - extended */ 
        {
            var usAddr = this.NextPCWord();
            var val = this.M6809ReadWord(usAddr);
            this.setRegD(val);
            this._flagnz16(val);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void STD_Extended() // 0xFD: /* STD - extended */ 
        {
            var usAddr = this.NextPCWord();
            var d = this.getRegD();
            this.M6809WriteWord(usAddr, d);
            this._flagnz16(d);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void LDU_Extended() // 0xFE: /* LDU - extended */ 
        {
            var usAddr = this.NextPCWord();
            this.regU = this.M6809ReadWord(usAddr);
            this._flagnz16(this.regU);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void STU_Extended() // 0xFF: /* STU - extended */ 
        {
            var usAddr = this.NextPCWord();
            this.M6809WriteWord(usAddr, this.regU);
            this._flagnz16(this.regU);
            this.regCC &= ~(int)F.OVERFLOW;
        }

        private void Illegal() 
        {
        }

        private void Illegal10() 
        {
        }

        private void Illegal11() 
        {
        }

        private void Page10() 
        {
            var op = this.NextPCByte(); /* Second half of opcode */
            this.iClocks -= _mc6809Cycles2[op]; /* Subtract execution time */
            if (this._debug)
                Console.WriteLine((this.regPC - 1).ToString("X4") + ": " + this._mnemonics10[op]);
            var instruction = this._instructions10[op];
            if (instruction == null)
            {
                Console.WriteLine("*** illegal p10 opcode: " + op.ToString("X4") + " at " + (this.regPC - 1).ToString("X4"));
                this.Halt();
            }
            else
            {
                instruction();
            }
        }

        private void Page11() 
        {
            var op = this.NextPCByte(); /* Second half of opcode */
            this.iClocks -= _mc6809Cycles2[op]; /* Subtract execution time */
            if (this._debug)
                Console.WriteLine((this.regPC - 1).ToString("X4") + ": " + this._mnemonics11[op]);
            var instruction = this._instructions11[op];
            if (instruction == null)
            {
                Console.WriteLine("*** illegal p11 opcode: " + op.ToString("X4"));
                this.Halt();
            }
            else
            {
                instruction();
            }
        }

        private delegate void InstrDelegate();
        private InstrDelegate[] _instructions = new InstrDelegate[255];
        private InstrDelegate[] _instructions10 = new InstrDelegate[255];
        private InstrDelegate[] _instructions11 = new InstrDelegate[255];

        private void Init_Instructions() 
        {
            _instructions[0x00] = new InstrDelegate(this.NEG_Direct);
            _instructions[0x01] = new InstrDelegate(this.Illegal);
            _instructions[0x02] = new InstrDelegate(this.Illegal);
            _instructions[0x03] = new InstrDelegate(this.COM_Direct);
            _instructions[0x04] = new InstrDelegate(this.LSR_Direct);
            _instructions[0x05] = new InstrDelegate(this.Illegal);
            _instructions[0x06] = new InstrDelegate(this.ROR_Direct);
            _instructions[0x07] = new InstrDelegate(this.ASR_Direct);
            _instructions[0x08] = new InstrDelegate(this.ASL_Direct);
            _instructions[0x09] = new InstrDelegate(this.ROL_Direct);
            _instructions[0x0A] = new InstrDelegate(this.DEC_Direct);
            _instructions[0x0B] = new InstrDelegate(this.Illegal);
            _instructions[0x0C] = new InstrDelegate(this.INC_Direct);
            _instructions[0x0D] = new InstrDelegate(this.TST_Direct);
            _instructions[0x0E] = new InstrDelegate(this.JMP_Direct);
            _instructions[0x0F] = new InstrDelegate(this.CLR_Direct);

            _instructions[0x10] = new InstrDelegate(this.Page10);
            _instructions[0x11] = new InstrDelegate(this.Page11);
            _instructions[0x12] = new InstrDelegate(this.nop);
            _instructions[0x13] = new InstrDelegate(this.sync);
            _instructions[0x14] = new InstrDelegate(this.Illegal);
            _instructions[0x15] = new InstrDelegate(this.Illegal);
            _instructions[0x16] = new InstrDelegate(this.lbra);
            _instructions[0x17] = new InstrDelegate(this.lbsr);
            _instructions[0x18] = new InstrDelegate(this.Illegal);
            _instructions[0x19] = new InstrDelegate(this.daa);
            _instructions[0x1A] = new InstrDelegate(this.orcc);
            _instructions[0x1B] = new InstrDelegate(this.Illegal);
            _instructions[0x1C] = new InstrDelegate(this.andcc);
            _instructions[0x1D] = new InstrDelegate(this.sex);
            _instructions[0x1E] = new InstrDelegate(this.exg);
            _instructions[0x1F] = new InstrDelegate(this.tfr);

            _instructions[0x20] = new InstrDelegate(this.bra);
            _instructions[0x21] = new InstrDelegate(this.brn);
            _instructions[0x22] = new InstrDelegate(this.bhi);
            _instructions[0x23] = new InstrDelegate(this.bls);
            _instructions[0x24] = new InstrDelegate(this.bcc);
            _instructions[0x25] = new InstrDelegate(this.bcs);
            _instructions[0x26] = new InstrDelegate(this.bne);
            _instructions[0x27] = new InstrDelegate(this.beq);
            _instructions[0x28] = new InstrDelegate(this.bvc);
            _instructions[0x29] = new InstrDelegate(this.bvs);
            _instructions[0x2A] = new InstrDelegate(this.bpl);
            _instructions[0x2B] = new InstrDelegate(this.bmi);
            _instructions[0x2C] = new InstrDelegate(this.bge);
            _instructions[0x2D] = new InstrDelegate(this.blt);
            _instructions[0x2E] = new InstrDelegate(this.bgt);
            _instructions[0x2F] = new InstrDelegate(this.ble);

            _instructions[0x30] = new InstrDelegate(this.leax);
            _instructions[0x31] = new InstrDelegate(this.leay);
            _instructions[0x32] = new InstrDelegate(this.leas);
            _instructions[0x33] = new InstrDelegate(this.leau);
            _instructions[0x34] = new InstrDelegate(this.pshs);
            _instructions[0x35] = new InstrDelegate(this.puls);
            _instructions[0x36] = new InstrDelegate(this.pshu);
            _instructions[0x37] = new InstrDelegate(this.pulu);
            _instructions[0x38] = new InstrDelegate(this.Illegal);
            _instructions[0x39] = new InstrDelegate(this.rts);
            _instructions[0x3A] = new InstrDelegate(this.abx);
            _instructions[0x3B] = new InstrDelegate(this.rti);
            _instructions[0x3C] = new InstrDelegate(this.cwai);
            _instructions[0x3D] = new InstrDelegate(this.mul);
            _instructions[0x3E] = new InstrDelegate(this.Illegal);
            _instructions[0x3F] = new InstrDelegate(this.swi);

            _instructions[0x40] = new InstrDelegate(this.nega);
            _instructions[0x41] = new InstrDelegate(this.Illegal);
            _instructions[0x42] = new InstrDelegate(this.Illegal);
            _instructions[0x43] = new InstrDelegate(this.coma);
            _instructions[0x44] = new InstrDelegate(this.lsra);
            _instructions[0x45] = new InstrDelegate(this.Illegal);
            _instructions[0x46] = new InstrDelegate(this.rora);
            _instructions[0x47] = new InstrDelegate(this.asra);
            _instructions[0x48] = new InstrDelegate(this.asla);
            _instructions[0x49] = new InstrDelegate(this.rola);
            _instructions[0x4A] = new InstrDelegate(this.deca);
            _instructions[0x4B] = new InstrDelegate(this.Illegal);
            _instructions[0x4C] = new InstrDelegate(this.inca);
            _instructions[0x4D] = new InstrDelegate(this.tsta);
            _instructions[0x4E] = new InstrDelegate(this.Illegal);
            _instructions[0x4F] = new InstrDelegate(this.clra);

            _instructions[0X50] = new InstrDelegate(this.negb);
            _instructions[0x51] = new InstrDelegate(this.Illegal);
            _instructions[0x52] = new InstrDelegate(this.Illegal);
            _instructions[0x53] = new InstrDelegate(this.comb);
            _instructions[0x54] = new InstrDelegate(this.lsrb);
            _instructions[0x55] = new InstrDelegate(this.Illegal);
            _instructions[0x56] = new InstrDelegate(this.rorb);
            _instructions[0x57] = new InstrDelegate(this.asrb);
            _instructions[0x58] = new InstrDelegate(this.aslb);
            _instructions[0x59] = new InstrDelegate(this.rolb);
            _instructions[0x5A] = new InstrDelegate(this.decb);
            _instructions[0x5B] = new InstrDelegate(this.Illegal);
            _instructions[0x5C] = new InstrDelegate(this.incb);
            _instructions[0x5D] = new InstrDelegate(this.tstb);
            _instructions[0x5E] = new InstrDelegate(this.Illegal);
            _instructions[0x5F] = new InstrDelegate(this.clrb);

            _instructions[0x60] = new InstrDelegate(this.NEG_Indexed);
            _instructions[0x61] = new InstrDelegate(this.Illegal);
            _instructions[0x62] = new InstrDelegate(this.Illegal);
            _instructions[0x63] = new InstrDelegate(this.COM_Indexed);
            _instructions[0x64] = new InstrDelegate(this.LSR_Indexed);
            _instructions[0x65] = new InstrDelegate(this.Illegal);
            _instructions[0x66] = new InstrDelegate(this.ROR_Indexed);
            _instructions[0x67] = new InstrDelegate(this.ASR_Indexed);
            _instructions[0x68] = new InstrDelegate(this.ASL_Indexed);
            _instructions[0x69] = new InstrDelegate(this.ROL_Indexed);
            _instructions[0x6A] = new InstrDelegate(this.DEC_Indexed);
            _instructions[0x6B] = new InstrDelegate(this.Illegal);
            _instructions[0x6C] = new InstrDelegate(this.INC_Indexed);
            _instructions[0x6D] = new InstrDelegate(this.TST_Indexed);
            _instructions[0x6E] = new InstrDelegate(this.JMP_Indexed);
            _instructions[0x6F] = new InstrDelegate(this.CLR_Indexed);

            _instructions[0x70] = new InstrDelegate(this.NEG_Extended);
            _instructions[0x71] = new InstrDelegate(this.Illegal);
            _instructions[0x72] = new InstrDelegate(this.Illegal);
            _instructions[0x73] = new InstrDelegate(this.COM_Extended);
            _instructions[0x74] = new InstrDelegate(this.LSR_Extended);
            _instructions[0x75] = new InstrDelegate(this.Illegal);
            _instructions[0x76] = new InstrDelegate(this.ROR_Extended);
            _instructions[0x77] = new InstrDelegate(this.ASR_Extended);
            _instructions[0x78] = new InstrDelegate(this.asle);
            _instructions[0x79] = new InstrDelegate(this.role);
            _instructions[0x7A] = new InstrDelegate(this.dece);
            _instructions[0x7B] = new InstrDelegate(this.Illegal);
            _instructions[0x7C] = new InstrDelegate(this.ince);
            _instructions[0x7D] = new InstrDelegate(this.tste);
            _instructions[0x7E] = new InstrDelegate(this.jmpe);
            _instructions[0x7F] = new InstrDelegate(this.clre);

            _instructions[0x80] = new InstrDelegate(this.SUBA_Immediate);
            _instructions[0x81] = new InstrDelegate(this.CMPA_Immediate);
            _instructions[0x82] = new InstrDelegate(this.SBCA_Immediate);
            _instructions[0x83] = new InstrDelegate(this.SUBD_Immediate);
            _instructions[0x84] = new InstrDelegate(this.ANDA_Immediate);
            _instructions[0x85] = new InstrDelegate(this.BITA_Immediate);
            _instructions[0x86] = new InstrDelegate(this.LDA_Immediate);
            _instructions[0x87] = new InstrDelegate(this.Illegal);
            _instructions[0x88] = new InstrDelegate(this.EORA_Immediate);
            _instructions[0x89] = new InstrDelegate(this.ADCA_Immediate);
            _instructions[0x8A] = new InstrDelegate(this.ORA_Immediate);
            _instructions[0x8B] = new InstrDelegate(this.ADDA_Immediate);
            _instructions[0x8C] = new InstrDelegate(this.CMPX_Immediate);
            _instructions[0x8D] = new InstrDelegate(this.bsr);
            _instructions[0x8E] = new InstrDelegate(this.LDX_Immediate);
            _instructions[0x8F] = new InstrDelegate(this.Illegal);

            _instructions[0x90] = new InstrDelegate(this.SUBA_Direct);
            _instructions[0x91] = new InstrDelegate(this.CMPA_Direct);
            _instructions[0x92] = new InstrDelegate(this.SBCA_Direct);
            _instructions[0x93] = new InstrDelegate(this.SUBD_Direct);
            _instructions[0x94] = new InstrDelegate(this.ANDA_Direct);
            _instructions[0x95] = new InstrDelegate(this.BITA_Direct);
            _instructions[0x96] = new InstrDelegate(this.LDA_Direct);
            _instructions[0x97] = new InstrDelegate(this.STA_Direct);
            _instructions[0x98] = new InstrDelegate(this.EORA_Direct);
            _instructions[0x99] = new InstrDelegate(this.ADCA_Direct);
            _instructions[0x9A] = new InstrDelegate(this.ORA_Direct);
            _instructions[0x9B] = new InstrDelegate(this.ADDA_Direct);
            _instructions[0x9C] = new InstrDelegate(this.CMPX_Direct);
            _instructions[0x9D] = new InstrDelegate(this.JSR_Direct);
            _instructions[0x9E] = new InstrDelegate(this.LDX_Direct);
            _instructions[0x9F] = new InstrDelegate(this.STX_Direct);

            _instructions[0xA0] = new InstrDelegate(this.SUBA_Indexed);
            _instructions[0xA1] = new InstrDelegate(this.CMPA_Indexed);
            _instructions[0xA2] = new InstrDelegate(this.SBCA_Indexed);
            _instructions[0xA3] = new InstrDelegate(this.SUBD_Indexed);
            _instructions[0xA4] = new InstrDelegate(this.ANDA_Indexed);
            _instructions[0xA5] = new InstrDelegate(this.BITA_Indexed);
            _instructions[0xA6] = new InstrDelegate(this.LDA_Indexed);
            _instructions[0xA7] = new InstrDelegate(this.STA_Indexed);
            _instructions[0xA8] = new InstrDelegate(this.EORA_Indexed);
            _instructions[0xA9] = new InstrDelegate(this.ADCA_Indexed);
            _instructions[0xAA] = new InstrDelegate(this.ORA_Indexed);
            _instructions[0xAB] = new InstrDelegate(this.ADDA_Indexed);
            _instructions[0xAC] = new InstrDelegate(this.CMPX_Indexed);
            _instructions[0xAD] = new InstrDelegate(this.JSR_Indexed);
            _instructions[0xAE] = new InstrDelegate(this.LDX_Indexed);
            _instructions[0xAF] = new InstrDelegate(this.STX_Indexed);

            _instructions[0xB0] = new InstrDelegate(this.SUBA_Extended);
            _instructions[0xB1] = new InstrDelegate(this.CMPA_Extended);
            _instructions[0xB2] = new InstrDelegate(this.SBCA_Extended);
            _instructions[0xB3] = new InstrDelegate(this.SUBD_Extended);
            _instructions[0xB4] = new InstrDelegate(this.ANDA_Extended);
            _instructions[0xB5] = new InstrDelegate(this.BITA_Extended);
            _instructions[0xB6] = new InstrDelegate(this.LDA_Extended);
            _instructions[0xB7] = new InstrDelegate(this.STA_Extended);
            _instructions[0xB8] = new InstrDelegate(this.EORA_Extended);
            _instructions[0xB9] = new InstrDelegate(this.ADCA_Extended);
            _instructions[0xBA] = new InstrDelegate(this.ORA_Extended);
            _instructions[0xBB] = new InstrDelegate(this.ADDA_Extended);
            _instructions[0xBC] = new InstrDelegate(this.CMPX_Extended);
            _instructions[0xBD] = new InstrDelegate(this.JSR_Extended);
            _instructions[0xBE] = new InstrDelegate(this.LDX_Extended);
            _instructions[0xBF] = new InstrDelegate(this.STX_Extended);

            _instructions[0xC0] = new InstrDelegate(this.SUBB_Immediate);
            _instructions[0xC1] = new InstrDelegate(this.CMPB_Immediate);
            _instructions[0xC2] = new InstrDelegate(this.SBCB_Immediate);
            _instructions[0xC3] = new InstrDelegate(this.ADDD_Immediate);
            _instructions[0xC4] = new InstrDelegate(this.ANDB_Immediate);
            _instructions[0xC5] = new InstrDelegate(this.BITB_Immediate);
            _instructions[0xC6] = new InstrDelegate(this.LDB_Immediate);
            _instructions[0xC7] = new InstrDelegate(this.EORB_Immediate);
            _instructions[0xC8] = new InstrDelegate(this.EORB_Immediate);
            _instructions[0xC9] = new InstrDelegate(this.ADCB_Immediate);
            _instructions[0xCA] = new InstrDelegate(this.ORB_Immediate);
            _instructions[0xCB] = new InstrDelegate(this.ADDB_Immediate);
            _instructions[0xCC] = new InstrDelegate(this.LDD_Immediate);
            _instructions[0xCD] = new InstrDelegate(this.Illegal);
            _instructions[0xCE] = new InstrDelegate(this.LDU_Immediate);
            _instructions[0xCF] = new InstrDelegate(this.Illegal);

            _instructions[0xD0] = new InstrDelegate(this.SUBB_Direct);
            _instructions[0xD1] = new InstrDelegate(this.CMPB_Direct);
            _instructions[0xD2] = new InstrDelegate(this.SBCB_Direct);
            _instructions[0xD3] = new InstrDelegate(this.ADDD_Direct);
            _instructions[0xD4] = new InstrDelegate(this.ANDB_Direct);
            _instructions[0xD5] = new InstrDelegate(this.BITB_Direct);
            _instructions[0xD6] = new InstrDelegate(this.LDB_Direct);
            _instructions[0xD7] = new InstrDelegate(this.STB_Direct);
            _instructions[0xD8] = new InstrDelegate(this.EORB_Direct);
            _instructions[0xD9] = new InstrDelegate(this.ADCB_Direct);
            _instructions[0xDA] = new InstrDelegate(this.ORB_Direct);
            _instructions[0xDB] = new InstrDelegate(this.ADDB_Direct);
            _instructions[0xDC] = new InstrDelegate(this.LDD_Direct);
            _instructions[0xDD] = new InstrDelegate(this.STD_Direct);
            _instructions[0xDE] = new InstrDelegate(this.LDU_Direct);
            _instructions[0xDF] = new InstrDelegate(this.STU_Direct);

            _instructions[0xE0] = new InstrDelegate(this.SUBB_Indexed);
            _instructions[0xE1] = new InstrDelegate(this.CMPB_Indexed);
            _instructions[0xE2] = new InstrDelegate(this.SBCD_Indexed);
            _instructions[0xE3] = new InstrDelegate(this.ADDD_Indexed);
            _instructions[0xE4] = new InstrDelegate(this.ANDB_Indexed);
            _instructions[0xE5] = new InstrDelegate(this.BITB_Indexed);
            _instructions[0xE6] = new InstrDelegate(this.LDB_Indexed);
            _instructions[0xE7] = new InstrDelegate(this.STB_Indexed);
            _instructions[0xE8] = new InstrDelegate(this.EORB_Indexed);
            _instructions[0xE9] = new InstrDelegate(this.ADCB_Indexed);
            _instructions[0xEA] = new InstrDelegate(this.ORB_Indexed);
            _instructions[0xEB] = new InstrDelegate(this.ADDB_Indexed);
            _instructions[0xEC] = new InstrDelegate(this.LDD_Indexed);
            _instructions[0xED] = new InstrDelegate(this.STD_Indexed);
            _instructions[0xEE] = new InstrDelegate(this.LDU_Indexed);
            _instructions[0xEF] = new InstrDelegate(this.STU_Indexed);

            _instructions[0xF0] = new InstrDelegate(this.SUBB_Extended);
            _instructions[0xF1] = new InstrDelegate(this.CMPB_Extended);
            _instructions[0xF2] = new InstrDelegate(this.SBCB_Extended);
            _instructions[0xF3] = new InstrDelegate(this.ADDD_Extended);
            _instructions[0xF4] = new InstrDelegate(this.ANDB_Extended);
            _instructions[0xF5] = new InstrDelegate(this.BITB_Extended);
            _instructions[0xF6] = new InstrDelegate(this.LDB_Extended);
            _instructions[0xF7] = new InstrDelegate(this.STB_Extended);
            _instructions[0xF8] = new InstrDelegate(this.EORB_Extended);
            _instructions[0xF9] = new InstrDelegate(this.ADCB_Extended);
            _instructions[0xFA] = new InstrDelegate(this.ORB_Extended);
            _instructions[0xFB] = new InstrDelegate(this.ADDB_Extended);
            _instructions[0xFC] = new InstrDelegate(this.LDD_Extended);
            _instructions[0xFD] = new InstrDelegate(this.STD_Extended);
            _instructions[0xFE] = new InstrDelegate(this.LDU_Extended);
            _instructions[0xFF] = new InstrDelegate(this.STU_Extended);
        }

        private void Init_Instructions10() 
        {
            _instructions10[0x00] = new InstrDelegate(this.Illegal10);
            _instructions10[0x01] = new InstrDelegate(this.Illegal10);
            _instructions10[0x02] = new InstrDelegate(this.Illegal10);
            _instructions10[0x03] = new InstrDelegate(this.Illegal10);
            _instructions10[0x04] = new InstrDelegate(this.Illegal10);
            _instructions10[0x05] = new InstrDelegate(this.Illegal10);
            _instructions10[0x06] = new InstrDelegate(this.Illegal10);
            _instructions10[0x07] = new InstrDelegate(this.Illegal10);
            _instructions10[0x08] = new InstrDelegate(this.Illegal10);
            _instructions10[0x09] = new InstrDelegate(this.Illegal10);
            _instructions10[0x0A] = new InstrDelegate(this.Illegal10);
            _instructions10[0x0B] = new InstrDelegate(this.Illegal10);
            _instructions10[0x0C] = new InstrDelegate(this.Illegal10);
            _instructions10[0x0D] = new InstrDelegate(this.Illegal10);
            _instructions10[0x0E] = new InstrDelegate(this.Illegal10);
            _instructions10[0x0F] = new InstrDelegate(this.Illegal10);

            _instructions10[0x10] = new InstrDelegate(this.Illegal10);
            _instructions10[0x11] = new InstrDelegate(this.Illegal10);
            _instructions10[0x12] = new InstrDelegate(this.Illegal10);
            _instructions10[0x13] = new InstrDelegate(this.Illegal10);
            _instructions10[0x14] = new InstrDelegate(this.Illegal10);
            _instructions10[0x15] = new InstrDelegate(this.Illegal10);
            _instructions10[0x16] = new InstrDelegate(this.Illegal10);
            _instructions10[0x17] = new InstrDelegate(this.Illegal10);
            _instructions10[0x18] = new InstrDelegate(this.Illegal10);
            _instructions10[0x19] = new InstrDelegate(this.Illegal10);
            _instructions10[0x1A] = new InstrDelegate(this.Illegal10);
            _instructions10[0x1B] = new InstrDelegate(this.Illegal10);
            _instructions10[0x1C] = new InstrDelegate(this.Illegal10);
            _instructions10[0x1D] = new InstrDelegate(this.Illegal10);
            _instructions10[0x1E] = new InstrDelegate(this.Illegal10);
            _instructions10[0x1F] = new InstrDelegate(this.Illegal10);

            _instructions10[0x20] = new InstrDelegate(this.Illegal10);
            _instructions10[0x21] = new InstrDelegate(this.LBRN_Relative);
            _instructions10[0x22] = new InstrDelegate(this.LBHI_Relative);
            _instructions10[0x23] = new InstrDelegate(this.LBLS_Relative);
            _instructions10[0x24] = new InstrDelegate(this.LBCC_Relative);
            _instructions10[0x25] = new InstrDelegate(this.LBCS_Relative);
            _instructions10[0x26] = new InstrDelegate(this.LBNE_Relative);
            _instructions10[0x27] = new InstrDelegate(this.LBEQ_Relative);
            _instructions10[0x28] = new InstrDelegate(this.LBVC_Relative);
            _instructions10[0x29] = new InstrDelegate(this.LBVS_Relative);
            _instructions10[0x2A] = new InstrDelegate(this.LBPL_Relative);
            _instructions10[0x2B] = new InstrDelegate(this.LBMI_Relative);
            _instructions10[0x2C] = new InstrDelegate(this.LBGE_Relative);
            _instructions10[0x2D] = new InstrDelegate(this.LBLT_Relative);
            _instructions10[0x2E] = new InstrDelegate(this.LBGT_Relative);
            _instructions10[0x2F] = new InstrDelegate(this.LBLE_Relative);

            _instructions10[0x30] = new InstrDelegate(this.Illegal10);
            _instructions10[0x31] = new InstrDelegate(this.Illegal10);
            _instructions10[0x32] = new InstrDelegate(this.Illegal10);
            _instructions10[0x33] = new InstrDelegate(this.Illegal10);
            _instructions10[0x34] = new InstrDelegate(this.Illegal10);
            _instructions10[0x35] = new InstrDelegate(this.Illegal10);
            _instructions10[0x36] = new InstrDelegate(this.Illegal10);
            _instructions10[0x37] = new InstrDelegate(this.Illegal10);
            _instructions10[0x38] = new InstrDelegate(this.Illegal10);
            _instructions10[0x39] = new InstrDelegate(this.Illegal10);
            _instructions10[0x3A] = new InstrDelegate(this.Illegal10);
            _instructions10[0x3B] = new InstrDelegate(this.Illegal10);
            _instructions10[0x3C] = new InstrDelegate(this.Illegal10);
            _instructions10[0x3D] = new InstrDelegate(this.Illegal10);
            _instructions10[0x3E] = new InstrDelegate(this.Illegal10);
            _instructions10[0x3F] = new InstrDelegate(this.SWI2_Inherent);

            _instructions10[0x40] = new InstrDelegate(this.Illegal10);
            _instructions10[0x41] = new InstrDelegate(this.Illegal10);
            _instructions10[0x42] = new InstrDelegate(this.Illegal10);
            _instructions10[0x43] = new InstrDelegate(this.Illegal10);
            _instructions10[0x44] = new InstrDelegate(this.Illegal10);
            _instructions10[0x45] = new InstrDelegate(this.Illegal10);
            _instructions10[0x46] = new InstrDelegate(this.Illegal10);
            _instructions10[0x47] = new InstrDelegate(this.Illegal10);
            _instructions10[0x48] = new InstrDelegate(this.Illegal10);
            _instructions10[0x49] = new InstrDelegate(this.Illegal10);
            _instructions10[0x4A] = new InstrDelegate(this.Illegal10);
            _instructions10[0x4B] = new InstrDelegate(this.Illegal10);
            _instructions10[0x4C] = new InstrDelegate(this.Illegal10);
            _instructions10[0x4D] = new InstrDelegate(this.Illegal10);
            _instructions10[0x4E] = new InstrDelegate(this.Illegal10);
            _instructions10[0x4F] = new InstrDelegate(this.Illegal10);

            _instructions10[0x50] = new InstrDelegate(this.Illegal10);
            _instructions10[0x51] = new InstrDelegate(this.Illegal10);
            _instructions10[0x52] = new InstrDelegate(this.Illegal10);
            _instructions10[0x53] = new InstrDelegate(this.Illegal10);
            _instructions10[0x54] = new InstrDelegate(this.Illegal10);
            _instructions10[0x55] = new InstrDelegate(this.Illegal10);
            _instructions10[0x56] = new InstrDelegate(this.Illegal10);
            _instructions10[0x57] = new InstrDelegate(this.Illegal10);
            _instructions10[0x58] = new InstrDelegate(this.Illegal10);
            _instructions10[0x59] = new InstrDelegate(this.Illegal10);
            _instructions10[0x5A] = new InstrDelegate(this.Illegal10);
            _instructions10[0x5B] = new InstrDelegate(this.Illegal10);
            _instructions10[0x5C] = new InstrDelegate(this.Illegal10);
            _instructions10[0x5D] = new InstrDelegate(this.Illegal10);
            _instructions10[0x5E] = new InstrDelegate(this.Illegal10);
            _instructions10[0x5F] = new InstrDelegate(this.Illegal10);

            _instructions10[0x60] = new InstrDelegate(this.Illegal10);
            _instructions10[0x61] = new InstrDelegate(this.Illegal10);
            _instructions10[0x62] = new InstrDelegate(this.Illegal10);
            _instructions10[0x63] = new InstrDelegate(this.Illegal10);
            _instructions10[0x64] = new InstrDelegate(this.Illegal10);
            _instructions10[0x65] = new InstrDelegate(this.Illegal10);
            _instructions10[0x66] = new InstrDelegate(this.Illegal10);
            _instructions10[0x67] = new InstrDelegate(this.Illegal10);
            _instructions10[0x68] = new InstrDelegate(this.Illegal10);
            _instructions10[0x69] = new InstrDelegate(this.Illegal10);
            _instructions10[0x6A] = new InstrDelegate(this.Illegal10);
            _instructions10[0x6B] = new InstrDelegate(this.Illegal10);
            _instructions10[0x6C] = new InstrDelegate(this.Illegal10);
            _instructions10[0x6D] = new InstrDelegate(this.Illegal10);
            _instructions10[0x6E] = new InstrDelegate(this.Illegal10);
            _instructions10[0x6F] = new InstrDelegate(this.Illegal10);

            _instructions10[0x70] = new InstrDelegate(this.Illegal10);
            _instructions10[0x71] = new InstrDelegate(this.Illegal10);
            _instructions10[0x72] = new InstrDelegate(this.Illegal10);
            _instructions10[0x73] = new InstrDelegate(this.Illegal10);
            _instructions10[0x74] = new InstrDelegate(this.Illegal10);
            _instructions10[0x75] = new InstrDelegate(this.Illegal10);
            _instructions10[0x76] = new InstrDelegate(this.Illegal10);
            _instructions10[0x77] = new InstrDelegate(this.Illegal10);
            _instructions10[0x78] = new InstrDelegate(this.Illegal10);
            _instructions10[0x79] = new InstrDelegate(this.Illegal10);
            _instructions10[0x7A] = new InstrDelegate(this.Illegal10);
            _instructions10[0x7B] = new InstrDelegate(this.Illegal10);
            _instructions10[0x7C] = new InstrDelegate(this.Illegal10);
            _instructions10[0x7D] = new InstrDelegate(this.Illegal10);
            _instructions10[0x7E] = new InstrDelegate(this.Illegal10);
            _instructions10[0x7F] = new InstrDelegate(this.Illegal10);

            _instructions10[0x80] = new InstrDelegate(this.Illegal10);
            _instructions10[0x81] = new InstrDelegate(this.Illegal10);
            _instructions10[0x82] = new InstrDelegate(this.Illegal10);
            _instructions10[0x83] = new InstrDelegate(this.Cmpd);
            _instructions10[0x84] = new InstrDelegate(this.Illegal10);
            _instructions10[0x85] = new InstrDelegate(this.Illegal10);
            _instructions10[0x86] = new InstrDelegate(this.Illegal10);
            _instructions10[0x87] = new InstrDelegate(this.Illegal10);
            _instructions10[0x88] = new InstrDelegate(this.Illegal10);
            _instructions10[0x89] = new InstrDelegate(this.Illegal10);
            _instructions10[0x8A] = new InstrDelegate(this.Illegal10);
            _instructions10[0x8B] = new InstrDelegate(this.Illegal10);
            _instructions10[0x8C] = new InstrDelegate(this.cmpy);
            _instructions10[0x8D] = new InstrDelegate(this.Illegal10);
            _instructions10[0x8E] = new InstrDelegate(this.ldy);
            _instructions10[0x8F] = new InstrDelegate(this.Illegal10);

            _instructions10[0x90] = new InstrDelegate(this.Illegal10);
            _instructions10[0x91] = new InstrDelegate(this.Illegal10);
            _instructions10[0x92] = new InstrDelegate(this.Illegal10);
            _instructions10[0x93] = new InstrDelegate(this.cmpdd);
            _instructions10[0x94] = new InstrDelegate(this.Illegal10);
            _instructions10[0x95] = new InstrDelegate(this.Illegal10);
            _instructions10[0x96] = new InstrDelegate(this.Illegal10);
            _instructions10[0x97] = new InstrDelegate(this.Illegal10);
            _instructions10[0x98] = new InstrDelegate(this.Illegal10);
            _instructions10[0x99] = new InstrDelegate(this.Illegal10);
            _instructions10[0x9A] = new InstrDelegate(this.Illegal10);
            _instructions10[0x9B] = new InstrDelegate(this.Illegal10);
            _instructions10[0x9C] = new InstrDelegate(this.cmpyd);
            _instructions10[0x9D] = new InstrDelegate(this.Illegal10);
            _instructions10[0x9E] = new InstrDelegate(this.ldyd);
            _instructions10[0x9F] = new InstrDelegate(this.sty);

            _instructions10[0xA0] = new InstrDelegate(this.Illegal10);
            _instructions10[0xA1] = new InstrDelegate(this.Illegal10);
            _instructions10[0xA2] = new InstrDelegate(this.Illegal10);
            _instructions10[0xA3] = new InstrDelegate(this.cmpdi);
            _instructions10[0xA4] = new InstrDelegate(this.Illegal10);
            _instructions10[0xA5] = new InstrDelegate(this.Illegal10);
            _instructions10[0xA6] = new InstrDelegate(this.Illegal10);
            _instructions10[0xA7] = new InstrDelegate(this.Illegal10);
            _instructions10[0xA8] = new InstrDelegate(this.Illegal10);
            _instructions10[0xA9] = new InstrDelegate(this.Illegal10);
            _instructions10[0xAA] = new InstrDelegate(this.Illegal10);
            _instructions10[0xAB] = new InstrDelegate(this.Illegal10);
            _instructions10[0xAC] = new InstrDelegate(this.cmpyi);
            _instructions10[0xAD] = new InstrDelegate(this.Illegal10);
            _instructions10[0xAE] = new InstrDelegate(this.ldyi);
            _instructions10[0xAF] = new InstrDelegate(this.styi);

            _instructions10[0xB0] = new InstrDelegate(this.Illegal10);
            _instructions10[0xB1] = new InstrDelegate(this.Illegal10);
            _instructions10[0xB2] = new InstrDelegate(this.Illegal10);
            _instructions10[0xB3] = new InstrDelegate(this.cmpde);
            _instructions10[0xB4] = new InstrDelegate(this.Illegal10);
            _instructions10[0xB5] = new InstrDelegate(this.Illegal10);
            _instructions10[0xB6] = new InstrDelegate(this.Illegal10);
            _instructions10[0xB7] = new InstrDelegate(this.Illegal10);
            _instructions10[0xB8] = new InstrDelegate(this.Illegal10);
            _instructions10[0xB9] = new InstrDelegate(this.Illegal10);
            _instructions10[0xBA] = new InstrDelegate(this.Illegal10);
            _instructions10[0xBB] = new InstrDelegate(this.Illegal10);
            _instructions10[0xBC] = new InstrDelegate(this.cmpye);
            _instructions10[0xBD] = new InstrDelegate(this.Illegal10);
            _instructions10[0xBE] = new InstrDelegate(this.ldye);
            _instructions10[0xBF] = new InstrDelegate(this.stye);

            _instructions10[0xC0] = new InstrDelegate(this.Illegal10);
            _instructions10[0xC1] = new InstrDelegate(this.Illegal10);
            _instructions10[0xC2] = new InstrDelegate(this.Illegal10);
            _instructions10[0xC3] = new InstrDelegate(this.Illegal10);
            _instructions10[0xC4] = new InstrDelegate(this.Illegal10);
            _instructions10[0xC5] = new InstrDelegate(this.Illegal10);
            _instructions10[0xC6] = new InstrDelegate(this.Illegal10);
            _instructions10[0xC7] = new InstrDelegate(this.Illegal10);
            _instructions10[0xC8] = new InstrDelegate(this.Illegal10);
            _instructions10[0xC9] = new InstrDelegate(this.Illegal10);
            _instructions10[0xCA] = new InstrDelegate(this.Illegal10);
            _instructions10[0xCB] = new InstrDelegate(this.Illegal10);
            _instructions10[0xCC] = new InstrDelegate(this.Illegal10);
            _instructions10[0xCD] = new InstrDelegate(this.Illegal10);
            _instructions10[0xCE] = new InstrDelegate(this.lds);
            _instructions10[0xCF] = new InstrDelegate(this.Illegal10);

            _instructions10[0xD0] = new InstrDelegate(this.Illegal10);
            _instructions10[0xD1] = new InstrDelegate(this.Illegal10);
            _instructions10[0xD2] = new InstrDelegate(this.Illegal10);
            _instructions10[0xD3] = new InstrDelegate(this.Illegal10);
            _instructions10[0xD4] = new InstrDelegate(this.Illegal10);
            _instructions10[0xD5] = new InstrDelegate(this.Illegal10);
            _instructions10[0xD6] = new InstrDelegate(this.Illegal10);
            _instructions10[0xD7] = new InstrDelegate(this.Illegal10);
            _instructions10[0xD8] = new InstrDelegate(this.Illegal10);
            _instructions10[0xD9] = new InstrDelegate(this.Illegal10);
            _instructions10[0xDA] = new InstrDelegate(this.Illegal10);
            _instructions10[0xDB] = new InstrDelegate(this.Illegal10);
            _instructions10[0xDC] = new InstrDelegate(this.Illegal10);
            _instructions10[0xDD] = new InstrDelegate(this.Illegal10);
            _instructions10[0xDE] = new InstrDelegate(this.ldsd);
            _instructions10[0xDF] = new InstrDelegate(this.stsd);

            _instructions10[0xE0] = new InstrDelegate(this.Illegal10);
            _instructions10[0xE1] = new InstrDelegate(this.Illegal10);
            _instructions10[0xE2] = new InstrDelegate(this.Illegal10);
            _instructions10[0xE3] = new InstrDelegate(this.Illegal10);
            _instructions10[0xE4] = new InstrDelegate(this.Illegal10);
            _instructions10[0xE5] = new InstrDelegate(this.Illegal10);
            _instructions10[0xE6] = new InstrDelegate(this.Illegal10);
            _instructions10[0xE7] = new InstrDelegate(this.Illegal10);
            _instructions10[0xE8] = new InstrDelegate(this.Illegal10);
            _instructions10[0xE9] = new InstrDelegate(this.Illegal10);
            _instructions10[0xEA] = new InstrDelegate(this.Illegal10);
            _instructions10[0xEB] = new InstrDelegate(this.Illegal10);
            _instructions10[0xEC] = new InstrDelegate(this.Illegal10);
            _instructions10[0xED] = new InstrDelegate(this.Illegal10);
            _instructions10[0xEE] = new InstrDelegate(this.ldsi);
            _instructions10[0xEF] = new InstrDelegate(this.stsi);

            _instructions10[0xF0] = new InstrDelegate(this.Illegal10);
            _instructions10[0xF1] = new InstrDelegate(this.Illegal10);
            _instructions10[0xF2] = new InstrDelegate(this.Illegal10);
            _instructions10[0xF3] = new InstrDelegate(this.Illegal10);
            _instructions10[0xF4] = new InstrDelegate(this.Illegal10);
            _instructions10[0xF5] = new InstrDelegate(this.Illegal10);
            _instructions10[0xF6] = new InstrDelegate(this.Illegal10);
            _instructions10[0xF7] = new InstrDelegate(this.Illegal10);
            _instructions10[0xF8] = new InstrDelegate(this.Illegal10);
            _instructions10[0xF9] = new InstrDelegate(this.Illegal10);
            _instructions10[0xFA] = new InstrDelegate(this.Illegal10);
            _instructions10[0xFB] = new InstrDelegate(this.Illegal10);
            _instructions10[0xFC] = new InstrDelegate(this.Illegal10);
            _instructions10[0xFD] = new InstrDelegate(this.Illegal10);
            _instructions10[0xFE] = new InstrDelegate(this.ldse);
            _instructions10[0xFF] = new InstrDelegate(this.stse);
        }

        private void Add11(int op, string name, InstrDelegate dlg) 
        {
            this._instructions11[op] = dlg;
            this._mnemonics11[op] = name;
        }

        private void Init_Instructionst11() 
        {
            for (var i = 0; i < 256; i++)
            {
                this._instructions11[i] = new InstrDelegate(this.Illegal11);
                this._mnemonics11[i] = "     ";
            }

            this.Add11(0x3F, "swi3", new InstrDelegate(this.SWI3_Inherent));
            this.Add11(0x83, "cmpu", new InstrDelegate(this.CMPU_Immediate));
            this.Add11(0x8C, "cmps", new InstrDelegate(this.CMPS_Immediate));
            this.Add11(0x93, "cmpu", new InstrDelegate(this.CMPU_Direct));
            this.Add11(0x9C, "cmps", new InstrDelegate(this.CMPS_Direct));
            this.Add11(0xA3, "cmpu", new InstrDelegate(this.CMPU_Indexed));
            this.Add11(0xAC, "cmps", new InstrDelegate(this.CMPS_Indexed));
            this.Add11(0xB3, "cmpu", new InstrDelegate(this.CMPU_Extended));
            this.Add11(0xBC, "cmps", new InstrDelegate(this.CMPS_Extended));

        }

        private string[] _mnemonics = new string[]  {
			"neg  ", "     ", "     ", "com  ", "lsr  ", "     ", "ror  ", "asr  ", // 00..07
			"asl  ", "rol  ", "dec  ", "     ", "inc  ", "tst  ", "jmp  ", "clr  ", // 08..0f
			"p10  ", "p11  ", "nop  ", "sync ", "     ", "     ", "lbra ", "lbsr ", // 10..17
			"     ", "daa  ", "orcc ", "     ", "andcc", "sex  ", "exg  ", "tfr  ", // 18..1f
			"bra  ", "brn  ", "bhi  ", "bls  ", "bcc  ", "bcs  ", "bne  ", "beq  ", // 20..27
			"bvc  ", "bvs  ", "bpl  ", "bmi  ", "bge  ", "blt  ", "bgt  ", "ble  ", // 28..2f
			"leax ", "leay ", "leas ", "leau ", "pshs ", "puls ", "pshu ", "pulu ", // 30..37
			"     ", "rts  ", "abx  ", "rti  ", "cwai ", "mul  ", "     ", "swi  ", // 38..3f
			"nega ", "     ", "     ", "coma ", "lsra ", "     ", "rora ", "asra ", // 40..47
			"asla ", "rola ", "deca ", "     ", "inca ", "tsta ", "     ", "clra ", // 48..4f
			"negb ", "     ", "     ", "comb ", "lsrb ", "     ", "rorb ", "asrb ", // 50..57
			"aslb ", "rolb ", "decb ", "     ", "incb ", "tstb ", "     ", "clrb ", // 58..5f
			"negi ", "     ", "     ", "comi ", "lsri ", "     ", "rori ", "asri ", // 60..67
			"asli ", "roli ", "deci ", "     ", "inci ", "tsti ", "jmpi ", "clri ", // 68..6f
			"nege ", "     ", "     ", "come ", "lsre ", "     ", "rore ", "asre ", // 70..77
			"asle ", "role ", "dece ", "     ", "ince ", "tste ", "jmpe ", "clre ", // 78..7f
			"suba ", "cmpa ", "sbca ", "subd ", "anda ", "bita ", "lda  ", "     ", // 80..87
			"eora ", "adca ", "ora  ", "adda ", "cmpx ", "bsr  ", "ldx  ", "     ", // 88..8f
			"subad", "cmpad", "sbcad", "subdd", "andad", "bitad", "ldad ", "stad ", // 90..97
			"eorad", "adcad", "orad ", "addad", "cmpxd", "jsrd ", "ldxd ", "stxd ", // 98..9f
			"subax", "cmpax", "sbcax", "subdx", "andax", "bitax", "ldax ", "stax ", // a0..a7
			"eorax", "adcax", "orax ", "addax", "cmpxx", "jsrx ", "ldxx ", "stxx ", // a8..af
			"subae", "cmpae", "sbcae", "subde", "andae", "bitae", "ldae ", "stae ", // b0..b7
			"eorae", "adcae", "orae ", "addae", "cmpxe", "jsre ", "ldxe ", "stxe ", // b8..bf
			"subb ", "cmpb ", "sbcb ", "addd ", "andb ", "bitb ", "ldb  ", "eorb ", // c0..c7
			"eorb ", "adcb ", "orb  ", "addb ", "ldd  ", "     ", "ldu  ", "     ", // c8..cf
			"sbbd ", "cmpbd", "sbcd ", "adddd", "andbd", "bitbd", "ldbd ", "stbd ", // d0..d7
			"eorbd", "adcbd", "orbd ", "addbd", "lddd ", "stdd ", "ldud ", "stud ", // d8..df
			"subbx", "cmpbx", "sbcbx", "adddx", "andbx", "bitbx", "ldbx ", "stbx ", // e0..e7
			"eorbx", "adcbx", "orbx ", "addbx", "lddx ", "stdx ", "ldux ", "stux ", // e8..ef
			"subbe", "cmpbe", "sbcbe", "addde", "andbe", "bitbe", "ldbe ", "stbe ", // f0..f7
			"eorbe", "adcbe", "orbe ", "addbe", "ldde ", "stde ", "ldue ", "stue " // f8..ff        
		};

        private string[] _mnemonics10 = new string[]  {
			"     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 00..07
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 08..0f
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 10..17
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 18..1f
            "     ", "lbrn ", "lbhi ", "lbls ", "lbcc ", "lbcs ", "lbne ", "lbeq ", // 20..27
            "lbvc ", "lbvs ", "lbpl ", "lbmi ", "lbge ", "lblt ", "lbgt ", "lble ", // 28..2f
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 30..37
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "swi2 ", // 38..3f
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 40..47
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 48..4f
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 50..57
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 58..5f
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 60..67
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 68..6f
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 70..77
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // 78..7f
            "     ", "     ", "     ", "cmpd ", "     ", "     ", "     ", "     ", // 80..87
            "     ", "     ", "     ", "     ", "cmpy ", "     ", "ldy  ", "     ", // 88..8f
            "     ", "     ", "     ", "cmpdd", "     ", "     ", "     ", "     ", // 90..97
            "     ", "     ", "     ", "     ", "cmpyd", "     ", "ldyd ", "sty  ", // 98..9f
            "     ", "     ", "     ", "cmpdi", "     ", "     ", "     ", "     ", // a0..a7
            "     ", "     ", "     ", "     ", "cmpyi", "     ", "ldyi ", "styi ", // a8..af
            "     ", "     ", "     ", "cmpde", "     ", "     ", "     ", "     ", // b0..b7
            "     ", "     ", "     ", "     ", "cmpye", "     ", "ldye ", "stye ", // b8..bf
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // c0..c7
            "     ", "     ", "     ", "     ", "     ", "     ", "lds  ", "     ", // c8..cf
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // d0..d7
            "     ", "     ", "     ", "     ", "     ", "     ", "ldsd ", "stsd ", // d8..df
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // e0..e7
            "     ", "     ", "     ", "     ", "     ", "     ", "ldsi ", "stsi ", // e8..ef
            "     ", "     ", "     ", "     ", "     ", "     ", "     ", "     ", // f0..f7
            "     ", "     ", "     ", "     ", "     ", "     ", "ldse ", "stse " // f8..ff
        };

        private string[] _mnemonics11 = new string[255];

        private int[] _mc6809Cycles = new int[]  {
			6, 0, 0, 6, 6, 0, 6, 6, 6, 6, 6, 0, 6, 6, 3, 6,          /* 00-0F */
            0, 0, 2, 4, 0, 0, 5, 9, 0, 2, 3, 0, 3, 2, 8, 6,          /* 10-1F */
            3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3,          /* 20-2F */
            4, 4, 4, 4, 5, 5, 5, 5, 0, 5, 3, 6, 9, 11, 0, 19,        /* 30-3F */
            2, 0, 0, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 0, 2,          /* 40-4F */
            2, 0, 0, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 0, 2,          /* 50-5F */
            6, 0, 0, 6, 6, 0, 6, 6, 6, 6, 6, 0, 6, 6, 3, 6,          /* 60-6F */
            7, 0, 0, 7, 7, 0, 7, 7, 7, 7, 7, 0, 7, 7, 4, 7,          /* 70-7F */
            2, 2, 2, 4, 2, 2, 2, 0, 2, 2, 2, 2, 4, 7, 3, 0,          /* 80-8F */
            4, 4, 4, 6, 4, 4, 4, 4, 4, 4, 4, 4, 6, 7, 5, 5,          /* 90-9F */
            4, 4, 4, 6, 4, 4, 4, 4, 4, 4, 4, 4, 6, 7, 5, 5,          /* A0-AF */
            5, 5, 5, 7, 5, 5, 5, 5, 5, 5, 5, 5, 7, 8, 6, 6,          /* B0-BF */
            2, 2, 2, 4, 2, 2, 2, 0, 2, 2, 2, 2, 3, 0, 3, 0,          /* C0-CF */
            4, 4, 4, 6, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5,          /* D0-DF */
            4, 4, 4, 6, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5,          /* E0-EF */
            5, 5, 5, 7, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6           /* F0-FF */
        };

        private int[] _mc6809Cycles2 = new int[]  {
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,          /* 00-0F */
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,          /* 10-1F */
            0, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,          /* 20-2F */
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20,         /* 30-3F */
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,          /* 40-4F */
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,          /* 50-5F */
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,          /* 60-6F */
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,          /* 70-7F */
            0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 4, 0,          /* 80-8F */
            0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 6, 6,          /* 90-9F */
            0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 6, 6,          /* A0-AF */
            0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 7, 7,          /* B0-BF */
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0,          /* C0-CF */
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 6,          /* D0-DF */
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 6,          /* E0-EF */
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7           /* F0-FF */
        };
    }
}
