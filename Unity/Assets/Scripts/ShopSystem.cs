using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public void ObtainGrapple()
    {
        PlayerManager.Instance.upgradeManager.GetComponent<UpgradesManager>().ObtainGrapplingHook();
    }

    public void ObtainJetpack()
    {
        PlayerManager.Instance.upgradeManager.GetComponent<UpgradesManager>().ObtainJetboots();
    }
}
