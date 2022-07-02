namespace SCP.Common.Exceptions
{
    public class BaseException : Exception
    {
        public string ErrorCode { get; set; } = string.Empty;
        public string TranslatedMessage { get; set; } = string.Empty;

        public BaseException(string errorCode, string translatedMessage)
        {
            ErrorCode = errorCode;
            TranslatedMessage = translatedMessage;
        }
    }
}
