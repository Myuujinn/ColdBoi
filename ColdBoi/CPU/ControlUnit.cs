using System.Collections.Generic;
using ColdBoi.CPU.BigInstructions;
using ColdBoi.CPU.Instructions;

namespace ColdBoi.CPU
{
    public class ControlUnit
    {
        private const byte BIG_INSTRUCTION_OPCODE = 0xcb;

        private Dictionary<byte, Instruction> InstructionMap { get; }
        private Dictionary<ushort, BigInstruction> BigInstructionMap { get; }
        private Processor Processor { get; }

        public ControlUnit(Processor processor)
        {
            this.InstructionMap = new Dictionary<byte, Instruction>();
            this.BigInstructionMap = new Dictionary<ushort, BigInstruction>();
            this.Processor = processor;
            
            this.InstructionMap.Add(AddAA.OPCODE, new AddAA(processor));
            this.InstructionMap.Add(AddHlBc.OPCODE, new AddHlBc(processor));
            this.InstructionMap.Add(AddHlDe.OPCODE, new AddHlDe(processor));
            this.InstructionMap.Add(AddHlHl.OPCODE, new AddHlHl(processor));
            this.InstructionMap.Add(AddHlSp.OPCODE, new AddHlSp(processor));
            
            this.InstructionMap.Add(AndA.OPCODE, new AndA(processor));
            this.InstructionMap.Add(AndB.OPCODE, new AndB(processor));
            this.InstructionMap.Add(AndC.OPCODE, new AndC(processor));
            this.InstructionMap.Add(AndD.OPCODE, new AndD(processor));
            this.InstructionMap.Add(AndE.OPCODE, new AndE(processor));
            this.InstructionMap.Add(AndH.OPCODE, new AndH(processor));
            this.InstructionMap.Add(AndL.OPCODE, new AndL(processor));
            this.InstructionMap.Add(AndHl.OPCODE, new AndHl(processor));
            this.InstructionMap.Add(AndN.OPCODE, new AndN(processor));

            this.InstructionMap.Add(CallNn.OPCODE, new CallNn(processor));
            
            this.InstructionMap.Add(CpN.OPCODE, new CpN(processor));
            
            this.InstructionMap.Add(Cpl.OPCODE, new Cpl(processor));
            
            this.InstructionMap.Add(DecA.OPCODE, new DecA(processor));
            this.InstructionMap.Add(DecB.OPCODE, new DecB(processor));
            this.InstructionMap.Add(DecBc.OPCODE, new DecBc(processor));
            this.InstructionMap.Add(DecC.OPCODE, new DecC(processor));
            this.InstructionMap.Add(DecD.OPCODE, new DecD(processor));
            this.InstructionMap.Add(DecDe.OPCODE, new DecDe(processor));
            this.InstructionMap.Add(DecE.OPCODE, new DecE(processor));
            this.InstructionMap.Add(DecH.OPCODE, new DecH(processor));
            this.InstructionMap.Add(DecHl.OPCODE, new DecHl(processor));
            this.InstructionMap.Add(DecL.OPCODE, new DecL(processor));
            this.InstructionMap.Add(DecSp.OPCODE, new DecSp(processor));
            
            this.InstructionMap.Add(Di.OPCODE, new Di(processor));
            
            this.InstructionMap.Add(Ei.OPCODE, new Ei(processor));
            
            this.InstructionMap.Add(IncA.OPCODE, new IncA(processor));
            this.InstructionMap.Add(IncB.OPCODE, new IncB(processor));
            this.InstructionMap.Add(IncC.OPCODE, new IncC(processor));
            this.InstructionMap.Add(IncD.OPCODE, new IncD(processor));
            this.InstructionMap.Add(IncE.OPCODE, new IncE(processor));
            this.InstructionMap.Add(IncH.OPCODE, new IncH(processor));
            this.InstructionMap.Add(IncHl.OPCODE, new IncHl(processor));
            this.InstructionMap.Add(IncL.OPCODE, new IncL(processor));
            
            this.InstructionMap.Add(JpHl.OPCODE, new JpHl(processor));
            this.InstructionMap.Add(JpNn.OPCODE, new JpNn(processor));
            
            this.InstructionMap.Add(JrNz.OPCODE, new JrNz(processor));
            this.InstructionMap.Add(JrZ.OPCODE, new JrZ(processor));

            this.InstructionMap.Add(LdAA.OPCODE, new LdAA(processor));
            this.InstructionMap.Add(LdAB.OPCODE, new LdAB(processor));
            this.InstructionMap.Add(LdABc.OPCODE, new LdABc(processor));
            this.InstructionMap.Add(LdAC.OPCODE, new LdAC(processor));
            this.InstructionMap.Add(LdAD.OPCODE, new LdAD(processor));
            this.InstructionMap.Add(LdADe.OPCODE, new LdADe(processor));
            this.InstructionMap.Add(LdAE.OPCODE, new LdAE(processor));
            this.InstructionMap.Add(LdAFF00N.OPCODE, new LdAFF00N(processor));
            this.InstructionMap.Add(LdAH.OPCODE, new LdAH(processor));
            this.InstructionMap.Add(LdAHl.OPCODE, new LdAHl(processor));
            this.InstructionMap.Add(LdAL.OPCODE, new LdAL(processor));
            this.InstructionMap.Add(LdAN.OPCODE, new LdAN(processor));
            this.InstructionMap.Add(LdANn.OPCODE, new LdANn(processor));
            this.InstructionMap.Add(LdBA.OPCODE, new LdBA(processor));
            this.InstructionMap.Add(LdBB.OPCODE, new LdBB(processor));
            this.InstructionMap.Add(LdBC.OPCODE, new LdBC(processor));
            this.InstructionMap.Add(LdBcNn.OPCODE, new LdBcNn(processor));
            this.InstructionMap.Add(LdBD.OPCODE, new LdBD(processor));
            this.InstructionMap.Add(LdBE.OPCODE, new LdBE(processor));
            this.InstructionMap.Add(LdBH.OPCODE, new LdBH(processor));
            this.InstructionMap.Add(LdBHl.OPCODE, new LdBHl(processor));
            this.InstructionMap.Add(LdBL.OPCODE, new LdBL(processor));
            this.InstructionMap.Add(LdBN.OPCODE, new LdBN(processor));
            this.InstructionMap.Add(LdCA.OPCODE, new LdCA(processor));
            this.InstructionMap.Add(LdCB.OPCODE, new LdCB(processor));
            this.InstructionMap.Add(LdCC.OPCODE, new LdCC(processor));
            this.InstructionMap.Add(LdCD.OPCODE, new LdCD(processor));
            this.InstructionMap.Add(LdCE.OPCODE, new LdCE(processor));
            this.InstructionMap.Add(LdCH.OPCODE, new LdCH(processor));
            this.InstructionMap.Add(LdCHl.OPCODE, new LdCHl(processor));
            this.InstructionMap.Add(LdCL.OPCODE, new LdCL(processor));
            this.InstructionMap.Add(LdCN.OPCODE, new LdCN(processor));
            this.InstructionMap.Add(LdDeNn.OPCODE, new LdDeNn(processor));
            this.InstructionMap.Add(LddHlA.OPCODE, new LddHlA(processor));
            this.InstructionMap.Add(LdDB.OPCODE, new LdDB(processor));
            this.InstructionMap.Add(LdDC.OPCODE, new LdDC(processor));
            this.InstructionMap.Add(LdDD.OPCODE, new LdDD(processor));
            this.InstructionMap.Add(LdDE.OPCODE, new LdDE(processor));
            this.InstructionMap.Add(LdDH.OPCODE, new LdDH(processor));
            this.InstructionMap.Add(LdDHl.OPCODE, new LdDHl(processor));
            this.InstructionMap.Add(LdDL.OPCODE, new LdDL(processor));
            this.InstructionMap.Add(LdDN.OPCODE, new LdDN(processor));
            this.InstructionMap.Add(LdEA.OPCODE, new LdEA(processor));
            this.InstructionMap.Add(LdEB.OPCODE, new LdEB(processor));
            this.InstructionMap.Add(LdEC.OPCODE, new LdEC(processor));
            this.InstructionMap.Add(LdED.OPCODE, new LdED(processor));
            this.InstructionMap.Add(LdEE.OPCODE, new LdEE(processor));
            this.InstructionMap.Add(LdEH.OPCODE, new LdEH(processor));
            this.InstructionMap.Add(LdEHl.OPCODE, new LdEHl(processor));
            this.InstructionMap.Add(LdEL.OPCODE, new LdEL(processor));
            this.InstructionMap.Add(LdFF00CA.OPCODE, new LdFF00CA(processor));
            this.InstructionMap.Add(LdFF00NA.OPCODE, new LdFF00NA(processor));
            this.InstructionMap.Add(LdHB.OPCODE, new LdHB(processor));
            this.InstructionMap.Add(LdHC.OPCODE, new LdHC(processor));
            this.InstructionMap.Add(LdHD.OPCODE, new LdHD(processor));
            this.InstructionMap.Add(LdHE.OPCODE, new LdHE(processor));
            this.InstructionMap.Add(LdHH.OPCODE, new LdHH(processor));
            this.InstructionMap.Add(LdHHl.OPCODE, new LdHHl(processor));
            this.InstructionMap.Add(LdHL.OPCODE, new LdHL(processor));
            this.InstructionMap.Add(LdLB.OPCODE, new LdLB(processor));
            this.InstructionMap.Add(LdLC.OPCODE, new LdLC(processor));
            this.InstructionMap.Add(LdLD.OPCODE, new LdLD(processor));
            this.InstructionMap.Add(LdLE.OPCODE, new LdLE(processor));
            this.InstructionMap.Add(LdLH.OPCODE, new LdLH(processor));
            this.InstructionMap.Add(LdLHl.OPCODE, new LdLHl(processor));
            this.InstructionMap.Add(LdLL.OPCODE, new LdLL(processor));
            this.InstructionMap.Add(LdHlN.OPCODE, new LdHlN(processor));
            this.InstructionMap.Add(LdHlNn.OPCODE, new LdHlNn(processor));
            this.InstructionMap.Add(LdiAHl.OPCODE, new LdiAHl(processor));
            this.InstructionMap.Add(LdNnA.OPCODE, new LdNnA(processor));
            this.InstructionMap.Add(LdSpNn.OPCODE, new LdSpNn(processor));
            
            this.InstructionMap.Add(Nop.OPCODE, new Nop(processor));
            
            this.InstructionMap.Add(OrB.OPCODE, new OrB(processor));
            this.InstructionMap.Add(OrC.OPCODE, new OrC(processor));

            this.InstructionMap.Add(PopHl.OPCODE, new PopHl(processor));

            this.InstructionMap.Add(PushAf.OPCODE, new PushAf(processor));
            this.InstructionMap.Add(PushBc.OPCODE, new PushBc(processor));
            this.InstructionMap.Add(PushDe.OPCODE, new PushDe(processor));
            this.InstructionMap.Add(PushHl.OPCODE, new PushHl(processor));
            
            this.InstructionMap.Add(Ret.OPCODE, new Ret(processor));
            this.InstructionMap.Add(Rst28.OPCODE, new Rst28(processor));
            
            this.InstructionMap.Add(XorA.OPCODE, new XorA(processor));
            this.InstructionMap.Add(XorC.OPCODE, new XorC(processor));
            
            this.BigInstructionMap.Add(SwapA.OPCODE, new SwapA(processor));
            
            this.BigInstructionMap.Add(SlaA.OPCODE, new SlaA(processor));
            this.BigInstructionMap.Add(SlaB.OPCODE, new SlaB(processor));
            this.BigInstructionMap.Add(SlaC.OPCODE, new SlaC(processor));
            this.BigInstructionMap.Add(SlaD.OPCODE, new SlaD(processor));
            this.BigInstructionMap.Add(SlaE.OPCODE, new SlaE(processor));
            this.BigInstructionMap.Add(SlaH.OPCODE, new SlaH(processor));
            this.BigInstructionMap.Add(SlaL.OPCODE, new SlaL(processor));
            this.BigInstructionMap.Add(SlaHl.OPCODE, new SlaHl(processor));
        }

