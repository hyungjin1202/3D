using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : UIBase
{
    [SerializeField]
    private Button _startButton;

    void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        GameManager.Instance.StartGame();       
    }

}
