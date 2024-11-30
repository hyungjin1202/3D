using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    private float _accTime;

    // Update is called once per frame
    void Update()
    {
        _accTime += Time.deltaTime * 5;

        Vector3 pos = transform.position;

        float height = Mathf.Sin(_accTime);

        Debug.Log(height);

        pos.y += height * 5;

        transform.position = pos;



    }
}
