using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class map_creat : MonoBehaviour {

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
    

    public Vector3 entrancevec;

    static int MAX_X = 55;
    static int MAX_Y = 55;

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

    void Start()
    {
    
}

    // Use this for initialization
    public void Mapcreat()
    {
        
            GameManager.instance.roomlist.Clear();
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


        for (i1 = 0; i1 < MAX_X + 4; i1++)
        {
            for (i2 = 0; i2 < MAX_X + 4; i2++)
            {
                map[i1, i2] = new wall();
                map_ex[i1, i2] = new clear();
                map_item[i1, i2] = new clean();
            }
        }//map全て壁にする、map_ex全て空欄に


        Xline[0] = Random.Range(5, 26);
        Xline[1] = Random.Range(Xline[0] + 6, Xline[0] + 25);

        Yline[0] = Random.Range(5, 26);
        Yline[1] = Random.Range(Yline[0] + 6, Yline[0] + 25);
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
                            }
                            else if (i1 == 1 && i2 == 2)
                            {
                                map[i3, i4].room_No = 1;
                            }
                            else if (i1 == 2 && i2 == 2)
                            {
                                map[i3, i4].room_No = 2;
                            }
                            else if (i1 == 0 && i2 == 1)
                            {
                                map[i3, i4].room_No = 3;
                            }
                            else if (i1 == 1 && i2 == 1)
                            {
                                map[i3, i4].room_No = 4;
                            }
                            else if (i1 == 2 && i2 == 1)
                            {
                                map[i3, i4].room_No = 5;
                            }
                            else if (i1 == 0 && i2 == 0)
                            {
                                map[i3, i4].room_No = 6;
                            }
                            else if (i1 == 1 && i2 == 0)
                            {
                                map[i3, i4].room_No = 7;
                            }
                            else if (i1 == 2 && i2 == 0)
                            {
                                map[i3, i4].room_No = 8;
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
            if (map[Xline[0], 58 - i1].number == 2 || i1 == 58)
            {
                break;
            }
            map[Xline[0], 58 - i1] = new wall();
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[Xline[1], 58 - i1].number == 2 || i1 == 58)
            {
                break;
            }
            map[Xline[1], 58 - i1] = new wall();
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[Xline[0], i1].number == 2 || i1 == 58)
            {
                break;
            }
            map[Xline[0], i1] = new wall();
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[Xline[1], i1].number == 2 || i1 == 58)
            {
                break;
            }
            map[Xline[1], i1] = new wall();
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[i1, Yline[0]].number == 2 || i1 == 58)
            {
                break;
            }
            map[i1, Yline[0]] = new wall();
            i1++;
        }
        
        i1 = 0;
        while (true)
        {
            if (map[i1, Yline[1]].number == 2 || i1 == 58)
            {
                break;
            }
            map[i1, Yline[1]] = new wall();
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[58 - i1, Yline[0]].number == 2 || i1 == 58)
            {
                break;
            }
            map[58 - i1, Yline[0]] = new wall();
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[58 - i1, Yline[1]].number == 2 || i1 == 58)
            {
                break;
            }
            map[58 - i1, Yline[1]] = new wall();
            i1++;
        }

        

        for (int x = 0; x < 59; x++)
        {
            for (int y = 0; y < 59; y++)
            {
                if (map[x, y].number == 0)
                {
                    Instantiate(wallObject, new Vector3(x, 0, y), Quaternion.identity);
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
       
        //階段を部屋に配置
        InstantiateInRoom(Kaidan);

        //アイテムを部屋に配置
        int itemnumber = Random.Range(5, 15);
        for(int i = 0; i < itemnumber; i++)
        {
            int a = Random.Range(0, 5);
            if(0 <= a && a < 2)
            {
                InstantiateInRoom(Item1);
            }
            else if(a == 2)
            {
                InstantiateInRoom(Item2);
            }
            else if(3 <= a)
            {
                InstantiateInRoom(Item3);
            }
        }

        //敵をランダムに配置
        enemynumber = Random.Range(5, 9);
        for (int i = 0; i < enemynumber; i++)
        {
            int a = Random.Range(0, 10);
            if(0 <= a && a < 4) {
                InstantiateEnemyInRoom(Enemy);
            }
            else if(4 <= a && a < 7)
            {
                InstantiateEnemyInRoom(Enemy2);
            }
            else if(7 <= a && a < 9)
            {
                InstantiateEnemyInRoom(Enemy2);
            }
            else
            {
                InstantiateEnemyInRoom(Enemy3);
            }
        }
    }

    private void InstantiateInRoom(GameObject obj)
    {
        Vector3 pos;
        do
        {
            pos = GameManager.instance.roomlist[Random.Range(0, GameManager.instance.roomlist.Count)];

        } while (map_item[(int)pos.x, (int)pos.z].exist == true || map[(int)pos.x, (int)pos.z].number == 5 || map[(int)pos.x, (int)pos.z].number == 0);

        if (obj.tag == "Kaidan")
        {
            map[(int)pos.x , (int)pos.z] = new kaidan();
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
        }
        Instantiate(obj, new Vector3(pos.x, 0, pos.z), Quaternion.identity);
    }

    //部屋にランダムに敵を生成
    private void InstantiateEnemyInRoom(GameObject obj)
    {
        Vector3 pos;
        do
        {
            pos = GameManager.instance.roomlist[Random.Range(0, GameManager.instance.roomlist.Count)];
        } while (map_ex[(int)pos.x, (int)pos.z].number == 5 || map_ex[(int)pos.x, (int)pos.z].number == 6 || map[(int)pos.x, (int)pos.z].number == 0);
        
        if (obj.tag == "Enemy")
        {
            map_ex[(int)pos.x, (int)pos.z] = new enemy1();
        }else if(obj.tag == "Enemy2")
        {
            map_ex[(int)pos.x, (int)pos.z] = new enemy2();
        }else if(obj.tag == "Enemy3")
        {
            map_ex[(int)pos.x, (int)pos.z] = new enemy3();
        }
        GameObject obj2 = Instantiate(obj, new Vector3(pos.x, 0, pos.z), Quaternion.identity);
        
        map_ex[(int)pos.x, (int)pos.z].obj = obj2;
            map_ex[(int)pos.x, (int)pos.z].enemy_script = obj2.GetComponent<Enemy_script>();
    }

    private void Addentrancelist(float x , float z)
    {

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




