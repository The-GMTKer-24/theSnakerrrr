using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] private float length;
    [SerializeField]
    private float startpos;
    [SerializeField] private GameObject camera;
    [SerializeField] private float speed;

    private void Start()
    {
        startpos = 0;
        //length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        if (transform.position.x >= startpos + length)
        {
            transform.position = new Vector3(startpos - length, transform.position.y, transform.position.z);
        }
    }
}
