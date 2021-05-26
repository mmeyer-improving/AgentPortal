using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AgentsCustomersOrders.Models
{
    public class AgentData
    {
        private readonly IConfiguration _configuration;

        public AgentData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Agent> AllAgentData()
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

        public Agent getSingleAgent(string agentCode)
        {
            var agent = new Agent();

            var connstring = _configuration.GetConnectionString("default");

            using(var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Agents WHERE AgentCode = @agentCode";
                cmd.Parameters.AddWithValue("@agentCode", agentCode);

                var reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    agent.AgentCode = reader["AgentCode"].ToString();
                    agent.AgentName = reader["AgentName"].ToString();
                    agent.WorkingArea = reader["WorkingArea"].ToString();
                    agent.Commission = Convert.ToDouble(reader["Commission"]);
                    agent.PhoneNo = reader["PhoneNo"].ToString();
                }
            }
            return agent;
        }
    }
    
}
