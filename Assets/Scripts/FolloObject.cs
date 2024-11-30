using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolloObject : MonoBehaviour
{
    [SerializeField]  
    private Transform _target;

    [SerializeField]
    private Vector3 _distance = new Vector3(0,3,5);
    
    

    // Update is called once per frame
    void LateUpdate()
    {
        if(_target != null)
        {   
            transform.position = Vector3.Lerp(transform.position, _target.position + _distance, 1f * Time.deltaTime);
            LookAt();
        }        
    }

    private void LookAt()
    {
        Vector3 look = _target.position - transform.position;
        look.Normalize();
        transform.forward = look;

        
    }
}
