
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
    public static bool isShowUI;//是否正处于UI界面
    public GameData gameData;

    private void Awake()
    {
        if (Instance != null)//场景中已有一个GameManager的实例化对象
        {
            Destroy(gameObject);//销毁
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);//防止场景加载时销毁该对象
        IGlobalManager[] managers = GetComponentsInChildren<IGlobalManager>();
        //初始化游戏管理器下所有管理器
        foreach (IGlobalManager item in managers)
        {
            item.Init();
            
        }
    }
    

    /// <summary>
    /// 新游戏
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
        GameSaveManager.NewSaveData(gameData);//新建存档
        
        Scene_Manager.Instance.LoadSceneAsync(EnumSceneType.地下一层);
    }
    /// <summary>
    /// 加载游戏
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
    /// 结束游戏
    /// </summary>
    public void QuitGame()
    {
        GameSaveManager.SaveGameData(gameData);
        UI_Manager.Instance.ShowWindow<UI_BreakCurtainWindow>(2);
        StartCoroutine(DoQuitGame());
    }
    private IEnumerator DoQuitGame()//延时关闭
    {
        yield return new WaitForSeconds(2F);
        Application.Quit();
    }
    /// <summary>
    /// 返回菜单
    /// </summary>
    public void BackMenu()
    {
        gameData.savaTime = DateTime.Now.ToLocalTime();//临时
        PlayerController.Instance.SavePlayerData();
        GameSaveManager.SaveGameData(gameData);
        Scene_Manager.Instance.LoadSceneAsync(EnumSceneType.菜单场景);
    }
    
   
}
