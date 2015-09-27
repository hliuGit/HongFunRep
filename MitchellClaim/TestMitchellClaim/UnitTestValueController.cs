using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MitchellClaim.Controllers;
using System.Web;
using System.Collections.Generic;
using System.Linq;

namespace TestMitchellClaim
{
    [TestClass]
    public class UnitTestValueController
    {
       
        [TestMethod]
        [Description("This method is used to test the scenario to write MitchellClaim xml file into sql database."), TestCategory("Insert")]
        public void SaveMitchellClaim_Test()
        {
            //Arrange
            var controller = new ValuesController();
            //Act
            bool retVal = controller.Get();
            //Assert
            //Assert.IsNotNull(retVal);
            Assert.AreEqual(true, retVal);
        }

        [TestMethod]
        [Description("This method is used to test get claim by id."), TestCategory("Get Claim By ID")]
        public void GetClaimByID_Test()
        {
            //Arrange
            var controller = new ValuesController();
            //Act
            MitchellClaim.Models.ClaimType retResult = controller.Get(1);
            //Assert
            //Assert.IsNotNull(retVal);
            Assert.IsTrue(retResult is MitchellClaim.Models.ClaimType);
        }

        [TestMethod]
        [Description("This method is used to test get claim by date range."), TestCategory("Get Claim By Range")]
        public void GetClaimByDateRange_Test()
        {
            //Arrange
            var controller = new ValuesController();
            DateTime start = new DateTime(2014, 7, 1);
            DateTime end = new DateTime(2014,7,15);
            //Act
            List<MitchellClaim.Models.ClaimType> retResults = controller.Get(start,end);
            //Assert
            //Assert.IsNotNull(retVal);
            Assert.IsTrue(retResults is List<MitchellClaim.Models.ClaimType>);
        }

        [TestMethod]
        [Description("This method is used to test get Vehicle"), TestCategory("Get Vehicle")]
        public void GetVehicle_Test()
        {
            //Arrange
            var controller = new ValuesController();
            int claimID = 1;
            int vehicleID = 1;
            //Act
            MitchellClaim.Models.VehicleInfoType retResult = controller.Get(claimID, vehicleID);
            //Assert
            //Assert.IsNotNull(retVal);
            Assert.IsTrue(retResult is MitchellClaim.Models.VehicleInfoType);
        }


        [TestMethod]
        [Description("This method is used to delete a claim from sql database."), TestCategory("Delete")]
        public void DeleteMitchellClaim_Test()
        {
            //Arrange
            var controller = new ValuesController();
            //Act
            bool retVal = controller.Delete(1);
            //Assert
            //Assert.IsNotNull(retVal);
            Assert.AreEqual(true, retVal);
        }
    }
}
