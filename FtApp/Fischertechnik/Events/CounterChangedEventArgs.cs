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
}
}
