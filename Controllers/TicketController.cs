using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetVue.Models;


namespace TeamworksInterrogationTool.api.Services
{
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<Ticket> GetTop100Tickets(int? locationNumber, int? ticketReference, int? titanCompanyId, string toDate, string fromDate)
        {
            TeamworksArchiveContext dbContext = new TeamworksArchiveContext();
            var CustomerServiceTickets = dbContext.Ticket;
            var filteredResults = from tickets in CustomerServiceTickets
                                  select tickets;
            if (locationNumber != null)
            {
                filteredResults = filteredResults.Where(tickets => tickets.CentreNumber == locationNumber);
            }
            if (ticketReference != null)
            {
                filteredResults = filteredResults.Where(tickets => tickets.TicketReference == ticketReference);
            }
            if (titanCompanyId != null)
            {
                filteredResults = filteredResults.Where(tickets => tickets.TitanCompanyId == titanCompanyId);
            }
            if (fromDate != null)
            {
                //var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                filteredResults = filteredResults.Where(tickets => tickets.CreateDate >= Convert.ToDateTime(fromDate));
            }
            if (toDate != null)
            {
                //var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                filteredResults = filteredResults.Where(tickets => tickets.CreateDate < Convert.ToDateTime(toDate));
            }
            return filteredResults.Take(100).OrderBy(o => o.CreateDate).ToList();
        }
        [HttpGet("[action]")]
        public IEnumerable<Ticket> GetTicketDetail(int ticketReference)
        {
            TeamworksArchiveContext dbContext = new TeamworksArchiveContext();
            var CustomerServiceTickets = dbContext.Ticket;
            var filteredResults = from tickets in CustomerServiceTickets
                                  select tickets;
            filteredResults = filteredResults.Where(tickets => tickets.TicketReference == ticketReference);

            return filteredResults.ToList();
        }
        [HttpGet("[action]")]
        public IEnumerable<TicketComment> GetTicketComments(int ticketReference)
        {
            TeamworksArchiveContext dbContext = new TeamworksArchiveContext();
            var CustomerServiceTickets = dbContext.TicketComment;
            var filteredResults = from ticketComments in CustomerServiceTickets
                                  select ticketComments;
            filteredResults = filteredResults.Where(ticketComments => ticketComments.TicketReference == ticketReference);

            return filteredResults.ToList();
        }

        public partial class Ticket
        {
            public int TicketReference { get; set; }
            public DateTime CreateDate { get; set; }
            public string CreateUser { get; set; }
            public string TicketStatus { get; set; }
            public string AssignedTo { get; set; }
            public string RequesterName { get; set; }
            public string CentreName { get; set; }
            public int? CentreNumber { get; set; }
            public DateTime? ClosedDate { get; set; }
            public DateTime? ResolvedDate { get; set; }
            public int? SourceSystemId { get; set; }
            public string CategoryDescription { get; set; }
            public string SubCategoryDescription { get; set; }
            public int? ProductTypeId { get; set; }
            public string ContactEmail { get; set; }
            public string ContactName { get; set; }
            public string ContactTelephone { get; set; }
            public int? ClosureCodeId { get; set; }
            public int? TitanCompanyId { get; set; }
            public string TitanBwRef { get; set; }
            public string CompanyName { get; set; }
        }
    }

}
