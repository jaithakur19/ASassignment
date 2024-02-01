﻿using System.Transactions;

namespace WebApplication3.Model
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Action {  get; set; }
        public DateTime Time { get; set; }
    }
}
