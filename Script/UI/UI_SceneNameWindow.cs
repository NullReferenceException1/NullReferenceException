using System.Collections;

using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// UI场景名称窗口(显示当前所在的场景)
/// </summary>

public class UI_SceneNameWindow : UI_WindowBase
{
    public Text chinaText;
    public Text englishText;
    public float timer;//显示
    public AudioClip clip;
    /// <summary>
    /// 显示内容
    /// </summary>
    public void Show(EnumSceneType enumScene)
    {
        GetSceneName(enumScene,out string chinaText,out string englishText);
        this.chinaText.text = chinaText;
        this.englishText.text = englishText;
        AudioManager.Instance.PlaySoundEffect(clip,1);
    }

    protected override void Update()
    {
        base.Update();
        if (canvasGroup.alpha == 1) StartCoroutine(DoClose());  
    }
  
    IEnumerator DoClose()
    {
        yield return new WaitForSeconds(timer);
        OnClose(2);
        StopCoroutine(DoClose());
    }
    /// <summary>
    /// 根据枚举获取场景名称
    /// </summary>
    /// <param name="enumScene"></param>
    /// <param name="chinaText"></param>
    /// <param name="engilshText"></param>
    private void GetSceneName(EnumSceneType enumScene,out string chinaText,out string engilshText)
    {
         chinaText = "";
        engilshText = "";
        switch (enumScene)
        {
            case EnumSceneType.地下一层:
                chinaText = "地下一层";
                engilshText = "the first floor underground ";
                break;
        }
    }
}
