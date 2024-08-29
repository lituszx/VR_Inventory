using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject prefabBulletGlock, prefabBulletScar, emptyBulletLeft, emptyBulletRight;
    public GameObject glockSound, scarSound;
    private const int MAX_HEALTH = 100;
    public GameObject Teleport, laserUI;
    public Transform canvasInventory;
    private Vector3 canvasInventoryDefaultScale;
    public HealthUI uiHealth;
    private int _health;
    public GameObject[] weapon1, weapon2;
    private int _currentWeapon1 = -1, _currentWeapon2 = -1;
    public int leftHand { get { return _currentWeapon1; } }
    public int rightHand { get { return _currentWeapon2; } }
    private void Start()
    {
        Invoke("AddItems", 1);
        canvasInventoryDefaultScale = canvasInventory.localScale;
        SetHealth(60);
        ResetWeapons();
    }
    public void ResetWeapons()
    {
        for (int i = 0; i < weapon1.Length; i++)
        {
            weapon1[i].SetActive(false);
        }
        for (int i = 0; i < weapon2.Length; i++)
        {
            weapon2[i].SetActive(false);
        }
    }
    public void SetWeapon1(int index)
    {
        if (_currentWeapon1 > -1)
            weapon1[_currentWeapon1].SetActive(!weapon1[_currentWeapon1].activeSelf);
        if (_currentWeapon1 != index)
        {
            _currentWeapon1 = index;
            weapon1[_currentWeapon1].SetActive(!weapon1[_currentWeapon1].activeSelf);
        }
        if (weapon1[_currentWeapon1].activeSelf == false)
            _currentWeapon1 = -1;
    }
    public void SetWeapon2(int index)
    {
        if (_currentWeapon2 > -1)
            weapon2[_currentWeapon2].SetActive(!weapon2[_currentWeapon2].activeSelf);
        if (_currentWeapon2 != index)
        {
            _currentWeapon2 = index;
            weapon2[_currentWeapon2].SetActive(!weapon2[_currentWeapon2].activeSelf);
        }
        if (weapon2[_currentWeapon2].activeSelf == false)
            _currentWeapon2 = -1;
    }
    private void AddItems()
    {
        GameManager.inventory.AddItem(1);
        GameManager.inventory.AddItem(1);
        GameManager.inventory.AddItem(0);
        GameManager.inventory.AddItem(2);
    }
    void Update()
    {
        Teleport.SetActive(GameManager.input.touchpadLeft);
        laserUI.SetActive(GameManager.input.touchpadRight);
        if (GameManager.input.touchpadRight)
        {
            canvasInventory.localScale = canvasInventoryDefaultScale;
        }
        else
        {
            canvasInventory.localScale = Vector3.zero;
        }
        if (!GameManager.input.touchpadLeft && !GameManager.input.touchpadRight)
        {
            if (GameManager.input.triggerLeftDown && _currentWeapon1 != -1)
            {
                //DISPARAR
                if (_currentWeapon1 == 0)
                {
                    weapon1[0].gameObject.GetComponent<Animator>().SetTrigger("Shoot");
                    GameObject newBullet = Instantiate(prefabBulletGlock, emptyBulletLeft.transform.position, transform.GetChild(0).localRotation);
                    GameObject newSound = Instantiate(glockSound, transform.position, Quaternion.Euler(0, 0, 0));
                    Destroy(newSound, 2f);
                }
                if (_currentWeapon1 == 1)
                {
                    weapon1[1].gameObject.GetComponentInChildren<Animator>().SetTrigger("Shoot");
                    GameObject newBullet = Instantiate(prefabBulletScar, emptyBulletLeft.transform.position, transform.GetChild(0).localRotation);
                    GameObject newSound = Instantiate(scarSound, transform.position, Quaternion.Euler(0, 0, 0));
                    Destroy(newSound, 2f);
                }

            }
            if (GameManager.input.triggerRightDown && _currentWeapon2 != -1)
            {
                //DISPARAR
                if (_currentWeapon2 == 0)
                {
                    weapon2[0].gameObject.GetComponent<Animator>().SetTrigger("Shoot");
                    GameObject newBullet = Instantiate(prefabBulletGlock, emptyBulletRight.transform.position, transform.GetChild(0).localRotation);
                    GameObject newSound = Instantiate(glockSound, transform.position, Quaternion.Euler(0, 0, 0));
                    Destroy(newSound, 2f);
                }
                if (_currentWeapon2 == 1)
                {
                    weapon2[1].gameObject.GetComponentInChildren<Animator>().SetTrigger("Shoot");
                    GameObject newBullet = Instantiate(prefabBulletScar, emptyBulletRight.transform.position, transform.GetChild(0).localRotation);
                    GameObject newSound = Instantiate(scarSound, transform.position, Quaternion.Euler(0, 0, 0));
                    Destroy(newSound, 2f);
                }
            }
        }
    }
    public void SetHealth(int health)
    {
        _health = health;
        uiHealth.SetValue((float)_health / MAX_HEALTH);
    }
    public void Cure(int health)
    {
        if (_health < MAX_HEALTH)
        {
            _health = _health + health;
        }
        uiHealth.SetValue((float)_health / MAX_HEALTH);
    }
}
