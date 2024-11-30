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
            // ����Ƽ¯��  3�Ǽӵ��� �����ϰ� ����.
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // õõ�� �����̴°� �������� ���ڴ� 
            _moveStop = true;


        }


        if (Input.GetKeyDown(KeyCode.Space))
        {   
            //transform.forward = new Vector3(1,0,0);
            Vector3 lookB = _targetB.position - _target.position; // target�� targetB�����º���

            lookB.Normalize(); // ������ 1�� �ٲ۴�. 

            Debug.Log("���� TargetB�� ���� ����: " + lookB);

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

        if (Input.GetKey(KeyCode.A)) // �������� ���� ������ �� �ְ���?  
        {
            _rotationEuler += new Vector3(0,90,0) * Time.deltaTime;

            _target.rotation = Quaternion.Euler(_rotationEuler);
        }

        if (Input.GetKey(KeyCode.D)) // ���������� ȸ���ϰ� 
        {
            _rotationEuler -= new Vector3(0, 90, 0) * Time.deltaTime;

            _target.rotation = Quaternion.Euler(_rotationEuler);
        }


    }
}
