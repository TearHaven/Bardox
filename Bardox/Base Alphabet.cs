using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardox
{
    abstract class BaseAl
    {
        protected string up { get; set; }
        protected string low { get; set; }

        public string Up
        {
            get { return up; }
            set { up = value; }
        }
        public string Low
        {
            get { return low; }
            set { low = value; }
        }


        public BaseAl(string up, string low)
        {
            this.up = up;
            this.low = low;
        }

        public abstract string Convert(BaseAl targetAl, string input);
    }
}
