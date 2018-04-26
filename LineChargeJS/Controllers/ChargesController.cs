using LineChargeJS.Data;
using LineChargeJS.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LineChargeJS.Controllers
{
    public class ChargesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/Charges
        public IEnumerable<Charge> Get()
        {
            return db.Charges.Include(c => c.ChargeType);

        }

        // POST api/Charges
        public void Post([FromBody] Charge charge)
        {
            var chargeType = db.ChargeTypes.Where(i => i.ChargeTypeID == charge.ChargeTypeID).Single();

            db.Charges.Add(new Charge
            {
                Amount = chargeType.Type == "Deposit" ? (double)charge.Amount : ((double)charge.Amount) * -1,
                ChargeTypeID = charge.ChargeTypeID,
                TransactionDate = DateTime.Now
            });

            db.SaveChanges();
        }
    }
}
