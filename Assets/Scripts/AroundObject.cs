using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundObject : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private float _distance = 3;

    private float _angle = 0;

    private Vector3 _direct = new Vector3(0, 0, 1);
    
    
    void Update()
    {
        if(_target != null)
        {
            _angle += Time.deltaTime * 100; // -> 쿼터니언 

            Quaternion qAngle = Quaternion.Euler(new Vector3(0,_angle,0));

            Vector3 rotateDirect = qAngle * _direct; // 여기까지가 회전된 벡터
            rotateDirect *= _distance;

            transform.position = _target.position + rotateDirect;            
        }

    }
}
