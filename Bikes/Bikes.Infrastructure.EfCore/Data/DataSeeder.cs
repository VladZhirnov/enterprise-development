using Bikes.Core.Entities;
using Bikes.Core.Enums;

namespace Bikes.Infrastructure.EfCore.Data;

/// <summary>
/// Provides seed data for the database
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Gets seed data for bike models
    /// </summary>
    public static List<BikeModel> BikeModels =>
    [
        new() {
            Id = 1,
            Type = BikeType.Sport,
            WheelSize = 28,
            MaxWeight = 120,
            Weight = 9.5,
            BrakeType = "Disc",
            ModelYear = 2023,
            PricePerHour = 15.0m
        },
        new() {
            Id = 2,
            Type = BikeType.Mountain,
            WheelSize = 26,
            MaxWeight = 130,
            Weight = 11.2,
            BrakeType = "Hydraulic",
            ModelYear = 2023,
            PricePerHour = 12.0m
        },
        new() {
            Id = 3,
            Type = BikeType.Hybrid,
            WheelSize = 27.5,
            MaxWeight = 110,
            Weight = 10.8,
            BrakeType = "Rim",
            ModelYear = 2023,
            PricePerHour = 8.0m
        },
        new() {
            Id = 4,
            Type = BikeType.Road,
            WheelSize = 28,
            MaxWeight = 100,
            Weight = 7.8,
            BrakeType = "Caliper",
            ModelYear = 2023,
            PricePerHour = 18.0m
        },
        new() {
            Id = 5,
            Type = BikeType.Sport,
            WheelSize = 29,
            MaxWeight = 125,
            Weight = 8.9,
            BrakeType = "Disc",
            ModelYear = 2024,
            PricePerHour = 20.0m
        },
        new() {
            Id = 6,
            Type = BikeType.Electric,
            WheelSize = 26,
            MaxWeight = 120,
            Weight = 18.5,
            BrakeType = "Disc",
            ModelYear = 2023,
            PricePerHour = 25.0m
        },
        new() {
            Id = 7,
            Type = BikeType.Bmx,
            WheelSize = 20,
            MaxWeight = 90,
            Weight = 10.2,
            BrakeType = "U-Brake",
            ModelYear = 2023,
            PricePerHour = 10.0m
        },
        new() {
            Id = 8,
            Type = BikeType.Sport,
            WheelSize = 24,
            MaxWeight = 80,
            Weight = 7.5,
            BrakeType = "Rim",
            ModelYear = 2023,
            PricePerHour = 12.0m
        },
        new() {
            Id = 9,
            Type = BikeType.Mountain,
            WheelSize = 27.5,
            MaxWeight = 135,
            Weight = 12.1,
            BrakeType = "Hydraulic",
            ModelYear = 2024,
            PricePerHour = 14.0m
        },
        new() {
            Id = 10,
            Type = BikeType.Road,
            WheelSize = 28,
            MaxWeight = 95,
            Weight = 7.2,
            BrakeType = "Caliper",
            ModelYear = 2023,
            PricePerHour = 16.0m
        }
    ];

    /// <summary>
    /// Gets seed data for bikes
    /// </summary>
    public static List<Bike> Bikes =>
    [
        new() {
            Id = 1,
            SerialNumber = "SN000001",
            Color = "Red",
            ModelId = 1
        },
        new() {
            Id = 2,
            SerialNumber = "SN000002",
            Color = "Blue",
            ModelId = 2
        },
        new() {
            Id = 3,
            SerialNumber = "SN000003",
            Color = "Green",
            ModelId = 3
        },
        new() {
            Id = 4,
            SerialNumber = "SN000004",
            Color = "Black",
            ModelId = 4
        },
        new() {
            Id = 5,
            SerialNumber = "SN000005",
            Color = "White",
            ModelId = 5
        },
        new() {
            Id = 6,
            SerialNumber = "SN000006",
            Color = "Yellow",
            ModelId = 6
        },
        new() {
            Id = 7,
            SerialNumber = "SN000007",
            Color = "Silver",
            ModelId = 7
        },
        new() {
            Id = 8,
            SerialNumber = "SN000008",
            Color = "Orange",
            ModelId = 8
        },
        new() {
            Id = 9,
            SerialNumber = "SN000009",
            Color = "Purple",
            ModelId = 9
        },
        new() {
            Id = 10,
            SerialNumber = "SN000010",
            Color = "Red",
            ModelId = 10
        }
    ];

    /// <summary>
    /// Gets seed data for clients
    /// </summary>
    public static List<Client> Clients =>
    [
        new() {
            Id = 1,
            FirstName = "Ivan",
            LastName = "Petrov",
            MiddleName = "Sergeevich",
            Phone = "+7-911-111-11-11"
        },
        new() {
            Id = 2,
            FirstName = "Maria",
            LastName = "Ivanova",
            MiddleName = "Alexandrovna",
            Phone = "+7-922-222-22-22"
        },
        new() {
            Id = 3,
            FirstName = "Alexey",
            LastName = "Sidorov",
            MiddleName = "Vladimirovich",
            Phone = "+7-933-333-33-33"
        },
        new() {
            Id = 4,
            FirstName = "Elena",
            LastName = "Kuznetsova",
            MiddleName = "Dmitrievna",
            Phone = "+7-944-444-44-44"
        },
        new() {
            Id = 5,
            FirstName = "Dmitry",
            LastName = "Smirnov",
            MiddleName = "Igorevich",
            Phone = "+7-955-555-55-55"
        },
        new() {
            Id = 6,
            FirstName = "Olga",
            LastName = "Popova",
            MiddleName = "Sergeevna",
            Phone = "+7-966-666-66-66"
        },
        new() {
            Id = 7,
            FirstName = "Sergey",
            LastName = "Vasiliev",
            MiddleName = "Petrovich",
            Phone = "+7-977-777-77-77"
        },
        new() {
            Id = 8,
            FirstName = "Anna",
            LastName = "Novikova",
            MiddleName = "Andreevna",
            Phone = "+7-988-888-88-88"
        },
        new() {
            Id = 9,
            FirstName = "Pavel",
            LastName = "Fedorov",
            MiddleName = "Nikolaevich",
            Phone = "+7-999-999-99-99"
        },
        new() {
            Id = 10,
            FirstName = "Natalya",
            LastName = "Morozova",
            MiddleName = "Viktorovna",
            Phone = "+7-900-000-00-00"
        }
    ];

    /// <summary>
    /// Gets seed data for rentals
    /// </summary>
    public static List<Rental> Rentals =>
    [
        new() {
            Id = 1,
            StartTime = new DateTime(2024, 1, 10, 9, 0, 0),
            DurationHours = 10,
            ClientId = 1,
            BikeId = 1
        },
        new() {
            Id = 2,
            StartTime = new DateTime(2024, 1, 12, 14, 30, 0),
            DurationHours = 5,
            ClientId = 2,
            BikeId = 1
        },
        new() {
            Id = 3,
            StartTime = new DateTime(2024, 1, 15, 10, 0, 0),
            DurationHours = 8,
            ClientId = 3,
            BikeId = 1
        },
        new() {
            Id = 4,
            StartTime = new DateTime(2024, 1, 18, 16, 0, 0),
            DurationHours = 12,
            ClientId = 4,
            BikeId = 2
        },
        new() {
            Id = 5,
            StartTime = new DateTime(2024, 1, 20, 11, 0, 0),
            DurationHours = 6,
            ClientId = 5,
            BikeId = 2
        },
        new() {
            Id = 6,
            StartTime = new DateTime(2024, 1, 22, 13, 0, 0),
            DurationHours = 3,
            ClientId = 6,
            BikeId = 2
        },
        new() {
            Id = 7,
            StartTime = new DateTime(2024, 1, 25, 15, 30, 0),
            DurationHours = 7,
            ClientId = 7,
            BikeId = 3
        },
        new() {
            Id = 8,
            StartTime = new DateTime(2024, 1, 28, 9, 30, 0),
            DurationHours = 9,
            ClientId = 8,
            BikeId = 3
        },
        new() {
            Id = 9,
            StartTime = new DateTime(2024, 2, 1, 12, 0, 0),
            DurationHours = 2,
            ClientId = 9,
            BikeId = 4
        },
        new() {
            Id = 10,
            StartTime = new DateTime(2024, 2, 3, 17, 0, 0),
            DurationHours = 4,
            ClientId = 10,
            BikeId = 4
        },
        new() {
            Id = 11,
            StartTime = new DateTime(2024, 2, 5, 10, 0, 0),
            DurationHours = 15,
            ClientId = 1,
            BikeId = 5
        },
        new() {
            Id = 12,
            StartTime = new DateTime(2024, 2, 8, 14, 0, 0),
            DurationHours = 8,
            ClientId = 1,
            BikeId = 5
        },
        new() {
            Id = 13,
            StartTime = new DateTime(2024, 2, 10, 16, 30, 0),
            DurationHours = 6,
            ClientId = 1,
            BikeId = 6
        },
        new() {
            Id = 14,
            StartTime = new DateTime(2024, 2, 12, 11, 0, 0),
            DurationHours = 11,
            ClientId = 1,
            BikeId = 7
        },
        new() {
            Id = 15,
            StartTime = new DateTime(2024, 2, 15, 13, 0, 0),
            DurationHours = 1,
            ClientId = 2,
            BikeId = 8
        },
        new() {
            Id = 16,
            StartTime = new DateTime(2024, 2, 18, 15, 0, 0),
            DurationHours = 5,
            ClientId = 2,
            BikeId = 9
        },
        new() {
            Id = 17,
            StartTime = new DateTime(2024, 2, 20, 9, 0, 0),
            DurationHours = 20,
            ClientId = 2,
            BikeId = 10
        },
        new() {
            Id = 18,
            StartTime = new DateTime(2024, 2, 22, 12, 30, 0),
            DurationHours = 12,
            ClientId = 6,
            BikeId = 10
        },
        new() {
            Id = 19,
            StartTime = new DateTime(2024, 2, 25, 14, 0, 0),
            DurationHours = 8,
            ClientId = 6,
            BikeId = 10
        },
        new() {
            Id = 20,
            StartTime = new DateTime(2024, 2, 28, 16, 0, 0),
            DurationHours = 10,
            ClientId = 3,
            BikeId = 10
        }
    ];
}