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
using System.Threading.Tasks;
using System.CodeDom.Compiler;

namespace NgpsqlPratice.WebApi.Controllers
{
    public class CostumerController : ApiController
    {
        // GET api/Costumer
        public async Task<HttpResponseMessage> Get(List<Costumer>costumers)
        {
            CostumerService costumerService = new CostumerService();
            costumers = await costumerService.Get();
            MapToRest(costumers);
            if(costumers != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, MapToRest(costumers));
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No rows found");

        }

        // Post api/Costumer
        public async Task<HttpResponseMessage> Post([FromBody]Costumer costumer)
        {
            CostumerService costumerService = new CostumerService();
            switch (await costumerService.Post(costumer))
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

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            CostumerService costumerService= new CostumerService();
            switch (await costumerService.Delete(id))
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

        public async Task<HttpResponseMessage> Put(Guid id, [FromBody] Costumer costumer)
        {
            CostumerService costumerService = new CostumerService();
            switch (await costumerService.Put(id, costumer))
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

        public async Task<HttpResponseMessage> GetById(Guid id, Costumer costumer)
        {
            CostumerService costumerService = new CostumerService();
            costumer = await costumerService.GetCostumerById(id);
            return Request.CreateResponse(HttpStatusCode.OK, MapFromRest(costumer));
        }

        public async Task<HttpResponseMessage> GetAll(int pageNumber, int pageSize, string sortByGender, string searchQuery, string filterByGender, List<Costumer> costumers) 
        {
            CostumerService costumerService = new CostumerService();
            costumers = await costumerService.GetAll(pageNumber, pageSize, sortByGender, searchQuery, filterByGender);
            MapToRest(costumers);
            if(costumers != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, MapToRest(costumers));
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No rows with those parameters foudn");
        }

        private CostumerRest MapFromRest(Costumer costumer)
        {
            CostumerRest costumerRest = new CostumerRest()
            {
                Id = costumer.Id,
                FirstName = costumer.FirstName,
                LastName = costumer.LastName,
                Gender = costumer.Gender,
                Email = costumer.Email,
                PhoneNumber = costumer.PhoneNumber,
            };
            return costumerRest;
            
        }

        private List<CostumerRest> MapToRest(List<Costumer>costumers)
        {
            List<CostumerRest> costumerList = new List<CostumerRest>();
            if (costumers != null)
            {
                foreach(Costumer costumer in costumers)
                {
                    CostumerRest costumerRest = new CostumerRest();
                    costumerRest.Id = costumer.Id;
                    costumerRest.FirstName = costumer.FirstName;
                    costumerRest.LastName = costumer.LastName;
                    costumerRest.Gender = costumer.Gender;
                    costumerRest.Email = costumer.Email;
                    costumerRest.PhoneNumber = costumer.PhoneNumber;
                    costumerList.Add(costumerRest);
                }
            }
            return costumerList;
        }
    }

}
