using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserUI : MonoBehaviour
{
    public Material materialIn, materialOut;
    LineRenderer _lineR;
    public float distanceMax = 100f;
    public LayerMask layerUI;
    private InventoryItem _currentIntem;
    void Start()
    {
        _lineR = GetComponent<LineRenderer>();
    }
    void Update()
    {
        _lineR.SetPosition(0, transform.position);
        Vector3 endPosition = transform.position + transform.forward * distanceMax;
        RaycastHit hitInfo;
        _lineR.material = materialOut;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, distanceMax, layerUI))
        {
            endPosition = hitInfo.point;
            _lineR.material = materialIn;
            if (_currentIntem != null)
                _currentIntem.Unselected();
            _currentIntem = hitInfo.collider.GetComponent<InventoryItem>();
            _currentIntem.SetSelected();
        }
        else
        {
            if (_currentIntem != null)
            {
                _currentIntem.Unselected();
                _currentIntem = null;
            }
        }
        _lineR.SetPosition(1, endPosition);
        if(_currentIntem != null && GameManager.input.triggerRightDown)
        {
            _currentIntem.GetComponent<ItemAction>().Action();
        }
    }
}
