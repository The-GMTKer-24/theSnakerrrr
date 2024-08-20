using System;
using UnityEngine;

public class PickupPulsing : MonoBehaviour
{
    [SerializeField] private AnimationCurve yPosition;
    [SerializeField] private AnimationCurve size;
    private float startSize;
    private float startY;

    public void Awake()
    {
        startSize = transform.localScale.x;
        startY = transform.position.y;
    }

    public void Update()
    {
        transform.position = new Vector3(transform.position.x,startY + yPosition.Evaluate(Time.time),
            transform.position.z);
        var newScale = startSize + size.Evaluate(Time.time);
        transform.localScale =
            new Vector3(newScale, newScale, newScale);
    }
}