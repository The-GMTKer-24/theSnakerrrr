using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnEntrance : MonoBehaviour
{
    [SerializeField] private GameObject[] victims;

    public void kill()
    {
        foreach (GameObject victim in victims)
        {
            Destroy(victim);
        }
    }
}
