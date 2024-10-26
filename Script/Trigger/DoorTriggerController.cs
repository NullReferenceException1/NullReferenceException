
using System.Threading;
using Cainos.PixelArtPlatformer_Dungeon;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///门 触发控制器
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class DoorTriggerController : MonoBehaviour
{
    private Door door;
    private bool isDoorRanger;
    [FoldoutGroup("引用")]
    public GameObject lockImage, noLockImage;
    [FoldoutGroup("音效")]
    public AudioClip openDoorClip, closeDoorClip, openLockClip; 

    [BoxGroup("是否上锁")]
    public bool isLock=false;//是否上锁
   
    private bool isClearEvent=false ;//是否允许移除事件，有添加才能有移除
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
            Event_Manager.Instance.AddEventListener(EnumEventType.开锁事件, OpenLock);
            isClearEvent = true;
        } 
    }
   
    public void OpenLock()//开锁
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
        if (isClearEvent) Event_Manager.Instance.RemoveEventListener(EnumEventType.开锁事件, OpenLock);
    }
   
    private void OnMouseOver() 
    {
        if (isDoorRanger&&UI_TextBoxWindow.Instance==null)
        {
            Player_Manager.Instance.SetMouseStyle(EnumMouseStyle.提示鼠标);
        }
        else Player_Manager.Instance.SetMouseStyle(EnumMouseStyle.默认鼠标);

        if (isDoorRanger && Input.GetMouseButtonDown(1))
        {
            if (isLock)//锁上的门
            {
                //触发文本
                GameScene_1.Instance.OpenDoorockEvent();
            }
            else//未锁
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
        Player_Manager.Instance.SetMouseStyle(EnumMouseStyle.默认鼠标);
    }
  

}
