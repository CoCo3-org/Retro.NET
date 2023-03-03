//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace CPU.M6502
{
    public class NOP_Implied : Instruction, IImplied
    {
        public NOP_Implied(CodeBlock codeBlock)
            : base(codeBlock)
        {
        }

        public override byte OpCode { get { return 0xEA; } }

        public override string Mnemonic { get { return "NOP"; } }
    }
}
