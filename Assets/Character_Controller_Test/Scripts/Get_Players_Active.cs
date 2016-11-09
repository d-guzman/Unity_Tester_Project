using UnityEngine;
using System.Collections;

public class Get_Players_Active : MonoBehaviour {
    GameObject[] players;
    // Update is called once per frame
    void Start() {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
}
