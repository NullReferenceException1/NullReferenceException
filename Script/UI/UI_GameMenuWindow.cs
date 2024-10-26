
using UnityEngine.UI;

/// <summary>
///ÓÎÏ·²Ëµ¥´°¿Ú
/// </summary>

public class UI_GameMenuWindow : UI_WindowBase
{
    public Button PlayGameButton;
    public Button LoadGameButton;
    public Button GameSettingButton;
    public Button QuitGameButton;

    
    public override void OnShow(float transitionTime = 0)
    {
        base.OnShow(transitionTime);
        PlayGameButton.onClick.AddListener(PlayGameButtonOnClick);
        LoadGameButton.onClick.AddListener(LoadGameButtonOnClick);
        GameSettingButton.onClick.AddListener(GameSettingButtonOnClick);
        QuitGameButton.onClick.AddListener(QuitGameButtonOnClick);
    }
    public override void OnClose(float transitionTime = 0)
    {
        base.OnClose(transitionTime);
        PlayGameButton.onClick.RemoveListener(PlayGameButtonOnClick);
        LoadGameButton.onClick.RemoveListener(LoadGameButtonOnClick);
        GameSettingButton.onClick.RemoveListener(GameSettingButtonOnClick);
        QuitGameButton.onClick.RemoveListener(QuitGameButtonOnClick);
    }
    private void PlayGameButtonOnClick()
    {    
        GameManager.Instance.NewGame();
        if (selectClip != null)
            AudioManager.Instance.PlaySoundEffect(selectClip, 0.3f);
    }

    private void LoadGameButtonOnClick()
    {
        if (selectClip != null)
            AudioManager.Instance.PlaySoundEffect(selectClip, 0.3f);
        UI_Manager.Instance.ShowWindow<UI_SavaWindow>();
        UI_Manager.Instance.CloseWindow<UI_GameMenuWindow>(0.5F);
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
