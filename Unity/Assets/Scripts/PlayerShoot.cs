using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float impulse;
    [SerializeField] private float verticalDamping;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float shakeTime;
    [SerializeField] private AnimationCurve shakeIntensity;
    [SerializeField] private GameObject ammonIcon1;
    [SerializeField] private GameObject ammonIcon2;

    public bool temporarilyDisabled;
    
    public int ammo;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<DrainAmmo>())
        {
            ammo = 0;
        }
        if (other.GetComponent<FreeAmmo>())
        {
            ammo = maxAmmo;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<FreeAmmo>())
        {
            ammo = maxAmmo;
        }
        if (other.GetComponent<DrainAmmo>())
        {
            ammo = 0;
        }
    }

    private void UpdateAmmo()
    {
        ammonIcon1.SetActive(false);
        ammonIcon2.SetActive(false);
        if (ammo > 0)
        {
            ammonIcon1.SetActive(true);
        }
        if (ammo > 1)
        {
            ammonIcon2.SetActive(true);
        }
    }

    private void Update()
    {
        UpdateAmmo();
    }

    private void OnShootButton(InputAction.CallbackContext obj)
    {
        if (ammo < 1 || temporarilyDisabled)
        {
            return;
        }
        ammo--;
        UpdateAmmo();

        Vector3 mousePosition = PlayerManager.Instance.mouseCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        PlayerManager.Instance.camera.GetComponent<CameraShake>().Shake(shakeTime, shakeIntensity);
        Vector2 delta = transform.position - mousePosition;
        delta.Normalize();
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().SetMoveDirection(delta * -1, this.transform);
        delta.y /= verticalDamping;
        rb.AddForce(delta * impulse, ForceMode2D.Impulse);

    }

    public void AddAmmo()
    {
        if( ammo < maxAmmo)
            ammo++;
    }

}
