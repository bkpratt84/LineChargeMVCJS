using LineChargeJS.Data;
using LineChargeJS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LineChargeJS.Controllers
{
    public class ChargeTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/ChargeTypes
        public IEnumerable<ChargeType> Get()
        {
            return db.ChargeTypes.ToList();
        }
    }
}