        private bool IsBigInstruction(byte b)
        {
            return b == BIG_INSTRUCTION_OPCODE;
        }

        public IInstruction FetchBigInstruction()
        {
            var lowerByteOpCode = this.Processor.Walk();
            var opCode = (ushort) (lowerByteOpCode + (BIG_INSTRUCTION_OPCODE << 8));

            var isValid = this.BigInstructionMap.TryGetValue(opCode, out var instruction);
            if (!isValid)
                throw new UnknownInstructionException($"0x{this.Processor.Registers.PC.Value:X4}: 0x{opCode:X4}");

            return instruction;
        }

        public IInstruction Fetch()
        {
            var opCode = this.Processor.Walk();

            if (IsBigInstruction(opCode))
                return FetchBigInstruction();

            var isValid = this.InstructionMap.TryGetValue(opCode, out var instruction);
            if (!isValid)
                throw new UnknownInstructionException($"0x{this.Processor.Registers.PC.Value:X4}: 0x{opCode:X2}");

            return instruction;
        }

        public byte[] FetchOperands(byte operandLength)
        {
            var operands = new byte[operandLength];

            for (var i = 0; i < operandLength; i++)
            {
                operands[i] = this.Processor.Walk();
            }

            return operands;
        }

        public void Execute(IInstruction instruction, byte[] operands)
        {
            instruction.Execute(operands);
        }
    }
}