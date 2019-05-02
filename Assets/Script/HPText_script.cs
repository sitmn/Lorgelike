using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText_script : MonoBehaviour {
    public Text HPText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void HP_Text()
    {
        HPText.text = player.player_hp + "/" + player.player_MAX_hp;
    }
}
