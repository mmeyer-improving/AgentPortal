using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgentsCustomersOrders.Models;

namespace AgentsCustomersOrders.ViewModels.Home
{
    public class AgentsViewModel
    {
        public Agent agent { get; set; }
        public List<Agent> Agents { get; internal set; }
    }
}
