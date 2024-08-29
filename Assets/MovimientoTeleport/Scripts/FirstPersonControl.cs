using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonControl : MonoBehaviour
{
    public float speed, speedRotation, jumpForce, gravity;
    public bool canJump, multyJumps;
    public int totalJumps, currentJumps;

    private CharacterController control;
    private Vector3 moveDir;
    private float rotX, rotY;

    public float height, lookHeight, distance, speedMove;
    public bool isThirdPerson;
    private RaycastHit hitCamera;
    private Vector3 posCamera, lookAtPos;
    private Camera mainCamera;
    private GameObject cameraParent;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Init();
    }
    private void Update()
    {
        speedMove = 3;
        Movement();
        CameraControl();
        ExitApp();
    }

    public void Init()
    {
        control = GetComponent<CharacterController>();
        moveDir = Vector3.zero;
        rotX = rotY = 0;

        Camera cameraFind = GameObject.FindObjectOfType(typeof(Camera)) as Camera;
        cameraParent = new GameObject("CameraParent");
        if (cameraFind != null) mainCamera = cameraFind;
        else
        {
            GameObject newCamera = new GameObject("FPC_camera");
            mainCamera = newCamera.AddComponent<Camera>();
        }
        AudioListener audioFind = GameObject.FindObjectOfType(typeof(AudioListener)) as AudioListener;
        if (audioFind == null) mainCamera.gameObject.AddComponent<AudioListener>();

        mainCamera.transform.SetParent(cameraParent.transform);
        mainCamera.transform.localPosition = Vector3.zero;
        mainCamera.transform.localRotation = Quaternion.Euler(Vector3.zero);

        cameraParent.transform.SetParent(transform);
    }
    public void Movement()
    {
        if (!control) return;

        if (control.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;

            currentJumps = 0;
            if (canJump) if (Input.GetKeyDown(KeyCode.Space)) moveDir.y = jumpForce;
        }
        else
        {
            if (canJump && multyJumps) if (Input.GetKeyDown(KeyCode.Space) && currentJumps < totalJumps)
                { moveDir.y = jumpForce; currentJumps++; }
        }
        moveDir.y -= gravity * Time.deltaTime;
        control.Move(moveDir * Time.deltaTime);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 20;
        }
        else
        {
            speed = 8;
        }
    }
    public void CameraControl()
    {
        if (!mainCamera) return;

        if (isThirdPerson)
        {
            Vector3 finalLookPos = transform.position;
            finalLookPos.y += lookHeight;
            cameraParent.transform.LookAt(finalLookPos);

            Vector3 finalCamPos = transform.position;
            finalCamPos -= transform.forward * distance;
            finalCamPos.y += height;

            if (Physics.Linecast(finalLookPos, finalCamPos, out hitCamera)) finalCamPos = hitCamera.point;

            cameraParent.transform.position = Vector3.Lerp(cameraParent.transform.position, finalCamPos, speedMove * Time.deltaTime);

            float distCamera = Vector3.Distance(finalLookPos, finalCamPos);
            if (distCamera < 1) { if (GetComponent<Renderer>().enabled) GetComponent<Renderer>().enabled = false; }
            else GetComponent<Renderer>().enabled = true;
        }
        else
        {
            if (cameraParent.transform.localRotation != Quaternion.Euler(Vector3.zero))
                cameraParent.transform.localRotation = Quaternion.Euler(Vector3.zero);

            cameraParent.transform.localPosition = new Vector3(0, height, 0);

            rotY += Input.GetAxis("Mouse Y") * speedMove;
            rotY = Mathf.Clamp(rotY, -90, 90);
            cameraParent.transform.localEulerAngles = new Vector3(-rotY, 0, 0);

            if (GetComponent<Renderer>().enabled) GetComponent<Renderer>().enabled = false;
        }
        rotX += Input.GetAxis("Mouse X") * speedMove;
        transform.rotation = Quaternion.Euler(0, rotX, 0);

    }
    public void ExitApp()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

#region INSPECTOR PARAMETERS
#if UNITY_EDITOR
[CustomEditor(typeof(FirstPersonControl))]
public class FirstPersonControlEditor : Editor
{

    public override void OnInspectorGUI()
    {
        FirstPersonControl FPC_script = (FirstPersonControl)target;
        GUIStyle headerStyle = new GUIStyle();
        headerStyle.fontStyle = FontStyle.Bold;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Move parameters", headerStyle);
        FPC_script.speed = EditorGUILayout.FloatField("Speed", FPC_script.speed);
        FPC_script.speedRotation = EditorGUILayout.FloatField("Speed rotation", FPC_script.speedRotation);
        FPC_script.gravity = EditorGUILayout.FloatField("Gravity", FPC_script.gravity);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Jump parameters", headerStyle);
        FPC_script.canJump = EditorGUILayout.Toggle("Can jump", FPC_script.canJump);
        if (FPC_script.canJump)
        {
            FPC_script.jumpForce = EditorGUILayout.FloatField("    Jump force", FPC_script.jumpForce);
        }
        FPC_script.multyJumps = EditorGUILayout.Toggle("Multy jumps", FPC_script.multyJumps);
        if (FPC_script.multyJumps)
        {
            FPC_script.totalJumps = EditorGUILayout.IntField("    Number of jumps", FPC_script.totalJumps);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Camera parameters", headerStyle);
        FPC_script.height = EditorGUILayout.FloatField("Height", FPC_script.height);
        FPC_script.speedMove = EditorGUILayout.FloatField("Speed rotate", FPC_script.speedMove);
        FPC_script.isThirdPerson = EditorGUILayout.Toggle("Is Third Person", FPC_script.isThirdPerson);
        if (FPC_script.isThirdPerson)
        {
            FPC_script.lookHeight = EditorGUILayout.FloatField("    Look height", FPC_script.lookHeight);
            FPC_script.distance = EditorGUILayout.FloatField("    Distance", FPC_script.distance);
        }
    }
}
#endif
#endregion