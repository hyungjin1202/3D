using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BehaviorNode 
{
   public abstract bool Execute();
}

// ������ Ž���ϴ� ��� 
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

// ���� �����ϴ� ��� 
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

        return true; // �׼��� �����ߴٴ� ���� 
    } 
}

// �ڽĳ���� �ϳ��� �����ϸ� �������� ������ 
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

// ��� �ڽ� ��尡 �����ؾ� �������� ������ 
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