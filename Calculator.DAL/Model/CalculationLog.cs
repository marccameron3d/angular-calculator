using System;

namespace Calculator.DAL.Model
{
    public class CalculationLog
    {
        public int Id { get; set; }

        public string IP { get; set; }

        public DateTime Timestamp { get; set; }

        public string Calculation { get; set; }

        public long? Result { get; set; }

    }
}
