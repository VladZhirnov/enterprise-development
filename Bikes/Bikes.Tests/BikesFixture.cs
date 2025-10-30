using Bikes.Domain.Enum;
using Bikes.Domain.Models;

namespace Bikes.Tests;

/// <summary>
/// A fixture for providing test data to all tests
/// </summary>
public class BikesFixture
{
    public readonly List<BikeModel> BikeModels;
    public readonly List<Bike> Bikes;
    public readonly List<Client> Clients;
    public readonly List<Rental> Rentals;

    /// <summary>
    /// Initializing test data
    /// </summary>
    public BikesFixture()
    {
        // Initialize BikeModels
        BikeModels =
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
                Type = BikeType.BMX,
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

        // Initialize Clients
        Clients =
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

        // Initialize Bikes
        Bikes =
        [
            new() {
                Id = 1,
                SerialNumber = "SN000001",
                Color = "Red",
                Model = BikeModels[0]
            },
            new() {
                Id = 2,
                SerialNumber = "SN000002",
                Color = "Blue",
                Model = BikeModels[1]
            },
            new() {
                Id = 3,
                SerialNumber = "SN000003",
                Color = "Green",
                Model = BikeModels[2]
            },
            new() {
                Id = 4,
                SerialNumber = "SN000004",
                Color = "Black",
                Model = BikeModels[3]
            },
            new() {
                Id = 5,
                SerialNumber = "SN000005",
                Color = "White",
                Model = BikeModels[4]
            },
            new() {
                Id = 6,
                SerialNumber = "SN000006",
                Color = "Yellow",
                Model = BikeModels[5]
            },
            new() {
                Id = 7,
                SerialNumber = "SN000007",
                Color = "Silver",
                Model = BikeModels[6]
            },
            new() {
                Id = 8,
                SerialNumber = "SN000008",
                Color = "Orange",
                Model = BikeModels[7]
            },
            new() {
                Id = 9,
                SerialNumber = "SN000009",
                Color = "Purple",
                Model = BikeModels[8]
            },
            new() {
                Id = 10,
                SerialNumber = "SN000010",
                Color = "Red",
                Model = BikeModels[9]
            }
        ];

        // Initialize Rentals
        Rentals =
        [
            new() {
                Id = 1,
                StartTime = new DateTime(2024, 1, 10, 9, 0, 0),
                DurationHours = 10,
                Client = Clients[0],
                Bike = Bikes[0]
            },
            new() {
                Id = 2,
                StartTime = new DateTime(2024, 1, 12, 14, 30, 0),
                DurationHours = 5,
                Client = Clients[1],
                Bike = Bikes[0]
            },
            new() {
                Id = 3,
                StartTime = new DateTime(2024, 1, 15, 10, 0, 0),
                DurationHours = 8,
                Client = Clients[2],
                Bike = Bikes[0]
            },
            new() {
                Id = 4,
                StartTime = new DateTime(2024, 1, 18, 16, 0, 0),
                DurationHours = 12,
                Client = Clients[3],
                Bike = Bikes[1]
            },
            new() {
                Id = 5,
                StartTime = new DateTime(2024, 1, 20, 11, 0, 0),
                DurationHours = 6,
                Client = Clients[4],
                Bike = Bikes[1]
            },
            new() {
                Id = 6,
                StartTime = new DateTime(2024, 1, 22, 13, 0, 0),
                DurationHours = 3,
                Client = Clients[5],
                Bike = Bikes[1]
            },
            new() {
                Id = 7,
                StartTime = new DateTime(2024, 1, 25, 15, 30, 0),
                DurationHours = 7,
                Client = Clients[6],
                Bike = Bikes[2]
            },
            new() {
                Id = 8,
                StartTime = new DateTime(2024, 1, 28, 9, 30, 0),
                DurationHours = 9,
                Client = Clients[7],
                Bike = Bikes[2]
            },
            new() {
                Id = 9,
                StartTime = new DateTime(2024, 2, 1, 12, 0, 0),
                DurationHours = 2,
                Client = Clients[8],
                Bike = Bikes[3]
            },
            new() {
                Id = 10,
                StartTime = new DateTime(2024, 2, 3, 17, 0, 0),
                DurationHours = 4,
                Client = Clients[9],
                Bike = Bikes[3]
            },
            new() {
                Id = 11,
                StartTime = new DateTime(2024, 2, 5, 10, 0, 0),
                DurationHours = 15,
                Client = Clients[0],
                Bike = Bikes[4]
            },
            new() {
                Id = 12,
                StartTime = new DateTime(2024, 2, 8, 14, 0, 0),
                DurationHours = 8,
                Client = Clients[0],
                Bike = Bikes[4]
            },
            new() {
                Id = 13,
                StartTime = new DateTime(2024, 2, 10, 16, 30, 0),
                DurationHours = 6,
                Client = Clients[0],
                Bike = Bikes[5]
            },
            new() {
                Id = 14,
                StartTime = new DateTime(2024, 2, 12, 11, 0, 0),
                DurationHours = 11,
                Client = Clients[0],
                Bike = Bikes[6]
            },
            new() {
                Id = 15,
                StartTime = new DateTime(2024, 2, 15, 13, 0, 0),
                DurationHours = 1,
                Client = Clients[1],
                Bike = Bikes[7]
            },
            new() {
                Id = 16,
                StartTime = new DateTime(2024, 2, 18, 15, 0, 0),
                DurationHours = 5,
                Client = Clients[1],
                Bike = Bikes[8]
            },
            new() {
                Id = 17,
                StartTime = new DateTime(2024, 2, 20, 9, 0, 0),
                DurationHours = 20,
                Client = Clients[1],
                Bike = Bikes[9]
            },
            new() {
                Id = 18,
                StartTime = new DateTime(2024, 2, 22, 12, 30, 0),
                DurationHours = 12,
                Client = Clients[5],
                Bike = Bikes[9]
            },
            new() {
                Id = 19,
                StartTime = new DateTime(2024, 2, 25, 14, 0, 0),
                DurationHours = 8,
                Client = Clients[5],
                Bike = Bikes[9]
            },
            new() {
                Id = 20,
                StartTime = new DateTime(2024, 2, 28, 16, 0, 0),
                DurationHours = 10,
                Client = Clients[2],
                Bike = Bikes[9]
            }
        ];
    }
}