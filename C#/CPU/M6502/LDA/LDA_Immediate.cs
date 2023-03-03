//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace CPU.M6502
{
    public class LDA_Immediate : LDA_Instruction, IImmediate
    {
        public LDA_Immediate(CodeBlock codeBlock)
            : base(codeBlock)
        {
        }
    }
}
