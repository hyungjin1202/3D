using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.CreatePageUI<StartUI>();         
    }

   
}
