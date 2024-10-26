using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

using UnityEngine.UI;

/// <summary>
///
/// </summary>

public class UI_TextBoxWindow : UI_WindowBase
{
    public static UI_TextBoxWindow Instance;
    private string[] contents;
    private int count;
    private bool isScrolling;
    private bool isKey;
    [FoldoutGroup("引用")]
    public Text contentText;
    [FoldoutGroup("引用")]
    public GameObject loadImage , continueImage;
    [FoldoutGroup("引用")]
    public Button openLockButton, quitButton;


    public float textSpeed;

    public override void OnShow(float transitionTime = 0)
    {
        base.OnShow(transitionTime);
        Instance = this;
        contentText.enabled = false;
        //按钮初始化
        openLockButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        openLockButton.interactable = false;
        openLockButton.onClick.AddListener(OpenLockButtonClick);
        quitButton.onClick.AddListener(QuitButtonClick);
    }


    public void Show(string[] contents, bool isKey)
    {
        this.contents = contents;
        this.isKey = isKey;
        ScrollingText();
        count++;
    }
  
    protected override void Update()
    {
        //TODO:第二次点击门触发文本框打不开
        if (canvasGroup.alpha >= 1) contentText.enabled = true;//淡入结束后加载文字
        else if (canvasGroup.alpha <= 0 && exitState)//淡出后隐藏
        {
            count = 0;
            Instance = null;
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(0)&& !isScrolling)
        {
            if (count < contents.Length)
            {
                ScrollingText();
                count++;
            }
            else
            {
                //弹出按钮//TODO:只有门才有开锁按钮
                openLockButton.gameObject.SetActive(true);
                quitButton.gameObject.SetActive(true);
                if (isKey)//有钥匙
                {
                    openLockButton.interactable = true;
                }
            }

        }
    }

    private void QuitButtonClick()
    {
        OnClose(0.5F);
    }

    private void OpenLockButtonClick()
    {
        Event_Manager.Instance.EventTrigger(EnumEventType.开锁事件);
        OnClose( 0.5F);
    }

    /// <summary>
    /// 文本打字机
    /// </summary>
    private void ScrollingText()
    {
        StartCoroutine(DoScrollingText());
    }
    private IEnumerator DoScrollingText()
    {
        continueImage.SetActive(false);
        loadImage.SetActive(true);
        isScrolling = true;
        contentText.text = "";
        foreach (char letter in contents[count].ToCharArray())
        {
            
            contentText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        isScrolling = false;
        continueImage.SetActive(true);
        loadImage.SetActive(false);
        StopCoroutine(DoScrollingText());
    }

}
