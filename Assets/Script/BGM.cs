using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

    public static bool BGM_start = false;
    private AudioSource bgm;

	// Use this for initialization
	void Start () {
        if(BGM.BGM_start == true)
        {
            Destroy(this.gameObject);
        }

        bgm = GetComponent<AudioSource>();

        DontDestroyOnLoad(this);

        bgm.PlayOneShot(bgm.clip);

        BGM.BGM_start = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
