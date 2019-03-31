using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using UnityEngine.UI;

public class map_creat : MonoBehaviour {
    //敵ステータス
    public const int ENEMY1_HP = 8;
    public const int ENEMY1_MP = 2;
    public const int ENEMY1_ATTACK = 2;
    public const int ENEMY1_DEFENSE = 0;
    public const int ENEMY1_ATTACK_RANGE = 1;
    public const int ENEMY1_ATTACK_TYPE = 0;
    public const bool ENEMY1_SLANTING_WALL = true;

    public const int ENEMY2_HP = 2;
    public const int ENEMY2_MP = 2;
    public const int ENEMY2_ATTACK = 5;
    public const int ENEMY2_DEFENSE = 0;
    public const int ENEMY2_ATTACK_RANGE = 1;
    public const int ENEMY2_ATTACK_TYPE = 0;
    public const bool ENEMY2_SLANTING_WALL = false;

    public const int ENEMY3_HP = 5;
    public const int ENEMY3_MP = 2;
    public const int ENEMY3_ATTACK = 3;
    public const int ENEMY3_DEFENSE = 0;
    public const int ENEMY3_ATTACK_RANGE = 2;
    public const int ENEMY3_ATTACK_TYPE = 0;
    public const bool ENEMY3_SLANTING_WALL = true;


    //武器ステータス
    public const string NAME_W1 = "ロングソード";
    public const int HP_W1 = 100;
    public const int ATTACK_W1 = 5;
    public const int DEFENSE_W1 = 3;
    public const int ATTACK_RANGE_W1 = 2;
    public const int ATTACK_TYPE_W1 = 0;
    public const bool ATTACK_THROUGH_W1 = false;
    public const bool SLANTING_WALL_W1 = false;

    public const string NAME_W2 = "ショットガン";
    public const int HP_W2 = 20;
    public const int ATTACK_W2 = 2;
    public const int DEFENSE_W2 = 1;
    public const int ATTACK_RANGE_W2 = 2;
    public const int ATTACK_TYPE_W2 = 1;
    public const bool ATTACK_THROUGH_W2 = false;
    public const bool SLANTING_WALL_W2 = true;

    public const string NAME_W3 = "ライフル";
    public const int HP_W3 = 0;
    public const int ATTACK_W3 = 5;
    public const int DEFENSE_W3 = 0;
    public const int ATTACK_RANGE_W3 = 3;
    public const int ATTACK_TYPE_W3 = 0;
    public const bool ATTACK_THROUGH_W3 = true;
    public const bool SLANTING_WALL_W3 = true;


    private GameObject FloorText;

    public GameObject wallObject;
    public GameObject wallObject1;
    public GameObject wallObject2;
    public GameObject wallObject3;
    public GameObject wallObject4;
    public GameObject floor;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    public GameObject Kaidan;
    public GameObject Material1;
    public GameObject Material2;
    public GameObject Material3;
    public GameObject Weapon1;
    public GameObject Weapon2;
    public GameObject Weapon3;

    //ミニマップ
    public GameObject MiniMapWall;
    public GameObject MiniMapFloor;
    public GameObject MiniMapClear;
    public static GameObject MiniMapPlayer;
    public GameObject MiniMapKaidan;
    public GameObject MiniMapItem;
    public static int minimapdistance  = 100;

    public Vector3 entrancevec;

    public static int MAX_X = 40;
    public static int MAX_Y = 40;

    public int[] Xline;
    public int[] Yline;

    public int[] room_0;

    int[,,] room_x;
    int[,,] room_y;

    private bool duplication = false;

    private int i1, i2, i3, i4, i5, i6, r, z1, z2, z3, z4,enemynumber;


    public static map_state[,] map;
    public static map_exist[,] map_ex;
    public static map_item[,] map_item;

    public static GameObject[,] mini_map;

    void Start()
    {
    
}

