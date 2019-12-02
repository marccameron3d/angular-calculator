using System;

namespace Calculator.DAL.Model
{
    /// <summary>
    /// Model of the Calculation Log Table
    /// </summary>
    public class CalculationLog
    {
        public int Id { get; set; }

        public string IP { get; set; }

        public DateTime Timestamp { get; set; }

        public string Calculation { get; set; }

        public long? Result { get; set; }

    }
}
