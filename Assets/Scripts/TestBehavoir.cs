using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehavoir : MonoBehaviour
{
    BehaviorNode _idleNode;

    private float _checkTime = 3; // 누적시간 확인 시간 

    private float _accTime = 0; // 누적시간용 

    [SerializeField]
    public int Hp = 100;
    

    void Start()
    {
        List<BehaviorNode> list = new List<BehaviorNode>();

        ConditionNode timecheckNode = new ConditionNode(IsOverTime);
        list.Add(timecheckNode);

        ConditionNode hpcheckNode = new ConditionNode(IsHpLessHalf);
        list.Add(hpcheckNode);


        _idleNode = new SelectorNode(list);
    }

    private bool IsOverTime()
    {
        return _checkTime < _accTime;   
    }
    private bool IsHpLessHalf()
    {
        return 50 >= Hp;
    }

    private void Update()
    {
        _accTime += Time.deltaTime;

        if (_idleNode != null)
        {
            bool result = _idleNode.Execute();

            if(result)
            {
                _accTime = 0;
                Hp -= 10;
                Debug.Log("결과 : " + Hp);
            }

        }
    }




}
