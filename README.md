# Retro.NET
### CIL (formerly MSIL) native Ahead-of-Time (AoT) compiler for 6x09 / 680x / 6502 / Z80 / 68k CPU's

10/13/2020 9:30 PM – The 4 main classes are wrappers for the very good Mono.Cecil.

```
MSIL.ModuleDefinition --> wraps Cecil.ModuleDefinition
MSIL.TypeDefinition --> wraps Cecil.TypeDefinition
MSIL.MethodDefinition --> wraps Cecil.ModuleDefinition
MSIL.Instruction --> wraps Cecil.Cil.Instruction
```
Each instruction inherits from MSIL.Instruction
```
MSIL.Instruction
|-- IL_add
|-- IL_add_ovf
ect -- some 219 instructions 
```
Then we will have some runtime classes.
```
MSIL.MSILStack --> stack for runtime
```

10/13/2020 9:29 PM -- Initial source code upload.  It's in the early stages still.
