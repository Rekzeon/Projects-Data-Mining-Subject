using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnKetThucMon_Nhom3.Make_a_tree_decision
{
    public class ConditionNode //Node Chưa giá trị quyết định
    {
        static int index = 1;  //Để phân biệt với node chứa giá trị quyết định
        string info;
        DecisionNode pNext;
        ResultNode pResult;

        public ConditionNode(string a)
        {
            info = a;
            PNext = null;
            PResult = null;
        }

        public string Info { get => info; set => info = value; }
        public DecisionNode PNext { get => pNext; set => pNext = value; }
        public ResultNode PResult { get => pResult; set => pResult = value; }
    }
}
