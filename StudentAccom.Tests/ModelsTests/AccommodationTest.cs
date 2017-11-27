using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentAccom.Models;

namespace StudentAccom.Tests.ModelsTests {
    [TestClass]
    public class AccommodationTest {
        [TestMethod]
        public void TestMethodAccommodation1() {
            var _accommodation = new Models.Accommodation() {
                Title = "New house in the best area of the city",
                TypeAccom = TypeAccom.House,
                Location = "Southampton",
                Description = "This house is fresh new, large and cozy. Percet for big groups of students.",
                RoomsNumber = 6,
                TypeRent = TypeRent.Monthly,
                Price = decimal.Parse("500,50"),
                CleaningFee = decimal.Parse("65,30"),
                Internet = true,
                CableTV = true
            };  
        }
    }
}
