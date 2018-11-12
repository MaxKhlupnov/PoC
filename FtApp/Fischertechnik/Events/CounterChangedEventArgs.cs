using System;
using System.Collections.Generic;
using System.Text;

namespace FtApp.Fischertechnik.Events
{
    public class CounterChangedEventArgs {

        public short[] Counters {get; set;}
        public CounterChangedEventArgs(short[] Counters)
        {
            this.Counters = Counters;
        }


        public override string ToString()
        {
            if (Counters != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Counters.Length; i++)
                {
                    sb.AppendFormat("C{0} {1}  |", i, Counters[i]);
                }
                return sb.ToString();
            }
            else
            {
                return base.ToString();
            }

        }
    }
}
