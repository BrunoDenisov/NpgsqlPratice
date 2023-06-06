using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NgpsqlPratice.Model;
using NgpsqlPratice.Model.Common;
using NgpsqlPratice.Repository;
using NgpsqlPratice.Service.Common;
using NgpsqlPratice.WebApi.Models;
//using NgpsqlPratice.WebApi.Models;

namespace NgpsqlPratice.Service
{
    public class CostumerService
    {
        public async Task<List<Costumer>> Get()
        {
            CostumerRepository costumerRepository = new CostumerRepository();
            List<Costumer> costumers = await costumerRepository.Get();
            return costumers;
        }

        public async Task<int> Post(Costumer costumer)
        {
            CostumerRepository costumerRepository = new CostumerRepository();
            int resault = await costumerRepository.Post(costumer);
            return resault;
        }

        public async Task<int> Delete(Guid Id)
        {
            CostumerRepository costumerRepository = new CostumerRepository();
            int resault = await costumerRepository.Delete(Id);
            return resault;
        }

        public async Task<int> Put(Guid id, Costumer costumer)
        {
            CostumerRepository costumerRepository= new CostumerRepository();
            int resault =  await costumerRepository.Put(id, costumer);
            return resault;
        }

        public async Task<Costumer> GetCostumerById(Guid id)
        {
            CostumerRepository costumerRepository = new CostumerRepository();
            Costumer costumer = await costumerRepository.GetCostumerByID(id);
            return costumer;
        }

        public async Task<List<Costumer>> GetAll(Filtering filtering, Paging paging, Sorting sorting)
        {
            CostumerRepository costumerRepository = new CostumerRepository();
            List<Costumer> costumers = await costumerRepository.GetAll(filtering, paging, sorting);
            return costumers;
        }
    }
}
