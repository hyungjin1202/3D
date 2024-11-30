using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDrop : MonoBehaviour
{
    void Start()
    {
        
    }   

    

    // Update is called once per frame
    void Update()
    {
        //transform.position += Vector3.down  * Time.deltaTime;
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("OnCollisionEnter :" + collision.gameObject.name);
    }


}
