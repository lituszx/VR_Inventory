using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionHealth : ItemAction
{
    public override void Action()
    {
        _itemUI.SubstractAmount();
        GameManager.player.Cure(_data.damage);
        base.Action();
    }
}
