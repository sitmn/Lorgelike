using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    public GameObject Player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(map_creat.map[(int)Player.transform.position.x , (int)Player.transform.position.z].number == 1 ||
            map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].number == 3 ||
            map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].number == 5)
        {
            if(map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].room_No == 0)
            {
                for (int i = 0; i < GameManager.instance.roomlist_0.Count; i++)
                {
                    Open_Mini_Map((int)GameManager.instance.roomlist_0[i].x, (int)GameManager.instance.roomlist_0[i].z);
                }
            }else if(map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].room_No == 1)
            {
                for (int i = 0; i < GameManager.instance.roomlist_1.Count; i++)
                {
                    Open_Mini_Map((int)GameManager.instance.roomlist_1[i].x, (int)GameManager.instance.roomlist_1[i].z);
                }
            }
            else if (map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].room_No == 2)
            {
                for (int i = 0; i < GameManager.instance.roomlist_2.Count; i++)
                {
                    Open_Mini_Map((int)GameManager.instance.roomlist_2[i].x, (int)GameManager.instance.roomlist_2[i].z);
                }
            }
            else if (map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].room_No == 3)
            {
                for (int i = 0; i < GameManager.instance.roomlist_3.Count; i++)
                {
                    Open_Mini_Map((int)GameManager.instance.roomlist_3[i].x, (int)GameManager.instance.roomlist_3[i].z);
                }
            }
            else if (map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].room_No == 4)
            {
                for (int i = 0; i < GameManager.instance.roomlist_4.Count; i++)
                {
                    Open_Mini_Map((int)GameManager.instance.roomlist_4[i].x, (int)GameManager.instance.roomlist_4[i].z);
                }
            }
            else if (map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].room_No == 5)
            {
                for (int i = 0; i < GameManager.instance.roomlist_5.Count; i++)
                {
                    Open_Mini_Map((int)GameManager.instance.roomlist_5[i].x, (int)GameManager.instance.roomlist_5[i].z);
                }
            }
            else if (map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].room_No == 6)
            {
                for (int i = 0; i < GameManager.instance.roomlist_6.Count; i++)
                {
                    Open_Mini_Map((int)GameManager.instance.roomlist_6[i].x, (int)GameManager.instance.roomlist_6[i].z);
                }
            }
            else if (map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].room_No == 7)
            {
                for (int i = 0; i < GameManager.instance.roomlist_7.Count; i++)
                {
                    Open_Mini_Map((int)GameManager.instance.roomlist_7[i].x, (int)GameManager.instance.roomlist_7[i].z);
                }
            }
            else if (map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].room_No == 8)
            {
                for (int i = 0; i < GameManager.instance.roomlist_8.Count; i++)
                {
                    Open_Mini_Map((int)GameManager.instance.roomlist_8[i].x, (int)GameManager.instance.roomlist_8[i].z);
                }
            }
        }

        Open_Mini_Map((int)Player.transform.position.x, (int)Player.transform.position.z);


        map_creat.MiniMapPlayer.transform.localPosition = new Vector3(Player.transform.position.x + map_creat.minimapdistance , 1 , Player.transform.position.z + map_creat.minimapdistance);
        
	}

    private void Open_Mini_Map(int x , int z)
    {
        if (map_creat.mini_map[x, z].activeSelf == false)
        {
            map_creat.mini_map[x, z].SetActive(true);
        }
        if (map_creat.mini_map[x + 1, z].activeSelf == false)
        {
            map_creat.mini_map[x + 1, z].SetActive(true);
        }
        if (map_creat.mini_map[x - 1, z].activeSelf == false)
        {
            map_creat.mini_map[x - 1, z].SetActive(true);
        }
        if (map_creat.mini_map[x, z + 1].activeSelf == false)
        {
            map_creat.mini_map[x, z + 1].SetActive(true);
        }
        if (map_creat.mini_map[x, z - 1].activeSelf == false)
        {
            map_creat.mini_map[x, z - 1].SetActive(true);
        }
        if (map_creat.mini_map[x + 1, z + 1].activeSelf == false)
        {
            map_creat.mini_map[x + 1, z + 1].SetActive(true);
        }
        if (map_creat.mini_map[x + 1, z - 1].activeSelf == false)
        {
            map_creat.mini_map[x + 1, z - 1].SetActive(true);
        }
        if (map_creat.mini_map[x - 1, z + 1].activeSelf == false)
        {
            map_creat.mini_map[x - 1, z + 1].SetActive(true);
        }
        if (map_creat.mini_map[x - 1, z - 1].activeSelf == false)
        {
            map_creat.mini_map[x - 1, z - 1].SetActive(true);
        }
    }
}
