using UnityEngine;
using System.Collections;

public class Get_Players_Active : MonoBehaviour {
    public GameObject player1;
    private bool P1_Out = false;

    //Temporarily commented out. To support a 2nd player, decomment this code and
    //the corresponding code block in Update.
    //public GameObject player2;
    //private bool P2_Out = false;

    private GameObject[] players;
    // Update is called once per frame
    void Start() {
        players = GameObject.FindGameObjectsWithTag("Player");
        //Debug.Log("Player's in PlayerList:" + players.Length);
    }

    void Update() {
        updatePlayerList();
    }

    private void updatePlayerList() {
        if (player1.activeInHierarchy == false && P1_Out == false) {
            players = GameObject.FindGameObjectsWithTag("Player");
            P1_Out = true;
            //Debug.Log("Player's in PlayerList:" + players.Length);
        }
        /*
        if (player2.activeInHierarchy == false && P2_Out == false) {
            players = GameObject.FindGameObjectsWithTag("Player");
            P2_Out = true;
            //Debug.Log("Player's in PlayerList:" + players.Length);
        }
        */
    }
}
