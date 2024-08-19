using System;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using WumpusUnity;

public class Credits : MonoBehaviour
{
    [SerializeField] private String mainMenu;
    [SerializeField] private TMP_Text credits;
    private const float creditsSpeed = 30f;



    // Update is called once per frame
    void Update()
    {
        if (credits.transform.position.y >= 1025)
        {
            SceneManager.LoadScene(mainMenu);
        }
        credits.transform.position += new Vector3(0, creditsSpeed * Time.deltaTime, 0);
    }
}
