using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    [Range(1, 20)]
    public float distanciaMax = 12;
    [Range(1, 5)]
    public float alturaY = 5;
    [Range(0, 20)]
    public float descensoMaximo = 10;
    public Transform room;
    private Vector3 _puntoFinal, _puntoControl;
    private const int NUMERO_DE_ESFERAS = 20;
    private Vector3[] _esferas = new Vector3[NUMERO_DE_ESFERAS];
    public LayerMask layerTeleport;
    private GameObject _marcador;
    private LineRenderer _lineRenderer;
    private Gradient _lineGradient = new Gradient();
    private float _timeGradient;
    private GradientColorKey[] _keyColors = new GradientColorKey[5];
    public GameObject tpSound;
    private void Start()
    {
        _marcador = transform.GetChild(0).gameObject;
        _lineRenderer = GetComponent<LineRenderer>();
        _keyColors[0].color = Color.red;
        _keyColors[0].time = 0;
        _keyColors[4].color = Color.red;
        _keyColors[4].time = 1;
        ChangeColorGradient(0);
    }
    void Update()
    {
        CalculateCurve();
        GetCollision();
        if(GameManager.input.triggerLeftDown && _marcador.activeSelf)
        {
            GameObject newSound = Instantiate(tpSound);
            Destroy(newSound, 3);
            room.position = _marcador.transform.position;
        }
        ChangeColorGradient(Time.deltaTime);
    }
    void ChangeColorGradient(float t)
    {
        _timeGradient = (_timeGradient + t) % (1 - 0.2f);
        _keyColors[1].color = Color.red;
        _keyColors[1].time = _timeGradient - 0.1f;
        _keyColors[2].color = Color.white;
        _keyColors[2].time = _timeGradient;
        _keyColors[3].color = Color.red;
        _keyColors[3].time = _timeGradient + 0.1f;
        _lineGradient.colorKeys = _keyColors;
        _lineRenderer.colorGradient = _lineGradient;
    }
    void GetCollision()
    {
        RaycastHit _hit;
        _marcador.SetActive(false);
        for (int i = 0; i < NUMERO_DE_ESFERAS - 1; i++)
        {
            if (Physics.Linecast(_esferas[i], _esferas[i + 1], out _hit, layerTeleport))
            {
                _marcador.SetActive(true);
                _marcador.transform.position = _hit.point;
                _marcador.transform.rotation = Quaternion.FromToRotation(_hit.normal, Vector3.up);
                return;
            }
        }
    }
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        //B(t) = (1-t) * (1-t) * P0 + 2 * t * (1-t) * P1 + t * t * P2
        //t vs de 0 a 1
        float u = 1 - t;
        Vector3 point = u * u * p0;
        point += 2 * t * u * p1;
        point += t * t * p2;
        return point;
    }
    private void CalculateCurve()
    {
        for (int i = 0; i < NUMERO_DE_ESFERAS; i++)
        {
            float t = (float)i / NUMERO_DE_ESFERAS;
            _puntoFinal = transform.position + transform.forward * distanciaMax;
            _puntoFinal.y = transform.position.y - descensoMaximo;
            _puntoControl = transform.position + (_puntoFinal - transform.position) / 2;
            _puntoControl.y += alturaY;
            _esferas[i] = CalculateBezierPoint(t, transform.position, _puntoControl, _puntoFinal);
            _lineRenderer.SetPositions(_esferas);
        }
    }
}
