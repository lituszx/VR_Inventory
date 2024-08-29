using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAction : MonoBehaviour
{
    protected InventoryItem _itemUI;
    protected ItemData _data;
    void Start()
    {
        _itemUI = GetComponent<InventoryItem>();
    }
    virtual public void Action()
    {
        if (_itemUI._cantidad == 0)
            Destroy(gameObject);

    }
    public void SetItem(ItemData item)
    {
        _data = item;
    }
}
