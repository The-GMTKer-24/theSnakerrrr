using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Camera camera;
    [SerializeField] public Camera mouseCamera;
    [SerializeField] private float impulse;
    [SerializeField] private float verticalDamping;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float shakeTime;
    [SerializeField] private AnimationCurve shakeIntensity;
    private int ammo;
    private InputMap inputMap;
    private InputAction shoot;
    public static PlayerShoot Instance;
    private void Awake()
    {
        Instance = this;
        inputMap = new InputMap();
        shoot = inputMap.Player.Fire;
    }

    private void OnEnable()
    {
        shoot.Enable();
        shoot.started += OnShootButton;
    }
    

    private void OnDisable()
    {
        shoot.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
    }
    
    private void OnShootButton(InputAction.CallbackContext obj)
    {
        if (ammo < 1)
        {
            return;
        }
        ammo--;
        //print(Input.mousePosition);
        Vector3 mousePosition = mouseCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        print(mousePosition);
        camera.GetComponent<CameraShake>().Shake(shakeTime, shakeIntensity);
        Vector2 delta = (Vector3)transform.position - mousePosition;
        delta.Normalize();
        delta.y /= verticalDamping;
        GameObject proj = Instantiate(projectile, transform.position,Quaternion.identity);
        proj.GetComponent<Projectile>().SetMoveDirection(delta * -1, this.transform);
        rb.AddForce(delta * impulse, ForceMode2D.Impulse);

    }

    public void AddAmmo()
    {
        if( ammo < maxAmmo)
            ammo++;
    }

}
