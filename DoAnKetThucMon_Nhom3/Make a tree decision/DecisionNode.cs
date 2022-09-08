using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnKetThucMon_Nhom3.Make_a_tree_decision
{
    public class DecisionNode
    {
        public static int index = 1;
        string info;
        List<ConditionNode> condition = new List<ConditionNode>();

        public string Info { get => info; set => info = value; }
        public List<ConditionNode> Condition { get => condition; set => condition = value; }

        //TẠO NODE
        //public void createAaddCondition(string INFO)
        //{
        //    ConditionNode temp = new ConditionNode();
        //    temp.Info = INFO;
        //    temp.PNext = null;
        //    temp.PResult = null;
        //    condition.Add(temp);
        //}
        
        ////TẠO NODE
        //public void createAaddCondition(string INFO, ResultNode KQ)
        //{
        //    ConditionNode temp = new ConditionNode();
        //    temp.Info = INFO;
        //    temp.PNext = null;
        //    temp.PResult = KQ;
        //    condition.Add(temp);
        //}

        public void addNextTTinh2ALL(DecisionNode nextD)
        {
            foreach (ConditionNode cn in condition)
                if (cn.PResult == null)
                    cn.PNext = nextD;
        }

        public void addResult2Condition(string INFO_TChat, ResultNode Result_Node) //add kết quả cho tính chất của thuộc tính đã xác định được kết quả
        {
            foreach(ConditionNode search in condition)
            {
                if (search.Info == INFO_TChat)
                {
                    search.PResult = Result_Node;
                    return;
                }                      
            }    
        }
    }
}
