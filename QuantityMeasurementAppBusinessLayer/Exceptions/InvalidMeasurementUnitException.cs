namespace QuantityMeasurementAppBusinessLayer.Exceptions
{
    public class InvalidMeasurementUnitException : BusinessLayerException
    {
        public InvalidMeasurementUnitException(string message) : base(message,400) { }
    }
}