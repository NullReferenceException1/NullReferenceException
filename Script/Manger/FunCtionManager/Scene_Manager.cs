using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.PostProcessing.PostProcessResources;

/// <summary>
///场景加载管理器
/// </summary>

public class Scene_Manager : GlobalManagerBase<Scene_Manager>
{
    private PlayerController player;
    private CameraShaker shaker;
    private CameraPost cameraPost;

    private Coroutine DoSceneAsyncCoroutine;
    private GameData gameData=>GameManager.Instance.gameData;
    /// <summary>
    /// 异步加载场景
    /// </summary>
    public void LoadSceneAsync(EnumSceneType sceneType)
    {
        
        
        DoSceneAsyncCoroutine =StartCoroutine(DoSceneAsync(sceneType));
    }
  
    private IEnumerator DoSceneAsync(EnumSceneType sceneType)
    {
        int sceneIndex = GetSceneIndex(sceneType);
        bool isLoad = false;
        UI_BreakCurtainWindow window = UI_Manager.Instance.ShowWindow<UI_BreakCurtainWindow>(2, EnumUI_Layer.Layer_2);
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneIndex);
        scene.allowSceneActivation = false;
        while (!scene.isDone)
        {
            if (window.FadaInEnd() && scene.progress >= 0.9F)
            {
                
                isLoad = true;
                break;
            }
            yield return null;
        }
        while (scene.isDone)
        {
            if (window.FadaInEnd())
            {
                isLoad = true;
                break;
            }
            yield return null;
        }
        if (isLoad)//加载完成
        {
            scene.allowSceneActivation = true;
            UI_Manager.Instance.CloseAllWindow(EnumUI_Layer.Layer_1);
            gameData.sceneType = sceneType;//记录当前的场景
            yield return null;      
            GameObject gameScene = GameObject.FindGameObjectWithTag("GameScene");
            if (gameScene != null) gameScene.GetComponent<GameSceneBase>().Init();
            UI_Manager.Instance.CloseWindow<UI_BreakCurtainWindow>(2);
            
            StopCoroutine(DoSceneAsyncCoroutine);

        }

    }

    

    /// <summary>
    /// 获取场景索引
    /// </summary>
    /// <param name="sceneType"></param>
    /// <returns></returns>
    private int GetSceneIndex(EnumSceneType sceneType)
    {
        switch (sceneType)
        {
            case EnumSceneType.菜单场景:
                return 0;
            case EnumSceneType.地下一层:
                return 1;
            default:
                return -1;
        }
    }
}
