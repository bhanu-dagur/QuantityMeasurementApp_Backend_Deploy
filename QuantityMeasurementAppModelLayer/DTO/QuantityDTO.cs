namespace QuantityMeasurementAppModelLayer.DTO
{
    public class QuantityDTO
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int EnumIndex { get; set; }

        public QuantityDTO(double value, string unit, int enumIndex)
        {
            Value = value;
            Unit = unit;
            EnumIndex = enumIndex;
        }
    }
}

