using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _player;
    private Transform _enemy;

    public void SetPlayer(Transform player)
    {
        _player = player;
    }

    public void SetEnemy(Transform enemy)
    {
        _enemy = enemy;
    }

    private void LateUpdate()
    {
        if (_player == null || _enemy == null) return;

        Vector3 playerPos = _player.position;
        Vector3 enemyPos = _enemy.position;

        // 가운데 포지션 
        Vector3 middlePos = (_player.position +_enemy.position) * 0.5f;

        transform.LookAt(middlePos);

        //transform.position = Vector3.Lerp(transform.position, new Vector3(middlePos.x, middlePos.y + 3f, middlePos.z - 10f), 0.1f);
        // 카메라 포지션 업데이트
        transform.position = new Vector3(middlePos.x, middlePos.y + 3f, middlePos.z - 10f);
    }

    
}
