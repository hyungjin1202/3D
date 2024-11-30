using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
   // 싱글톤 인스턴스
    private static GameManager instance;
    
    // 외부에서 접근 가능한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("GameManager가 존재하지 않습니다!");
            return instance;
        }
    }

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 존재하는 경우 새로운 인스턴스 제거
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("InGameScene"); 
        UIManager.Instance.RemoveUI<StartUI>(); 

        // 배경 맵을 로드하는 코드를 추가해야 함
        GameObject map = Resources.Load<GameObject>("3D/Prefab/Map");
        Debug.Log("맵 리소스 로드 : " + map);    
        GameObject mapInstance = Instantiate(map);
        Debug.Log("맵 인스턴스 생성 : " + mapInstance);

        DontDestroyOnLoad(mapInstance);
    }




}
