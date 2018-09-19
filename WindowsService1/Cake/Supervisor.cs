using System;
using System.Threading;

namespace WindowsService1.Cake
{
    public class Supervisor
    {
        private const int _TIMER_DUE_TIME = 20 * 1000; //every minute
        private Timer _timer = null;
        Factory _factory;

        public Supervisor(Factory factory)
        {
            _factory = factory;
            factory.CreateCakes();
            _timer = new Timer(_timer_Elapsed, null, _TIMER_DUE_TIME, Timeout.Infinite);
        }

        private void _timer_Elapsed(object state)
        {
            try
            {
                var CakesQuantityForEachProcedure = _factory.GetCakesQuantityForEachProcedure();
                int nbCookedCake = _factory.GetCookedCakes();
                int nbCakeBeingPrepared = CakesQuantityForEachProcedure[0];
                int nbCakeBeingCooked = CakesQuantityForEachProcedure[1];
                int nbCakeBeingPacked = CakesQuantityForEachProcedure[2];
                Console.WriteLine($"nbCookedCake={nbCookedCake} / nbCakeBeingPrepared={nbCakeBeingPrepared} / nbCakeBeingCooked={nbCakeBeingCooked} / nbCakeBeingPacked={nbCakeBeingPacked}");
            }
            catch
            {

            }
            finally
            {
                _timer.Change(_TIMER_DUE_TIME, Timeout.Infinite);
            }
        }
    }
}