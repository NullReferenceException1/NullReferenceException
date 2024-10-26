
using System.Threading;
using Cainos.PixelArtPlatformer_Dungeon;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///�� ����������
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class DoorTriggerController : MonoBehaviour
{
    private Door door;
    private bool isDoorRanger;
    [FoldoutGroup("����")]
    public GameObject lockImage, noLockImage;
    [FoldoutGroup("��Ч")]
    public AudioClip openDoorClip, closeDoorClip, openLockClip; 

    [BoxGroup("�Ƿ�����")]
    public bool isLock=false;//�Ƿ�����
   
    private bool isClearEvent=false ;//�Ƿ������Ƴ��¼�������Ӳ������Ƴ�
    private void Start()
    {
        door = GetComponent<Door>();
        lockImage.gameObject.SetActive(false);
        noLockImage.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isDoorRanger = true;
        isClearEvent = false;
        if (isLock) 
        {
            lockImage.gameObject.SetActive(true);
            Event_Manager.Instance.AddEventListener(EnumEventType.�����¼�, OpenLock);
            isClearEvent = true;
        } 
    }
   
    public void OpenLock()//����
    {
        isLock = false;
        lockImage.gameObject.SetActive(false);
        noLockImage.gameObject.SetActive(true);
        AudioManager.Instance.PlaySoundEffect(openLockClip);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        lockImage.gameObject.SetActive(false);
        noLockImage.gameObject.SetActive(false);
        isDoorRanger = false;
        if (isClearEvent) Event_Manager.Instance.RemoveEventListener(EnumEventType.�����¼�, OpenLock);
    }
   
    private void OnMouseOver() 
    {
        if (isDoorRanger&&UI_TextBoxWindow.Instance==null)
        {
            Player_Manager.Instance.SetMouseStyle(EnumMouseStyle.��ʾ���);
        }
        else Player_Manager.Instance.SetMouseStyle(EnumMouseStyle.Ĭ�����);

        if (isDoorRanger && Input.GetMouseButtonDown(1))
        {
            if (isLock)//���ϵ���
            {
                //�����ı�
                GameScene_1.Instance.OpenDoorockEvent();
            }
            else//δ��
            {
                if (door.IsOpened == false)
                {
                    door.Open();
                    AudioManager.Instance.PlaySoundEffect(openDoorClip);
                }
                else
                {
                    door.Close();               
                    AudioManager.Instance.PlaySoundEffect(closeDoorClip,0.3F,0.5F);
                }
            
            }
        }
    }
    private void OnMouseExit()
    {
        Player_Manager.Instance.SetMouseStyle(EnumMouseStyle.Ĭ�����);
    }
  

}
