using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	public class MethodDefinition 
	{
		public Cecil.MethodDefinition CecilMethodDefinition { get; private set; }

		public TypeDefinition ParentType { get; private set; }

		public List<Instruction> Instructions { get; } = new List<Instruction>();
		public Dictionary<int, Instruction> InstructionDict { get; } = new Dictionary<int, Instruction>();

		public int CurrentInstructionIndex { get; set; }
		public Instruction CurrentInstruction { get; private set; }

		public List<object> LocalVariables { get; } = new List<object>();

		public MethodDefinition(Cecil.MethodDefinition cecilMethodDefinition, TypeDefinition parentType) 
		{
			this.CecilMethodDefinition = cecilMethodDefinition;
			this.ParentType = parentType;

			for (int x = 0; x < cecilMethodDefinition.Body.Variables.Count; x++)
				this.LocalVariables.Add(null);

			// Console.WriteLine("---------------------------------------");
			// Console.WriteLine($"METHOD: {cecilMethodDefinition.Name} -> [{cecilMethodDefinition.FullName}] VARS:" + this.LocalVariables.Count);

			if (cecilMethodDefinition.Name == "Main")			
				this.ParentType.ParentModule.MainMethod = this;

			int count = 0;
			foreach (var cecilInstruction in this.CecilMethodDefinition.Body.Instructions)
			{
				Instruction instruction = this.CIL_BuildInstruction(cecilInstruction);
				this.Instructions.Add(instruction);
				this.InstructionDict.Add(instruction.Offset, instruction);
				instruction.Index = count;
				count++;
			}
		}

		public void Execute() 
		{
			// Console.WriteLine("EXECUTE: " + this.CecilMethodDefinition.FullName + "!!!");

			this.CurrentInstructionIndex = 0;
			this.CurrentInstruction = this.Instructions[0];

			while (true)
			{
				this.CurrentInstruction = this.Instructions[this.CurrentInstructionIndex];

				if (this.CurrentInstruction.GetType() == typeof(IL_ret))
					break;

				this.CurrentInstruction.Execute();
			}

			// it should have pushed the parameters on the stack ... pop them off...
			this.ParentType.ParentModule.MethodStack.Pop(this.CecilMethodDefinition.Parameters.Count);
		}

		public void CilListing() 
		{
			Console.WriteLine("METHOD: " + this.CecilMethodDefinition.FullName);
			foreach (var instruction in this.Instructions)
				instruction.CilListing();
			Console.WriteLine();
		}

		//public void SetCurrentInstruction(Instruction currentInstruction)
		//{
		//	this.CurrentInstructionIndex = currentInstruction.Index - 1;
		//	// this.CurrentInstruction = currentInstruction;
		//}

		protected Instruction CIL_BuildInstruction(Cecil.Cil.Instruction instruction) 
		{
			// Console.WriteLine(instruction.ToString()); // + " -> " + instruction.Offset.ToString("X"));

			Instruction op = null;
			switch (instruction.OpCode.Code)
			{
				// 0x00 | nop | Do nothing (No operation). | Base instruction
				case Cecil.Cil.Code.Nop:
					op = new IL_nop(instruction, this);
					break;

				// 0x01 | break | Inform a debugger that a breakpoint has been reached. | Base instruction
				case Cecil.Cil.Code.Break:
					op = new IL_break(instruction, this);
					break;

				// 0x02 | ldarg.0 | Load argument 0 onto the stack. | Base instruction
				case Cecil.Cil.Code.Ldarg_0:
					op = new IL_ldarg_0(instruction, this);
					break;

				// 0x03 | ldarg.1 | Load argument 1 onto the stack. | Base instruction
				case Cecil.Cil.Code.Ldarg_1:
					op = new IL_ldarg_1(instruction, this);
					break;

				// 0x04 | ldarg.2 | Load argument 2 onto the stack. | Base instruction
				case Cecil.Cil.Code.Ldarg_2:
					op = new IL_ldarg_2(instruction, this);
					break;

				// 0x05 | ldarg.3 | Load argument 3 onto the stack. | Base instruction
				case Cecil.Cil.Code.Ldarg_3:
					op = new IL_ldarg_3(instruction, this);
					break;

				// 0x06 | ldloc.0 | Load local variable 0 onto stack. | Base instruction
				case Cecil.Cil.Code.Ldloc_0:
					op = new IL_ldloc_0(instruction, this);
					break;

				// 0x07 | ldloc.1 | Load local variable 1 onto stack. | Base instruction
				case Cecil.Cil.Code.Ldloc_1:
					op = new IL_ldloc_1(instruction, this);
					break;

				// 0x08 | ldloc.2 | Load local variable 2 onto stack. | Base instruction
				case Cecil.Cil.Code.Ldloc_2:
					op = new IL_ldloc_2(instruction, this);
					break;

				// 0x09 | ldloc.3 | Load local variable 3 onto stack. | Base instruction
				case Cecil.Cil.Code.Ldloc_3:
					op = new IL_ldloc_3(instruction, this);
					break;

				// 0x0A | stloc.0 | Pop a value from stack into local variable 0. | Base instruction
				case Cecil.Cil.Code.Stloc_0:
					op = new IL_stloc_0(instruction, this);
					break;

				// 0x0B | stloc.1 | Pop a value from stack into local variable 1. | Base instruction
				case Cecil.Cil.Code.Stloc_1:
					op = new IL_stloc_1(instruction, this);
					break;

				// 0x0C | stloc.2 | Pop a value from stack into local variable 2. | Base instruction
				case Cecil.Cil.Code.Stloc_2:
					op = new IL_stloc_2(instruction, this);
					break;

				// 0x0D | stloc.3 | Pop a value from stack into local variable 3. | Base instruction
				case Cecil.Cil.Code.Stloc_3:
					op = new IL_stloc_3(instruction, this);
					break;

				// 0x0E | ldarg.s | Load argument numbered num onto the stack, short form. | Base instruction
				case Cecil.Cil.Code.Ldarg_S:
					op = new IL_ldarg_s(instruction, this);
					break;

				// 0x0F | ldarga.s | Fetch the address of argument argNum, short form. | Base instruction
				case Cecil.Cil.Code.Ldarga_S:
					op = new IL_ldarga_s(instruction, this);
					break;

				// 0x10 | starg.s | Store value to the argument numbered num, short form. | Base instruction
				case Cecil.Cil.Code.Starg_S:
					op = new IL_starg_s(instruction, this);
					break;

				// 0x11 | ldloc.s | Load local variable of index indx onto stack, short form. | Base instruction
				case Cecil.Cil.Code.Ldloc_S:
					op = new IL_ldloc_s(instruction, this);
					break;

				// 0x12 | ldloca.s | Load address of local variable with index indx, short form. | Base instruction
				case Cecil.Cil.Code.Ldloca_S:
					op = new IL_ldloca_s(instruction, this);
					break;

				// 0x13 | stloc.s | Pop a value from stack into local variable indx, short form. | Base instruction
				case Cecil.Cil.Code.Stloc_S:
					op = new IL_stloc_s(instruction, this);
					break;

				// 0x14 | ldnull | Push a null reference on the stack. | Base instruction
				case Cecil.Cil.Code.Ldnull:
					op = new IL_ldnull(instruction, this);
					break;

				// 0x15 | ldc.i4.m1 | Push -1 onto the stack as int32. | Base instruction
				case Cecil.Cil.Code.Ldc_I4_M1:
					op = new IL_ldc_i4_m1(instruction, this);
					break;

				// 0x16 | ldc.i4.0 | Push 0 onto the stack as int32. | Base instruction
				case Cecil.Cil.Code.Ldc_I4_0:
					op = new IL_ldc_i4_0(instruction, this);
					break;

				// 0x17 | ldc.i4.1 | Push 1 onto the stack as int32. | Base instruction
				case Cecil.Cil.Code.Ldc_I4_1:
					op = new IL_ldc_i4_1(instruction, this);
					break;

				// 0x18 | ldc.i4.2 | Push 2 onto the stack as int32. | Base instruction
				case Cecil.Cil.Code.Ldc_I4_2:
					op = new IL_ldc_i4_2(instruction, this);
					break;

				// 0x19 | ldc.i4.3 | Push 3 onto the stack as int32. | Base instruction
				case Cecil.Cil.Code.Ldc_I4_3:
					op = new IL_ldc_i4_3(instruction, this);
					break;

				// 0x1A | ldc.i4.4 | Push 4 onto the stack as int32. | Base instruction
				case Cecil.Cil.Code.Ldc_I4_4:
					op = new IL_ldc_i4_4(instruction, this);
					break;

				// 0x1B | ldc.i4.5 | Push 5 onto the stack as int32. | Base instruction
				case Cecil.Cil.Code.Ldc_I4_5:
					op = new IL_ldc_i4_5(instruction, this);
					break;

				// 0x1C | ldc.i4.6 | Push 6 onto the stack as int32. | Base instruction
				case Cecil.Cil.Code.Ldc_I4_6:
					op = new IL_ldc_i4_6(instruction, this);
					break;

				// 0x1D | ldc.i4.7 | Push 7 onto the stack as int32. | Base instruction
				case Cecil.Cil.Code.Ldc_I4_7:
					op = new IL_ldc_i4_7(instruction, this);
					break;

				// 0x1E | ldc.i4.8 | Push 8 onto the stack as int32. | Base instruction
				case Cecil.Cil.Code.Ldc_I4_8:
					op = new IL_ldc_i4_8(instruction, this);
					break;

				// 0x1F | ldc.i4.s | Push num onto the stack as int32, short form. | Base instruction
				case Cecil.Cil.Code.Ldc_I4_S:
					op = new IL_ldc_i4_s(instruction, this);
					break;

				// 0x20 | ldc.i4 | Push num of type int32 onto the stack as int32. | Base instruction
				case Cecil.Cil.Code.Ldc_I4:
					op = new IL_ldc_i4(instruction, this);
					break;

				// 0x21 | ldc.i8 | Push num of type int64 onto the stack as int64. | Base instruction
				case Cecil.Cil.Code.Ldc_I8:
					op = new IL_ldc_i8(instruction, this);
					break;

				// 0x22 | ldc.r4 | Push num of type float32 onto the stack as F. | Base instruction
				case Cecil.Cil.Code.Ldc_R4:
					op = new IL_ldc_r4(instruction, this);
					break;

				// 0x23 | ldc.r8 | Push num of type float64 onto the stack as F. | Base instruction
				case Cecil.Cil.Code.Ldc_R8:
					op = new IL_ldc_r8(instruction, this);
					break;

				// 0x25 | dup | Duplicate the value on the top of the stack. | Base instruction
				case Cecil.Cil.Code.Dup:
					op = new IL_dup(instruction, this);
					break;

				// 0x26 | pop | Pop value from the stack. | Base instruction
				case Cecil.Cil.Code.Pop:
					op = new IL_pop(instruction, this);
					break;

				// 0x27 | jmp | Exit current method and jump to the specified method. | Base instruction
				case Cecil.Cil.Code.Jmp:
					op = new IL_jmp(instruction, this);
					break;

				// 0x28 | call | Call method described by method. | Base instruction
				case Cecil.Cil.Code.Call:
					op = new IL_call(instruction, this);
					break;

				// 0x29 | calli | Call method indicated on the stack with arguments described by callsitedescr. | Base instruction
				case Cecil.Cil.Code.Calli:
					op = new IL_calli(instruction, this);
					break;

				// 0x2A | ret | Return from method, possibly with a value. | Base instruction
				case Cecil.Cil.Code.Ret:
					op = new IL_ret(instruction, this);
					break;

				// 0x2B | br.s | Branch to target, short form. | Base instruction
				case Cecil.Cil.Code.Br_S:
					op = new IL_br_s(instruction, this);
					break;

				// 0x2C | brfalse.s | Branch to target if value is zero (false), short form. | Base instruction
				case Cecil.Cil.Code.Brfalse_S:
					op = new IL_brfalse_s(instruction, this);
					break;

				// 0x2D | brtrue.s | Branch to target if value is non-zero (true), short form. | Base instruction
				case Cecil.Cil.Code.Brtrue_S:
					op = new IL_brtrue_s(instruction, this);
					break;

				// 0x2E | beq.s | Branch to target if equal, short form. | Base instruction
				case Cecil.Cil.Code.Beq_S:
					op = new IL_beq_s(instruction, this);
					break;

				// 0x2F | bge.s | Branch to target if greater than or equal to, short form. | Base instruction
				case Cecil.Cil.Code.Bge_S:
					op = new IL_bge_s(instruction, this);
					break;

				// 0x30 | bgt.s | Branch to target if greater than, short form. | Base instruction
				case Cecil.Cil.Code.Bgt_S:
					op = new IL_bgt_s(instruction, this);
					break;

				// 0x31 | ble.s | Branch to target if less than or equal to, short form. | Base instruction
				case Cecil.Cil.Code.Ble_S:
					op = new IL_ble_s(instruction, this);
					break;

				// 0x32 | blt.s | Branch to target if less than, short form. | Base instruction
				case Cecil.Cil.Code.Blt_S:
					op = new IL_blt_s(instruction, this);
					break;

				// 0x33 | bne.un.s | Branch to target if unequal or unordered, short form. | Base instruction
				case Cecil.Cil.Code.Bne_Un_S:
					op = new IL_bne_un_s(instruction, this);
					break;

				// 0x34 | bge.un.s | Branch to target if greater than or equal to (unsigned or unordered), short form | Base instruction
				case Cecil.Cil.Code.Bge_Un_S:
					op = new IL_bge_un_s(instruction, this);
					break;

				// 0x35 | bgt.un.s | Branch to target if greater than (unsigned or unordered), short form. | Base instruction
				case Cecil.Cil.Code.Bgt_Un_S:
					op = new IL_bgt_un_s(instruction, this);
					break;

				// 0x36 | ble.un.s | Branch to target if less than or equal to (unsigned or unordered), short form | Base instruction
				case Cecil.Cil.Code.Ble_Un_S:
					op = new IL_ble_un_s(instruction, this);
					break;

				// 0x37 | blt.un.s | Branch to target if less than (unsigned or unordered), short form. | Base instruction
				case Cecil.Cil.Code.Blt_Un_S:
					op = new IL_blt_un_s(instruction, this);
					break;

				// 0x38 | br | Branch to target. | Base instruction
				case Cecil.Cil.Code.Br:
					op = new IL_br(instruction, this);
					break;

				// 0x39 | brfalse | Branch to target if value is zero (false). | Base instruction
				case Cecil.Cil.Code.Brfalse:
					op = new IL_brfalse(instruction, this);
					break;

				// 0x3A | brtrue | Branch to target if value is non-zero (true). | Base instruction
				case Cecil.Cil.Code.Brtrue:
					op = new IL_brtrue(instruction, this);
					break;

				// 0x3B | beq | Branch to target if equal. | Base instruction
				case Cecil.Cil.Code.Beq:
					op = new IL_beq(instruction, this);
					break;

				// 0x3C | bge | Branch to target if greater than or equal to. | Base instruction
				case Cecil.Cil.Code.Bge:
					op = new IL_bge(instruction, this);
					break;

				// 0x3D | bgt | Branch to target if greater than. | Base instruction
				case Cecil.Cil.Code.Bgt:
					op = new IL_bgt(instruction, this);
					break;

				// 0x3E | ble | Branch to target if less than or equal to. | Base instruction
				case Cecil.Cil.Code.Ble:
					op = new IL_ble(instruction, this);
					break;

				// 0x3F | blt | Branch to target if less than. | Base instruction
				case Cecil.Cil.Code.Blt:
					op = new IL_blt(instruction, this);
					break;

				// 0x40 | bne.un | Branch to target if unequal or unordered. | Base instruction
				case Cecil.Cil.Code.Bne_Un:
					op = new IL_bne_un(instruction, this);
					break;

				// 0x41 | bge.un | Branch to target if greater than or equal to (unsigned or unordered). | Base instruction
				case Cecil.Cil.Code.Bge_Un:
					op = new IL_bge_un(instruction, this);
					break;

				// 0x42 | bgt.un | Branch to target if greater than (unsigned or unordered). | Base instruction
				case Cecil.Cil.Code.Bgt_Un:
					op = new IL_bgt_un(instruction, this);
					break;

				// 0x43 | ble.un | Branch to target if less than or equal to (unsigned or unordered). | Base instruction
				case Cecil.Cil.Code.Ble_Un:
					op = new IL_ble_un(instruction, this);
					break;

				// 0x44 | blt.un | Branch to target if less than (unsigned or unordered). | Base instruction
				case Cecil.Cil.Code.Blt_Un:
					op = new IL_blt_un(instruction, this);
					break;

				// 0x45 | switch | Jump to one of n values. | Base instruction
				case Cecil.Cil.Code.Switch:
					op = new IL_switch(instruction, this);
					break;

				// 0x46 | ldind.i1 | Indirect load value of type int8 as int32 on the stack. | Base instruction
				case Cecil.Cil.Code.Ldind_I1:
					op = new IL_ldind_i1(instruction, this);
					break;

				// 0x47 | ldind.u1 | Indirect load value of type unsigned int8 as int32 on the stack | Base instruction
				case Cecil.Cil.Code.Ldind_U1:
					op = new IL_ldind_u1(instruction, this);
					break;

				// 0x48 | ldind.i2 | Indirect load value of type int16 as int32 on the stack. | Base instruction
				case Cecil.Cil.Code.Ldind_I2:
					op = new IL_ldind_i2(instruction, this);
					break;

				// 0x49 | ldind.u2 | Indirect load value of type unsigned int16 as int32 on the stack | Base instruction
				case Cecil.Cil.Code.Ldind_U2:
					op = new IL_ldind_u2(instruction, this);
					break;

				// 0x4A | ldind.i4 | Indirect load value of type int32 as int32 on the stack. | Base instruction
				case Cecil.Cil.Code.Ldind_I4:
					op = new IL_ldind_i4(instruction, this);
					break;

				// 0x4B | ldind.u4 | Indirect load value of type unsigned int32 as int32 on the stack | Base instruction
				case Cecil.Cil.Code.Ldind_U4:
					op = new IL_ldind_u4(instruction, this);
					break;

				// 0x4C | ldind.i8 | Indirect load value of type int64 as int64 on the stack. | Base instruction
				case Cecil.Cil.Code.Ldind_I8:
					op = new IL_ldind_i8(instruction, this);
					break;

				// 0x4D | ldind.i | Indirect load value of type native int as native int on the stack | Base instruction
				case Cecil.Cil.Code.Ldind_I:
					op = new IL_ldind_i(instruction, this);
					break;

				// 0x4E | ldind.r4 | Indirect load value of type float32 as F on the stack. | Base instruction
				case Cecil.Cil.Code.Ldind_R4:
					op = new IL_ldind_r4(instruction, this);
					break;

				// 0x4F | ldind.r8 | Indirect load value of type float64 as F on the stack. | Base instruction
				case Cecil.Cil.Code.Ldind_R8:
					op = new IL_ldind_r8(instruction, this);
					break;

				// 0x50 | ldind.ref | Indirect load value of type object ref as O on the stack. | Base instruction
				case Cecil.Cil.Code.Ldind_Ref:
					op = new IL_ldind_ref(instruction, this);
					break;

				// 0x51 | stind.ref | Store value of type object ref (type O) into memory at address | Base instruction
				case Cecil.Cil.Code.Stind_Ref:
					op = new IL_stind_ref(instruction, this);
					break;

				// 0x52 | stind.i1 | Store value of type int8 into memory at address | Base instruction
				case Cecil.Cil.Code.Stind_I1:
					op = new IL_stind_i1(instruction, this);
					break;

				// 0x53 | stind.i2 | Store value of type int16 into memory at address | Base instruction
				case Cecil.Cil.Code.Stind_I2:
					op = new IL_stind_i2(instruction, this);
					break;

				// 0x54 | stind.i4 | Store value of type int32 into memory at address | Base instruction
				case Cecil.Cil.Code.Stind_I4:
					op = new IL_stind_i4(instruction, this);
					break;

				// 0x55 | stind.i8 | Store value of type int64 into memory at address | Base instruction
				case Cecil.Cil.Code.Stind_I8:
					op = new IL_stind_i8(instruction, this);
					break;

				// 0x56 | stind.r4 | Store value of type float32 into memory at address | Base instruction
				case Cecil.Cil.Code.Stind_R4:
					op = new IL_stind_r4(instruction, this);
					break;

				// 0x57 | stind.r8 | Store value of type float64 into memory at address | Base instruction
				case Cecil.Cil.Code.Stind_R8:
					op = new IL_stind_r8(instruction, this);
					break;

				// 0x58 | add | Add two values, returning a new value. | Base instruction
				case Cecil.Cil.Code.Add:
					op = new IL_add(instruction, this);
					break;

				// 0x59 | sub | Subtract value2 from value1, returning a new value. | Base instruction
				case Cecil.Cil.Code.Sub:
					op = new IL_sub(instruction, this);
					break;

				// 0x5A | mul | Multiply values. | Base instruction
				case Cecil.Cil.Code.Mul:
					op = new IL_mul(instruction, this);
					break;

				// 0x5B | div | Divide two values to return a quotient or floating-point result. | Base instruction
				case Cecil.Cil.Code.Div:
					op = new IL_div(instruction, this);
					break;

				// 0x5C | div.un | Divide two values, unsigned, returning a quotient. | Base instruction
				case Cecil.Cil.Code.Div_Un:
					op = new IL_div_un(instruction, this);
					break;

				// 0x5D | rem | Remainder when dividing one value by another. | Base instruction
				case Cecil.Cil.Code.Rem:
					op = new IL_rem(instruction, this);
					break;

				// 0x5E | rem.un | Remainder when dividing one unsigned value by another. | Base instruction
				case Cecil.Cil.Code.Rem_Un:
					op = new IL_rem_un(instruction, this);
					break;

				// 0x5F | and | Bitwise AND of two integral values, returns an integral value. | Base instruction
				case Cecil.Cil.Code.And:
					op = new IL_and(instruction, this);
					break;

				// 0x60 | or | Bitwise OR of two integer values, returns an integer. | Base instruction
				case Cecil.Cil.Code.Or:
					op = new IL_or(instruction, this);
					break;

				// 0x61 | xor | Bitwise XOR of integer values, returns an integer. | Base instruction
				case Cecil.Cil.Code.Xor:
					op = new IL_xor(instruction, this);
					break;

				// 0x62 | shl | Shift an integer left (shifting in zeros), return an integer. | Base instruction
				case Cecil.Cil.Code.Shl:
					op = new IL_shl(instruction, this);
					break;

				// 0x63 | shr | Shift an integer right (shift in sign), return an integer. | Base instruction
				case Cecil.Cil.Code.Shr:
					op = new IL_shr(instruction, this);
					break;

				// 0x64 | shr.un | Shift an integer right (shift in zero), return an integer. | Base instruction
				case Cecil.Cil.Code.Shr_Un:
					op = new IL_shr_un(instruction, this);
					break;

				// 0x65 | neg | Negate value. | Base instruction
				case Cecil.Cil.Code.Neg:
					op = new IL_neg(instruction, this);
					break;

				// 0x66 | not | Bitwise complement (logical not). | Base instruction
				case Cecil.Cil.Code.Not:
					op = new IL_not(instruction, this);
					break;

				// 0x67 | conv.i1 | Convert to int8, pushing int32 on stack. | Base instruction
				case Cecil.Cil.Code.Conv_I1:
					op = new IL_conv_i1(instruction, this);
					break;

				// 0x68 | conv.i2 | Convert to int16, pushing int32 on stack. | Base instruction
				case Cecil.Cil.Code.Conv_I2:
					op = new IL_conv_i2(instruction, this);
					break;

				// 0x69 | conv.i4 | Convert to int32, pushing int32 on stack. | Base instruction
				case Cecil.Cil.Code.Conv_I4:
					op = new IL_conv_i4(instruction, this);
					break;

				// 0x6A | conv.i8 | Convert to int64, pushing int64 on stack. | Base instruction
				case Cecil.Cil.Code.Conv_I8:
					op = new IL_conv_i8(instruction, this);
					break;

				// 0x6B | conv.r4 | Convert to float32, pushing F on stack. | Base instruction
				case Cecil.Cil.Code.Conv_R4:
					op = new IL_conv_r4(instruction, this);
					break;

				// 0x6C | conv.r8 | Convert to float64, pushing F on stack. | Base instruction
				case Cecil.Cil.Code.Conv_R8:
					op = new IL_conv_r8(instruction, this);
					break;

				// 0x6D | conv.u4 | Convert to unsigned int32, pushing int32 on stack. | Base instruction
				case Cecil.Cil.Code.Conv_U4:
					op = new IL_conv_u4(instruction, this);
					break;

				// 0x6E | conv.u8 | Convert to unsigned int64, pushing int64 on stack. | Base instruction
				case Cecil.Cil.Code.Conv_U8:
					op = new IL_conv_u8(instruction, this);
					break;

				// 0x6F | callvirt | Call a method associated with an object. | Object model instruction
				case Cecil.Cil.Code.Callvirt:
					op = new IL_callvirt(instruction, this);
					break;

				// 0x70 | cpobj | Copy a value type from src to dest. | Object model instruction
				case Cecil.Cil.Code.Cpobj:
					op = new IL_cpobj(instruction, this);
					break;

				// 0x71 | ldobj | Copy the value stored at address src to the stack. | Object model instruction
				case Cecil.Cil.Code.Ldobj:
					op = new IL_ldobj(instruction, this);
					break;

				// 0x72 | ldstr | Push a string object for the literal string. | Object model instruction
				case Cecil.Cil.Code.Ldstr:
					op = new IL_ldstr(instruction, this);
					break;

				// 0x73 | newobj | Allocate an uninitialized object or value type and call ctor. | Object model instruction
				case Cecil.Cil.Code.Newobj:
					op = new IL_newobj(instruction, this);
					break;

				// 0x74 | castclass | Cast obj to class. | Object model instruction
				case Cecil.Cil.Code.Castclass:
					op = new IL_castclass(instruction, this);
					break;

				// 0x75 | isinst | Test if obj is an instance of class, returning null or an instance of that class or interface. | Object model instruction
				case Cecil.Cil.Code.Isinst:
					op = new IL_isinst(instruction, this);
					break;

				// 0x76 | conv.r.un | Convert unsigned integer to floating-point, pushing F on stack. | Base instruction
				case Cecil.Cil.Code.Conv_R_Un:
					op = new IL_conv_r_un(instruction, this);
					break;

				// 0x79 | unbox | Extract a value-type from obj, its boxed representation, and push a managed pointer to it to the top of the stack | Object model instruction
				case Cecil.Cil.Code.Unbox:
					op = new IL_unbox(instruction, this);
					break;

				// 0x7A | throw | Throw an exception. | Object model instruction
				case Cecil.Cil.Code.Throw:
					op = new IL_throw(instruction, this);
					break;

				// 0x7B | ldfld | Push the value of field of object (or value type) obj, onto the stack. | Object model instruction
				case Cecil.Cil.Code.Ldfld:
					op = new IL_ldfld(instruction, this);
					break;

				// 0x7C | ldflda | Push the address of field of object obj on the stack. | Object model instruction
				case Cecil.Cil.Code.Ldflda:
					op = new IL_ldflda(instruction, this);
					break;

				// 0x7D | stfld | Replace the value of field of the object obj with value. | Object model instruction
				case Cecil.Cil.Code.Stfld:
					op = new IL_stfld(instruction, this);
					break;

				// 0x7E | ldsfld | Push the value of the static field on the stack. | Object model instruction
				case Cecil.Cil.Code.Ldsfld:
					op = new IL_ldsfld(instruction, this);
					break;

				// 0x7F | ldsflda | Push the address of the static field, field, on the stack. | Object model instruction
				case Cecil.Cil.Code.Ldsflda:
					op = new IL_ldsflda(instruction, this);
					break;

				// 0x80 | stsfld | Replace the value of the static field with val. | Object model instruction
				case Cecil.Cil.Code.Stsfld:
					op = new IL_stsfld(instruction, this);
					break;

				// 0x81 | stobj | Store a value of type typeTok at an address. | Object model instruction
				case Cecil.Cil.Code.Stobj:
					op = new IL_stobj(instruction, this);
					break;

				// 0x82 | conv.ovf.i1.un | Convert unsigned to an int8 (on the stack as int32) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_I1_Un:
					op = new IL_conv_ovf_i1_un(instruction, this);
					break;

				// 0x83 | conv.ovf.i2.un | Convert unsigned to an int16 (on the stack as int32) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_I2_Un:
					op = new IL_conv_ovf_i2_un(instruction, this);
					break;

				// 0x84 | conv.ovf.i4.un | Convert unsigned to an int32 (on the stack as int32) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_I4_Un:
					op = new IL_conv_ovf_i4_un(instruction, this);
					break;

				// 0x85 | conv.ovf.i8.un | Convert unsigned to an int64 (on the stack as int64) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_I8_Un:
					op = new IL_conv_ovf_i8_un(instruction, this);
					break;

				// 0x86 | conv.ovf.u1.un | Convert unsigned to an unsigned int8 (on the stack as int32) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_U1_Un:
					op = new IL_conv_ovf_u1_un(instruction, this);
					break;

				// 0x87 | conv.ovf.u2.un | Convert unsigned to an unsigned int16 (on the stack as int32) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_U2_Un:
					op = new IL_conv_ovf_u2_un(instruction, this);
					break;

				// 0x88 | conv.ovf.u4.un | Convert unsigned to an unsigned int32 (on the stack as int32) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_U4_Un:
					op = new IL_conv_ovf_u4_un(instruction, this);
					break;

				// 0x89 | conv.ovf.u8.un | Convert unsigned to an unsigned int64 (on the stack as int64) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_U8_Un:
					op = new IL_conv_ovf_u8_un(instruction, this);
					break;

				// 0x8A | conv.ovf.i.un | Convert unsigned to a native int (on the stack as native int) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_I_Un:
					op = new IL_conv_ovf_i_un(instruction, this);
					break;

				// 0x8B | conv.ovf.u.un | Convert unsigned to a native unsigned int (on the stack as native int) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_U_Un:
					op = new IL_conv_ovf_u_un(instruction, this);
					break;

				// 0x8C | box | Convert a boxable value to its boxed form | Object model instruction
				case Cecil.Cil.Code.Box:
					op = new IL_box(instruction, this);
					break;

				// 0x8D | newarr | Create a new array with elements of type etype. | Object model instruction
				case Cecil.Cil.Code.Newarr:
					op = new IL_newarr(instruction, this);
					break;

				// 0x8E | ldlen | Push the length (of type native unsigned int) of array on the stack. | Object model instruction
				case Cecil.Cil.Code.Ldlen:
					op = new IL_ldlen(instruction, this);
					break;

				// 0x8F | ldelema | Load the address of element at index onto the top of the stack. | Object model instruction
				case Cecil.Cil.Code.Ldelema:
					op = new IL_ldelema(instruction, this);
					break;

				// 0x90 | ldelem.i1 | Load the element with type int8 at index onto the top of the stack as an int32. | Object model instruction
				case Cecil.Cil.Code.Ldelem_I1:
					op = new IL_ldelem_i1(instruction, this);
					break;

				// 0x91 | ldelem.u1 | Load the element with type unsigned int8 at index onto the top of the stack as an int32. | Object model instruction
				case Cecil.Cil.Code.Ldelem_U1:
					op = new IL_ldelem_u1(instruction, this);
					break;

				// 0x92 | ldelem.i2 | Load the element with type int16 at index onto the top of the stack as an int32. | Object model instruction
				case Cecil.Cil.Code.Ldelem_I2:
					op = new IL_ldelem_i2(instruction, this);
					break;

				// 0x93 | ldelem.u2 | Load the element with type unsigned int16 at index onto the top of the stack as an int32. | Object model instruction
				case Cecil.Cil.Code.Ldelem_U2:
					op = new IL_ldelem_u2(instruction, this);
					break;

				// 0x94 | ldelem.i4 | Load the element with type int32 at index onto the top of the stack as an int32. | Object model instruction
				case Cecil.Cil.Code.Ldelem_I4:
					op = new IL_ldelem_i4(instruction, this);
					break;

				// 0x95 | ldelem.u4 | Load the element with type unsigned int32 at index onto the top of the stack as an int32. | Object model instruction
				case Cecil.Cil.Code.Ldelem_U4:
					op = new IL_ldelem_u4(instruction, this);
					break;

				// 0x96 | ldelem.i8 | Load the element with type int64 at index onto the top of the stack as an int64. | Object model instruction
				case Cecil.Cil.Code.Ldelem_I8:
					op = new IL_ldelem_i8(instruction, this);
					break;

				// 0x97 | ldelem.i | Load the element with type native int at index onto the top of the stack as a native int. | Object model instruction
				case Cecil.Cil.Code.Ldelem_I:
					op = new IL_ldelem_i(instruction, this);
					break;

				// 0x98 | ldelem.r4 | Load the element with type float32 at index onto the top of the stack as an F | Object model instruction
				case Cecil.Cil.Code.Ldelem_R4:
					op = new IL_ldelem_r4(instruction, this);
					break;

				// 0x99 | ldelem.r8 | Load the element with type float64 at index onto the top of the stack as an F. | Object model instruction
				case Cecil.Cil.Code.Ldelem_R8:
					op = new IL_ldelem_r8(instruction, this);
					break;

				// 0x9A | ldelem.ref | Load the element at index onto the top of the stack as an O. The type of the O is the same as the element type of the array pushed on the CIL stack. | Object model instruction
				case Cecil.Cil.Code.Ldelem_Ref:
					op = new IL_ldelem_ref(instruction, this);
					break;

				// 0x9B | stelem.i | Replace array element at index with the i value on the stack. | Object model instruction
				case Cecil.Cil.Code.Stelem_I:
					op = new IL_stelem_i(instruction, this);
					break;

				// 0x9C | stelem.i1 | Replace array element at index with the int8 value on the stack. | Object model instruction
				case Cecil.Cil.Code.Stelem_I1:
					op = new IL_stelem_i1(instruction, this);
					break;

				// 0x9D | stelem.i2 | Replace array element at index with the int16 value on the stack. | Object model instruction
				case Cecil.Cil.Code.Stelem_I2:
					op = new IL_stelem_i2(instruction, this);
					break;

				// 0x9E | stelem.i4 | Replace array element at index with the int32 value on the stack. | Object model instruction
				case Cecil.Cil.Code.Stelem_I4:
					op = new IL_stelem_i4(instruction, this);
					break;

				// 0x9F | stelem.i8 | Replace array element at index with the int64 value on the stack. | Object model instruction
				case Cecil.Cil.Code.Stelem_I8:
					op = new IL_stelem_i8(instruction, this);
					break;

				// 0xA0 | stelem.r4 | Replace array element at index with the float32 value on the stack. | Object model instruction
				case Cecil.Cil.Code.Stelem_R4:
					op = new IL_stelem_r4(instruction, this);
					break;

				// 0xA1 | stelem.r8 | Replace array element at index with the float64 value on the stack. | Object model instruction
				case Cecil.Cil.Code.Stelem_R8:
					op = new IL_stelem_r8(instruction, this);
					break;

				// 0xA2 | stelem.ref | Replace array element at index with the ref value on the stack. | Object model instruction
				case Cecil.Cil.Code.Stelem_Ref:
					op = new IL_stelem_ref(instruction, this);
					break;

				// 0xA3 | ldelem | Load the element at index onto the top of the stack. | Object model instruction
				case Cecil.Cil.Code.Ldelem_Any:
					op = new IL_ldelem(instruction, this);
					break;

				// 0xA4 | stelem | Replace array element at index with the value on the stack | Object model instruction
				case Cecil.Cil.Code.Stelem_Any:
					op = new IL_stelem(instruction, this);
					break;

				// 0xA5 | unbox.any | Extract a value-type from obj, its boxed representation, and copy to the top of the stack | Object model instruction
				case Cecil.Cil.Code.Unbox_Any:
					op = new IL_unbox_any(instruction, this);
					break;

				// 0xB3 | conv.ovf.i1 | Convert to an int8 (on the stack as int32) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_I1:
					op = new IL_conv_ovf_i1(instruction, this);
					break;

				// 0xB4 | conv.ovf.u1 | Convert to an unsigned int8 (on the stack as int32) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_U1:
					op = new IL_conv_ovf_u1(instruction, this);
					break;

				// 0xB5 | conv.ovf.i2 | Convert to an int16 (on the stack as int32) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_I2:
					op = new IL_conv_ovf_i2(instruction, this);
					break;

				// 0xB6 | conv.ovf.u2 | Convert to an unsigned int16 (on the stack as int32) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_U2:
					op = new IL_conv_ovf_u2(instruction, this);
					break;

				// 0xB7 | conv.ovf.i4 | Convert to an int32 (on the stack as int32) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_I4:
					op = new IL_conv_ovf_i4(instruction, this);
					break;

				// 0xB8 | conv.ovf.u4 | Convert to an unsigned int32 (on the stack as int32) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_U4:
					op = new IL_conv_ovf_u4(instruction, this);
					break;

				// 0xB9 | conv.ovf.i8 | Convert to an int64 (on the stack as int64) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_I8:
					op = new IL_conv_ovf_i8(instruction, this);
					break;

				// 0xBA | conv.ovf.u8 | Convert to an unsigned int64 (on the stack as int64) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_U8:
					op = new IL_conv_ovf_u8(instruction, this);
					break;

				// 0xC2 | refanyval | Push the address stored in a typed reference. | Object model instruction
				case Cecil.Cil.Code.Refanyval:
					op = new IL_refanyval(instruction, this);
					break;

				// 0xC3 | ckfinite | Throw ArithmeticException if value is not a finite number. | Base instruction
				case Cecil.Cil.Code.Ckfinite:
					op = new IL_ckfinite(instruction, this);
					break;

				// 0xC6 | mkrefany | Push a typed reference to ptr of type class onto the stack. | Object model instruction
				case Cecil.Cil.Code.Mkrefany:
					op = new IL_mkrefany(instruction, this);
					break;

				// 0xD0 | ldtoken | Convert metadata token to its runtime representation. | Object model instruction
				case Cecil.Cil.Code.Ldtoken:
					op = new IL_ldtoken(instruction, this);
					break;

				// 0xD1 | conv.u2 | Convert to unsigned int16, pushing int32 on stack. | Base instruction
				case Cecil.Cil.Code.Conv_U2:
					op = new IL_conv_u2(instruction, this);
					break;

				// 0xD2 | conv.u1 | Convert to unsigned int8, pushing int32 on stack. | Base instruction
				case Cecil.Cil.Code.Conv_U1:
					op = new IL_conv_u1(instruction, this);
					break;

				// 0xD3 | conv.i | Convert to native int, pushing native int on stack. | Base instruction
				case Cecil.Cil.Code.Conv_I:
					op = new IL_conv_i(instruction, this);
					break;

				// 0xD4 | conv.ovf.i | Convert to a native int (on the stack as native int) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_I:
					op = new IL_conv_ovf_i(instruction, this);
					break;

				// 0xD5 | conv.ovf.u | Convert to a native unsigned int (on the stack as native int) and throw an exception on overflow. | Base instruction
				case Cecil.Cil.Code.Conv_Ovf_U:
					op = new IL_conv_ovf_u(instruction, this);
					break;

				// 0xD6 | add.ovf | Add signed integer values with overflow check. | Base instruction
				case Cecil.Cil.Code.Add_Ovf:
					op = new IL_add_ovf(instruction, this);
					break;

				// 0xD7 | add.ovf.un | Add unsigned integer values with overflow check. | Base instruction
				case Cecil.Cil.Code.Add_Ovf_Un:
					op = new IL_add_ovf_un(instruction, this);
					break;

				// 0xD8 | mul.ovf | Multiply signed integer values. Signed result shall fit in same size | Base instruction
				case Cecil.Cil.Code.Mul_Ovf:
					op = new IL_mul_ovf(instruction, this);
					break;

				// 0xD9 | mul.ovf.un | Multiply unsigned integer values. Unsigned result shall fit in same size | Base instruction
				case Cecil.Cil.Code.Mul_Ovf_Un:
					op = new IL_mul_ovf_un(instruction, this);
					break;

				// 0xDA | sub.ovf | Subtract native int from a native int. Signed result shall fit in same size | Base instruction
				case Cecil.Cil.Code.Sub_Ovf:
					op = new IL_sub_ovf(instruction, this);
					break;

				// 0xDB | sub.ovf.un | Subtract native unsigned int from a native unsigned int. Unsigned result shall fit in same size. | Base instruction
				case Cecil.Cil.Code.Sub_Ovf_Un:
					op = new IL_sub_ovf_un(instruction, this);
					break;

				// 0xDC | endfinally | End finally clause of an exception block. | Base instruction
				case Cecil.Cil.Code.Endfinally:
					op = new IL_endfinally(instruction, this);
					break;

				// 0xDD | leave | Exit a protected region of code. | Base instruction
				case Cecil.Cil.Code.Leave:
					op = new IL_leave(instruction, this);
					break;

				// 0xDE | leave.s | Exit a protected region of code, short form. | Base instruction
				case Cecil.Cil.Code.Leave_S:
					op = new IL_leave_s(instruction, this);
					break;

				// 0xDF | stind.i | Store value of type native int into memory at address | Base instruction
				case Cecil.Cil.Code.Stind_I:
					op = new IL_stind_i(instruction, this);
					break;

				// 0xE0 | conv.u | Convert to native unsigned int, pushing native int on stack. | Base instruction
				case Cecil.Cil.Code.Conv_U:
					op = new IL_conv_u(instruction, this);
					break;

				// 0xFE00 | arglist | Return argument list handle for the current method. | Base instruction
				case Cecil.Cil.Code.Arglist:
					op = new IL_arglist(instruction, this);
					break;

				// 0xFE01 | ceq | Push 1 (of type int32) if value1 equals value2, else push 0. | Base instruction
				case Cecil.Cil.Code.Ceq:
					op = new IL_ceq(instruction, this);
					break;

				// 0xFE02 | cgt | Push 1 (of type int32) if value1 > value2, else push 0. | Base instruction
				case Cecil.Cil.Code.Cgt:
					op = new IL_cgt(instruction, this);
					break;

				// 0xFE03 | cgt.un | Push 1 (of type int32) if value1 > value2, unsigned or unordered, else push 0. | Base instruction
				case Cecil.Cil.Code.Cgt_Un:
					op = new IL_cgt_un(instruction, this);
					break;

				// 0xFE04 | clt | Push 1 (of type int32) if value1 < value2, else push 0. | Base instruction
				case Cecil.Cil.Code.Clt:
					op = new IL_clt(instruction, this);
					break;

				// 0xFE05 | clt.un | Push 1 (of type int32) if value1 < value2, unsigned or unordered, else push 0. | Base instruction
				case Cecil.Cil.Code.Clt_Un:
					op = new IL_clt_un(instruction, this);
					break;

				// 0xFE06 | ldftn | Push a pointer to a method referenced by method, on the stack. | Base instruction
				case Cecil.Cil.Code.Ldftn:
					op = new IL_ldftn(instruction, this);
					break;

				// 0xFE07 | ldvirtftn | Push address of virtual method on the stack. | Object model instruction
				case Cecil.Cil.Code.Ldvirtftn:
					op = new IL_ldvirtftn(instruction, this);
					break;

				// 0xFE09 | ldarg | Load argument numbered num onto the stack. | Base instruction
				case Cecil.Cil.Code.Ldarg:
					op = new IL_ldarg(instruction, this);
					break;

				// 0xFE0A | ldarga | Fetch the address of argument argNum. | Base instruction
				case Cecil.Cil.Code.Ldarga:
					op = new IL_ldarga(instruction, this);
					break;

				// 0xFE0B | starg | Store value to the argument numbered num. | Base instruction
				case Cecil.Cil.Code.Starg:
					op = new IL_starg(instruction, this);
					break;

				// 0xFE0C | ldloc | Load local variable of index indx onto stack. | Base instruction
				case Cecil.Cil.Code.Ldloc:
					op = new IL_ldloc(instruction, this);
					break;

				// 0xFE0D | ldloca | Load address of local variable with index indx. | Base instruction
				case Cecil.Cil.Code.Ldloca:
					op = new IL_ldloca(instruction, this);
					break;

				// 0xFE0E | stloc | Pop a value from stack into local variable indx. | Base instruction
				case Cecil.Cil.Code.Stloc:
					op = new IL_stloc(instruction, this);
					break;

				// 0xFE0F | localloc | Allocate space from the local memory pool. | Base instruction
				case Cecil.Cil.Code.Localloc:
					op = new IL_localloc(instruction, this);
					break;

				// 0xFE11 | endfilter | End an exception handling filter clause. | Base instruction
				case Cecil.Cil.Code.Endfilter:
					op = new IL_endfilter(instruction, this);
					break;

				// 0xFE12 | unaligned. | Subsequent pointer instruction might be unaligned. | Prefix to instruction
				case Cecil.Cil.Code.Unaligned:
					op = new IL_unaligned_(instruction, this);
					break;

				// 0xFE13 | volatile. | Subsequent pointer reference is volatile. | Prefix to instruction
				case Cecil.Cil.Code.Volatile:
					op = new IL_volatile_(instruction, this);
					break;

				// 0xFE14 | tail. | Subsequent call terminates current method | Prefix to instruction
				case Cecil.Cil.Code.Tail:
					op = new IL_tail_(instruction, this);
					break;

				// 0xFE15 | initobj | Initialize the value at address dest. | Object model instruction
				case Cecil.Cil.Code.Initobj:
					op = new IL_initobj(instruction, this);
					break;

				// 0xFE16 | constrained. | Call a virtual method on a type constrained to be type T | Prefix to instruction
				case Cecil.Cil.Code.Constrained:
					op = new IL_constrained_(instruction, this);
					break;

				// 0xFE17 | cpblk | Copy data from memory to memory. | Base instruction
				case Cecil.Cil.Code.Cpblk:
					op = new IL_cpblk(instruction, this);
					break;

				// 0xFE18 | initblk | Set all bytes in a block of memory to a given byte value. | Base instruction
				case Cecil.Cil.Code.Initblk:
					op = new IL_initblk(instruction, this);
					break;

				// 0xFE19 | no. | The specified fault check(s) normally performed as part of the execution of the subsequent instruction can/shall be skipped. | Prefix to instruction
				case Cecil.Cil.Code.No:
					op = new IL_no_(instruction, this);
					break;

				// 0xFE1A | rethrow | Rethrow the current exception. | Object model instruction
				case Cecil.Cil.Code.Rethrow:
					op = new IL_rethrow(instruction, this);
					break;

				// 0xFE1C | sizeof | Push the size, in bytes, of a type as an unsigned int32. | Object model instruction
				case Cecil.Cil.Code.Sizeof:
					op = new IL_sizeof(instruction, this);
					break;

				// 0xFE1D | refanytype | Push the type token stored in a typed reference. | Object model instruction
				case Cecil.Cil.Code.Refanytype:
					op = new IL_refanytype(instruction, this);
					break;

				// 0xFE1E | readonly. | Specify that the subsequent array address operation performs no type check at runtime, and that it returns a controlled-mutability managed pointer | Prefix to instruction
				case Cecil.Cil.Code.Readonly:
					op = new IL_readonly_(instruction, this);
					break;

				default:
					throw new Exception($"Instruction {instruction.OpCode.Code} NOT implemente! ");
			}

			return op;
		}

		public void MC6801_MethodDefinition(StringBuilder sb)
		{
			sb.AppendLine("* -----------------------------------------------------------------------------");
			sb.AppendLine($"* MethodDefinition: [{this.CecilMethodDefinition.Name}][{this.CecilMethodDefinition.FullName}]");
			sb.AppendLine("* -----------------------------------------------------------------------------");

			sb.AppendLine($"{this.ParentType.CecilType.Name}_{this.CecilMethodDefinition.Name.Replace('.', '_')} equ $");

			foreach (var instruction in this.Instructions)
				instruction.MC6801_UnOptimized_Code(sb);

			sb.AppendLine();
		}

		public void MC6809_MethodDefinition(StringBuilder sb)
		{
			sb.AppendLine("* -----------------------------------------------------------------------------");
			sb.AppendLine($"* MethodDefinition: [{this.CecilMethodDefinition.Name}][{this.CecilMethodDefinition.FullName}]");
			sb.AppendLine("* -----------------------------------------------------------------------------");

			sb.AppendLine($"{this.ParentType.CecilType.Name}_{this.CecilMethodDefinition.Name.Replace('.', '_')} equ $");

			foreach (var instruction in this.Instructions)
				instruction.MC6809_UnOptimized_Code(sb);

			sb.AppendLine();
		}

		public void MC68000_MethodDefinition(StringBuilder sb)
		{
			sb.AppendLine("* -----------------------------------------------------------------------------");
			sb.AppendLine($"* MethodDefinition: [{this.CecilMethodDefinition.Name}][{this.CecilMethodDefinition.FullName}]");
			sb.AppendLine("* -----------------------------------------------------------------------------");

			sb.AppendLine($"{this.ParentType.CecilType.Name}_{this.CecilMethodDefinition.Name.Replace('.', '_')} equ $");

			foreach (var instruction in this.Instructions)
				instruction.MC68000_UnOptimized_Code(sb);

			sb.AppendLine();
		}

		public void Z80_MethodDefinition(StringBuilder sb)
		{
			sb.AppendLine("* -----------------------------------------------------------------------------");
			sb.AppendLine($"* MethodDefinition: [{this.CecilMethodDefinition.Name}][{this.CecilMethodDefinition.FullName}]");
			sb.AppendLine("* -----------------------------------------------------------------------------");

			sb.AppendLine($"{this.ParentType.CecilType.Name}_{this.CecilMethodDefinition.Name.Replace('.', '_')} equ $");

			foreach (var instruction in this.Instructions)
				instruction.Z80_UnOptimized_Code(sb);

			sb.AppendLine();
		}

		public void MOS6502_MethodDefinition(StringBuilder sb)
		{
			sb.AppendLine("* -----------------------------------------------------------------------------");
			sb.AppendLine($"* MethodDefinition: [{this.CecilMethodDefinition.Name}][{this.CecilMethodDefinition.FullName}]");
			sb.AppendLine("* -----------------------------------------------------------------------------");

			sb.AppendLine($"{this.ParentType.CecilType.Name}_{this.CecilMethodDefinition.Name.Replace('.', '_')} equ $");

			foreach (var instruction in this.Instructions)
				instruction.Z80_UnOptimized_Code(sb);

			sb.AppendLine();
		}
	}
}
