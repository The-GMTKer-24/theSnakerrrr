using System;
using JetBrains.Annotations;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private String mainMenu;
    [SerializeField] private TMP_Text credits;
    [SerializeField] private TMP_Text finalMessage;
    [SerializeField] private float creditsSpeed = 30f;

    private byte finalMessageAlpha;

    private void FixedUpdate()
    {
        if (credits.transform.position.y >= 1350 && finalMessage.color != Color.white)
        {
            finalMessageAlpha++;
            finalMessage.color = new Color32(255, 255, 255, finalMessageAlpha);
        }
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (credits.transform.position.y >= 1850 || Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(mainMenu);
        }

        credits.transform.position += new Vector3(0, creditsSpeed * Time.deltaTime, 0);
    }
}
