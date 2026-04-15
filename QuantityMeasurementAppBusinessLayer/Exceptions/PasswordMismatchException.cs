namespace QuantityMeasurementAppBusinessLayer.Exceptions
{
    public class PasswordMismatchException : BusinessLayerException
    {
        public PasswordMismatchException(string message) : base(message,401) { }
    }
}