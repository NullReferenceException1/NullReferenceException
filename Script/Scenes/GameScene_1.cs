using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;


/// <summary>
///地下一层
/// </summary>
public class GameScene_1 : GameSceneBase
{
    public PlayerController player;
    public CameraShaker shaker;
    public CameraPost cameraPost;
    public static GameScene_1 Instance;
    private GameData gameData=>GameManager.Instance.gameData;


    [Button]
    /// <summary>
    /// 游戏场景初始化
    /// </summary>
    public override void Init()
    {
        Instance = this;
        //初始化
        //生成玩家
        player = Instantiate(Res_Manager.Instance.Load<GameObject>("Player")).GetComponent<PlayerController>();
        shaker = Object.FindAnyObjectByType<CameraShaker>(FindObjectsInactive.Include);
        cameraPost = Object.FindAnyObjectByType<CameraPost>(FindObjectsInactive.Include);
        player.Init();
        shaker.Init();
        cameraPost.Init();
        AudioManager.Instance.PlayBgmSound(Res_Manager.Instance.Load<AudioClip>("Magical Ambiance Loop 2"));
        UI_Manager.Instance.ShowWindow<UI_SceneNameWindow>().Show(gameData.sceneType);
    }

    #region 场景事件--开门
    [TextArea]
    public string[] str;
    public void OpenDoorockEvent()
    {
        if (!GameManager.isShowUI)
        {
             UI_TextBoxWindow window =UI_Manager.Instance.ShowWindow<UI_TextBoxWindow>(0.5f);
            window.Show(str, PlayerController.Instance.isKey);
        }
            
    }

    #endregion


}
    
