using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCameras : MonoBehaviour
{
    [SerializeField] private Camera perspective;

    [SerializeField] private Camera orthographic;
    // Start is called before the first frame update
    void Start()
    {
        float fov = (float)Math.PI * 2 * perspective.fieldOfView / 360;
        float depth = perspective.gameObject.transform.position.z;

        float size = (float)Math.Tan(fov / 2) * depth;

        orthographic.orthographicSize = Math.Abs(size);
    }
}
