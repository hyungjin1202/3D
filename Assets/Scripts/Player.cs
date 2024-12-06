using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CHARSTATE
{
    IDLE,
    WALK,
    ATTACK,
    HIT,
}

public class Player : MonoBehaviour
{
    public CHARSTATE _state = CHARSTATE.IDLE;

    private Vector3 _direct = Vector3.zero;

    private float _speed = 3;

    [SerializeField]
    private Animator _animator;

    private void Awake()
    {
        if(_animator != null)
            _animator = GetComponent<Animator>();
    }   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            
        }


        switch(_state)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_state == CHARSTATE.ATTACK || _state == CHARSTATE.HIT)
                return;

            _state = CHARSTATE.ATTACK;

            _animator.CrossFade("Attack", 0.1f);


        }
        
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            if (_state == CHARSTATE.ATTACK || _state == CHARSTATE.HIT)
                return;

            _direct = (Vector3.left + Vector3.back).normalized;            
            _state = CHARSTATE.WALK;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            if (_state == CHARSTATE.ATTACK || _state == CHARSTATE.HIT)
                return;

            _direct = (Vector3.left + Vector3.forward).normalized;
            _state = CHARSTATE.WALK;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (_state == CHARSTATE.ATTACK || _state == CHARSTATE.HIT)
                return;

            _direct = Vector3.left;
            _state = CHARSTATE.WALK;
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            if (_state == CHARSTATE.ATTACK || _state == CHARSTATE.HIT)
                return;

            _direct = (Vector3.right + Vector3.forward).normalized;
            _state = CHARSTATE.WALK;
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            if (_state == CHARSTATE.ATTACK || _state == CHARSTATE.HIT)
                return;

            _direct = (Vector3.right + Vector3.back).normalized;
            _state = CHARSTATE.WALK;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (_state == CHARSTATE.ATTACK || _state == CHARSTATE.HIT)
                return;

            _direct = Vector3.back;
            _state = CHARSTATE.WALK;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (_state == CHARSTATE.ATTACK || _state == CHARSTATE.HIT)
                return;

            _direct = Vector3.right;
            _state = CHARSTATE.WALK;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (_state == CHARSTATE.ATTACK || _state == CHARSTATE.HIT)
                return;

            _direct = Vector3.forward;
            _state = CHARSTATE.WALK;
        }
        
        else
        {
            if (_state == CHARSTATE.ATTACK || _state == CHARSTATE.HIT)
                return;

            _state = CHARSTATE.IDLE;
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
    }


}
