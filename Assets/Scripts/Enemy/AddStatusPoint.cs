using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStatusPoint : MonoBehaviour
{
    private void OnDestroy()
    {
        GameManager.MainGameManager.settingStatusManager.AllotmentPoint();
    }
}
