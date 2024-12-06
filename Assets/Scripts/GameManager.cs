using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int Player_DAMAGE = 10;  // 충돌 데미지 상수 정의

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
        
        UIManager.Instance.RemoveUI<StartUI>();

        StartCoroutine(LoadGameSceneAsync("InGameScene"));
    }

    private IEnumerator LoadGameSceneAsync(string sceneName)
    {
        // 로드가 완되면 밑에 코드를 실행한다. 
        yield return SceneManager.LoadSceneAsync(sceneName);
        
        // 배경 맵을 로드하는 코드를 추가해야 함
        GameObject map = Resources.Load<GameObject>("3D/Prefab/Map");
        Debug.Log("맵 리소스 로드 : " + map);    
        GameObject mapInstance = Instantiate(map);
        Debug.Log("맵 인스턴스 생성 : " + mapInstance);

        // 캐릭터랑 적을 띄우면 됩니다. 
        GameObject player = Resources.Load<GameObject>("3D/Prefab/Player");
        GameObject playerInstance = Instantiate(player);
        Debug.Log("플레이어 인스턴스 생성 : " + playerInstance); 


        // 적 캐릭터 로드
        GameObject enemy = Resources.Load<GameObject>("3D/Prefab/Enemy");
        GameObject enemyInstance = Instantiate(enemy);
        Debug.Log("적 인스턴스 생성 : " + enemyInstance);

        // UI를 만들어보자
        Hpbar hpbar = UIManager.Instance.CreateWorldUI<Hpbar>(playerInstance.transform);
        hpbar.SetTarget(playerInstance.transform);

        // UI를 만들어보자
        hpbar = UIManager.Instance.CreateWorldUI<Hpbar>(enemyInstance.transform);
        hpbar.SetTarget(enemyInstance.transform);

        enemyInstance.GetComponent<Enemy>().SetHpbar(hpbar);    


        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetPlayer(playerInstance.transform);
        cameraController.SetEnemy(enemyInstance.transform);

    }

    

    public void PlayerDamage(Enemy enemy)
    {
        enemy.TakeDamage(Player_DAMAGE);
        Debug.Log($"현재 적 체력: {enemy.GetCurrentHp()}");
    }

}
