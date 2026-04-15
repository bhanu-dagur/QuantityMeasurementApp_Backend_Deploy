namespace QuantityMeasurementAppModelLayer.DTO;

public class AirthmeticDTO
{
    public QuantityDTO Quantity1 { get; set; }
    public QuantityDTO Quantity2 { get; set; }

    public AirthmeticDTO(QuantityDTO quantity1, QuantityDTO quantity2)
    {
        this.Quantity1 = quantity1;
        this.Quantity2 = quantity2;
    }
}