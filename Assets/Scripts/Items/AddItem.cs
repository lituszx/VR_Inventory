using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    public int id;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("Recogido");
            GameManager.inventory.AddItem(id);
            Destroy(gameObject);
        }
    }
}
