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

    public Vector3 _retreatPosition; // 고정으로 후퇴할 포지션

    public Vector3 _targetPosition; // 

    private STATE _state;

    private float _accAttack;  // 누적시간을 받아두기 위한 변수
    private float _accCoolTime; // 누적시간을 받아두기 위한 변수

    private float _attackTime = 3; // 3초에 한번 공격함 
    private float _detectDistance = 4f; // 적이 이정도 거리내로오면 state변경 

    private float _speed = 1.5f;
    private float _attackDistance = 1;

    private bool _attackPossible = true; // 현재 공격이 가능한 상태인지
                                          
    BehaviorNode _idleNode;
    BehaviorNode _walkNode;
    BehaviorNode _attackNode;


    void Start()
    {
        ConditionNode isAtkPossible = new ConditionNode(IsAttackPossible); // 공격할 수 있니?
        ConditionNode distanceOutNode = new ConditionNode(IsPlayerFarAttackDistance_Con); // 공격 범위 밖에 있니? 
        ConditionNode attackInnerNode = new ConditionNode(IsInnerAttakDistance_Con); // 공격 범위 안에 있니?

        DoActionNode changeIDLEStateNode = new DoActionNode(ChangeStateIDLE_Do); // State를 IDLE으로 바꿔라.
        DoActionNode changeAttackStateNode = new DoActionNode(ChangeStateAttack_Do); // State를 공격으로 바꿔라.
        DoActionNode toWalkNode = new DoActionNode(ChangeStateWALK_Do); // STATE를 WALK 로 바꿔라 
        DoActionNode walkActioNode = new DoActionNode(ToPlayerMove_Do); // 플레이어를 향해 이동해라.
        DoActionNode coolTimeNode = new DoActionNode(CoolTime);  // 쿨타임을 돌린다.
        
        SequenceNode IsInnerAttackNode = new SequenceNode();
        IsInnerAttackNode.AddSequence(attackInnerNode);         // 공격범위 안에있니?
        IsInnerAttackNode.AddSequence(isAtkPossible);           // 공격(쿨)할 수 있니?
        IsInnerAttackNode.AddSequence(changeAttackStateNode);   // 공격으로 바꿔줘.

        // IDEL 행동트리 최상단 
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

    // 공격 범위 밖이니 ? 
    private bool IsPlayerFarAttackDistance_Con()
    {
        var distance = Vector3.Distance(PlayerTrans.position, transform.position);
        return distance >= _attackDistance;
    }

    // 공격 범위내에 있니? 
    private bool IsInnerAttakDistance_Con()
    {
        var distance = Vector3.Distance(PlayerTrans.position, transform.position);
        return distance < _attackDistance;
    }
    
    // 공격 할 수 있니? 
    private bool IsAttackPossible()
    {
        return _attackPossible;
    }

    // 공격쿨은 아직이니? 

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

    // 에디터에서만서 실행된느 기능 ---> 개발용도
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
