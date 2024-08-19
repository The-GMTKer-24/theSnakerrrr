using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfficeTeleport : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    
    [SerializeField] private TriggerTeleport buildingEntrance;
    [SerializeField] private TriggerTeleport officeEntrance;
    [SerializeField] private TriggerTeleport buildingExit;
    [SerializeField] private TriggerTeleport officeExit;

    [SerializeField] private AnimationCurve fadeOut;
    [SerializeField] private AnimationCurve fadeIn;
    [SerializeField] private float fadeInTime = 3;
    [SerializeField] private float fadeOutTime = 3;
    [SerializeField] private Image blackScreen;

    private void Awake()
    {
        TriggerTeleport[] points = new[] { buildingEntrance, officeEntrance, buildingExit, officeExit };

        foreach (TriggerTeleport point in points)
        {
            try
            {
                point.fadeOut = fadeOut;
                point.fadeIn = fadeIn;
                point.fadeInTime = fadeInTime;
                point.fadeOutTime = fadeOutTime;
                point.blackScreen = blackScreen;
                point.playerManager = playerManager;
            } catch {}
        }

        buildingEntrance.newPosition = officeEntrance.transform.position;
        officeEntrance.newPosition = buildingEntrance.transform.position;
        buildingExit.newPosition = officeExit.transform.position;
        officeExit.newPosition = buildingExit.transform.position;
    }

    private void FixedUpdate()
    {
        
    }
}
