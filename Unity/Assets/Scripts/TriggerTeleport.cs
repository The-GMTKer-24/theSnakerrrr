using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerTeleport : MonoBehaviour
{
    [SerializeField] private Renderer popup;
    [HideInInspector] public String playerTag = "Player";
    [HideInInspector] public Vector2 newPosition;
    [HideInInspector] public AnimationCurve fadeOut;
    [HideInInspector] public AnimationCurve fadeIn;
    [HideInInspector] public float fadeInTime;
    [HideInInspector] public float fadeOutTime;
    [HideInInspector] public Image blackScreen;
    [HideInInspector] public PlayerManager playerManager;
    private bool teleporting;
    private float timer;

    private void Awake()
    {
        popup.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            popup.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            popup.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && !teleporting && Input.GetKey(KeyCode.Q))
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    private void Start()
    {
        blackScreen.color = new Color(0f, 0f, 0f, 0f);
    }

    private IEnumerator TeleportPlayer()
    {
        playerManager.DisableMovement();
        
        teleporting = true;
        
        if (gameObject.GetComponent<KillOnEntrance>() != null)
        {
            print("Killing");
            gameObject.GetComponent<KillOnEntrance>().kill();
        }
        
        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;
            blackScreen.color = new Color(0f, 0f, 0f, fadeIn.Evaluate(timer / fadeInTime));
            if (PlayerManager.Instance.player == null)
            {
                teleporting = false;
                timer = 0;
                playerManager.EnableMovement();
                blackScreen.color = new Color(0f, 0f, 0f, 0f);
                yield break;
            }
            yield return null;
        }
        PlayerManager.Instance.player.transform.position = newPosition;
        PlayerManager.Instance.camera.transform.position = new Vector3(newPosition.x, newPosition.y, PlayerManager.Instance.camera.transform.position.z);
        timer = 0;
        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            blackScreen.color = new Color(0f, 0f, 0f, fadeOut.Evaluate(timer/ fadeOutTime));
            yield return null;
        }

        timer = 0;
        teleporting = false;
        
        playerManager.EnableMovement();
    }

}
