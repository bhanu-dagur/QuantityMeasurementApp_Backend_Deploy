namespace QuantityMeasurementAppBusinessLayer.Exceptions
{
    public class BusinessLayerException : Exception
    {
        public int StatusCode { get; set; }
        public BusinessLayerException(string message,int statusCode = 400) : base(message) { 
            StatusCode = statusCode;
        }
    }
}