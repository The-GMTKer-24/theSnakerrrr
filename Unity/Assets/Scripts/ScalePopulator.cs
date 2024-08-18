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
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Transform scaleStart;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = -rowWidth; i < rowWidth; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                Vector3 position = new Vector3(i * inRowSpacing, 0, j * betweenRowSpacing);
                if (j % 2 == 0)
                {
                    position += new Vector3(inRowSpacing / 2, 0, 0);
                }
                Instantiate(scale, position + scaleStart.position, Quaternion.Euler(rotation), this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
