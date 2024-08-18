using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private bool jetbootsEnabled;
    [SerializeField] private bool grappleEnabled;

    [SerializeField] private GameObject jetboots;
    [SerializeField] private GameObject grappleHooks;
    void Start()
    {
        jetboots.SetActive(jetbootsEnabled);
        grappleHooks.SetActive(grappleEnabled);
    }
}
