using System.Collections;

using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// UI�������ƴ���(��ʾ��ǰ���ڵĳ���)
/// </summary>

public class UI_SceneNameWindow : UI_WindowBase
{
    public Text chinaText;
    public Text englishText;
    public float timer;//��ʾ
    public AudioClip clip;
    /// <summary>
    /// ��ʾ����
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
    /// ����ö�ٻ�ȡ��������
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
            case EnumSceneType.����һ��:
                chinaText = "����һ��";
                engilshText = "the first floor underground ";
                break;
        }
    }
}
