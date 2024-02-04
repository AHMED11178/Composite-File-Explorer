using System;
using System.Collections.Generic;

namespace HW_2_SWE_316
{
    public class File : Component
    {
        public override string Name { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }

        public override long CalculateSize()
        {
            return Size;
        }

        public override List<Component> GetSubcomponents()
        {
            return new List<Component>();
        }

        public override string DisplayContentVirtically()
        {
            return Name;
        }

        public override string DisplayContentHorozantally()
        {
            return Name;
        }


    }


}


