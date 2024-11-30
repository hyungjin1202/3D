using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoSingletone<TestManager>
{
    AgentManager _agentManager;
    
    void Start()
    {
        _agentManager = new AgentManager();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            _agentManager.CreatePlayer(new Vector3(-1, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _agentManager.CreatePlayer(new Vector3(1,0,0));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _agentManager.CreateMap();
        }


    }
}
