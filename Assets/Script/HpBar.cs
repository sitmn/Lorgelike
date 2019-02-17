using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {

    public GameObject Player;
    private Slider slider;

    // Use this for initialization
    void Start () {
        slider = this.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        float hp_bar = (float)player.player_hp / (float)player.player_MAX_hp;
        slider.value = hp_bar;
	}
}
