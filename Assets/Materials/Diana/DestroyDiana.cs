using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDiana : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Bullet")
        {
            transform.gameObject.SetActive(false);
        }
    }
}
