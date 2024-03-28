using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Setup
{
    public static class SetupParameters
    {
        private static int sets = 5;
        private static TimeSpan work = new TimeSpan(0, 0, 1);
        private static TimeSpan rest = new TimeSpan(0, 0, 0);

        public static int Sets
        {
            get { return sets; }
            set
            {
                if(value > 0 && value <= 99)
                {
                    sets = value;
                }
            }
        }

        public static TimeSpan Work
        {
            get { return work; }
            set
            {
                if(value.TotalSeconds > 1 && value.TotalMinutes < 100)
                {
                    work = value;
                }
            }
        }

        public static TimeSpan Rest
        {
            get { return rest; }
            set
            {
                if (value.TotalSeconds > 0 && value.TotalMinutes < 100)
                {
                    rest = value;
                }
            }
        }
    }
}
