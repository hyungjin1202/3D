using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CHARSTATE _state = CHARSTATE.IDLE;

    private Vector3 _direct = Vector3.zero;

    private float _speed = 3;

    private int _maxHp = 100;
    private int _curHp = 0;   

    private Hpbar _hpbar;

    public void SetHpbar(Hpbar hpbar)
    {
        _hpbar = hpbar;
    }

    [SerializeField]
    private Animator _animator;

    private void Awake()
    {
        if (_animator != null)
            _animator = GetComponent<Animator>();

        _curHp = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case CHARSTATE.IDLE:
                IDLE_STATE();
                break;
            case CHARSTATE.WALK:
                WALK_STATE();
                break;
            case CHARSTATE.ATTACK:
                ATTACK_STATE();
                break;
            case CHARSTATE.HIT:
                HIT_STATE();
                break;
        }
    }

    private void IDLE_STATE()
    {
        _direct = Vector3.zero;
        _animator.Play("Idle");
    }

    private void WALK_STATE()
    {
        _animator.Play("Walk");
        transform.position += _direct.normalized * Time.deltaTime * _speed;
        transform.forward = _direct.normalized;
    }

    private void ATTACK_STATE()
    {
        //_animator.Play("Attack");

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime > 0.99f)
        {
            // STate �ٲ��ָ� �ǰڴ�. 
            Debug.Log(stateInfo.normalizedTime);
            _state = CHARSTATE.IDLE;
        }
    }

    private void HIT_STATE()
    {
        _animator.Play("Hit");

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime > 0.99f)
        {
            // STate �ٲ��ָ� �ǰڴ�. 
            Debug.Log(stateInfo.normalizedTime);
            _state = CHARSTATE.IDLE;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter : " + other.name);

        _curHp -= 10;

        _hpbar.SetHp(_maxHp,_curHp);

        Debug.Log("현재 체력 : " + _curHp);

        if(_state != CHARSTATE.HIT)
            _state = CHARSTATE.HIT;
    }
}
