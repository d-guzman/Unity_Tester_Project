using UnityEngine;
using System.Collections;

public class DeathZoneInteraction : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "DeathZone")
        {
            gameObject.SetActive(false);
        }
    }
}
