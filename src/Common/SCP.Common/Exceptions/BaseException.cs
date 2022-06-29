namespace SCP.Common.Exceptions
{
    internal class BaseException : Exception
    {
        public string ErrorCode { get; set; } = string.Empty;
        public string TranslatedMessage { get; set; } = string.Empty;
    }
}
