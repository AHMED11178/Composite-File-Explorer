using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_2_SWE_316
{
    public abstract class Component
    {
        public abstract string Name { get; set; }
        public abstract long CalculateSize();
        public abstract List<Component> GetSubcomponents();

        public abstract string DisplayContentVirtically();

        public abstract string DisplayContentHorozantally();
    }
}
