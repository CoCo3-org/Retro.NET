//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace CPU.M6502
{
    public class RTS_Implied : Instruction, IImplied
    {
        public RTS_Implied(CodeBlock codeBlock)
            : base(codeBlock)
        {
        }

        public override byte OpCode { get { return 0x60; } }

        public override string Mnemonic { get { return "RTS"; } }
    }
}
