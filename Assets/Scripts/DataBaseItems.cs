using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseItems : MonoBehaviour
{
    public DataRead data;
    public void LoadData()
    {
        data = JsonUtility.FromJson<DataRead>(Resources.Load<TextAsset>("Files/Items").text);
    }
    public ItemData FetchItem(int id)
    {
        for (int i = 0; i < data.itemData.Length; i++)
        {
            if (id == data.itemData[i].id)
                return data.itemData[i];
        }
        return null;
    }
}
[System.Serializable]
public class DataRead
{
    public ItemData[] itemData;
}
[System.Serializable]
public class ItemData
{
    public enum Type { Garbage = 0, Weapon = 1, HealthPotion = 2 }
    public int id;
    public string name = null;
    public Type type = 0;
    public string description = null;
    public int damage = 0;
    public string spritePath = null;
    public bool stackable = false;
    public ItemData(int id = -1)
    {
        this.id = id;
    }
    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>(spritePath);
    }
}
