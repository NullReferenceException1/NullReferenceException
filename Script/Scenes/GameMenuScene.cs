using DG.Tweening;
using UnityEngine;

/// <summary>
///��Ϸ���˵�����������
/// </summary>
public class GameMenuScene : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private void Start()
    {
        canvasGroup.DOFade(1, 2);
        UI_Manager.Instance.ShowWindow<UI_GameMenuWindow>(2);
        //�������˵�

        AudioManager.Instance.PlayBgmSound(Res_Manager.Instance.Load<AudioClip>("To Mars And Back_Piano Loop"));
    }
   
    
}
