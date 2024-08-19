using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinValue : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    void Update()
    {
        text.text = PlayerManager.Instance.level1Collectables.ToString();
    }
}
