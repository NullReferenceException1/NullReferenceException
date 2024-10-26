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
    [FoldoutGroup("����")]
    public Text contentText;
    [FoldoutGroup("����")]
    public GameObject loadImage , continueImage;
    [FoldoutGroup("����")]
    public Button openLockButton, quitButton;


    public float textSpeed;

    public override void OnShow(float transitionTime = 0)
    {
        base.OnShow(transitionTime);
        Instance = this;
        contentText.enabled = false;
        //��ť��ʼ��
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
        //TODO:�ڶ��ε���Ŵ����ı���򲻿�
        if (canvasGroup.alpha >= 1) contentText.enabled = true;//����������������
        else if (canvasGroup.alpha <= 0 && exitState)//����������
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
                //������ť//TODO:ֻ���Ų��п�����ť
                openLockButton.gameObject.SetActive(true);
                quitButton.gameObject.SetActive(true);
                if (isKey)//��Կ��
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
        Event_Manager.Instance.EventTrigger(EnumEventType.�����¼�);
        OnClose( 0.5F);
    }

    /// <summary>
    /// �ı����ֻ�
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
