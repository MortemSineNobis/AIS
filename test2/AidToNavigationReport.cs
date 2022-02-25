using System;
using System.Collections.Generic;
using System.Text;

namespace test2
{
    public class AidToNavigationReport : Decoded
    {
        
        public int aid_type { get; set; }
        public string name { get; set; }
        public int accuracy { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public int to_bow { get; set; }
        public int to_stern { get; set; }
        public int to_port { get; set; }
        public int to_starboard { get; set; }
        public int epfd { get; set; }
        public int second { get; set; }
        public int off_position { get; set; }
        public int regional { get; set; }
        public int raim { get; set; }
        public int virtual_aid { get; set; }
        public int assigned { get; set; }
        public string name_extension { get; set; }
    }
}
