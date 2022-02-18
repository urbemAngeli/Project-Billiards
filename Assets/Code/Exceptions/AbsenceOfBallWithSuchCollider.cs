using System;

namespace Code.Exceptions
{
    public class AbsenceOfBallWithSuchCollider : Exception
    {
        public AbsenceOfBallWithSuchCollider(string message) : base(message) 
        {
        }
    }
}