
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///
/// </summary>

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public static bool isShowUI;//�Ƿ�������UI����
    public GameData gameData;

    private void Awake()
    {
        if (Instance != null)//����������һ��GameManager��ʵ��������
        {
            Destroy(gameObject);//����
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);//��ֹ��������ʱ���ٸö���
        IGlobalManager[] managers = GetComponentsInChildren<IGlobalManager>();
        //��ʼ����Ϸ�����������й�����
        foreach (IGlobalManager item in managers)
        {
            item.Init();
            
        }
    }
    

    /// <summary>
    /// ����Ϸ
    /// </summary>
    public void NewGame()
    {
        gameData = new GameData()
        {
            DataID = $"{UnityEngine.Random.Range(0, 100)}{UnityEngine.Random.Range(0, 100)}{UnityEngine.Random.Range(0, 100)}",
            playerCoord = new SVector3(1, -1, 0),
            Hp = 3,
            savaName = $"{UnityEngine.Random.Range(0, 100)}",
            savaTime = DateTime.Now.ToLocalTime()
        };
        GameSaveManager.NewSaveData(gameData);//�½��浵
        
        Scene_Manager.Instance.LoadSceneAsync(EnumSceneType.����һ��);
    }
    /// <summary>
    /// ������Ϸ
    /// </summary>
     public void LoadGame(GameData gameData)
    {
        this.gameData = gameData;
        Scene_Manager.Instance.LoadSceneAsync(gameData.sceneType);
    }
    public void DeleaGame(GameData gameData)
    {
        GameSaveManager.DeleteSaveData(gameData);
    }

    /// <summary>
    /// ������Ϸ
    /// </summary>
    public void QuitGame()
    {
        GameSaveManager.SaveGameData(gameData);
        UI_Manager.Instance.ShowWindow<UI_BreakCurtainWindow>(2);
        StartCoroutine(DoQuitGame());
    }
    private IEnumerator DoQuitGame()//��ʱ�ر�
    {
        yield return new WaitForSeconds(2F);
        Application.Quit();
    }
    /// <summary>
    /// ���ز˵�
    /// </summary>
    public void BackMenu()
    {
        gameData.savaTime = DateTime.Now.ToLocalTime();//��ʱ
        PlayerController.Instance.SavePlayerData();
        GameSaveManager.SaveGameData(gameData);
        Scene_Manager.Instance.LoadSceneAsync(EnumSceneType.�˵�����);
    }
    
   
}
