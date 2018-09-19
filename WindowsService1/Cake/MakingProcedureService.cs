using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsService1.Cake
{
    public class MakingProcedureService
    {
        public int _capacity;
        public int _makingTime;
        CakeResourcesManager _stockManager;
        ProcedureName ProcedureName { get; set; }
        AvailableResource remainingResource;
        AvailableResource currentResource;

        public MakingProcedureService(int capacity, int makingTime, CakeResourcesManager stockManager, ProcedureName procedureName)
        {
            _capacity = capacity;
            _makingTime = makingTime;
            ProcedureName = procedureName;
            _stockManager = stockManager;

            int procedure = (int)ProcedureName;
            remainingResource = (AvailableResource)(procedure - 1);
            currentResource = (AvailableResource)(procedure);
        }

        public void Start()
        {            
            var beingMadeCakes = _stockManager.GetCakesFromStock(_capacity, remainingResource);
            _stockManager.PutCakeForOneProcedure(beingMadeCakes, ProcedureName);
        }

        public void Finish()
        {
            var beingMadeCakes = _stockManager.RemoveProcedure(ProcedureName);
            _stockManager.PutCakesIntoStock(beingMadeCakes, currentResource);
        }

        public async Task DoWorkAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    Start();
                }
                catch (Exception e)
                {

                }
                await Task.Delay(TimeSpan.FromMilliseconds(_makingTime));
                try
                {
                    Finish();
                }
                catch (Exception e)
                {

                }
            }
        }
    }

    public enum ProcedureName
    {
        Prepare = 2,
        Cook = 3,
        Pack = 4
    }
}
