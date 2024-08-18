using System;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField]
    private Vector3 spinRate;
    private Vector3 speen;
    public void Update()
    {
        speen += spinRate * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(speen);
    }
}