using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Animator anim;
    public float contador;
    public bool opened;
    void Start()
    {
        anim = GetComponent<Animator>();
        contador = 0;
    }
    private void Update()
    {
        if (opened == true)
        {
            contador += Time.deltaTime;
            if(contador >= 5)
            {
                anim.SetBool("Active", false);
                contador = 0;
                opened = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            anim.SetBool("Active", true);
            anim.SetBool("Null", false);
            opened = true;
        }
    }
}
