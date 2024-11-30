using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BehaviorNode 
{
   public abstract bool Execute();
}

// 조건을 탐색하는 노드 
public class ConditionNode : BehaviorNode
{
    public delegate bool NoedCondition();

    private NoedCondition _condition;

    public ConditionNode(NoedCondition con)
    {
        _condition = con;
    }

    public override bool Execute()
    {
        return _condition();
    } 
}

// 실제 실행하는 노드 
public class DoActionNode : BehaviorNode
{
    public delegate void NodeAction();

    private NodeAction _action;

    public DoActionNode(NodeAction action)
    {
        _action = action;
    }

    public override bool Execute()
    {
        _action();

        return true; // 액션이 성공했다는 가정 
    } 
}

// 자식노드중 하나라도 성공하면 성공으로 간주함 
public class SelectorNode : BehaviorNode
{
    private List<BehaviorNode> _hasNodeList = new List<BehaviorNode>();

    public SelectorNode(List<BehaviorNode> list)
    {
        _hasNodeList = list;
    }

    public override bool Execute()
    {
        foreach (var node in _hasNodeList)
        {
            if(node.Execute())
                return true;
            
        }

        return false;
    }

}

// 모든 자식 노드가 성공해야 성공으로 간주함 
public class SequenceNode :  BehaviorNode
{
    private List<BehaviorNode> _hasNodeList = new List<BehaviorNode>();

    public SequenceNode() {}

    public void AddSequence(BehaviorNode node)
    {
        _hasNodeList.Add(node);
    }


    public override bool Execute()
    {
        foreach (var node in _hasNodeList)
        {
            if(node.Execute() == false)
                return false;
            
        }

        return true;
    }   


}