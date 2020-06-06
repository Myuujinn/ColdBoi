using System;

namespace ColdBoi.CPU
{
    public class UnknownInstructionException : Exception
    {
        public UnknownInstructionException()
        {
            
        }
        
        public UnknownInstructionException(string message) : base(message)
        {
            
        }
        
        public UnknownInstructionException(string message, Exception inner) : base(message, inner)
        {
            
        }
        
    }
}