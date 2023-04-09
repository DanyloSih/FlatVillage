using System;

namespace FlatVillage.Generators
{
    public class AttemptsRunOutException : Exception
    {
        public AttemptsRunOutException() : base("The attempts are over!")
        {
        }
    }
}
