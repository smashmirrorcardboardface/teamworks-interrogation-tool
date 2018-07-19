using System;
using System.Collections.Generic;

namespace dotnetVue.Models
{
    public partial class TicketComment
    {
        public int CommentId { get; set; }
        public int TicketReference { get; set; }
        public bool PrivateComment { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string Comment { get; set; }
        public int TicketStatusId { get; set; }
        public bool? Question { get; set; }
        public bool? IsSystemComment { get; set; }
        public string UserBrowser { get; set; }
    }
}
