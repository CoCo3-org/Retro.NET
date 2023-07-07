# Retro.NET
### CIL (formerly MSIL) native Ahead-of-Time (AoT) compiler for 6x09 / 680x / 6502 / Z80 / 68k CPU's

---

7/7/2023 12:15 AM - After a long hiatus – back to working on this project. Will likely be moving it to my personal GitHub account as it is not going to be CoCo specific.  I will surely move the website content for it to my new website [Retro-Tech.Xyz](https://retro-tech.xyz/).  First thing I need to do getting back into it is ramp up the unit-tests. I need to make a list of Tests I need. I surely need hundreds if not thousands of them. The only way to make systematic progress on a project this big is through Unit & Integration tests. My mistake has been not to concentrate on Unit Tests and Integration Tests!!

---

2/27/2023 4:00 PM - *Change of plans again:* removed __Retro.IDE__ and will concetrate solely on this project until I have version 0.1 at least!!

---

~12/11/2022 9:55 PM - *Please see* __Retro.IDE__ as all future development on this project will be done there!!~

---

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
