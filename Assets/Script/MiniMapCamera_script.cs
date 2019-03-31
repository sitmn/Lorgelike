using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera_script : MonoBehaviour
{

    private GameObject Player;

    private int distance = 20;

    // Use this for initialization
    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(this.Player.transform.position.x + map_creat.minimapdistance, distance , this.Player.transform.position.z + map_creat.minimapdistance);
    }
}
