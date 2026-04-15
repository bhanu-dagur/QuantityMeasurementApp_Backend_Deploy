using QuantityMeasurementAppModelLayer.Core;
using QuantityMeasurementAppModelLayer.Entity;
using QuantityMeasurementAppModelLayer.DTO;
using QuantityMeasurementAppModelLayer.Units;
using QuantityMeasurementAppRepositoryLayer.Interface;
using QuantityMeasurementAppBusinessLayer.Interface;
using QuantityMeasurementAppBusinessLayer.Exceptions;

namespace QuantityMeasurementAppBusinessLayer.Services
{
    public class MeasurementService : IMeasurementService
    {
        // private readonly IMeasurementHistoryRepository _repository;
        private readonly IMeasurementRepository _repository;

        public MeasurementService(IMeasurementRepository repository)
        {
            _repository = repository;
        }

        private Enum ParseUnit(Type unitType, string unit)
        {
            try
            {
                return (Enum)Enum.Parse(unitType, unit);
            }
            catch (ArgumentException)
            {
                throw new UnknowUnitException(unit, unitType.Name);
            }
            
        }
        public async Task<QuantityDTO> PerformConversion(QuantityDTO q, string toUnit,string userId) 
        {
            Type unitType = GetUnitFromIndex(q.EnumIndex);
            // Enum unit = (Enum)Enum.Parse(unitType, q.Unit);
            // Enum targetUnit = (Enum)Enum.Parse(unitType, toUnit);

            Enum unit = ParseUnit(unitType, q.Unit);
            Enum targetUnit = ParseUnit(unitType, toUnit);
            
            Quantity quantity = new Quantity(q.Value, unit);

            QuantityDTO res = quantity.ConvertTo(targetUnit);
            if(!string.IsNullOrEmpty(userId))
            await AddToHistory(q, null, res, "Conversion",userId);

            return res;
        }

        public async Task<QuantityDTO> PerformAddition(QuantityDTO q1, QuantityDTO q2, string toUnit,string userId)
        {
            if(q1.EnumIndex != q2.EnumIndex) throw new UnitMismatchException("Cannot add units of different types");

            Type unitType = GetUnitFromIndex(q1.EnumIndex);
            // Enum unit1 =(Enum) Enum.Parse(unitType, q1.Unit);
            // Enum unit2 =(Enum) Enum.Parse(unitType, q2.Unit);
            // Enum targetUnit = (Enum)Enum.Parse(unitType, toUnit);

            Enum unit1 = ParseUnit(unitType, q1.Unit);
            Enum unit2 = ParseUnit(unitType, q2.Unit);
            Enum targetUnit = ParseUnit(unitType, toUnit);

            Quantity quantity1 = new Quantity(q1.Value, unit1);
            Quantity quantity2 = new Quantity(q2.Value, unit2);
            QuantityDTO result = quantity1.Add(quantity2, targetUnit);

            if(!string.IsNullOrEmpty(userId)) 
            await AddToHistory(q1, q2, result, "Addition",userId);

            return result;
        }

        public async Task<QuantityDTO> PerformSubtraction(QuantityDTO q1, QuantityDTO q2, string toUnit,string userId)
        {
            if (q1.EnumIndex != q2.EnumIndex) throw new UnitMismatchException("Cannot subtract units of different types");

            Type unitType = GetUnitFromIndex(q1.EnumIndex);

            Enum unit1 = ParseUnit(unitType, q1.Unit);
            Enum unit2 = ParseUnit(unitType, q2.Unit);
            Enum targetUnit = ParseUnit(unitType, toUnit);

            Quantity quantity1 = new Quantity(q1.Value, unit1);
            Quantity quantity2 = new Quantity(q2.Value, unit2);

            QuantityDTO result = quantity1.Subtract(quantity2, targetUnit);

            if(!string.IsNullOrEmpty(userId))
            await AddToHistory(q1, q2, result, "Subtraction",userId); 
            return result;
        }

        public async Task<QuantityDTO> PerformDivision(QuantityDTO q1,QuantityDTO q2, string toUnit,string userId)
        {
            if (q1.EnumIndex != q2.EnumIndex) throw new UnitMismatchException("Cannot divide units of different types");

            Type unitType = GetUnitFromIndex(q1.EnumIndex);

            Enum unit1 = ParseUnit(unitType, q1.Unit);
            Enum unit2 = ParseUnit(unitType, q2.Unit);
            Enum targetUnit = ParseUnit(unitType, toUnit);

            Quantity quantity1 = new Quantity(q1.Value, unit1);
            Quantity quantity2 = new Quantity(q2.Value, unit2);
            if(quantity2.Value == 0) throw new DivideByZeroException("Cannot divide by zero");

            QuantityDTO result = quantity1.Divide(quantity2, targetUnit);

            if(!string.IsNullOrEmpty(userId))
            await AddToHistory(q1, q2, result, "Division",userId);
            return result;
        }

        public async Task<bool> CheckEquality(QuantityDTO q1, QuantityDTO q2,string userId)
        {
            Type unitType = GetUnitFromIndex(q1.EnumIndex);

            Enum unit1 = ParseUnit(unitType, q1.Unit);
            Enum unit2 = ParseUnit(unitType, q2.Unit);

            double val1 = q1.Value;
            double val2 = q2.Value;
            var quantity1 = new Quantity(val1, unit1);
            var quantity2 = new Quantity(val2, unit2);

            if(!string.IsNullOrEmpty(userId))
            await AddToHistory(q1, q2, q1, "Equality check",userId);
            Console.WriteLine(quantity1.Equals(quantity2));
            return quantity1.Equals(quantity2);
        }

        private async Task AddToHistory(QuantityDTO q1, QuantityDTO? q2, QuantityDTO result,string operation,string userId)
        {
            QuantityMeasurementHistoryEntity history = new QuantityMeasurementHistoryEntity();

            history.InputValue1 = q1.Value;
            history.InputUnit1 = q1.Unit;
            history.InputValue2 = q2 == null ? 0 : q2.Value;
            history.InputUnit2 = q2 == null ? "" : q2.Unit;
            history.TargetUnit = result.Unit;
            history.Operation = operation;
            history.ResultValue = result.Value;
            history.ResultUnit = result.Unit;
            history.UserId = int.Parse(userId);
            await _repository.SaveHistory(history);
        }
        private Type GetUnitFromIndex(int index)
        {
            return index switch
            {
                1 => typeof(LengthUnit),
                2 => typeof(WeightUnit),
                3 => typeof(VolumeUnit),
                4 => typeof(TemperatureUnit),
                _ => throw new InvalidMeasurementUnitException("Invalid unit index in measurement service"),
            };
        }

    }
}