using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class AgentManager 
{ 
    public void CreatePlayer(Vector3 spawnPostion)
    {
        Debug.Log("�÷��̾� ����");
        // �÷��̾� �Ʊ� ����� ��ȯ..
        var res = Resources.Load("3D/Prefab/KyleRobot");
        GameObject go = Object.Instantiate(res) as GameObject;
        go.transform.position = spawnPostion;
    }   
   
    // ���� ť�� ȣ�� 
    public void CreateMap()
    {
        var res = Resources.Load("3D/Prefab/Map");
        GameObject go = Object.Instantiate(res) as GameObject;
    }    

    public void Clear()
    {
       
    }

}
