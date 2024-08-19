using UnityEngine;
using UnityEngine.Serialization;

public class Shopkeeper : MonoBehaviour
{
    [FormerlySerializedAs("Overlay")] [SerializeField] private GameObject overlay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            overlay.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            overlay.SetActive(false);
        }
    }
}
