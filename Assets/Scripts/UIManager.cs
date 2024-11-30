using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    // 싱글톤 인스턴스

    [SerializeField]
    private Canvas _uiCanvas;

    [SerializeField]
    private EventSystem _eventSystem;

    private static UIManager instance;
    
    // 외부에서 접근 가능한 프로퍼티
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("UIManager가 존재하지 않습니다!");


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
            DontDestroyOnLoad(_uiCanvas.gameObject);
            DontDestroyOnLoad(_eventSystem.gameObject);
        }
        else
        {
            // 이미 존재하는 경우 새로운 인스턴스 제거
            Destroy(gameObject);
        }
    }

    private Dictionary<string, UIBase> _uiList = new Dictionary<string, UIBase>();

    public T CreatePageUI<T>() where T : UIBase
    {
        string pageName = typeof(T).Name;

        GameObject uiObject = Resources.Load<GameObject>($"UI/Prefab/{pageName}");
        GameObject uiInstance = Instantiate(uiObject, _uiCanvas.transform);

        T ui = uiInstance.GetComponent<T>();
        _uiList.Add(pageName, ui);

        return ui;
    }

    public void RemoveUI<T>() where T : UIBase
    {
        string pageName = typeof(T).Name;
        if (_uiList.ContainsKey(pageName))
        {
            Destroy(_uiList[pageName].gameObject);
            _uiList.Remove(pageName);
        }
    }
}
