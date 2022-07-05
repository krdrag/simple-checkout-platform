namespace SCP.Common.Exceptions
{
    public class MessageException : BaseException
    {
        public MessageException() : base("MESSAGEQUEUE_GENERAL", "Received response invalid.")
        {
        }
    }
}
