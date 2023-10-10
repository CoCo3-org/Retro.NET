# Retro.NET
### CIL (formerly MSIL) native Ahead-of-Time (AoT) compiler for 6x09 / 680x / 6502 / Z80 / 68k CPU's

---

10/10/2023 3:30 AM: Back at it again. 

Keep getting overwhelmed and I can’t get motivated! Try and try again! Failure is NOT an option!

Move the project back to [github.com/CoCo3-org](https://github.com/CoCo3-org/Retro.NET) and [CoCo3.org](https://coco3.org/retro-net/). 

---

8/6/2023 1:35 AM: I need a plan were I start off simple w/ a blank program – the “Do Nothing” program.  Then next I do a “HelloWorld”. 

From there I keep adding C# language features keeping each sample with a new feature as simple as possible. This will lay the groundwork for more complex program examples up until I have covered all the C# language features and all CIL instructions. 

Every step needs to be tested w/ TDD and using the TDD strategy of only adding the bare minimum amount of code for each test to pass.  

At the program executable level I will run an Integration Tests. For the internal workings I will run Unit Tests. 

At the same time I will create a GUI app that I will be able to visually see the operation of the internal CIL and each CPU instruction emulated, so we can follow along and make progress easier, and debug easier.  

Between the Integration Tests and the Unit Tests and the GUI emulation layer I should not lose sight of where I am in the development process. Which is a problem.

---

7/20/2023 5:30 AM: - I will want to create a stand-alone `EmuLib` library.   

---

7/7/2023 12:15 AM - After a long hiatus. I need to do getting back into it and ramp up the unit-tests. Make a list of Tests I need. My mistake has been not to concentrate on Unit Tests and Test Driven Development (TDD)!!

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
