
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;


/// <summary>
/// 玩家管理类
/// </summary>
public class Player_Manager:GlobalManagerBase<Player_Manager>
{

    public static GameData gameData=>GameManager.Instance.gameData;
    private Coroutine deathEventCoroutine;
    [FoldoutGroup("鼠标样式")]
    private Texture2D currentMouseStyle;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !GameManager.isShowUI&&PlayerController.Instance!=null)
        {
            ShowEscWindow();
        }
    }



    /// <summary>
    /// 切换鼠标样式
    /// </summary>
    /// <param name="mouseStyle"></param>
    public void SetMouseStyle(EnumMouseStyle mouseStyle)
    {
        string name= "DefaultMouse";
        switch (mouseStyle)
        {
            case EnumMouseStyle.默认鼠标:
                name = "DefaultMouse";
                break;
            case EnumMouseStyle.提示鼠标:
                name = "HintMouse";
                break;
            case EnumMouseStyle.钥匙鼠标:
                name = "KeyMouse"; 
                break;
            default:
                break;
        }
        currentMouseStyle =(Texture2D)Resources.Load(name);
        Cursor.SetCursor(currentMouseStyle, Vector2.zero, CursorMode.Auto);
    }


    #region 玩家动态数据存储
    /// <summary>
    /// 保存玩家当前的坐标
    /// </summary>
    public static void SavePlayerCoord(Vector3 postion)
    {
       gameData.playerCoord = new SVector3(postion);
    }
    /// <summary>
    /// 获取玩家保存的坐标
    /// </summary>
    public static Vector3 GetPlayerCoord()
    {
        return gameData.playerCoord.GetVector();
    }
   /// <summary>
   /// 保存玩家当前的血量
   /// </summary>
   /// <param name="value"></param>
    public static void SavePlayerHP(float value)
    {
        gameData.Hp = value;
    }
    /// <summary>
    /// 获取玩家保存的血量
    /// </summary>
    /// <returns></returns>
    public static float GetPlayerHP()
    {
        return gameData.Hp;
    }
    #endregion
    #region UI界面
    /// <summary>
    /// 打开ESC窗户
    /// </summary>
    public void ShowEscWindow()
    {
        UI_Manager.Instance.ShowWindow<UI_GameEscWindow>(1);
    }
    /// <summary>
    /// 关闭ESC窗户
    /// </summary>
    public void CloseEscWindow()
    {
        UI_Manager.Instance.CloseWindow<UI_GameEscWindow>(1);
    }
    #endregion
   


    #region 玩家死亡重生事件
    /// <summary>
    /// 玩家死亡重生事件
    /// </summary>
    public void PlayerDeathEvent()
    {
        deathEventCoroutine = StartCoroutine(DoDeathEvent());
    }
    IEnumerator DoDeathEvent()
    {
        yield return null;
        while (true)
        {
            if (UI_BreakCurtainWindow.Instance.FadaInEnd())//幕布淡入结束
            {
                PlayerController.Instance.ReloadInit();//重新加载玩家
                StopCoroutine(deathEventCoroutine);
                break;
            }
            yield return null;
        }
    }

    #endregion
}

