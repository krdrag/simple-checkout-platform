namespace SCP.Transaction.Domain.Exceptions
{
    public class IncorrectSagaStateException : Exception
    {
        public IncorrectSagaStateException()
            : base("Incorrect Saga State.")
        {

        }
    }
}
