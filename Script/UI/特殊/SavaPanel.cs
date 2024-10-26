using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
///´æµµÒ³Ãæ
/// </summary>

public class SavaPanel : MonoBehaviour
{
    public Text Name;
    public Text Time;

    private GameData gameData;
    public Button deleaButton;
    public void Init(GameData gameData)
    {
        this.gameData = gameData;
        GetComponent<Button>().onClick.AddListener(LoadGameOnClick);
        deleaButton.onClick.AddListener(DeleaButtonOnClick);
        Name.text = gameData.savaName;
        Time.text = (gameData.savaTime).ToString();
    }

    private void DeleaButtonOnClick()//É¾³ý´æµµ
    {     
        GameManager.Instance.DeleaGame(gameData);
        Destroy(gameObject);
    }

    private void LoadGameOnClick()//¼ÓÔØ´æµµ
    {
        GameManager.Instance.LoadGame(gameData);
    }
   
}
