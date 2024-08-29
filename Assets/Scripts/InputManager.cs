using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private KeyCode _touchpadKeyLeft, _touchpadKeyRight;
    public KeyCode touchpadKeyLeft { set { _touchpadKeyLeft = value; } } 
    public KeyCode touchpadKeyRight { set { _touchpadKeyRight = value; } }

    private bool _touchpadLeft, _touchpadLeftDown, _touchpadLeftUp;
    public bool touchpadLeft { get { return _touchpadLeft; } }
    public bool touchpadLeftDown { get { return _touchpadLeftDown; } }
    public bool touchpadLeftUp { get { return _touchpadLeftUp; } }

    private bool _touchpadRight, _touchpadRightDown, _touchpadRightUp;
    public bool touchpadRight{ get { return _touchpadRight; } }
    public bool touchpadRightDown { get { return _touchpadRightDown; } }
    public bool touchpadRightUp { get { return _touchpadRightUp; } }

    private bool _triggerLeft, _triggerLeftDown, _triggerLeftUp;
    public bool triggerLeft { get { return _triggerLeft; } }
    public bool triggerLeftDown { get { return _triggerLeftDown; } }
    public bool triggerLeftUp { get { return _triggerLeftUp; } }

    private bool _triggerRight, _triggerRightDown, _triggerRightUp;
    public bool triggerRight { get { return _triggerRight; } }
    public bool triggerRightDown { get { return _triggerRightDown; } }
    public bool triggerRightUp { get { return _triggerRightUp; } }

    void Update()
    {
        _triggerLeft = Input.GetMouseButton(0);
        _triggerLeftDown = Input.GetMouseButtonDown(0);
        _triggerLeftUp = Input.GetMouseButtonUp(0);

        _triggerRight = Input.GetMouseButton(1);
        _triggerRightDown = Input.GetMouseButtonDown(1);
        _triggerRightUp = Input.GetMouseButtonUp(1);

        _touchpadLeft = Input.GetKey(_touchpadKeyLeft);
        _touchpadLeftDown = Input.GetKeyDown(_touchpadKeyLeft);
        _touchpadLeftUp = Input.GetKeyUp(_touchpadKeyLeft);
        
        _touchpadRight = Input.GetKey(_touchpadKeyRight);
        _touchpadRightDown = Input.GetKeyDown(_touchpadKeyRight);
        _touchpadRightUp = Input.GetKeyUp(_touchpadKeyRight);

        if(triggerLeftDown)
            GameManager.manager.ShowLog("He pulsado el trigger Izquierdo");
        if (triggerLeftUp)
            GameManager.manager.ShowLog("He soltado el trigger Izquierdo");
        if (triggerRightDown)
            GameManager.manager.ShowLog("He pulsado el trigger Derecho");
        if (triggerRightUp)
            GameManager.manager.ShowLog("He soltado el trigger Derecho");
        if (touchpadLeftDown)
            GameManager.manager.ShowLog("He pulsado el pad Izquierdo");
        if (touchpadLeftUp)
            GameManager.manager.ShowLog("He soltado el pad Izquierdo");
        if (touchpadRightDown)
            GameManager.manager.ShowLog("He pulsado el pad Derecho");
        if (touchpadRightUp)
            GameManager.manager.ShowLog("He soltado el pad Derecho");
    }
}
