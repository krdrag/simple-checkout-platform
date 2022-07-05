namespace SCP.Common.Exceptions
{
    public class BaseException : Exception
    {
        public string ErrorCode { get; set; } = string.Empty;
        public string? TranslatedMessage { get; set; } = string.Empty;

        public BaseException(string errorCode, string message, string? translatedMessage = null)
            :base(message)
        {
            ErrorCode = errorCode;
            TranslatedMessage = translatedMessage;
        }
    }
}
