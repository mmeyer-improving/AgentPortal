using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgentsCustomersOrders.Models;
using AgentsCustomersOrders.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AgentsCustomersOrders.Controllers
{
    public class AgentsController : Controller
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IConfiguration _configuration;
        private AgentData _agentData;

        public AgentsController(ILogger<AgentsController> logger, IConfiguration configuration, AgentData agentdata)
        {
            _logger = logger;
            _configuration = configuration;
            _agentData = agentdata;
        }

        //Index
        public IActionResult Index()
        {
            var agents = _agentData.AllAgentData();

            var vm = new AgentsViewModel();
            vm.Agents = agents;

            return View(vm);
        }

        //Create Agent
        [HttpGet]
        public IActionResult NewAgent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewAgent(Agent agent)
        {
            _agentData.CreateNewAgent(agent);
            return RedirectToAction("Index");
        }

        //Get agent details
        public IActionResult Detail(string id)
        {
            var agent = _agentData.getSingleAgent(id);
            var vm = new AgentsViewModel();
            vm.agent = agent;

            return View(vm);
        }

        //Edit agent
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var agent = _agentData.getSingleAgent(id);
            var vm = new AgentsViewModel();
            vm.agent = agent;

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(Agent agent)
        {
            _agentData.UpdateAgent(agent);
            return RedirectToAction("Index");
        }


        //Delete agent
        [HttpPost]
        public IActionResult Delete(Agent agent)
        {
            _agentData.DeleteAgent(agent.AgentCode);
            return RedirectToAction("Index");
        }
    }
}
