using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jetboots : MonoBehaviour
{
    private InputMap inputMap;
    private InputAction jet;

    private void Awake()
    {
        inputMap = new InputMap();
        jet = inputMap.Player.Jump;
    }

    private void OnEnable()
    {
        jet.Enable();
    }


    private void OnDisable()
    {
        jet.Disable();
    }

    private void FixedUpdate()
    {
        if (jet.IsPressed())
        {
            OnJetButton();
        }
    }

    private void OnJetButton()
    {
        GameObject player = PlayerManager.Instance.player;
        Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();

        if (rigidbody2D.velocityY < 15f)
        {
            rigidbody2D.velocityY += 3f;
        }
    }
}
