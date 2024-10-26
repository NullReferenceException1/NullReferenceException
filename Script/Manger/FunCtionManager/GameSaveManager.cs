
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


/// <summary>
///��Ϸ�浵������
/// </summary>
public static class GameSaveManager
{

    public static string filePath;
    public static string savePath;
    static GameSaveManager()
    {
        filePath = $"{Application.persistentDataPath}/{nameof(GameData)}";
        savePath = $"{Application.persistentDataPath}/{nameof(SaveData)}";
    }

    /// <summary>
    /// �½��浵
    /// </summary>
    public static void NewSaveData(GameData gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();//���������л���
        SaveData data;
 
        if(new FileInfo(savePath).Exists)//����浵�ļ�����
        {
            //�ȶ�ȡ�浵�ļ�
            using (FileStream fileStream = File.Open(savePath, FileMode.Open))
            {
                data = (SaveData)bf.Deserialize(fileStream);
            }
            //ת����SaveData����д������
            
            data.saveIDLis.Add(gameData.DataID);
     

            //�����µ��ļ����ǵ����ļ�
            using (FileStream fileStream = File.Create(savePath))
            {
                bf.Serialize(fileStream, data);
            }  
        }
        else//�����ھʹ���һ��
        {
            data =new SaveData();
            data.saveIDLis.Add(gameData.DataID);
            using (FileStream fileStream = File.Create(savePath))
            {
                bf.Serialize(fileStream, data);
            }
        }
        SaveGameData(gameData);
    }

    /// <summary>
    /// ��ȡ�浵
    /// </summary>
    /// <returns></returns>
    public static SaveData GetSaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fileStream = File.Open(savePath, FileMode.Open))//���ļ�
        {
            return (SaveData)bf.Deserialize(fileStream);
        }
    }
    /// <summary>
    /// ɾ���浵
    /// </summary>
    /// <param name="gameData"></param>
    public static void DeleteSaveData(GameData gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();//���������л���
        SaveData data;

        if (new FileInfo(savePath).Exists)//����浵�ļ�����
        {
            DeleteGameDate(gameData);
            //�ȶ�ȡ�浵�ļ�
            using (FileStream fileStream = File.Open(savePath, FileMode.Open))
            {
                data = (SaveData)bf.Deserialize(fileStream);
            }
            //ת����SaveData����д������
            data.saveIDLis.Remove(gameData.DataID);
            //�����µ��ļ����ǵ����ļ�
            using (FileStream fileStream = File.Create(savePath))
            {
                bf.Serialize(fileStream, data);
            }  
        }   
    }


    /// <summary>
    /// ������Ϸ����
    /// </summary>
    /// <returns></returns>
    public static void SaveGameData(GameData gameData)
    {

        BinaryFormatter bf = new BinaryFormatter();//���������л���
        using (FileStream fileStream = File.Create(filePath + gameData.DataID))//�����ļ�
        {
            bf.Serialize(fileStream, gameData);

        }
    }
   
    /// <summary>
    /// ��ȡ��Ϸ����
    /// </summary>
    /// <returns></returns>
    public static GameData GetGameData(string id)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fileStream = File.Open(filePath+ id, FileMode.Open))//���ļ�
        {
            return (GameData)bf.Deserialize(fileStream);
        }
    }
   
    /// <summary>
    /// ɾ����Ϸ����
    /// </summary>
    public static void DeleteGameDate(GameData gameData)
    {
        File.Delete(filePath+gameData.DataID);
    }

    
}
