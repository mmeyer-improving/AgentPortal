using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AgentsCustomersOrders.Models;
using AgentsCustomersOrders.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AgentsCustomersOrders.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly AgentData _agentData;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, AgentData agentData)
        {
            _logger = logger;
            _configuration = configuration;
            _agentData = agentData;
        }

        public IActionResult Index()
        {
            var agents = _agentData.AllAgentData();

            var vm = new HomeViewModel();
            vm.Agents = agents;

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
