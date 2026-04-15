namespace QuantityMeasurementAppBusinessLayer.Exceptions
{
    public class UnitMismatchException : BusinessLayerException
    {
        public UnitMismatchException(string message) : base(message,400) { }
    }
}