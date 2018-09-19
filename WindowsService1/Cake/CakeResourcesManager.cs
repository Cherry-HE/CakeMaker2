using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1.Cake
{
    public class CakeResourcesManager
    {
        // available cakes in stocks
        public Dictionary<AvailableResource, int> _availableStock = new Dictionary<AvailableResource, int>();
        // cake quantity during each procedure
        public Dictionary<ProcedureName, int> _cakeQuantityForEachProcedure = new Dictionary<ProcedureName, int>();

        private readonly object _object = new object();

        public CakeResourcesManager()
        {
            _availableStock = new Dictionary<AvailableResource, int>()
            {
                {AvailableResource.Ingredients,int.MaxValue},
                {AvailableResource.PreparedCakes,0 },
                {AvailableResource.CookedCakes,0 },
                {AvailableResource.PackedCakes,0 }
            };
            _cakeQuantityForEachProcedure = new Dictionary<ProcedureName, int>()
            {
                {ProcedureName.Prepare,0 },
                {ProcedureName.Cook,0 },
                {ProcedureName.Pack,0 }
            };
        }

        public int GetCakesFromStock(int commandNb, AvailableResource availableResource)
        {
            if (!_availableStock.ContainsKey(availableResource))
            {
                throw new ArgumentException();
            }
            int getCakesNb = commandNb;
            if (commandNb > _availableStock[availableResource])
            {
                getCakesNb = _availableStock[availableResource];

            }
            lock (_object)
            {
                _availableStock[availableResource] = _availableStock[availableResource] - getCakesNb;
            }
            return getCakesNb;
        }

        public void PutCakesIntoStock(int prepareNb, AvailableResource availableResource)
        {
            if (!_availableStock.ContainsKey(availableResource))
            {
                throw new ArgumentException();
            }

            lock (_object)
            {
                _availableStock[availableResource] += prepareNb;
            }
        }

        public void PutCakeForOneProcedure(int cakes, ProcedureName procedureName)
        {
            lock (_object)
            {
                _cakeQuantityForEachProcedure[procedureName] = cakes;
            }
        }

        public int RemoveProcedure(ProcedureName procedureName)
        {
            var beingMadeCakes = _cakeQuantityForEachProcedure[procedureName];
            lock (_object)
            {
                _cakeQuantityForEachProcedure[procedureName] = 0;
            }
            return beingMadeCakes;
        }

        public int GetPackedCakesNb()
        {
            lock (_object)
                return _availableStock[AvailableResource.PackedCakes];
        }

        public List<int> GetEachProcedureCakeQuantity()
        {
            lock (_object)
            {
                return _cakeQuantityForEachProcedure.Values.ToList();
            }
        }
    }

    public enum AvailableResource
    {
        Ingredients = 1,
        PreparedCakes = 2,
        CookedCakes = 3,
        PackedCakes = 4,
    }
}
