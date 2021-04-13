using SmartHome.Configuration.Abstractions.Models;
using System;

namespace SmartHome.Data.Infrastructure.Abstractions.Models
{
    public class SensorDataConverter
    {
        private readonly Sensor _sensor;

        public SensorDataConverter(Sensor sensor)
        {
            _sensor = sensor;
        }

        public decimal NormalValueFrom(decimal value) =>
            _sensor.MaxValue == _sensor.MinValue
                ? throw new ArithmeticException("Abnormal sensor configuration.")
                : Round(_sensor.MinNormalValue
                        + (_sensor.MaxNormalValue - _sensor.MinNormalValue) / (_sensor.MaxValue - _sensor.MinValue)
                        * (value - _sensor.MinValue));

        private decimal Round(decimal value) =>
            Math.Round(value, _sensor.Precision, MidpointRounding.ToEven);
    }
}
