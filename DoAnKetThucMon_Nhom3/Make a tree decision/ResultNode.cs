using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnKetThucMon_Nhom3.Make_a_tree_decision
{
    public class ResultNode
    {
        public static int index = 0;
        string info;

        public string Info { get => info; set => info = value; }

        public ResultNode(string INFO)
        {
            info = INFO;
        }
    }
}
