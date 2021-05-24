﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AgentsCustomersOrders.Models;
using AgentsCustomersOrders.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgentsCustomersOrders.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private List<Agent> AllAgentData()
        {
            var agents = new List<Agent>();

            using (var conn = new SqlConnection("Server=.;Database=AgentsCustomersOrders;Trusted_Connection=True;"))
            {
                conn.Open();

                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Agents";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var agentCode = reader["AgentCode"].ToString();
                    var agentName = reader["AgentName"].ToString();
                    var workingArea = reader["WorkingArea"].ToString();
                    var commission = Convert.ToDouble(reader["Commission"]);
                    var phoneNo = reader["PhoneNo"].ToString();

                    agents.Add(new Agent
                    {
                        AgentCode = agentCode,
                        AgentName = agentName,
                        WorkingArea = workingArea,
                        Commission = commission,
                        PhoneNo = phoneNo
                    });
                }
            }
            return agents;
        }

        public IActionResult Index()
        {
            var agents = AllAgentData();

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
