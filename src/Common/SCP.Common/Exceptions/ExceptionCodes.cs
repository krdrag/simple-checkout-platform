namespace SCP.Common.Exceptions
{
    public class ExceptionCode
    {
        public ExceptionCode(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; set; }
        public string Message { get; set; }
    }

    public static class ExceptionCodes
    {
        public static BaseException Get(ExceptionCode exCode)
        {
            return new BaseException(exCode.Code, exCode.Message);
        }

        // SAGA
        public static ExceptionCode SagaIncorrectState = new("SAGA_INCORRECT-STATE", "Incorrect transaction state.");

        // Session
        public static ExceptionCode SessionNotFound = new("SESSION_NOT-FOUND", "Session not found.");

        //Transaction
        public static ExceptionCode TransactionNotFound = new("TRANSACTION_NOT-FOUND", "Transaction not found.");
        public static ExceptionCode TransactionNotPaid = new("TRANSACTION_TOTAL-NOT-PAID", "Total not paid correctly.");

    }
}
