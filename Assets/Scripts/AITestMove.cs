using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITestMove : MonoBehaviour
{
    // Start is called before the first frame update
      // 이동 속도 변수 선언
    public float moveSpeed = 1f;

    void Update()
    {
        // 수평, 수직 입력값 받기
        float horizontalInput = 0f;
        float verticalInput = 0f;

        // WASD 키 입력 확인
        if (Input.GetKey(KeyCode.W)) verticalInput = 1f;
        if (Input.GetKey(KeyCode.S)) verticalInput = -1f;
        if (Input.GetKey(KeyCode.D)) horizontalInput = -1f;
        if (Input.GetKey(KeyCode.A)) horizontalInput = 1f;

        // 이동 벡터 계산
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        
        // 초당 moveSpeed 만큼 이동
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
