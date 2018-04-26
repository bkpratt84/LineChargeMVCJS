using LineCharge1.Models;
using LineChargeJS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LineChargeJS.Controllers
{
    public class TotalsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/Totals
        public IEnumerable<ChargeTotal> Get()
        {
            IEnumerable<ChargeTotal> query = from charge in db.Charges
                                             join chargeType in db.ChargeTypes on charge.ChargeTypeID equals chargeType.ChargeTypeID
                                             group charge by chargeType.Type into itemGroup
                                             select new ChargeTotal
                                             {
                                                 Type = itemGroup.Key,
                                                 Amount = itemGroup.Sum(x => x.Amount >= 0 ? x.Amount : x.Amount * -1)
                                             };

            return query.OrderByDescending(i => i.Amount);
        }
    }
}
