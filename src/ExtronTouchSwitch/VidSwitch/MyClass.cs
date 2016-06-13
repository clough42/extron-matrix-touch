using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtronTouchSwitch
{
    class MyClass
    {
        private DateTime date;
       
        public MyClass()
        {
        }

        public int Hours
        {
            get { return date.Hour; }
            set { date = new DateTime(2000, 1, 1, value, 1, 1); }
        }


    }
}
