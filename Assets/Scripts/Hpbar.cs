using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 얘는 3D 오브젝에 붙어 있을 UI
public class Hpbar : UIBase
{
    [SerializeField] private Image _hpBar;

    [SerializeField] private Transform _target;

    private void Start()
    {
        _hpBar.fillAmount = 1;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        WorldToScreenPosition();        
    }

    // 월드 좌표를 스크린 좌표로 변환하는 메서드 추가
    public void WorldToScreenPosition()
    {
        if(_target == null)
            return;

        Camera mainCamera = Camera.main;
        transform.position = mainCamera.WorldToScreenPoint(_target.position);        
        
    }

    public void SetHp(int maxHp, int curHp)
    {
        _hpBar.fillAmount = (float)curHp / (float)maxHp;
    }
}
