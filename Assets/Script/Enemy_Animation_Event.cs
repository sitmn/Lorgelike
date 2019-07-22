using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animation_Event : MonoBehaviour {

    private AudioSource attack;
    private AudioSource damage;

	// Use this for initialization
	void Start () {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        attack = audioSources[0];
        damage = audioSources[1];
	}
	
    void Attack_Sound_Effect()
    {
        /*if(this.gameObject.tag == "bat")
        {

        }else if(this.gameObject.tag == "ghost")
        {
            
        }
        else if (this.gameObject.tag == "rabbit")
        {

        }
        else if (this.gameObject.tag == "slime")
        {

        }*/

        attack.PlayOneShot(attack.clip);

    }

    void Damage_Sound_Effect()
    {
        damage.PlayOneShot(damage.clip);
    }


}
