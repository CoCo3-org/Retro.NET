
namespace CPU.M6502
{
    public class LDX_Instruction : Instruction
    {
        public LDX_Instruction(CodeBlock codeBlock)
            : base(codeBlock)
        {
        }

        public override string Mnemonic { get { return "LDX"; } }
    }
}
