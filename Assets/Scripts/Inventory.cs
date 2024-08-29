using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _cantidadSlots = 9;
    private List<GameObject> _slots = new List<GameObject>();
    private List<ItemData> _items = new List<ItemData>();

    void Start()
    {
        for (int i = 0; i < _cantidadSlots; i++)
        {
            _items.Add(new ItemData());
            _slots.Add(Instantiate(GameManager.inventorySlot, GameManager.manager.slotsPanel.transform));
        }
    }
    public void AddItem(int id)
    {
        ItemData itemToAdd = GameManager.data.FetchItem(id);
        int index = CheckItemInInventory(id);
        if (itemToAdd.stackable && (index > -1))
        {
            _slots[index].GetComponentInChildren<InventoryItem>().AddAmount();
        }
        else
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].id == -1)
                {
                    _items[i] = itemToAdd;
                    GameObject obj = Instantiate(GameManager.inventoryItem, _slots[i].transform);
                    obj.name = _items[i].name;
                    switch (itemToAdd.type)
                    {
                        case ItemData.Type.Garbage:
                            obj.AddComponent<ItemAction>();
                            break;
                        case ItemData.Type.Weapon:
                            obj.AddComponent<ItemActionWeapon>();
                            break;
                        case ItemData.Type.HealthPotion:
                            obj.AddComponent<ItemActionHealth>();
                            break;
                        default:
                            break;
                    }
                    obj.GetComponent<ItemAction>().SetItem(itemToAdd);
                    obj.GetComponent<InventoryItem>().SetImage(itemToAdd.GetSprite());
                    break;
                }
            }
        }
    }
    public int CheckItemInInventory(int id)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].id == id) return i;
        }
        return -1;
    }
}
