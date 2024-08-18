using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class GrapplingHook : MonoBehaviour
{
    private InputMap inputMap;
    private InputAction useHook;

    private Rigidbody2D ropeHingeAnchorRb;
    private bool ropeAttached;
    [SerializeField] private float shakeTime;
    [SerializeField] private AnimationCurve shakeIntensity;
    [SerializeField] public Camera mainCamera;
    public LineRenderer lineRenderer;
    [SerializeField] public DistanceJoint2D distanceJoint;

    // Start is called before the first frame update
    void Start()
    {
        distanceJoint.enabled = false;
    }

    private void Awake()
    {
        inputMap = new InputMap();
        useHook = inputMap.Player.Use;
        useHook.started += ConnectHook;
        useHook.canceled += DisconnectHook;
    }


    private void OnEnable()
    {
        useHook.Enable();
    }


    private void OnDisable()
    {
        useHook.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (distanceJoint.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    void ConnectHook(InputAction.CallbackContext callbackContext)
    {
        Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, mousePos);
        lineRenderer.SetPosition(1, transform.position);
        distanceJoint.connectedAnchor = mousePos;
        distanceJoint.enabled = true;
        lineRenderer.enabled = true;

    }

    void DisconnectHook(InputAction.CallbackContext callbackContext)
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

}
