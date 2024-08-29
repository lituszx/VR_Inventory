using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static GameObject inventorySlot, inventoryItem;
    public static DataBaseItems data;
    public static InputManager input;
    public static GameManagerMonoBehaviour manager;
    public static Inventory inventory;
    public static Player player;
}
public class GameManagerMonoBehaviour : MonoBehaviour
{
    public GameObject prefabSlot, prefabItem;
    public KeyCode touchpadKeyLeft, touchpadKeyRight;
    public GameObject slotsPanel;
    void Start()
    {
        GameManager.player = GameObject.FindObjectOfType<Player>();

        GameManager.inventoryItem = prefabItem;
        GameManager.inventorySlot = prefabSlot;

        GameObject go = new GameObject("Data Base Manager");
        go.transform.parent = transform;
        GameManager.data = go.AddComponent<DataBaseItems>();
        GameManager.data.LoadData();

        go = new GameObject("Input Manager");
        go.transform.parent = transform;
        GameManager.input = go.AddComponent<InputManager>();
        GameManager.manager = this;

        GameManager.input.touchpadKeyLeft = touchpadKeyLeft;
        GameManager.input.touchpadKeyRight = touchpadKeyRight;

        go = new GameObject("Inventory");
        go.transform.parent = transform;
        GameManager.inventory = go.AddComponent<Inventory>();
        

    }
    public void ShowLog(string Text)
    {
        Debug.Log(Text);
    }
}
