using NgpsqlPratice.Model;
using NgpsqlPratice.Service;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using NgpsqlPratice.WebApi.Models;

namespace NgpsqlPratice.WebApi.Controllers
{
    public class CostumerController : ApiController
    {
        // GET api/Costumer
        public HttpResponseMessage Get()
        {
            CostumerService costumerService = new CostumerService();
            var resault = costumerService.Get();
            if(resault != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, costumerService.Get());
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No rows found");
        }

        // Post api/Costumer
        public HttpResponseMessage Post([FromBody]Costumer costumer)
        {
            CostumerService costumerService = new CostumerService();
            switch (costumerService.Post(costumer))
            {
                case 1:
                    return Request.CreateResponse(HttpStatusCode.OK, "Row Inserted");
                case 2:
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Row can't be inserted");
                case 3:
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                default:
                    return null;
            }
        }

        // Delete api/Costumer

        public HttpResponseMessage Delete(Guid id)
        {
            CostumerService costumerService= new CostumerService();
            switch (costumerService.Delete(id))
            {
                case 1:
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No costumer with that Id exists");
                case 2:
                    return Request.CreateResponse(HttpStatusCode.OK, "Costumer removed");
                case 3:
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                default:
                    return null;
            }
        }

        public HttpResponseMessage Put(Guid id, [FromBody] Costumer costumer)
        {
            CostumerService costumerService = new CostumerService();
            switch (costumerService.Put(id, costumer))
            {
                case 1:
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No costumer with that Id exists");
                case 2:
                    return Request.CreateResponse(HttpStatusCode.OK, "Costumer updated");
                case 3:
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                default:
                    return null;
            }
        }

        public HttpResponseMessage GetById(Guid id)
        {
            CostumerService costumerService = new CostumerService();
            return Request.CreateResponse(HttpStatusCode.OK, costumerService.GetCostumerById(id));
        }

        public Guid MapGuidToRest(Guid id)
        {
            CostumerRest mappedCostumer = new CostumerRest();
            mappedCostumer.mapId= id;
            return mappedCostumer.mapId;
        }
    }

}
