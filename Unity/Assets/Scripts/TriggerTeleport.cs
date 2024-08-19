using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerTeleport : MonoBehaviour
{
    [SerializeField] private String playerTag = "Player";
    [SerializeField] private Vector2 newPosition;
    [SerializeField] private AnimationCurve fadeOut;
    [SerializeField] private AnimationCurve fadeIn;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private Image blackScreen;
    private bool teleporting;
    private float timer;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag(playerTag) && !teleporting)
        {
            StartCoroutine(TeleportPlayer());        
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && !teleporting)
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
        teleporting = true;
        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;
            blackScreen.color = new Color(0f, 0f, 0f, fadeIn.Evaluate(timer / fadeInTime));
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
    }

}
