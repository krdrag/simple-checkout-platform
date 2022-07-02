namespace SCP.Common.Exceptions
{
    public class IncorrectSagaStateException : BaseException
    {
        public IncorrectSagaStateException() : base("SAGA_INCORERCT-STATE", "Incorrect saga state")
        {
        }
    }
}
