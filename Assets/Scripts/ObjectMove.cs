using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Transform _targetB;

    private float Speed = 3f;

    private Vector3 _rotationEuler;

    private bool _move = false;
    private bool _moveStop = false;

    private void Start()
    {
        if(_target != null)
        {
            _rotationEuler = _target.rotation.eulerAngles;
        }

        

    }

    void Update()
    {
        if (_target == null)
            return;

        if(_move)
        {
            _target.position += Vector3.right * Time.deltaTime * Speed;

            if(Speed <= 0)
            {
                _move = false;
                _moveStop = false;
            }
        }

        if (_moveStop)
        {   
            Speed = Mathf.Lerp(Speed, 0, 3.5f * Time.deltaTime);

            if (Speed < 0.01f)
                Speed = 0;

            
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Speed = 3;
            _move = true;   
            // 유니티짱이  3의속도로 일정하게 간다.
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 천천히 움직이는게 멈췄으면 좋겠다 
            _moveStop = true;


        }


        if (Input.GetKeyDown(KeyCode.Space))
        {   
            //transform.forward = new Vector3(1,0,0);
            Vector3 lookB = _targetB.position - _target.position; // target이 targetB를보는벡터

            lookB.Normalize(); // 단위를 1로 바꾼다. 

            Debug.Log("내가 TargetB를 보는 방향: " + lookB);

            _target.forward = lookB;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            _target.position += Vector3.left * Time.deltaTime * Speed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _target.position += Vector3.right * Time.deltaTime * Speed;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _target.position += Vector3.forward * Time.deltaTime * Speed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _target.position += Vector3.back * Time.deltaTime * Speed;
        }

        if (Input.GetKey(KeyCode.A)) // 왼쪽으로 돌게 변경할 수 있겠죠?  
        {
            _rotationEuler += new Vector3(0,90,0) * Time.deltaTime;

            _target.rotation = Quaternion.Euler(_rotationEuler);
        }

        if (Input.GetKey(KeyCode.D)) // 오른쪽으로 회전하게 
        {
            _rotationEuler -= new Vector3(0, 90, 0) * Time.deltaTime;

            _target.rotation = Quaternion.Euler(_rotationEuler);
        }


    }
}
