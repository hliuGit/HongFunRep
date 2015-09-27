using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Configuration;
using System.ComponentModel;

namespace MitchellClaim.Models
{
    public class ClaimType
    {
        public long ClaimID { get; set; }
        public string ClaimNumber { get; set; }
        public string ClaimantFirstName { get; set; }
        public string ClaimantLastName { get; set; }
        public string Status { get; set; }
        public DateTime? LossDate { get; set; }
        public long? LossInfoTypeID
        { get; set; }
        public long? VehicleID { get; set; }
        public long? AssignedAdjusterID { get; set; }
    }
    public class VehicleInfoType
    {
        public long VehicleInfoTypeID { get; set; }
        public string Vin { get; set; }
        public int? ModelYear { get; set; }
        public string MakeDescription { get; set; }
        public string ModelDescription { get; set; }
        public string EngineDescription { get; set; }
        public string ExteriorColor { get; set; }
        public string LicPlate { get; set; }
        public string LicPlateState { get; set; }
        public DateTime? LicPlateExpDate { get; set; }
        public string DamageDescription { get; set; }
        public long? Mileage
        { get; set; }

    }
    public class LossInfoType
    {
        public long LossInfoTypeID { get; set; }
        public string CauseOfLoss { get; set; }

        public DateTime? ReportedDate { get; set; }
        public string LossDescription { get; set; }
    }

