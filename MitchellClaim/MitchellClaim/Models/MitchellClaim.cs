using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MitchellClaim.Models
{
    [XmlRoot(ElementName="MitchellClaim", Namespace = "http://www.mitchell.com/examples/claim")]
    public class MitchellClaim
    {
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string ClaimNumber { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string ClaimantFirstName { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string ClaimantLastName { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string Status { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public DateTime? LossDate { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public LossInfo LossInfo { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public Vehicles Vehicles { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public long? AssignedAdjusterID { get; set; }
    }

    [XmlRoot(Namespace = "http://www.mitchell.com/examples/claim")]
    public class LossInfo
    {
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string CauseOfLoss { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public DateTime? ReportedDate { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string LossDescription { get; set; }
    }

    [XmlRoot(Namespace = "http://www.mitchell.com/examples/claim")]
    public class Vehicles
    {
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public List<VehicleDetails> VehicleDetails { get; set; }
    }

    [XmlRoot(Namespace = "http://www.mitchell.com/examples/claim")]
    public class VehicleDetails
    {
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string Vin { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public int? ModelYear { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string MakeDescription { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string ModelDescription { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string EngineDescription { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string ExteriorColor { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string LicPlate { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string LicPlateState { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public DateTime? LicPlateExpDate { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public string DamageDescription { get; set; }
        [XmlElement(Namespace = "http://www.mitchell.com/examples/claim")]
        public long? Mileage
        { get; set; }

    }

    public class  MitchellClaimReader
    {
        private MitchellClaim  GetMitchellClaim(string xmlFileName)
        {
            var rdMitchellClaim = new MitchellClaim();
            using(var stream = File.OpenRead(xmlFileName))
            {
                var serializer = new XmlSerializer(typeof(MitchellClaim));
                rdMitchellClaim = (MitchellClaim)serializer.Deserialize(stream);
            }
            return rdMitchellClaim;
        }

        public bool SaveMitchellClaim(string xmlFileName)
        {
            var claimReader =  GetMitchellClaim(xmlFileName) as MitchellClaim;
            var objInfoType = claimReader.LossInfo;
            var vehicles = claimReader.Vehicles;
            var vehicleDetails = vehicles.VehicleDetails as List<VehicleDetails>;
            var isSaveSuccess = true;
            try
            {
                int InfoType = Data.AddLossInfoType(objInfoType);
                //only one vehicle this time
                int vehicleInfoID = -1;
                foreach (var vehicle in vehicleDetails)
                {
                    vehicleInfoID = Data.AddVehicleInfoType(vehicle);
                }
                int claimTypeID = Data.AddClaimType(claimReader, vehicleInfoID, InfoType);
            }
            catch
            {
                throw;
                isSaveSuccess = false;
                
            }
            return isSaveSuccess;
        }

        public ClaimType GetClaimByID(int id)
        {
            var claims = Data.GetClaimType() as List<ClaimType>;
            var claim = claims.Where(p => p.ClaimID == id).FirstOrDefault();
            return claim;
        }

        public List<ClaimType> GetClaimBydate(DateTime start, DateTime end)
        {
            var claims = Data.GetClaimType() as List<ClaimType>;
            var claimList = claims.Where(p => p.LossDate >= start && p.LossDate <= end).ToList();
            return claimList;
        }

        public VehicleInfoType GetVehicle(int claimID, int vehicleID)
        {
            return Data.GetVehicle(claimID, vehicleID);
        }

        public bool DeleteClaim(int claimID)
        {
           return  Data.DeleteClaim(claimID);
           
        }
    }
    
}