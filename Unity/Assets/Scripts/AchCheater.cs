using UnityEngine;
using UnityEngine.UI;

public class AchCheater : MonoBehaviour
{
    private void Update()
    {
        Image img = this.GetComponent<Image>();
        img.enabled = Achievements.Cheater;
    }
}
