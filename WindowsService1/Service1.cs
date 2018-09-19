using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WindowsService1.Cake;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        private Supervisor _supervisor;
        private Factory _factory;

        public Service1(Supervisor supervisor, Factory factory) : base()
        {
            _supervisor = supervisor;
            _factory = factory;

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        public void Run()
        {
            this.OnStart(null);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
