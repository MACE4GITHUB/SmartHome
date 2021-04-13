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
                : _sensor.MaxValue > _sensor.MinValue
                    ? GetProgressNormalValueFrom(value)
                    : GetRegressNormalValueFrom(value);

        private decimal GetProgressNormalValueFrom(decimal currentValue) =>
            Round(_sensor.MinNormalValue
                  - ((_sensor.MinNormalValue - _sensor.MaxNormalValue) / (_sensor.MinValue - _sensor.MaxValue))
                  * (_sensor.MinValue - currentValue));

        private decimal GetRegressNormalValueFrom(decimal currentValue) =>
            Round(_sensor.MinNormalValue
                  + ((_sensor.MaxNormalValue - _sensor.MinNormalValue) / (_sensor.MinValue - _sensor.MaxValue))
                  * (_sensor.MinValue - currentValue));

        private decimal Round(decimal value) =>
            Math.Round(value, _sensor.Precision, MidpointRounding.ToEven);
    }
}
