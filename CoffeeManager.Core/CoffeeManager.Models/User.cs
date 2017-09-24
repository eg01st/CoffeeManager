﻿using System;

namespace CoffeeManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CoffeeRoomNo { get; set; }
        public bool IsActive { get; set; }
        public int DayShiftPersent { get; set; }
        public int NightShiftPercent { get; set; }
        public decimal SalaryRate { get; set; }
        public decimal CurrentEarnedAmount { get; set; }
        public decimal EntireEarnedAmount { get; set; }
        public decimal MinimumPayment { get; set; }
        public Nullable<int> ExpenceId { get; set; }

        public UserPenalty[] Penalties { get; set; }

        public UserEarningsHistory[] Earnings { get; set; }
    }
}
