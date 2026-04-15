namespace QuantityMeasurementAppBusinessLayer.Exceptions
{
    public class UnknowUnitException : BusinessLayerException
    {
        public UnknowUnitException(string unit,string type) : base($"{unit} is not a valid {type} unit",400) { } 
    }
}