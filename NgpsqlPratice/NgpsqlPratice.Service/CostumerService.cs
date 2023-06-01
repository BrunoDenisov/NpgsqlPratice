using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NgpsqlPratice.Model;
using NgpsqlPratice.Model.Common;
using NgpsqlPratice.Repository;
using NgpsqlPratice.Service.Common;

namespace NgpsqlPratice.Service
{
    public class CostumerService
    {
        public List<Costumer> Get()
        {
            CostumerRepository costumerRepository = new CostumerRepository();
            List<Costumer> costumers = costumerRepository.Get();
            return costumers;
        }

        public int Post(Costumer costumer)
        {
            CostumerRepository costumerRepository = new CostumerRepository();
            int resault = costumerRepository.Post(costumer);
            return resault;
        }

        public int Delete(Guid Id)
        {
            CostumerRepository costumerRepository = new CostumerRepository();
            int resault = costumerRepository.Delete(Id);
            return resault;
        }

        public int Put(Guid id, Costumer costumer)
        {
            CostumerRepository costumerRepository= new CostumerRepository();
            int resault = costumerRepository.Put(id, costumer);
            return resault;
        }
    }
}
