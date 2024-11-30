using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE
{
    IDLE = 0, 
    WALK,
    ATTACK,
    RETREAT,
}

public class Enemy2D : MonoBehaviour
{
    [SerializeField]
    public Transform PlayerTrans;

    public Vector3 _retreatPosition; // �������� ������ ������

    public Vector3 _targetPosition; // 

    private STATE _state;

    private float _accAttack;  // �����ð��� �޾Ƶα� ���� ����
    private float _accCoolTime; // �����ð��� �޾Ƶα� ���� ����

    private float _attackTime = 3; // 3�ʿ� �ѹ� ������ 
    private float _detectDistance = 4f; // ���� ������ �Ÿ����ο��� state���� 

    private float _speed = 1.5f;
    private float _attackDistance = 1;

    private bool _attackPossible = true; // ���� ������ ������ ��������
                                          
    BehaviorNode _idleNode;
    BehaviorNode _walkNode;
    BehaviorNode _attackNode;


    void Start()
    {
        ConditionNode isAtkPossible = new ConditionNode(IsAttackPossible); // ������ �� �ִ�?
        ConditionNode distanceOutNode = new ConditionNode(IsPlayerFarAttackDistance_Con); // ���� ���� �ۿ� �ִ�? 
        ConditionNode attackInnerNode = new ConditionNode(IsInnerAttakDistance_Con); // ���� ���� �ȿ� �ִ�?

        DoActionNode changeIDLEStateNode = new DoActionNode(ChangeStateIDLE_Do); // State�� IDLE���� �ٲ��.
        DoActionNode changeAttackStateNode = new DoActionNode(ChangeStateAttack_Do); // State�� �������� �ٲ��.
        DoActionNode toWalkNode = new DoActionNode(ChangeStateWALK_Do); // STATE�� WALK �� �ٲ�� 
        DoActionNode walkActioNode = new DoActionNode(ToPlayerMove_Do); // �÷��̾ ���� �̵��ض�.
        DoActionNode coolTimeNode = new DoActionNode(CoolTime);  // ��Ÿ���� ������.
        
        SequenceNode IsInnerAttackNode = new SequenceNode();
        IsInnerAttackNode.AddSequence(attackInnerNode);         // ���ݹ��� �ȿ��ִ�?
        IsInnerAttackNode.AddSequence(isAtkPossible);           // ����(��)�� �� �ִ�?
        IsInnerAttackNode.AddSequence(changeAttackStateNode);   // �������� �ٲ���.

        // IDEL �ൿƮ�� �ֻ�� 
        List<BehaviorNode> idleNode = new List<BehaviorNode>();

        SequenceNode idletoWalk = new SequenceNode();
        idletoWalk.AddSequence(coolTimeNode);
        idletoWalk.AddSequence(distanceOutNode);
        idletoWalk.AddSequence(toWalkNode);

        idleNode.Add(idletoWalk);
        idleNode.Add(IsInnerAttackNode);
        _idleNode = new SelectorNode(idleNode);

        List<BehaviorNode> walkselectNode = new List<BehaviorNode>();

        SequenceNode walkSeqNode = new SequenceNode();
        walkSeqNode.AddSequence(coolTimeNode);
        walkSeqNode.AddSequence(distanceOutNode);
        walkSeqNode.AddSequence(walkActioNode);
        walkSeqNode.AddSequence(IsInnerAttackNode);

        walkselectNode.Add(walkSeqNode);

        _walkNode = new SelectorNode(walkselectNode);

        List<BehaviorNode> attackselectNode = new List<BehaviorNode>();
        attackselectNode.Add(changeIDLEStateNode);
        _attackNode = new SelectorNode(attackselectNode);
    }

    // ���� ���� ���̴� ? 
    private bool IsPlayerFarAttackDistance_Con()
    {
        var distance = Vector3.Distance(PlayerTrans.position, transform.position);
        return distance >= _attackDistance;
    }

    // ���� �������� �ִ�? 
    private bool IsInnerAttakDistance_Con()
    {
        var distance = Vector3.Distance(PlayerTrans.position, transform.position);
        return distance < _attackDistance;
    }
    
    // ���� �� �� �ִ�? 
    private bool IsAttackPossible()
    {
        return _attackPossible;
    }

    // �������� �����̴�? 

    private bool IsNotAttackPossible()
    {
        return _attackPossible == false;
    }

    private void ChangeStateIDLE_Do()
    {
        Debug.Log("ATTACK!!!!!!");
        _state = STATE.IDLE;
    }

    private void ChangeStateWALK_Do()
    {
        Debug.Log("ChangeStateWALK_Do");
        _state = STATE.WALK;
    }

    private void ChangeStateAttack_Do()
    {
        _state = STATE.ATTACK;
        _attackPossible = false;
        _accCoolTime = 0;
    }

    private void ToPlayerMove_Do()
    {
        var todir = PlayerTrans.position - transform.position;
        todir.Normalize();
        transform.position += todir * Time.deltaTime * _speed;
    }

    // �����Ϳ������� ����ȴ� ��� ---> ���߿뵵
    private void OnDrawGizmos()
    {
        var todir = PlayerTrans.position - transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectDistance);
        Gizmos.DrawRay(transform.position, todir);
    }

    private void CoolTime()
    {
        if (_attackPossible)
            return;

        _accCoolTime += Time.deltaTime;

        if (_accCoolTime >= _attackTime)
            _attackPossible = true;
    }

    private void ATTACK_DO()
    {
        Debug.Log("ATTACK!!!!!!");
        _state = STATE.IDLE;
    }

    private void RETREAT_DO()
    {
        CoolTime();

        var retreatVect = transform.position - PlayerTrans.position;
        retreatVect.Normalize();
        retreatVect *= 3;
        _retreatPosition = transform.position + retreatVect;

        var todir = _retreatPosition - transform.position;
        todir.Normalize();

        transform.position += todir * Time.deltaTime * _speed;

        if(_attackPossible)
            _state = STATE.WALK;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(_state)
        {
            case STATE.IDLE:
                //IDLE_DO();
                if(_idleNode != null)
                    _idleNode.Execute();
                break;
            case STATE.WALK:
                //WALK_DO();
                if(_walkNode != null)
                    _walkNode.Execute();
                break;
            case STATE.ATTACK:
                if(_attackNode != null)
                    _attackNode.Execute();
                break;
            case STATE.RETREAT:
                RETREAT_DO();
                break;
        }
        
    }
}
