using Microsoft.EntityFrameworkCore;
using SmartHome.Configuration.Infrastructure.Models;

namespace SmartHome.Configuration.Infrastructure.Extentions
{

    /// <summary>
    /// Determines ModelBuilder Extensions.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Inserts default data into database.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var sensorEntity = modelBuilder.Entity<SensorDb>();
            var sensorTypeEntity = modelBuilder.Entity<SensorTypeDb>();

            sensorTypeEntity
                .HasMany(x => x.Sensors)
                .WithOne(x => x.SensorType)
                .HasForeignKey(x => x.SensorTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            sensorTypeEntity
                .HasData(
                    new SensorTypeDb[]
                    {
                        new(){
                            SensorTypeId = "f03e0aa8-e4fb-4b10-ab86-e56525706cd1",
                            TypeName = "Thermometer Fahrenheit",
                            TypeDescription = "Standard english Fahrenheit thermometer.",
                            MinValue = 32m,
                            MinNormalValue = 0m,
                            MaxValue = 212m,
                            MaxNormalValue = 100m
                        },
                        new(){
                            SensorTypeId = "7d44ccaa-29b7-47c3-ab10-31e6af0dc976",
                            TypeName = "Thermometer Kelvin",
                            TypeDescription = "Standard english Kelvin thermometer.",
                            MinValue = 273.15m,
                            MinNormalValue = 0m,
                            MaxValue = 373.15m,
                            MaxNormalValue = 100m
                        }
                    }
                );

            sensorEntity.HasData(new SensorDb[]
            {
                        new ()
                        {
                            SensorTypeId = "f03e0aa8-e4fb-4b10-ab86-e56525706cd1",
                            SensorId = "4437b083-cbec-4a5d-9b9a-e97eef83ccc4",
                            Name = "Thermometer Fahrenheit 1",
                            Description = "Some thermometer Fahrenheit in the room 1.",
                            IsEnabled = true,
                            Precision = 1
                        },
                        new ()
                        {
                            SensorTypeId = "f03e0aa8-e4fb-4b10-ab86-e56525706cd1",
                            SensorId = "b1efc9a9-31fb-4fc3-b95e-610eb2bf6c35",
                            Name = "Thermometer Fahrenheit 2",
                            Description = "Some thermometer Fahrenheit in the room 1.",
                            IsEnabled = true,
                            Precision = 1
                        },
                        new ()
                        {
                            SensorTypeId = "f03e0aa8-e4fb-4b10-ab86-e56525706cd1",
                            SensorId = "7d5df23a-00f8-41b9-b20d-e7fc0cd8e9c1",
                            Name = "Thermometer Fahrenheit 3",
                            Description = "Some thermometer Fahrenheit in the room 1.",
                            IsEnabled = true,
                            Precision = 1
                        },
                        new ()
                        {
                            SensorTypeId = "f03e0aa8-e4fb-4b10-ab86-e56525706cd1",
                            SensorId = "56d9baa6-4b4c-4bc9-bf9f-a9b3b32900fa",
                            Name = "Thermometer Fahrenheit 4",
                            Description = "Some thermometer Fahrenheit in the room 2.",
                            IsEnabled = true,
                            Precision = 1
                        },
                        new ()
                        {
                            SensorTypeId = "f03e0aa8-e4fb-4b10-ab86-e56525706cd1",
                            SensorId = "95e5a7d2-e00b-4992-bce5-67976d8fb1bc",
                            Name = "Thermometer Fahrenheit 5",
                            Description = "Some thermometer Fahrenheit in the room 2.",
                            IsEnabled = true,
                            Precision = 1
                        },
                        new ()
                        {
                            SensorTypeId = "f03e0aa8-e4fb-4b10-ab86-e56525706cd1",
                            SensorId = "00302a46-dcd9-4dd5-b83f-e20e065d0fd1",
                            Name = "Thermometer Fahrenheit 6",
                            Description = "Some thermometer Fahrenheit in the room 2.",
                            IsEnabled = false,
                            Precision = 1
                        },
                        new ()
                        {
                            SensorTypeId = "7d44ccaa-29b7-47c3-ab10-31e6af0dc976",
                            SensorId = "1970b5b3-fce8-45f3-b737-566814c5142d",
                            Name = "Thermometer Kelvin 1",
                            Description = "Some thermometer Kelvin in the room 3.",
                            IsEnabled = true,
                            Precision = 1
                        },
                        new ()
                        {
                            SensorTypeId = "7d44ccaa-29b7-47c3-ab10-31e6af0dc976",
                            SensorId = "03d98811-ff1b-4ed3-b156-552b9e7bf51a",
                            Name = "Thermometer Kelvin 2",
                            Description = "Some thermometer Kelvin in the room 3.",
                            IsEnabled = true,
                            Precision = 1
                        },
                        new ()
                        {
                            SensorTypeId = "7d44ccaa-29b7-47c3-ab10-31e6af0dc976",
                            SensorId = "fa67ba5a-b3ee-4081-a5a6-63e55322317b",
                            Name = "Thermometer Kelvin 3",
                            Description = "Some thermometer Kelvin in the room 3.",
                            IsEnabled = true,
                            Precision = 1
                        },
                        new ()
                        {
                            SensorTypeId = "7d44ccaa-29b7-47c3-ab10-31e6af0dc976",
                            SensorId = "b89c9df7-6457-4138-b5e5-4227b95f983e",
                            Name = "Thermometer Kelvin 4",
                            Description = "Some thermometer Kelvin in the room 4.",
                            IsEnabled = true,
                            Precision = 1
                        },
                        new ()
                        {
                            SensorTypeId = "7d44ccaa-29b7-47c3-ab10-31e6af0dc976",
                            SensorId = "29cf1e3c-3d83-435d-a439-bd03d904d483",
                            Name = "Thermometer Kelvin 5",
                            Description = "Some thermometer Kelvin in the room 4.",
                            IsEnabled = true,
                            Precision = 1
                        },
                        new ()
                        {
                            SensorTypeId = "7d44ccaa-29b7-47c3-ab10-31e6af0dc976",
                            SensorId = "4e7f6114-5594-4ebf-a17c-291edc4c8326",
                            Name = "Thermometer Kelvin 6",
                            Description = "Some thermometer Kelvin in the room 5.",
                            IsEnabled = false,
                            Precision = 1
                        }
            });
        }
    }
}