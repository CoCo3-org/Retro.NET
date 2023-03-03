
namespace CPU.M6502
{
    public class LDY_Instruction : Instruction
    {
        public LDY_Instruction(CodeBlock codeBlock)
            : base(codeBlock)
        {
        }

        public override string Mnemonic { get { return "LDY"; } }
    }
}
