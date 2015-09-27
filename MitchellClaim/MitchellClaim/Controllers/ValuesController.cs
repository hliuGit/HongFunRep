using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MitchellClaim.Models;
using System.IO;

namespace MitchellClaim.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public bool Get()
        {
            var isSaveSuccessful = true;
            string xmlfile;
            if (HttpContext.Current != null)
            {
                xmlfile = HttpContext.Current.Server.MapPath("~/App_Data/create-claim.xml");

            }
            else {
                
             
                xmlfile =@"C:\Users\Hong\Documents\Visual Studio 2013\Projects\MitchellClaim\MitchellClaim\App_Data\create-claim.xml";
            }
            var xmlreader = new MitchellClaim.Models.MitchellClaimReader();
            isSaveSuccessful = xmlreader.SaveMitchellClaim(xmlfile);

            
            return isSaveSuccessful;
        }

        // GET api/values/5
        public ClaimType Get(int id)
        {
            var xmlreader = new MitchellClaim.Models.MitchellClaimReader();
            var claim = xmlreader.GetClaimByID(id) as ClaimType;
            return claim;
        }

        // GET api/values/Get/2014-07-01/2014-07-14
        public List<ClaimType> Get(DateTime start, DateTime end)
        {
            var xmlreader = new MitchellClaim.Models.MitchellClaimReader();
            var claim = xmlreader.GetClaimBydate(start, end) as List<ClaimType>;
            return claim;
        }

        // GET api/values/Get/1/1
        public VehicleInfoType Get(int claimID, int vehicleID)
        {
            var xmlreader = new MitchellClaim.Models.MitchellClaimReader();
            var vehicle = xmlreader.GetVehicle(claimID, vehicleID) as VehicleInfoType;
            return vehicle;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put( )
        {
            
        }

        // DELETE api/values/5
        public bool Delete(int id)
        {
            var xmlreader = new MitchellClaim.Models.MitchellClaimReader();
            return xmlreader.DeleteClaim(id);
        }
    }
}
