
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 游戏数据
/// </summary>
[Serializable]
public class GameData
{
    public string DataID;//作为每一份游戏数据的唯一ID
    /// <summary>
    /// 场景枚举
    /// </summary>
    public EnumSceneType sceneType; 
    /// <summary>
    /// 玩家坐标
    /// </summary>
    public SVector3 playerCoord;
    /// <summary>
    /// 玩家血量
    /// </summary>
    public float Hp;

    /// <summary>
    /// 存档的名字
    /// </summary>
    public string savaName;
    /// <summary>
    /// 存档的时间
    /// </summary>
    public DateTime savaTime;
}
[Serializable]
public struct SVector3
{
    private float x, y, z;

    public SVector3(Vector3 vector3)
    {
        this.x = vector3.x;
        this.y = vector3.y;
        this.z = vector3.z;
    }

    public SVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 GetVector()
    {
        return new Vector3(x,y,z);
    }
}