    // Use this for initialization
    public void Mapcreat()
    {
        FloorText = GameObject.Find("FloorLevel");
            GameManager.instance.floorlevel++;
        
        
        GameManager.instance.roomlist.Clear();
        GameManager.instance.roomlist_0.Clear();
        GameManager.instance.roomlist_1.Clear();
        GameManager.instance.roomlist_2.Clear();
        GameManager.instance.roomlist_3.Clear();
        GameManager.instance.roomlist_4.Clear();
        GameManager.instance.roomlist_5.Clear();
        GameManager.instance.roomlist_6.Clear();
        GameManager.instance.roomlist_7.Clear();
        GameManager.instance.roomlist_8.Clear();
        GameManager.instance.entrancelist_0.Clear();
            GameManager.instance.entrancelist_1.Clear();
            GameManager.instance.entrancelist_2.Clear();
            GameManager.instance.entrancelist_3.Clear();
            GameManager.instance.entrancelist_4.Clear();
            GameManager.instance.entrancelist_5.Clear();
            GameManager.instance.entrancelist_6.Clear();
            GameManager.instance.entrancelist_7.Clear();
            GameManager.instance.entrancelist_8.Clear();


        map = new map_state[MAX_X + 4, MAX_Y + 4];
        room_x = new int[3, 2, 3];
        room_y = new int[3, 2, 3];

        map_ex = new map_exist[MAX_X + 4, MAX_Y + 4];

        map_item = new map_item[MAX_X + 4, MAX_Y + 4];

        mini_map = new GameObject[MAX_X + 4,MAX_Y + 4];

        for (i1 = 0; i1 < MAX_X + 4; i1++)
        {
            for (i2 = 0; i2 < MAX_X + 4; i2++)
            {
                map[i1, i2] = new wall();
                map_ex[i1, i2] = new clear();
                map_item[i1, i2] = new clean();
            }
        }//map全て壁にする、map_ex全て空欄に


        Xline[0] = Random.Range(5, MAX_X - 29);
        Xline[1] = Random.Range(Xline[0] + 6, Xline[0] + MAX_X - 29);

        Yline[0] = Random.Range(5, MAX_Y - 29);
        Yline[1] = Random.Range(Yline[0] + 6, Yline[0] + MAX_Y - 29);
        //部屋を分割



        for (i3 = 0; i3 < 3; i3++)
        {
            room_x[0, 0, i3] = Random.Range(1, Xline[0] - 3);
            room_x[0, 1, i3] = Random.Range(room_x[0, 0, i3] + 2, Xline[0] - 1);
            room_x[1, 0, i3] = Random.Range(Xline[0] + 2, Xline[1] - 3);
            room_x[1, 1, i3] = Random.Range(room_x[1, 0, i3] + 2, Xline[1] - 1);
            room_x[2, 0, i3] = Random.Range(Xline[1] + 2, MAX_X - 3);
            room_x[2, 1, i3] = Random.Range(room_x[2, 0, i3] + 2, MAX_X - 1);
        }
        for (i3 = 0; i3 < 3; i3++)
        {
            room_y[0, 0, i3] = Random.Range(1, Yline[0] - 3);
            room_y[0, 1, i3] = Random.Range(room_y[0, 0, i3] + 2, Yline[0] - 1);
            room_y[1, 0, i3] = Random.Range(Yline[0] + 2, Yline[1] - 3);
            room_y[1, 1, i3] = Random.Range(room_y[1, 0, i3] + 2, Yline[1] - 1);
            room_y[2, 0, i3] = Random.Range(Yline[1] + 2, MAX_Y - 3);
            room_y[2, 1, i3] = Random.Range(room_y[2, 0, i3] + 2, MAX_Y - 1);
        }

        i2 = 0;
        for (i1 = 0; i1 < 9; i1++) {
            room_0[i1] = Random.Range(0, 2);
            if (room_0[i1] == 0)
            {
                i2++;
            }
        }
        if (i2 > 6)
        {
            room_0[0] = 1;
            room_0[1] = 1;
        }


        //部屋をつくる

        for (i1 = 0; i1 < 3; i1++)
        {
            for (i2 = 0; i2 < 3; i2++)
            {
                if ((i1 == 0 && i2 == 0 && room_0[0] == 1) ||
                    (i1 == 1 && i2 == 0 && room_0[1] == 1) ||
                    (i1 == 2 && i2 == 0 && room_0[2] == 1) ||
                    (i1 == 0 && i2 == 1 && room_0[3] == 1) ||
                    (i1 == 1 && i2 == 1 && room_0[4] == 1) ||
                    (i1 == 2 && i2 == 1 && room_0[5] == 1) ||
                    (i1 == 0 && i2 == 2 && room_0[6] == 1) ||
                    (i1 == 1 && i2 == 2 && room_0[7] == 1) ||
                    (i1 == 2 && i2 == 2 && room_0[8] == 1))
                {
                    for (i3 = room_x[i1, 0, i2]; i3 <= room_x[i1, 1, i2]; i3++)
                    {
                        for (i4 = room_y[i2, 0, i1]; i4 <= room_y[i2, 1, i1]; i4++)
                        {
                            map[i3, i4] = new room();
                            if (i1 == 0 && i2 == 2)
                            {
                                map[i3, i4].room_No = 0;
                                GameManager.instance.roomlist_0.Add(new Vector3(i3, 0, i4));
                            }
                            else if (i1 == 1 && i2 == 2)
                            {
                                map[i3, i4].room_No = 1;
                                GameManager.instance.roomlist_1.Add(new Vector3(i3, 0, i4));
                            }
                            else if (i1 == 2 && i2 == 2)
                            {
                                map[i3, i4].room_No = 2;
                                GameManager.instance.roomlist_2.Add(new Vector3(i3, 0, i4));
                            }
                            else if (i1 == 0 && i2 == 1)
                            {
                                map[i3, i4].room_No = 3;
                                GameManager.instance.roomlist_3.Add(new Vector3(i3, 0, i4));
                            }
                            else if (i1 == 1 && i2 == 1)
                            {
                                map[i3, i4].room_No = 4;
                                GameManager.instance.roomlist_4.Add(new Vector3(i3, 0, i4));
                            }
                            else if (i1 == 2 && i2 == 1)
                            {
                                map[i3, i4].room_No = 5;
                                GameManager.instance.roomlist_5.Add(new Vector3(i3, 0, i4));
                            }
                            else if (i1 == 0 && i2 == 0)
                            {
                                map[i3, i4].room_No = 6;
                                GameManager.instance.roomlist_6.Add(new Vector3(i3, 0, i4));
                            }
                            else if (i1 == 1 && i2 == 0)
                            {
                                map[i3, i4].room_No = 7;
                                GameManager.instance.roomlist_7.Add(new Vector3(i3, 0, i4));
                            }
                            else if (i1 == 2 && i2 == 0)
                            {
                                map[i3, i4].room_No = 8;
                                GameManager.instance.roomlist_8.Add(new Vector3(i3, 0, i4));
                            }
                            
                            GameManager.instance.roomlist.Add(new Vector3(i3, 0, i4));
                        }
                    }
                }
            }
        }
        //2で部屋を設定,部屋終了

        for (i1 = 0; i1 < 2; i1++)
        {
            for (i2 = 1; i2 <= MAX_X + 2; i2++)
            {
                map[i2, Yline[i1]].number = 10;
                map[Xline[i1], i2].number = 10;
            }
        }

        for (i1 = 0; i1 < 9; i1++)
        {
            if (room_0[i1] == 1)
            {
                switch (i1)
                {
                    case 0:





                        r = Random.Range(0, 3);
                        z1 = Random.Range(room_x[0, 0, 0], room_x[0, 1, 0] + 1);
                        z2 = Random.Range(room_y[0, 1, 0], room_y[0, 0, 0] + 1);

                        if (r == 0 || r == 1)
                        {
                            z2 = Random.Range(room_y[0, 1, 0], room_y[0, 0, 0] + 1);
                            i2 = room_x[0, 1, 0];

                            map[i2, z2] = new entrance();
                            map[i2, z2].room_No = 6;


                            for (int i = 0; i < GameManager.instance.entrancelist_6.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_6[i] == new Vector3(i2, 0, z2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_6.Add(new Vector3(i2, 0, z2));
                            }
                            duplication = false;

                            i2++;

                            while (true)
                            {
                                if (i2 == Xline[0])
                                {
                                    break;
                                }
                                map[i2, z2].number = 10;

                                i2++;

                            }
                            map[i2, z2] = new intersection();
                        }
                        if (r == 0 || r == 2)
                        {
                            z1 = Random.Range(room_x[0, 0, 0], room_x[0, 1, 0] + 1);
                            i2 = room_y[0, 1, 0];

                            map[z1, i2] = new entrance();
                            map[z1, i2].room_No = 6;

                            for(int i = 0;i < GameManager.instance.entrancelist_6.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_6[i] == new Vector3(z1, 0, i2))
                                {
                                    duplication = true;
                                }
                            }
                            if(duplication != true)
                            {
                                GameManager.instance.entrancelist_6.Add(new Vector3(z1, 0, i2));
                            }
                            duplication = false;
                            i2++;

                            while (true) {

                                if (i2 == Yline[0])
                                {
                                    break;
                                }
                                map[z1, i2].number = 10;
                                i2++;

                            }
                            map[z1, i2] = new intersection();
                        }
                        break;


                    case 1:
                        z1 = Random.Range(room_x[1, 0, 0], room_x[1, 1, 0] + 1);
                        z2 = Random.Range(room_y[0, 0, 1], room_y[0, 1, 1] + 1);

                        r = Random.Range(0, 7);
                        if (r == 0 || r == 3 || r == 5 || r == 6)
                        {
                            z2 = Random.Range(room_y[0, 0, 1], room_y[0, 1, 1] + 1);
                            i2 = room_x[1, 0, 0];

                            map[i2, z2] = new entrance();
                            map[i2, z2].room_No = 7;

                            for (int i = 0; i < GameManager.instance.entrancelist_7.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_7[i] == new Vector3(i2, 0, z2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_7.Add(new Vector3(i2, 0, z2));
                            }
                            duplication = false;

                            i2--;
                            while (true)
                            {
                                if (i2 == Xline[0])
                                {
                                    break;
                                }
                                map[i2, z2].number = 10;
                                i2--;
                            }
                            map[i2, z2] = new intersection();
                        }
                        if (r == 1 || r == 3 || r == 4 || r == 6)
                        {
                            z1 = Random.Range(room_x[1, 0, 0], room_x[1, 1, 0] + 1);
                            i2 = room_y[0, 1, 1];

                            map[z1, i2] = new entrance();
                            map[z1, i2].room_No = 7;

                            for (int i = 0; i < GameManager.instance.entrancelist_7.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_7[i] == new Vector3(z1, 0, i2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_7.Add(new Vector3(z1, 0, i2));
                            }
                            duplication = false;

                            i2++;
                            while (true)
                            {
                                if (i2 == Yline[0])
                                {
                                    break;
                                }
                                map[z1, i2].number = 10;
                                i2++;
                            }
                            map[z1, i2] = new intersection();
                        }
                        if (r == 2 || r == 4 || r == 5 || r == 6)
                        {
                            z2 = Random.Range(room_y[0, 0, 1], room_y[0, 1, 1] + 1);
                            i2 = room_x[1, 1, 0];
                            map[i2, z2] = new entrance();
                            map[i2, z2].room_No = 7;

                            for (int i = 0; i < GameManager.instance.entrancelist_7.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_7[i] == new Vector3(i2, 0, z2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_7.Add(new Vector3(i2, 0, z2));
                            }
                            duplication = false;

                            i2++;
                            while (true)
                            {
                                if (i2 == Xline[1])
                                {
                                    break;
                                }
                                map[i2, z2].number = 10;    //変更
                                i2++;
                            }
                            map[i2, z2] = new intersection();    //変更
                        }
                        break;


                    case 2:
                        z1 = Random.Range(room_x[2, 0, 0], room_x[2, 1, 0] + 1);
                        z2 = Random.Range(room_y[0, 0, 2], room_y[0, 1, 2] + 1);

                        r = Random.Range(0, 3);
                        if (r == 0 || r == 1)
                        {
                            z2 = Random.Range(room_y[0, 0, 2], room_y[0, 1, 2] + 1);
                            i2 = room_x[2, 0, 0];
                            map[i2, z2] = new entrance();
                            map[i2, z2].room_No = 8;

                            for (int i = 0; i < GameManager.instance.entrancelist_8.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_8[i] == new Vector3(i2, 0, z2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_8.Add(new Vector3(i2, 0, z2));
                            }
                            duplication = false;

                            i2--;
                            while (true)
                            {
                                if (i2 == Xline[1])
                                {
                                    break;
                                }
                                map[i2, z2].number = 10;
                                i2--;
                            }
                            map[i2, z2] = new intersection();
                        }
                        if (r == 0 || r == 2)
                        {
                            z1 = Random.Range(room_x[2, 0, 0], room_x[2, 1, 0] + 1);
                            i2 = room_y[0, 1, 2];
                            map[z1, i2] = new entrance();
                            map[z1, i2].room_No = 8;

                            for (int i = 0; i < GameManager.instance.entrancelist_8.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_8[i] == new Vector3(z1, 0, i2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_8.Add(new Vector3(z1, 0, i2));
                            }
                            duplication = false;
                            i2++;
                            while (true)
                            {
                                if (i2 == Yline[0])
                                {
                                    break;
                                }
                                map[z1, i2].number = 10;
                                i2++;
                            }
                            map[z1, i2] = new intersection();
                        }
                        break;


                    case 3:
                        z1 = Random.Range(room_x[0, 0, 1], room_x[0, 1, 1] + 1);
                        z2 = Random.Range(room_y[1, 0, 0], room_y[1, 1, 0] + 1);

                        r = Random.Range(0, 7);
                        if (r == 0 || r == 3 || r == 5 || r == 6)
                        {
                            z1 = Random.Range(room_x[0, 0, 1], room_x[0, 1, 1] + 1);
                            i2 = room_y[1, 0, 0];
                            map[z1, i2] = new entrance();
                            map[z1, i2].room_No = 3;

                            for (int i = 0; i < GameManager.instance.entrancelist_3.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_3[i] == new Vector3(z1, 0, i2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_3.Add(new Vector3(z1, 0, i2));
                            }
                            duplication = false;

                            i2--;
                            while (true)
                            {
                                if (i2 == Yline[0])
                                {
                                    break;
                                }
                                map[z1, i2].number = 10;
                                i2--;
                            }
                            map[z1, i2] = new intersection();
                        }
                        if (r == 1 || r == 3 || r == 4 || r == 6)
                        {
                            z2 = Random.Range(room_y[1, 0, 0], room_y[1, 1, 0] + 1);
                            i2 = room_x[0, 1, 1];
                            map[i2, z2] = new entrance();
                            map[i2, z2].room_No = 3;

                            for (int i = 0; i < GameManager.instance.entrancelist_3.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_3[i] == new Vector3(i2, 0, z2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_3.Add(new Vector3(i2, 0, z2));
                            }
                            duplication = false;
                            i2++;
                            while (true)
                            {
                                if (i2 == Xline[0])
                                {
                                    break;
                                }
                                map[i2, z2].number = 10;
                                i2++;
                            }
                            map[i2, z2] = new intersection();
                        }
                        if (r == 2 || r == 4 || r == 5 || r == 6)
                        {
                            z1 = Random.Range(room_x[0, 0, 1], room_x[0, 1, 1] + 1);
                            i2 = room_y[1, 1, 0];
                            map[z1, i2] = new entrance();
                            map[z1, i2].room_No = 3;

                            for (int i = 0; i < GameManager.instance.entrancelist_3.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_3[i] == new Vector3(z1, 0, i2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_3.Add(new Vector3(z1, 0, i2));
                            }
                            duplication = false;
                            i2++;
                            while (true)
                            {
                                if (i2 == Yline[1])
                                {
                                    break;
                                }
                                map[z1, i2].number = 10;
                                i2++;
                            }
                            map[z1, i2] = new intersection();
                        }
                        break;


                    case 4:
                        z1 = Random.Range(room_x[1, 0, 1], room_x[1, 1, 1] + 1);
                        z2 = Random.Range(room_y[1, 0, 1], room_y[1, 1, 1] + 1);

                        r = Random.Range(0, 15);
                        if (r == 0 || r == 4 || r == 7 || r == 8 || r == 10 || r == 12 || r == 13 || r == 14)//↑
                        {
                            z1 = Random.Range(room_x[1, 0, 1], room_x[1, 1, 1] + 1);
                            i2 = room_y[1, 0, 1];
                            map[z1, i2] = new entrance();
                            map[z1, i2].room_No = 4;

                            for (int i = 0; i < GameManager.instance.entrancelist_4.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_4[i] == new Vector3(z1, 0, i2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_4.Add(new Vector3(z1, 0, i2));
                            }
                            duplication = false;
                            i2--;
                            while (true)
                            {
                                if (i2 == Yline[0])
                                {
                                    break;
                                }
                                map[z1, i2].number = 10;
                                i2--;
                            }
                            map[z1, i2] = new intersection();
                        }
                        if (r == 1 || r == 4 || r == 5 || r == 9 || r == 10 || r == 11 || r == 13 || r == 14)//→
                        {
                            z2 = Random.Range(room_y[1, 0, 1], room_y[1, 1, 1] + 1);
                            i2 = room_x[1, 1, 1];
                            map[i2, z2] = new entrance();
                            map[i2, z2].room_No = 4;

                            for (int i = 0; i < GameManager.instance.entrancelist_4.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_4[i] == new Vector3(i2, 0, z2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_4.Add(new Vector3(i2, 0, z2));
                            }
                            duplication = false;
                            i2++;
                            while (true)
                            {
                                if (i2 == Xline[1])
                                {
                                    break;
                                }
                                map[i2, z2].number = 10;
                                i2++;
                            }
                            map[i2, z2] = new intersection();
                        }
                        if (r == 2 || r == 5 || r == 6 || r == 8 || r == 10 || r == 11 || r == 12 || r == 14)//↓
                        {
                            z1 = Random.Range(room_x[1, 0, 1], room_x[1, 1, 1] + 1);
                            i2 = room_y[1, 1, 1];
                            map[z1, i2] = new entrance();
                            map[z1, i2].room_No = 4;

                            for (int i = 0; i < GameManager.instance.entrancelist_4.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_4[i] == new Vector3(z1, 0, i2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_4.Add(new Vector3(z1, 0, i2));
                            }
                            duplication = false;
                            i2++;
                            while (true)
                            {
                                if (i2 == Yline[1])
                                {
                                    break;
                                }
                                map[z1, i2].number = 10;
                                i2++;
                            }
                            map[z1, i2] = new intersection();
                        }
                        if (r == 3 || r == 6 || r == 7 || r == 9 || r == 11 || r == 12 || r == 13 || r == 14)//←
                        {
                            z2 = Random.Range(room_y[1, 0, 1], room_y[1, 1, 1] + 1);
                            i2 = room_x[1, 0, 1];
                            map[i2, z2] = new entrance();
                            map[i2, z2].room_No = 4;

                            for (int i = 0; i < GameManager.instance.entrancelist_4.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_4[i] == new Vector3(i2, 0, z2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_4.Add(new Vector3(i2, 0, z2));
                            }
                            duplication = false;
                            i2--;
                            while (true)
                            {
                                if (i2 == Xline[0])
                                {
                                    break;
                                }
                                map[i2, z2].number = 10;
                                i2--;
                            }
                            map[i2, z2] = new intersection();
                        }
                        break;


                    case 5:
                        z1 = Random.Range(room_x[2, 0, 1], room_x[2, 1, 1] + 1);
                        z2 = Random.Range(room_y[1, 0, 2], room_y[1, 1, 2] + 1);

                        r = Random.Range(0, 7);
                        if (r == 0 || r == 3 || r == 5 || r == 6)
                        {
                            z1 = Random.Range(room_x[2, 0, 1], room_x[2, 1, 1] + 1);
                            i2 = room_y[1, 0, 2];
                            map[z1, i2] = new entrance();
                            map[z1, i2].room_No = 5;

                            for (int i = 0; i < GameManager.instance.entrancelist_5.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_5[i] == new Vector3(z1, 0, i2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_5.Add(new Vector3(z1, 0, i2));
                            }
                            duplication = false;
                            i2--;
                            while (true)
                            {
                                if (i2 == Yline[0])
                                {
                                    break;
                                }
                                map[z1, i2].number = 10;
                                i2--;
                            }
                            map[z1, i2] = new intersection();
                        }
                        if (r == 1 || r == 3 || r == 4 || r == 6)
                        {
                            z2 = Random.Range(room_y[1, 0, 2], room_y[1, 1, 2] + 1);
                            i2 = room_x[2, 0, 1];
                            map[i2, z2] = new entrance();
                            map[i2, z2].room_No = 5;

                            for (int i = 0; i < GameManager.instance.entrancelist_5.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_5[i] == new Vector3(i2, 0, z2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_5.Add(new Vector3(i2, 0, z2));
                            }
                            duplication = false;
                            i2--;
                            while (true)
                            {
                                if (i2 == Xline[1])
                                {
                                    break;
                                }
                                map[i2, z2].number = 10;
                                i2--;
                            }
                            map[i2, z2] = new intersection();
                        }
                        if (r == 2 || r == 4 || r == 5 || r == 6)
                        {
                            z1 = Random.Range(room_x[2, 0, 1], room_x[2, 1, 1] + 1);
                            i2 = room_y[1, 1, 2];
                            map[z1, i2] = new entrance();
                            map[z1, i2].room_No = 5;

                            for (int i = 0; i < GameManager.instance.entrancelist_5.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_5[i] == new Vector3(z1, 0, i2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_5.Add(new Vector3(z1, 0, i2));
                            }
                            duplication = false;
                            i2++;
                            while (true)
                            {
                                if (i2 == Yline[1])
                                {
                                    break;
                                }
                                map[z1, i2].number = 10;
                                i2++;
                            }
                            map[z1, i2] = new intersection();
                        }
                        break;


                    case 6:
                        z1 = Random.Range(room_x[0, 0, 2], room_x[0, 1, 2] + 1);
                        z2 = Random.Range(room_y[2, 0, 0], room_y[2, 1, 0] + 1);

                        r = Random.Range(0, 3);
                        if (r == 0 || r == 1)
                        {
                            z2 = Random.Range(room_y[2, 0, 0], room_y[2, 1, 0] + 1);
                            i2 = room_x[0, 1, 2];
                            map[i2, z2] = new entrance();
                            map[i2, z2].room_No = 0;

                            for (int i = 0; i < GameManager.instance.entrancelist_0.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_0[i] == new Vector3(i2, 0, z2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_0.Add(new Vector3(i2, 0, z2));
                            }
                            duplication = false;

                            i2++;
                            while (true)
                            {
                                if (i2 == Xline[0])
                                {
                                    break;
                                }
                                map[i2, z2].number = 10;
                                i2++;
                            }
                            map[i2, z2] = new intersection();
                        }
                        if (r == 0 || r == 2)
                        {
                            z1 = Random.Range(room_x[0, 0, 2], room_x[0, 1, 2] + 1);
                            i2 = room_y[2, 0, 0];
                            map[z1, i2] = new entrance();
                            map[z1, i2].room_No = 0;

                            for (int i = 0; i < GameManager.instance.entrancelist_0.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_0[i] == new Vector3(z1, 0, i2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_0.Add(new Vector3(z1, 0, i2));
                            }
                            duplication = false;
                            i2--;
                            while (true)
                            {
                                if (i2 == Yline[1])
                                {
                                    break;
                                }
                                map[z1, i2].number = 10;
                                i2--;
                            }
                            map[z1, i2] = new intersection();
                        }
                        break;


                    case 7:
                        z1 = Random.Range(room_x[1, 0, 2], room_x[1, 1, 2] + 1);
                        z2 = Random.Range(room_y[2, 0, 1], room_y[2, 1, 1] + 1);

                        r = Random.Range(0, 7);
                        if (r == 0 || r == 3 || r == 5 || r == 6)
                        {
                            z2 = Random.Range(room_y[2, 0, 1], room_y[2, 1, 1] + 1);
                            i2 = room_x[1, 0, 2];
                            map[i2, z2] = new entrance();
                            map[i2, z2].room_No = 1;

                            for (int i = 0; i < GameManager.instance.entrancelist_1.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_1[i] == new Vector3(i2, 0, z2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_1.Add(new Vector3(i2, 0, z2));
                            }
                            duplication = false;
                            i2--;
                            while (true)
                            {
                                if (i2 == Xline[0])
                                {
                                    break;
                                }
                                map[i2, z2].number = 10;
                                i2--;
                            }
                            map[i2, z2] = new intersection();
                        }
                        if (r == 1 || r == 3 || r == 4 || r == 6)
                        {
                            z1 = Random.Range(room_x[1, 0, 2], room_x[1, 1, 2] + 1);
                            i2 = room_y[2, 0, 1];
                            map[z1, i2] = new entrance();
                            map[z1, i2].room_No = 1;

                            for (int i = 0; i < GameManager.instance.entrancelist_1.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_1[i] == new Vector3(z1, 0, i2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_1.Add(new Vector3(z1, 0, i2));
                            }
                            duplication = false;
                            i2--;
                            while (true)
                            {
                                if (i2 == Yline[1])
                                {
                                    break;
                                }
                                map[z1, i2].number = 10;
                                i2--;
                            }
                            map[z1, i2] = new intersection();
                        }
                        if (r == 2 || r == 4 || r == 5 || r == 6)
                        {
                            z2 = Random.Range(room_y[2, 0, 1], room_y[2, 1, 1] + 1);
                            i2 = room_x[1, 1, 2];
                            map[i2, z2] = new entrance();
                            map[i2, z2].room_No = 1;

                            for (int i = 0; i < GameManager.instance.entrancelist_1.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_1[i] == new Vector3(i2, 0, z2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_1.Add(new Vector3(i2, 0, z2));
                            }
                            duplication = false;
                            i2++;
                            while (true)
                            {
                                if (i2 == Xline[1])
                                {
                                    break;
                                }
                                map[i2, z2].number = 10;    //変更
                                i2++;
                            }
                            map[i2, z2] = new intersection();       //変更
                        }
                        break;


                    case 8:
                        z1 = Random.Range(room_x[2, 0, 2], room_x[2, 1, 2] + 1);
                        z2 = Random.Range(room_y[2, 0, 2], room_y[2, 1, 2] + 1);

                        r = Random.Range(0, 3);
                        if (r == 0 || r == 1)
                        {
                            z2 = Random.Range(room_y[2, 0, 2], room_y[2, 1, 2] + 1);
                            i2 = room_x[2, 0, 2];
                            map[i2, z2] = new entrance();
                            map[i2, z2].room_No = 2;

                            for (int i = 0; i < GameManager.instance.entrancelist_2.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_2[i] == new Vector3(i2, 0, z2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_2.Add(new Vector3(i2, 0, z2));
                            }
                            duplication = false;
                            i2--;
                            while (true)
                            {
                                if (i2 == Xline[1])
                                {
                                    break;
                                }
                                map[i2, z2].number = 10;
                                i2--;
                            }
                            map[i2, z2] = new intersection();
                        }
                        if (r == 0 || r == 2)
                        {
                            z1 = Random.Range(room_x[2, 0, 2], room_x[2, 1, 2] + 1);
                            i2 = room_y[2, 0, 2];
                            map[z1, i2] = new entrance();
                            map[z1, i2].room_No = 2;

                            for (int i = 0; i < GameManager.instance.entrancelist_2.Count; i++)
                            {
                                if (GameManager.instance.entrancelist_2[i] == new Vector3(z1, 0, i2))
                                {
                                    duplication = true;
                                }
                            }
                            if (duplication != true)
                            {
                                GameManager.instance.entrancelist_2.Add(new Vector3(z1, 0, i2));
                            }
                            duplication = false;
                            i2--;
                            while (true)
                            {
                                if (i2 == Yline[1])
                                {
                                    break;
                                }
                                map[z1, i2].number = 10;
                                i2--;
                            }
                            map[z1, i2] = new intersection();
                        }
                        break;
                }
            }
        }

        //通路の交差点は残す
        for (i1 = 0; i1 < 2; i1++)
        {
            for (i2 = 0; i2 < 2; i2++)
            {
                map[Xline[i1], Yline[i2]] = new intersection();
            }
        }
        

        //部屋と通路をつなぐ

        i1 = 0;
        while (true)
        {
            if (map[Xline[0], MAX_X + 3 - i1].number == 2 || i1 == MAX_X + 3)
            {
                break;
            }
            map[Xline[0], MAX_X + 3 - i1] = new wall();
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[Xline[1], MAX_X + 3 - i1].number == 2 || i1 == MAX_X + 3)
            {
                break;
            }
            map[Xline[1], MAX_X + 3 - i1] = new wall();
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[Xline[0], i1].number == 2 || i1 == MAX_X + 3)
            {
                break;
            }
            map[Xline[0], i1] = new wall();
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[Xline[1], i1].number == 2 || i1 == MAX_X + 3)
            {
                break;
            }
            map[Xline[1], i1] = new wall();
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[i1, Yline[0]].number == 2 || i1 == MAX_X + 3)
            {
                break;
            }
            map[i1, Yline[0]] = new wall();
            i1++;
        }
        
        i1 = 0;
        while (true)
        {
            if (map[i1, Yline[1]].number == 2 || i1 == MAX_X + 3)
            {
                break;
            }
            map[i1, Yline[1]] = new wall();
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[MAX_X + 3 - i1, Yline[0]].number == 2 || i1 == MAX_X + 3)
            {
                break;
            }
            map[MAX_X + 3 - i1, Yline[0]] = new wall();
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[MAX_X + 3 - i1, Yline[1]].number == 2 || i1 == MAX_X + 3)
            {
                break;
            }
            map[MAX_X + 3 - i1, Yline[1]] = new wall();
            i1++;
        }


        //階段を部屋に配置
        InstantiateInRoom_map_item(Kaidan);

        for (int x = 0; x < MAX_X + 4; x++)
        {
            for (int y = 0; y < MAX_X + 4; y++)
            {
                if (map[x, y].number == 0)
                {
                    Instantiate(wallObject, new Vector3(x, 0, y), Quaternion.identity);

                    mini_map[x,y] = Instantiate(MiniMapWall, new Vector3(x + minimapdistance, 0, y + minimapdistance), Quaternion.identity);
                    mini_map[x, y].SetActive(false);
                }

                if (map[x, y].number == 1 || map[x, y].number == 2 || map[x, y].number == 3 || map[x, y].number == 5 || map[x, y].number == 10)
                {
                    if (map[x, y].number != 5)
                    {
                        mini_map[x, y] = Instantiate(MiniMapFloor, new Vector3(x + minimapdistance, 0, y + minimapdistance), Quaternion.identity);
                    }
                    mini_map[x, y].SetActive(false);
                }
                
                if (map[x, y].number == 99)
                {
                    Instantiate(wallObject3, new Vector3(x, 0, y), Quaternion.identity);
                }/*壁以外色付け用
                if (map[x, y] == 2)
                {
                    Instantiate(wallObject2, new Vector3(x, 0, y), Quaternion.identity);
                }
        if (map[x, y] == 3)//通路の分岐点
        {
            Instantiate(wallObject3, new Vector3(x, 0, y), Quaternion.identity);
        }
                if (map[x, y] == 4)//部屋の入口
                {
                    Instantiate(wallObject4, new Vector3(x, 0, y), Quaternion.identity);
                }
                */

                  Instantiate(floor, new Vector3(x, -1, y), Quaternion.identity);


                
            }
        }

        for (int x = -25; x < MAX_X + 25; x++)
        {
            for (int y = -25; y < MAX_X + 25; y++)
            {
                //ミニマップの何もない場所
                Instantiate(MiniMapClear, new Vector3(x + minimapdistance, -1, y + minimapdistance), Quaternion.identity);
            }
        }

        //アイテムを部屋に配置
        int itemnumber = Random.Range(5, 15);
        for(int i = 0; i < itemnumber; i++)
        {
            int a = Random.Range(0, 30);
            if(0 <= a && a < 2)
            {
                InstantiateInRoom_map_item(Item1);
            }
            else if(a == 2)
            {
                InstantiateInRoom_map_item(Item2);
            }
            else if(a == 3)
            {
                InstantiateInRoom_map_item(Item3);
            }else if(a == 4)
            {
                InstantiateInRoom_map_item(Material1);
            }else if(a == 5)
            {
                InstantiateInRoom_map_item(Material2);
            }else if(a == 6)
            {
                InstantiateInRoom_map_item(Material3);
            }else if(a >= 7 && a<= 15)
            {
                InstantiateInRoom_map_item(Weapon1);
            }else if(a >= 16 && a<= 23)
            {
                InstantiateInRoom_map_item(Weapon2);
            }else if(a >= 24 && a<= 30)
            {
                InstantiateInRoom_map_item(Weapon3);
            }
        }

        //敵をランダムに配置
        enemynumber = Random.Range(5, 9);
        Random_Enemy_Instantiate(enemynumber);


        FloorText.GetComponent<Text>().text = GameManager.instance.floorlevel +"階";
    }

    

    private void InstantiateInRoom_map_item(GameObject obj)
    {
        Vector3 pos;
        do
        {
            pos = GameManager.instance.roomlist[Random.Range(0, GameManager.instance.roomlist.Count)];

        } while (map_item[(int)pos.x, (int)pos.z].exist == true || map[(int)pos.x, (int)pos.z].number == 5 || map[(int)pos.x, (int)pos.z].number == 0);

        if (obj.tag == "kaidan")
        {
            map[(int)pos.x , (int)pos.z] = new kaidan();
            map[(int)pos.x , (int)pos.z].number = 5;
        }else if(obj.tag == "Item1")
        {
            map_item[(int)pos.x, (int)pos.z] = new item1();
        }
        else if (obj.tag == "Item2")
        {
            map_item[(int)pos.x, (int)pos.z] = new item2();
        }
        else if (obj.tag == "Item3")
        {
            map_item[(int)pos.x, (int)pos.z] = new item3();
        }else if(obj.tag == "material1")
        {
            map_item[(int)pos.x, (int)pos.z] = new material1();
        }
        else if(obj.tag == "material2")
        {
            map_item[(int)pos.x, (int)pos.z] = new material2();
        }
        else if(obj.tag == "material3")
        {
            map_item[(int)pos.x, (int)pos.z] = new material3();
        }
        else if(obj.tag == "weapon1")
        {
            map_item[(int)pos.x, (int)pos.z] = new weapon1(NAME_W1, HP_W1 , ATTACK_W1 , DEFENSE_W1, ATTACK_RANGE_W1 , ATTACK_TYPE_W1 , ATTACK_THROUGH_W1 , SLANTING_WALL_W1);
        }
        else if(obj.tag == "weapon2")
        {
            map_item[(int)pos.x, (int)pos.z] = new weapon2(NAME_W2, HP_W2, ATTACK_W2, DEFENSE_W2, ATTACK_RANGE_W2, ATTACK_TYPE_W2, ATTACK_THROUGH_W2, SLANTING_WALL_W2);
        }
        else if(obj.tag == "weapon3")
        {
            map_item[(int)pos.x, (int)pos.z] = new weapon3(NAME_W3, HP_W3, ATTACK_W3, DEFENSE_W3, ATTACK_RANGE_W3, ATTACK_TYPE_W3, ATTACK_THROUGH_W3, SLANTING_WALL_W3);
        }
        GameObject obj2 = Instantiate(obj, new Vector3(pos.x, 0, pos.z), Quaternion.identity);

        if (obj.tag != "kaidan")
        {
            //ミニマップも含めてアイテムを生成
            Instantiate_item(obj2, (int)pos.x, (int)pos.z);
            
        }
        else if(obj.tag == "kaidan")
        {
            mini_map[(int)pos.x , (int)pos.z] = Instantiate(MiniMapKaidan, new Vector3(pos.x + map_creat.minimapdistance, 0, pos.z + map_creat.minimapdistance) , Quaternion.identity);
        }
    }
    /*private void InstantiateInRoom_map_weapon(GameObject obj)
    {
        Vector3 pos;
        do
        {
            pos = GameManager.instance.roomlist[Random.Range(0, GameManager.instance.roomlist.Count)];

        } while (map_weapon[(int)pos.x , (int)pos.z].exist == true ||map_item[(int)pos.x, (int)pos.z].exist == true || map[(int)pos.x, (int)pos.z].number == 5 || map[(int)pos.x, (int)pos.z].number == 0);

        if (obj.tag == "Material1")
        {
            map_weapon[(int)pos.x, (int)pos.z] = new material();
            map_weapon[(int)pos.x, (int)pos.z].material_no = 1;
        }
        else if (obj.tag == "Material2")
        {
            map_weapon[(int)pos.x, (int)pos.z] = new material();
            map_weapon[(int)pos.x, (int)pos.z].material_no = 2;
        }
        else if (obj.tag == "Material3")
        {
            map_weapon[(int)pos.x, (int)pos.z] = new material();
            map_weapon[(int)pos.x, (int)pos.z].material_no = 3;
        }
        else if (obj.tag == "Equipment")
        {
            map_weapon[(int)pos.x, (int)pos.z] = new equipment();
        }
        else if (obj.tag == "Cannon")
        {
            map_weapon[(int)pos.x, (int)pos.z] = new cannon();
        }
        GameObject obj2 = Instantiate(obj, new Vector3(pos.x, 0, pos.z), Quaternion.identity);
        map_weapon[(int)pos.x, (int)pos.z].obj = obj2;
    }*/
    
    private void Instantiate_item(GameObject Map_item , int pos_x , int pos_z)
    {
        //アイテムを生成し、そのアイテムの子オブジェクトにミニマップ用のオブジェクトを追加
        map_item[pos_x, pos_z].obj = Map_item;
        map_item[pos_x, pos_z].minimap_item = Instantiate(MiniMapItem, new Vector3(pos_x + map_creat.minimapdistance, 1, pos_z + map_creat.minimapdistance), Quaternion.identity);
        map_item[pos_x, pos_z].minimap_item.transform.parent = map_item[pos_x, pos_z].obj.transform;
        map_item[pos_x, pos_z].minimap_item.SetActive(false);
    }

    //敵の種類を決定
    public void Random_Enemy_Instantiate(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int a = Random.Range(0, 10);
            if (0 <= a && a < 4)
            {
                InstantiateEnemyInRoom(Enemy);
            }
            else if (4 <= a && a < 7)
            {
                InstantiateEnemyInRoom(Enemy2);
            }
            else if (7 <= a && a < 9)
            {
                InstantiateEnemyInRoom(Enemy2);
            }
            else
            {
                InstantiateEnemyInRoom(Enemy3);
            }
        }
    }

    //部屋にランダムに敵を生成
    private void InstantiateEnemyInRoom(GameObject obj)
    {
        Vector3 pos;
        do
        {
            pos = GameManager.instance.roomlist[Random.Range(0, GameManager.instance.roomlist.Count)];
        } while (map_ex[(int)pos.x, (int)pos.z].number == 5 || map_ex[(int)pos.x, (int)pos.z].number == 6 || map[(int)pos.x, (int)pos.z].number == 3 || map[(int)pos.x , (int)pos.z].room_No == player.exist_room_no);
        
        if (obj.tag == "Enemy")
        {
            //マップ管理のクラスと、敵のステータスを入れる
            map_ex[(int)pos.x, (int)pos.z] = new enemy();
            map_ex[(int)pos.x, (int)pos.z].state = new enemystate(ENEMY1_HP, ENEMY1_HP, ENEMY1_MP, ENEMY1_MP, ENEMY1_ATTACK, ENEMY1_ATTACK, ENEMY1_DEFENSE, ENEMY1_DEFENSE, ENEMY1_ATTACK_RANGE, ENEMY1_ATTACK_TYPE, ENEMY1_SLANTING_WALL);
        }
        else if(obj.tag == "Enemy2")
        {
            map_ex[(int)pos.x, (int)pos.z] = new enemy();
            map_ex[(int)pos.x, (int)pos.z].state = new enemystate(ENEMY2_HP, ENEMY2_HP, ENEMY2_MP, ENEMY2_MP, ENEMY2_ATTACK, ENEMY2_ATTACK, ENEMY2_DEFENSE, ENEMY2_DEFENSE, ENEMY2_ATTACK_RANGE, ENEMY2_ATTACK_TYPE, ENEMY2_SLANTING_WALL);
        }
        else if(obj.tag == "Enemy3")
        {
            map_ex[(int)pos.x, (int)pos.z] = new enemy();
            map_ex[(int)pos.x, (int)pos.z].state = new enemystate(ENEMY3_HP, ENEMY3_HP, ENEMY3_MP, ENEMY3_MP, ENEMY3_ATTACK, ENEMY3_ATTACK, ENEMY3_DEFENSE, ENEMY3_DEFENSE, ENEMY3_ATTACK_RANGE, ENEMY3_ATTACK_TYPE, ENEMY3_SLANTING_WALL);
        }
        GameObject obj2 = Instantiate(obj, new Vector3(pos.x, 0, pos.z), Quaternion.identity);
        
        map_ex[(int)pos.x, (int)pos.z].obj = obj2;
            map_ex[(int)pos.x, (int)pos.z].enemy_script = obj2.GetComponent<Enemy_script>();
    }
    

    private void MapDebug(int a)
    {
        Debug.Log(a);
        for(int i = 0; i < GameManager.instance.entrancelist_0.Count; i++)
        {
            Debug.Log(GameManager.instance.entrancelist_0[i]+"A");
            map[(int)GameManager.instance.entrancelist_0[i].x, (int)GameManager.instance.entrancelist_0[i].z] = new test();
        }
        for (int i = 0; i < GameManager.instance.entrancelist_1.Count; i++)
        {
            Debug.Log(GameManager.instance.entrancelist_1[i] + "B");
            map[(int)GameManager.instance.entrancelist_1[i].x, (int)GameManager.instance.entrancelist_1[i].z] = new test();
        }
        for (int i = 0; i < GameManager.instance.entrancelist_2.Count; i++)
        {
            Debug.Log(GameManager.instance.entrancelist_2[i] + "C");
            map[(int)GameManager.instance.entrancelist_2[i].x, (int)GameManager.instance.entrancelist_2[i].z] = new test();
        }
        for (int i = 0; i < GameManager.instance.entrancelist_3.Count; i++)
        {
            Debug.Log(GameManager.instance.entrancelist_3[i] + "D");
            map[(int)GameManager.instance.entrancelist_3[i].x, (int)GameManager.instance.entrancelist_3[i].z] = new test();
        }
        for (int i = 0; i < GameManager.instance.entrancelist_4.Count; i++)
        {
            Debug.Log(GameManager.instance.entrancelist_4[i] + "E");
            map[(int)GameManager.instance.entrancelist_4[i].x, (int)GameManager.instance.entrancelist_4[i].z] = new test();
        }
        for (int i = 0; i < GameManager.instance.entrancelist_5.Count; i++)
        {
            Debug.Log(GameManager.instance.entrancelist_5[i] + "F");
            map[(int)GameManager.instance.entrancelist_5[i].x, (int)GameManager.instance.entrancelist_5[i].z] = new test();
        }
        for (int i = 0; i < GameManager.instance.entrancelist_6.Count; i++)
        {
            Debug.Log(GameManager.instance.entrancelist_6[i] + "G");
            map[(int)GameManager.instance.entrancelist_6[i].x, (int)GameManager.instance.entrancelist_6[i].z] = new test();
        }
        for (int i = 0; i < GameManager.instance.entrancelist_7.Count; i++)
        {
            Debug.Log(GameManager.instance.entrancelist_7[i] + "H");
            map[(int)GameManager.instance.entrancelist_7[i].x, (int)GameManager.instance.entrancelist_7[i].z] = new test();
        }
        for (int i = 0; i < GameManager.instance.entrancelist_8.Count; i++)
        {
            Debug.Log(GameManager.instance.entrancelist_8[i] + "I");
            map[(int)GameManager.instance.entrancelist_8[i].x, (int)GameManager.instance.entrancelist_8[i].z] = new test();
        }
    }
}




