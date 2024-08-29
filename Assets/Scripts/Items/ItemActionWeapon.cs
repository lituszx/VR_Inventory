using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionWeapon : ItemAction
{
    public override void Action()
    {
        switch (_data.id)
        {
            case 0:
                if (GameManager.player.rightHand != 0)
                {
                    GameManager.player.SetWeapon2(0);
                }
                else if(GameManager.player.leftHand !=0)
                {
                    GameManager.player.SetWeapon1(0);
                }
                else
                {
                    GameManager.player.SetWeapon2(0);
                }
                break;
            case 2:
                if (GameManager.player.rightHand != 1)
                {
                    GameManager.player.SetWeapon2(1);
                }
                else if (GameManager.player.leftHand != 1)
                {
                    GameManager.player.SetWeapon1(1);
                }
                else
                {
                    GameManager.player.SetWeapon2(1);
                }
                break;

        }
        base.Action();
    }
}
