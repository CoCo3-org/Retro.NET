# Retro.NET
CIL (formerly MSIL) native compiler for MC6801 / MC6809 / HD6309 / MC68k CPU's

There are not a lot of classes … the 4 most important ones are wrappers for the very good Mono.Cecil.

```
MSIL.ModuleDefinition --> wraps Cecil.ModuleDefinition
MSIL.TypeDefinition --> wraps Cecil.TypeDefinition
MSIL.MethodDefinition --> wraps Cecil.ModuleDefinition
MSIL.Instruction --> wraps Cecil.Cil.Instruction
|-- IL_add
|-- IL_add_ovf
... ect ... some 219 instructions 
MSIL.MSILStack --> stack for runtime

```

10/13/2020 9:30:20 PM – I need to document the classes. Oh gads! There are NO Tests???  NO wonder I have not made progress on this in forever.  I am becoming more and more convinced that Unit Testing is where it’s at for gauging and making progress; even though it may not seem that way short term. 

10/13/2020 9:29:27 PM – Please understand this is a code dump of this project.  It’s in the early, early stages of an idea still (let’s put it that way!)
