using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Event : MonoBehaviour {

    private AudioSource walk;
    private AudioSource attack;
    private AudioSource damage;

	// Use this for initialization
	void Start () {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        walk = audioSources[0];
        attack = audioSources[1];
        damage = audioSources[2];

    }
	
    void Walk_Sound_Effect()
    {
        walk.PlayOneShot(walk.clip);
    }

	void Attack_Sound_Effect()
    {
        attack.PlayOneShot(attack.clip);
    }

    void Damage_Sound_Effect()
    {
        damage.PlayOneShot(damage.clip);
    }

    void Heel_Sound_Effect()
    {

    }

    void Make_Success_Sound_Effect()
    {

    }

    void Make_Miss_Sound_Effect()
    {

    }

}
