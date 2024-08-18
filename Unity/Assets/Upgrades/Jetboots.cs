using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Jetboots : MonoBehaviour
{
    private InputMap inputMap;
    private InputAction jet;
    [SerializeField]
    float jetForce = 5f;
    [SerializeField]
    float maxForce = 15f;
    [SerializeField] private GameObject bootsUI;

    [SerializeField] private float maxFuel = 20f;
    [SerializeField] private float fuelRegenRate = 0.5f;
    [SerializeField]
    private float currentFuel;
    private void Awake()
    {
        inputMap = new InputMap();
        jet = inputMap.Player.Jump;
        currentFuel = maxFuel;
    }

    private void OnEnable()
    {
        bootsUI.SetActive(true);
        jet.Enable();
    }


    private void OnDisable()
    {
        bootsUI.SetActive(false);
        jet.Disable();
    }

    private void FixedUpdate()
    {
        if (jet.IsPressed() && currentFuel > 0)
        {
            OnJetButton();
        }
        else
        {
            if (PlayerManager.Instance.player.GetComponent<PlayerMovement>().LastGroundTime >= 0)
                currentFuel = Math.Min(currentFuel + fuelRegenRate * Time.fixedDeltaTime, maxFuel);
        }
    }

    private void OnJetButton()
    {
        if (PlayerManager.Instance.player.GetComponent<PlayerMovement>().Jumping)
            return;
        GameObject player = PlayerManager.Instance.player;
        Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
        currentFuel -= Time.fixedDeltaTime;
        if (rigidbody2D.velocityY < maxForce)
        {
            rigidbody2D.velocityY +=jetForce;
        }
    }
}
