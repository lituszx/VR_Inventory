using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Color selectedColor;
    private Color unselectedColor;
    private Image _img;
    private Text _text;
    private Image _parentImage;

    private int cantidad = 0;
    public int _cantidad { get { return cantidad; } }
    void Awake()
    {
        _img = GetComponent<Image>();
        _text = GetComponentInChildren<Text>();
        _parentImage = transform.parent.GetComponent<Image>();
        unselectedColor = _parentImage.color;
        ResetAmount();
    }
    public void AddAmount(int amount = 1)
    {
        if (cantidad + amount > 99) return;
        cantidad += amount;
        _text.text = cantidad.ToString("00");
        _text.gameObject.SetActive(cantidad > 1);
    }
    public void SubstractAmount(int amount = 1)
    {
        if (cantidad - amount < 0) return;
        cantidad -= amount;
        _text.text = cantidad.ToString("00");
        _text.gameObject.SetActive(cantidad > 1);
    }
    public void ResetAmount()
    {
        cantidad = 1;
        _text.gameObject.SetActive(false);
    }
    public void SetImage(Sprite sprite)
    {
        _img.sprite = sprite;
    }
    public void SetSelected()
    {
        _parentImage.color = selectedColor;
    }
    public void Unselected()
    {
        _parentImage.color = unselectedColor;
    }
}
