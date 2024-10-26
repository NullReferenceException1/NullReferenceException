using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
///UI存档页面
/// </summary>

public class UI_SavaWindow : UI_WindowBase
{
    public GameObject savaButtonPrefab;
    public Button backButton;
    public Transform root;
    private SaveData data;
    private List<string> saveIDLis=new List<string>();
    public override void OnShow(float transitionTime = 0)
    {
        base.OnShow(transitionTime);
        backButton.onClick.AddListener(BackButtonOnClick);
        //先获取存档数据，拿到已有的存档id
         data = GameSaveManager.GetSaveData();
        this.saveIDLis = data.saveIDLis;
        foreach (string item in saveIDLis)
        {
            GameData gameData = GameSaveManager.GetGameData(item);
            Instantiate(savaButtonPrefab, root).GetComponent<SavaPanel>().Init(gameData);
        }
    }

    private void BackButtonOnClick()
    {
        UI_Manager.Instance.CloseWindow<UI_SavaWindow>(0.5F);
        UI_Manager.Instance.ShowWindow<UI_GameMenuWindow>(0.5F);
    }

   
    




}
