using NUnit.Framework;
using System.Collections.Generic;
using CarConnect.dao;
using CarConnect.entity;

namespace CarConnectTests
{
    [TestFixture]
    public class VehicleServiceTests
    {
        private IVehicleService vehicleService;

        [SetUp]
        public void Setup()
        {
            vehicleService = new VehicleService();
        }

        [Test]
        public void TestAddNewVehicle()
        {
            var vehicle = new Vehicle
            {
                Model = "NUnitTestCar",
                Make = "TestMake",
                Year = 2023,
                Color = "Green",
                RegistrationNumber = "NUNIT123",
                Availability = true,
                DailyRate = 1500
            };

            vehicleService.AddVehicle(vehicle);

            var added = vehicleService.GetAvailableVehicles().Find(v => v.RegistrationNumber == "NUNIT123");
            Assert.IsNotNull(added);
        }

        [Test]
        public void TestUpdateVehicleDetails()
        {
            var vehicle = vehicleService.GetVehicleById(1);
            vehicle.Color = "Black";

            vehicleService.UpdateVehicle(vehicle);
            var updated = vehicleService.GetVehicleById(1);

            Assert.IsNotNull(updated);
            Assert.AreEqual("Black", updated.Color);
        }

        [Test]
        public void TestGetAvailableVehicles()
        {
            List<Vehicle> availableVehicles = vehicleService.GetAvailableVehicles();
            Assert.IsNotNull(availableVehicles);
            Assert.IsTrue(availableVehicles.Count > 0);
        }

       
    }
}
