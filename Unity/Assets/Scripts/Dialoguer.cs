using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialoguer : MonoBehaviour
{
    [SerializeField] private Collider2D collider1;
    [SerializeField] private Collider2D collider2;
    [SerializeField] private Collider2D collider3;
    [SerializeField] private Collider2D collider4;
    [SerializeField] private Collider2D collider5;

    [SerializeField] private String textDefault;
    [SerializeField] private String text1;
    [SerializeField] private String text2;
    [SerializeField] private String text3;
    [SerializeField] private String text4;
    [SerializeField] private String text5;

    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private TMP_Text text;

    void Update()
    {
        if (collider5 != null && collider5.IsTouchingLayers(collisionLayers))
        {
            text.text = text5;
        }
        else if (collider4 != null && collider4.IsTouchingLayers(collisionLayers))
        {
            text.text = text4;
        }
        else if (collider3 != null && collider3.IsTouchingLayers(collisionLayers))
        {
            text.text = text3;
        }
        else if (collider2 != null && collider2.IsTouchingLayers(collisionLayers))
        {
            text.text = text2;
        }
        else if (collider1 != null && collider1.IsTouchingLayers(collisionLayers))
        {
            text.text = text1;
        }
        else
        {
            text.text = textDefault;
        }
    }
}
