using System.Collections.Generic;
using System.Threading;

namespace WindowsService1.Cake
{
    public class Factory
    {
        // three services for each cake making procedure: prepare, cook and pack 
        public MakingProcedureService _prepareCakeService;
        public MakingProcedureService _cookCakeService;
        public MakingProcedureService _packCakeService;

        public CakeResourcesManager _stock;
        public CancellationTokenSource _tokenSource;
        public CancellationToken _token;


        public void Cancel()
        {
            _tokenSource.Cancel();
        }

        public Factory(CakeResourcesManager stock)
        {
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            _stock = stock;
            _prepareCakeService = new MakingProcedureService(15, 10000, stock, ProcedureName.Prepare);
            _cookCakeService = new MakingProcedureService(10, 10000, stock, ProcedureName.Cook);
            _packCakeService = new MakingProcedureService(5, 10000, stock, ProcedureName.Pack);

        }

        public void CreateCakes()
        {
            _prepareCakeService.DoWorkAsync(_token);
            _cookCakeService.DoWorkAsync(_token);
            _packCakeService.DoWorkAsync(_token);
        }

        public int GetCookedCakes()
        {
            return _stock.GetPackedCakesNb();
        }

        public List<int> GetCakesQuantityForEachProcedure()
        {
            return _stock.GetEachProcedureCakeQuantity();
        }

    }
}