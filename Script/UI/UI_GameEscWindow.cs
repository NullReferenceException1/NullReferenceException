using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///��ϷExc����
/// </summary>

public class UI_GameEscWindow : UI_WindowBase
{
    [BoxGroup("������Ϸ��ť"), HideLabel]
    public Button BackGameButton;
    [BoxGroup("���ز˵���ť"), HideLabel]
    public Button BackMenuButton;
    [BoxGroup("��Ϸ���ð�ť"), HideLabel]
    public Button GameSettingButton;
    [BoxGroup("������Ϸ��ť"), HideLabel]
    public Button QuitGameButton;

  
    public override void OnShow(float transitionTime = 0)
    {
        base.OnShow(transitionTime);
        BackGameButton.onClick.AddListener(BackGameButtonOnClick);
        BackMenuButton.onClick.AddListener(BackMenuButtonButtonOnClick);
        GameSettingButton.onClick.AddListener(GameSettingButtonOnClick);
        QuitGameButton.onClick.AddListener(QuitGameButtonOnClick);
    }
    public override void OnClose(float transitionTime = 0)
    {
        base.OnClose(transitionTime);
        BackGameButton.onClick.RemoveListener(BackGameButtonOnClick);
        BackMenuButton.onClick.RemoveListener(BackMenuButtonButtonOnClick);
        GameSettingButton.onClick.RemoveListener(GameSettingButtonOnClick);
        QuitGameButton.onClick.RemoveListener(QuitGameButtonOnClick);
    }



    private void BackGameButtonOnClick()
    {   
        Player_Manager.Instance.CloseEscWindow();
        if (selectClip != null)
            AudioManager.Instance.PlaySoundEffect(selectClip, 0.3f);
    }

    private void BackMenuButtonButtonOnClick()
    {
        
        GameManager.Instance.BackMenu();
        if (selectClip != null)
            AudioManager.Instance.PlaySoundEffect(selectClip, 0.3f);
    }

    private void GameSettingButtonOnClick()
    {
        if (selectClip != null)
            AudioManager.Instance.PlaySoundEffect(selectClip, 0.3f);
    }

    private void QuitGameButtonOnClick()
    {
        GameManager.Instance.QuitGame();
        if (selectClip != null)
            AudioManager.Instance.PlaySoundEffect(selectClip, 0.3f);
    }
}
