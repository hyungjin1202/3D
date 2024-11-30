using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class AgentManager 
{ 
    public void CreatePlayer(Vector3 spawnPostion)
    {
        Debug.Log("플레이어 생성");
        // 플레이어 아까 만든거 소환..
        var res = Resources.Load("3D/Prefab/KyleRobot");
        GameObject go = Object.Instantiate(res) as GameObject;
        go.transform.position = spawnPostion;
    }   
   
    // 넓은 큐브 호출 
    public void CreateMap()
    {
        var res = Resources.Load("3D/Prefab/Map");
        GameObject go = Object.Instantiate(res) as GameObject;
    }    

    public void Clear()
    {
       
    }

}
