using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCopier : MonoBehaviour
{
    [SerializeField] private TMP_Text originalText;
    [SerializeField] private TMP_Text copyText;

    private void Update()
    {
        // Copy the text from the original onto the copy
        copyText.text = originalText.text;
    }
}
