using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePopulator : MonoBehaviour
{
    [SerializeField] private GameObject scale;

    [SerializeField] private int rowWidth;
    [SerializeField] private int rowCount;
    [SerializeField] private float inRowSpacing;
    [SerializeField] private float betweenRowSpacing;
    [SerializeField] private float maxInRowRandom;
    [SerializeField] private float maxSideRandom;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Transform scaleStart;
    [SerializeField] private float xClearZone;
    [SerializeField] private float zClearZone;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < rowCount; j++)
        {
            float x = -rowWidth * inRowSpacing / 2;
            for (int i = 0; i < rowWidth; i++)
            {
                float xRandom = (UnityEngine.Random.value * 2 - 1) * maxInRowRandom;
                float zRandom = (UnityEngine.Random.value * 2 - 1) * maxSideRandom;
                Vector3 position = new Vector3(x, 0, j * betweenRowSpacing + zRandom);
                if (j % 2 == 0)
                {
                    position += new Vector3(inRowSpacing / 2, 0, 0);
                }

                if (Math.Abs(position.x) > xClearZone || Math.Abs(position.z) > zClearZone)
                {
                    Instantiate(scale, position + scaleStart.position, Quaternion.Euler(rotation), this.transform);
                }

                x += inRowSpacing;
                x += xRandom;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
