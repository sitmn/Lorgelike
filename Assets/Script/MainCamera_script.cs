using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_script : MonoBehaviour {

    private GameObject Player;

    private int distance = 8;

	// Use this for initialization
	void Start () {
        this.Player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.localPosition = new Vector3(this.Player.transform.position.x, distance, this.Player.transform.position.z - 5);
    }
}