    public class Data
    {
        public static int AddLossInfoType(LossInfo lossInfo)
        {
            //string  ConnectionString = ConfigurationManager.ConnectionStrings["ClaimsConn"].ConnectionString;// "Data Source=(localdb)\\Projects;Initial Catalog=CFN_CCE_ServiceBroker_TRACING;Integrated Security=true;";
            string ConnectionString = @"Data Source=(localdb)\ProjectsV12;Initial Catalog=MTL_ClaimData;Integrated Security=true;";
            int lossInfoTypeID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand("dbo.[AddLossInfoType]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60;
                    connection.Open();

                    command.Parameters.AddWithValue("@CauseOfLoss", lossInfo.CauseOfLoss);
                    command.Parameters.AddWithValue("@ReportedDate", lossInfo.ReportedDate);
                    command.Parameters.AddWithValue("@LossDescription", lossInfo.LossDescription);
                    lossInfoTypeID = System.Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lossInfoTypeID;

        }

        public static int AddVehicleInfoType(VehicleDetails vehicleInfo)
        {
            //string  ConnectionString = ConfigurationManager.ConnectionStrings["ClaimsConn"].ConnectionString;// "Data Source=(localdb)\\Projects;Initial Catalog=CFN_CCE_ServiceBroker_TRACING;Integrated Security=true;";
            string ConnectionString = @"Data Source=(localdb)\ProjectsV12;Initial Catalog=MTL_ClaimData;Integrated Security=true;";
            int vehicleInfoTypeID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand("dbo.[AddVehicleInfoType]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60;
                    connection.Open();

                    command.Parameters.AddWithValue("@Vin", vehicleInfo.Vin);
                    command.Parameters.AddWithValue("@ModelYear", vehicleInfo.ModelYear);
                    command.Parameters.AddWithValue("@MakeDescription", vehicleInfo.MakeDescription);
                    command.Parameters.AddWithValue("@ModelDescription", vehicleInfo.ModelDescription);
                    command.Parameters.AddWithValue("@EngineDescription", vehicleInfo.EngineDescription);
                    command.Parameters.AddWithValue("@ExteriorColor", vehicleInfo.ExteriorColor);
                    command.Parameters.AddWithValue("@LicPlate", vehicleInfo.LicPlate);
                    command.Parameters.AddWithValue("@LicPlateState", vehicleInfo.LicPlateState);
                    command.Parameters.AddWithValue("@LicPlateExpDate", vehicleInfo.LicPlateExpDate);
                    command.Parameters.AddWithValue("@DamageDescription", vehicleInfo.DamageDescription);
                    command.Parameters.AddWithValue("@Mileage", vehicleInfo.Mileage);

                    vehicleInfoTypeID = System.Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                throw;
            }
            return vehicleInfoTypeID;

        }

        public static int AddClaimType(MitchellClaim ClaimInfo, int vehicleInfoID, int lossInfoID)
        {
            //string  ConnectionString = ConfigurationManager.ConnectionStrings["ClaimsConn"].ConnectionString;// "Data Source=(localdb)\\Projects;Initial Catalog=CFN_CCE_ServiceBroker_TRACING;Integrated Security=true;";
            string ConnectionString = @"Data Source=(localdb)\ProjectsV12;Initial Catalog=MTL_ClaimData;Integrated Security=true;";
            int ClaimTypeID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand("dbo.[AddClaimType]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60;
                    connection.Open();

                    command.Parameters.AddWithValue("@ClaimNumber", ClaimInfo.ClaimNumber);
                    command.Parameters.AddWithValue("@ClaimantFirstName", ClaimInfo.ClaimantFirstName);
                    command.Parameters.AddWithValue("@ClaimantLastName", ClaimInfo.ClaimantLastName);
                    command.Parameters.AddWithValue("@Status", ClaimInfo.Status);
                    command.Parameters.AddWithValue("@LossDate", ClaimInfo.LossDate);
                    command.Parameters.AddWithValue("@lossInfoTypeID", lossInfoID);
                    command.Parameters.AddWithValue("@VehicleID", vehicleInfoID);
                    command.Parameters.AddWithValue("@AssignedAdjusterID", ClaimInfo.AssignedAdjusterID);
                   

                    ClaimTypeID = System.Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ClaimTypeID;

        }

        public static VehicleInfoType GetVehicle(int claimID, int vehicleID)
        {
            var vehicle = new VehicleInfoType();
            try
            {
                // var ConnectionString = ConfigurationManager.ConnectionStrings["ClaimsConn"].ToString();// "Data Source=(localdb)\\Projects;Initial Catalog=CFN_CCE_ServiceBroker;Integrated Security=true; Asynchronous Processing=true;Max Pool Size=500";
                string ConnectionString = @"Data Source=(localdb)\ProjectsV12;Initial Catalog=MTL_ClaimData;Integrated Security=true;";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand("dbo.[GetVehicle]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120;
                    connection.Open();
                    command.Parameters.AddWithValue("@claimID", claimID);
                    command.Parameters.AddWithValue("@vehicleID", vehicleID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vehicle.VehicleInfoTypeID = reader.GetValue<long>("VehicleInfoTypeID");
                            vehicle.Vin = reader.GetValue<string>("Vin");
                            vehicle.ModelYear = reader.GetValue<int>("ModelYear");
                            vehicle.MakeDescription = reader.GetValue<string>("MakeDescription");
                            vehicle.ModelDescription = reader.GetValue<string>("ModelDescription");
                            vehicle.EngineDescription = reader.GetValue<string>("EngineDescription");
                            vehicle.ExteriorColor = reader.GetValue<string>("ExteriorColor");
                            vehicle.LicPlate = reader.GetValue<string>("LicPlate");
                            vehicle.LicPlateState = reader.GetValue<string>("LicPlateState");
                            vehicle.LicPlateExpDate = reader.GetValue<DateTime>("LicPlateExpDate");
                            vehicle.DamageDescription = reader.GetValue<string>("DamageDescription");
                            vehicle.Mileage = reader.GetValue<long?>("Mileage");
                            
                        }
                        //return reader.Select(r => ContractBuilder(r)).ToList();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return  vehicle ;
        }

        public static List<ClaimType> GetClaimType()
        {
            List<ClaimType> claims = new List<ClaimType>();

            try
            {
               // var ConnectionString = ConfigurationManager.ConnectionStrings["ClaimsConn"].ToString();// "Data Source=(localdb)\\Projects;Initial Catalog=CFN_CCE_ServiceBroker;Integrated Security=true; Asynchronous Processing=true;Max Pool Size=500";
                string ConnectionString = @"Data Source=(localdb)\ProjectsV12;Initial Catalog=MTL_ClaimData;Integrated Security=true;";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand("dbo.[GetClaims]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClaimType claim = new ClaimType();
                            claim.ClaimID = reader.GetValue<long>("ClaimID");
                            claim.ClaimNumber = reader.GetValue<string>("ClaimNumber");
                            claim.ClaimantFirstName = reader.GetValue<string>("ClaimantFirstName");
                            claim.ClaimantLastName = reader.GetValue<string>("ClaimantLastName");
                            claim.Status = reader.GetValue<string>("Status");
                            claim.LossDate = reader.GetValue<DateTime?>("LossDate");
                            claim.LossInfoTypeID = reader.GetValue<long?>("LossInfoTypeID");
                            claim.VehicleID = reader.GetValue<long?>("VehicleID");
                            claim.AssignedAdjusterID = reader.GetValue<long?>("AssignedAdjusterID");
                            claims.Add(claim);
                        }
                        //return reader.Select(r => ContractBuilder(r)).ToList();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return claims;
        }

        public static bool DeleteClaim(int claimID)
        {
            //var ConnectionString = ConfigurationManager.ConnectionStrings["ClaimsConn"].ToString();// "Data Source=(localdb)\\Projects;Initial Catalog=CFN_CCE_ServiceBroker_TRACING;Integrated Security=true;";
            string ConnectionString = @"Data Source=(localdb)\ProjectsV12;Initial Catalog=MTL_ClaimData;Integrated Security=true;";
            var isDeleteSuccessful = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand("dbo.[DeleteClaimType]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60;
                    connection.Open();

                    command.Parameters.AddWithValue("@ClaimID", claimID);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            isDeleteSuccessful = true;
            return isDeleteSuccessful;

        }

    }
}





