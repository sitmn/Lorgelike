using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //ミニマップのアイテムのSetActive
	    if(map_creat.mini_map[(int)transform.position.x, (int)transform.position.z].activeSelf == true)
        {
            if(map_creat.map_item[(int)transform.position.x,(int)transform.position.z].minimap_item.activeSelf == false)
            {
                map_creat.map_item[(int)transform.position.x, (int)transform.position.z].minimap_item.SetActive(true);
            }
        }
        else if (map_creat.mini_map[(int)transform.position.x, (int)transform.position.z].activeSelf == false)
        {
            if (map_creat.map_item[(int)transform.position.x, (int)transform.position.z].minimap_item.activeSelf == true)
            {
                map_creat.map_item[(int)transform.position.x, (int)transform.position.z].minimap_item.SetActive(false);
            }
        }



    }
}
