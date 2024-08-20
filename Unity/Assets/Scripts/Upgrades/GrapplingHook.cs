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
    [SerializeField] private LayerMask layerMask;
    [FormerlySerializedAs("minimumRopeLength")] [SerializeField] private float ropeLength;
    [SerializeField] private float ropeDistance;
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
            lineRenderer.SetPosition(0, PlayerManager.Instance.player.transform.position);
        }
    }

    void ConnectHook(InputAction.CallbackContext callbackContext)
    {
        Vector2 mousePosition = PlayerManager.Instance.mouseCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        // GetComponent<Camera>().GetComponent<CameraShake>().Shake(shakeTime, shakeIntensity);
        Vector2 delta = mousePosition - (Vector2)PlayerManager.Instance.player.transform.position;
        delta.Normalize();
        // amoungus
        Debug.DrawRay(PlayerManager.Instance.player.transform.position, mousePosition, Color.green,5,false);
        RaycastHit2D hit = Physics2D.Raycast(PlayerManager.Instance.player.transform.position, delta, ropeDistance, layerMask);

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.GetComponent<Ungrapplable>())
            {
                print("Animation pending");
                return;
                
            }
            lineRenderer.SetPosition(0, PlayerManager.Instance.player.transform.position);
            lineRenderer.SetPosition(1, hit.point);
            distanceJoint.connectedAnchor = hit.point;
            distanceJoint.distance = Mathf.Min(Vector2.Distance(PlayerManager.Instance.player.transform.position, hit.point), ropeLength);
            distanceJoint.enabled = true;
            lineRenderer.enabled = true;
        }

    }
    

    void DisconnectHook(InputAction.CallbackContext callbackContext)
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

}
