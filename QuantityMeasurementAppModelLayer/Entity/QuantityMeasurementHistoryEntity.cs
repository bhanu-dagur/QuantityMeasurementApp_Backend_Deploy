namespace QuantityMeasurementAppModelLayer.Entity
{
    public class QuantityMeasurementHistoryEntity
    {
       
        public int Id { get; set; }

        public double InputValue1 { get; set; }

        public string InputUnit1 { get; set; }

        public double? InputValue2 { get; set; }

        public string? InputUnit2 { get; set; }

        public string TargetUnit { get; set; }
        public string Operation { get; set; }
        public double ResultValue { get; set; }

        public string ResultUnit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }
    }
}