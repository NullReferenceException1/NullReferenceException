
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


/// <summary>
///游戏存档管理器
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
    /// 新建存档
    /// </summary>
    public static void NewSaveData(GameData gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();//二进制序列化器
        SaveData data;
 
        if(new FileInfo(savePath).Exists)//如果存档文件存在
        {
            //先读取存档文件
            using (FileStream fileStream = File.Open(savePath, FileMode.Open))
            {
                data = (SaveData)bf.Deserialize(fileStream);
            }
            //转换成SaveData后再写入数据
            
            data.saveIDLis.Add(gameData.DataID);
     

            //创建新的文件覆盖掉旧文件
            using (FileStream fileStream = File.Create(savePath))
            {
                bf.Serialize(fileStream, data);
            }  
        }
        else//不存在就创建一个
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
    /// 获取存档
    /// </summary>
    /// <returns></returns>
    public static SaveData GetSaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fileStream = File.Open(savePath, FileMode.Open))//打开文件
        {
            return (SaveData)bf.Deserialize(fileStream);
        }
    }
    /// <summary>
    /// 删除存档
    /// </summary>
    /// <param name="gameData"></param>
    public static void DeleteSaveData(GameData gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();//二进制序列化器
        SaveData data;

        if (new FileInfo(savePath).Exists)//如果存档文件存在
        {
            DeleteGameDate(gameData);
            //先读取存档文件
            using (FileStream fileStream = File.Open(savePath, FileMode.Open))
            {
                data = (SaveData)bf.Deserialize(fileStream);
            }
            //转换成SaveData后再写入数据
            data.saveIDLis.Remove(gameData.DataID);
            //创建新的文件覆盖掉旧文件
            using (FileStream fileStream = File.Create(savePath))
            {
                bf.Serialize(fileStream, data);
            }  
        }   
    }


    /// <summary>
    /// 保存游戏数据
    /// </summary>
    /// <returns></returns>
    public static void SaveGameData(GameData gameData)
    {

        BinaryFormatter bf = new BinaryFormatter();//二进制序列化器
        using (FileStream fileStream = File.Create(filePath + gameData.DataID))//创建文件
        {
            bf.Serialize(fileStream, gameData);

        }
    }
   
    /// <summary>
    /// 获取游戏数据
    /// </summary>
    /// <returns></returns>
    public static GameData GetGameData(string id)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fileStream = File.Open(filePath+ id, FileMode.Open))//打开文件
        {
            return (GameData)bf.Deserialize(fileStream);
        }
    }
   
    /// <summary>
    /// 删除游戏数据
    /// </summary>
    public static void DeleteGameDate(GameData gameData)
    {
        File.Delete(filePath+gameData.DataID);
    }

    
}
