using System;
using System.ComponentModel.DataAnnotations;

namespace Calculator.DAL.Model
{
    /// <summary>
    /// Model of the Calculation Log Table
    /// </summary>
    public class CalculatorLog
    {
        [Key]
        public int Id { get; set; }

        public string IPAddress { get; set; }

        public DateTime Timestamp { get; set; }

        public string Calculation { get; set; }

    }
}
