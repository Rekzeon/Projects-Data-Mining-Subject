using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DoAnKetThucMon_Nhom3.Make_a_tree_decision;

namespace DoAnKetThucMon_Nhom3
{
    public class TreeDecision
    {
        DecisionNode root = null;

        public DecisionNode Root { get => root; set => root = value; }

        public void addNode2Tree(DecisionNode nodeADD)
        {
            root = nodeADD;
        }

        public ConditionNode findTChatNode(DecisionNode a, string INFO)
        {
            foreach (ConditionNode check in a.Condition)
                if (check.Info == INFO)
                    return check;
                else if (check.PNext != null)
                    findTChatNode(check.PNext, INFO);
            return null;
        }

        public DecisionNode findTTinhNode(DecisionNode Root_Node, string INFO)
        {
            if (Root_Node.Info == INFO)
                return Root_Node;
            else
                foreach (ConditionNode cn in Root_Node.Condition)
                    if (cn.PResult != null)
                        return findTTinhNode(cn.PNext, INFO);
            return null;
                
        }

        public string findResult2Decide(DoiTuong Obj_2_Decide, DecisionNode Root_Node)
        {
            foreach (ConditionNode cn in Root_Node.Condition)
                if (cn.Info.Equals(Obj_2_Decide.takeValueTTinh(Root_Node.Info)))
                    if (cn.PResult != null)
                        return cn.PResult.Info;
                    else
                        return findResult2Decide(Obj_2_Decide, cn.PNext);
            return null;
        }

        public void addNewD(DecisionNode Add_Node, DecisionNode Root_Node, TreeDecision TD)
        {
            if (Root_Node == null)
            {
                //Root_Node = Add_Node;
                TD.root = Add_Node;
                return;
            }    
                
            foreach(ConditionNode cn in Root_Node.Condition)
            {
                if (cn.PResult == null && cn.PNext == null)
                {
                    cn.PNext = Add_Node;
                    return;
                }                        
                if (cn.PNext != null)
                    addNewD(Add_Node, cn.PNext,TD);
            }                   
        }
    }
}
