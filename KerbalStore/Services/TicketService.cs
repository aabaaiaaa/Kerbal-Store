using KerbalStore.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Services
{
    public class TicketService : ITicketService
    {
        private readonly ILogger<TicketService> log;
        private readonly ITicketRepository ticketRepository;

        public TicketService(ILogger<TicketService> log, ITicketRepository ticketRepository)
        {
            this.log = log;
            this.ticketRepository = ticketRepository;
        }

        public void SendTicket(string name, string rocketPart)
        {
            log.LogInformation($"Name: {name}, RocketPart: {rocketPart}");

            ticketRepository.SubmitTicketRequest(name, rocketPart);
        }
    }
}
