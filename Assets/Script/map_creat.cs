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
    public GameObject kaidan;

    static int MAX_X = 55;
    static int MAX_Y = 55;

    public int[] Xline;
    public int[] Yline;

    public int[] room_0;

    int[,,] room_x;
    int[,,] room_y;
    

    private int i1, i2, i3, i4, i5, i6, r, z1, z2, z3, z4,enemynumber;


    public static int[,] map;

    // Use this for initialization
    public void Mapcreat()
    {

        map = new int[MAX_X + 4, MAX_Y + 4];
        room_x = new int[3, 2, 3];
        room_y = new int[3, 2, 3];


        for (i1 = 0; i1 < MAX_X + 4; i1++)
        {
            for (i2 = 0; i2 < MAX_X + 4; i2++)
            {
                map[i1, i2] = 0;
            }
        }//map全て壁にする


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
                            map[i3, i4] = 2;
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
                map[i2, Yline[i1]] = 1;
                map[Xline[i1], i2] = 1;
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

                            map[i2, z2] = 4;
                            i2++;

                            while (true)
                            {
                                if (i2 == Xline[0])
                                {
                                    break;
                                }
                                map[i2, z2] = 1;

                                i2++;

                            }
                            map[i2, z2] = 3;
                        }
                        if (r == 0 || r == 2)
                        {
                            z1 = Random.Range(room_x[0, 0, 0], room_x[0, 1, 0] + 1);
                            i2 = room_y[0, 1, 0];

                            map[z1, i2] = 4;
                            i2++;

                            while (true) {

                                if (i2 == Yline[0])
                                {
                                    break;
                                }
                                map[z1, i2] = 1;
                                i2++;

                            }
                            map[z1, i2] = 3;
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

                            map[i2, z2] = 4;
                            i2--;
                            while (true)
                            {
                                if (i2 == Xline[0])
                                {
                                    break;
                                }
                                map[i2, z2] = 1;
                                i2--;
                            }
                            map[i2, z2] = 3;
                        }
                        if (r == 1 || r == 3 || r == 4 || r == 6)
                        {
                            z1 = Random.Range(room_x[1, 0, 0], room_x[1, 1, 0] + 1);
                            i2 = room_y[0, 1, 1];

                            map[z1, i2] = 4;
                            i2++;
                            while (true)
                            {
                                if (i2 == Yline[0])
                                {
                                    break;
                                }
                                map[z1, i2] = 1;
                                i2++;
                            }
                            map[z1, i2] = 3;
                        }
                        if (r == 2 || r == 4 || r == 5 || r == 6)
                        {
                            z2 = Random.Range(room_y[0, 0, 1], room_y[0, 1, 1] + 1);
                            i2 = room_x[1, 1, 0];
                            map[i2, z2] = 4;
                            i2++;
                            while (true)
                            {
                                if (i2 == Xline[1])
                                {
                                    break;
                                }
                                map[i2, z2] = 1;    //変更
                                i2++;
                            }
                            map[i2, z2] = 3;    //変更
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
                            map[i2, z2] = 4;
                            i2--;
                            while (true)
                            {
                                if (i2 == Xline[1])
                                {
                                    break;
                                }
                                map[i2, z2] = 1;
                                i2--;
                            }
                            map[i2, z2] = 3;
                        }
                        if (r == 0 || r == 2)
                        {
                            z1 = Random.Range(room_x[2, 0, 0], room_x[2, 1, 0] + 1);
                            i2 = room_y[0, 1, 2];
                            map[z1, i2] = 4;
                            i2++;
                            while (true)
                            {
                                if (i2 == Yline[0])
                                {
                                    break;
                                }
                                map[z1, i2] = 1;
                                i2++;
                            }
                            map[z1, i2] = 3;
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
                            map[z1, i2] = 4;
                            i2--;
                            while (true)
                            {
                                if (i2 == Yline[0])
                                {
                                    break;
                                }
                                map[z1, i2] = 1;
                                i2--;
                            }
                            map[z1, i2] = 3;
                        }
                        if (r == 1 || r == 3 || r == 4 || r == 6)
                        {
                            z2 = Random.Range(room_y[1, 0, 0], room_y[1, 1, 0] + 1);
                            i2 = room_x[0, 1, 1];
                            map[i2, z2] = 4;
                            i2++;
                            while (true)
                            {
                                if (i2 == Xline[0])
                                {
                                    break;
                                }
                                map[i2, z2] = 1;
                                i2++;
                            }
                            map[i2, z2] = 3;
                        }
                        if (r == 2 || r == 4 || r == 5 || r == 6)
                        {
                            z1 = Random.Range(room_x[0, 0, 1], room_x[0, 1, 1] + 1);
                            i2 = room_y[1, 1, 0];
                            map[z1, i2] = 4;
                            i2++;
                            while (true)
                            {
                                if (i2 == Yline[1])
                                {
                                    break;
                                }
                                map[z1, i2] = 1;
                                i2++;
                            }
                            map[z1, i2] = 3;
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
                            map[z1, i2] = 4;
                            i2--;
                            while (true)
                            {
                                if (i2 == Yline[0])
                                {
                                    break;
                                }
                                map[z1, i2] = 1;
                                i2--;
                            }
                            map[z1, i2] = 3;
                        }
                        if (r == 1 || r == 4 || r == 5 || r == 9 || r == 10 || r == 11 || r == 13 || r == 14)//→
                        {
                            z2 = Random.Range(room_y[1, 0, 1], room_y[1, 1, 1] + 1);
                            i2 = room_x[1, 1, 1];
                            map[i2, z2] = 4;
                            i2++;
                            while (true)
                            {
                                if (i2 == Xline[1])
                                {
                                    break;
                                }
                                map[i2, z2] = 1;
                                i2++;
                            }
                            map[i2, z2] = 3;
                        }
                        if (r == 2 || r == 5 || r == 6 || r == 8 || r == 10 || r == 11 || r == 12 || r == 14)//↓
                        {
                            z1 = Random.Range(room_x[1, 0, 1], room_x[1, 1, 1] + 1);
                            i2 = room_y[1, 1, 1];
                            map[z1, i2] = 4;
                            i2++;
                            while (true)
                            {
                                if (i2 == Yline[1])
                                {
                                    break;
                                }
                                map[z1, i2] = 1;
                                i2++;
                            }
                            map[z1, i2] = 3;
                        }
                        if (r == 3 || r == 6 || r == 7 || r == 9 || r == 11 || r == 12 || r == 13 || r == 14)//←
                        {
                            z2 = Random.Range(room_y[1, 0, 1], room_y[1, 1, 1] + 1);
                            i2 = room_x[1, 0, 1];
                            map[i2, z2] = 4;
                            i2--;
                            while (true)
                            {
                                if (i2 == Xline[0])
                                {
                                    break;
                                }
                                map[i2, z2] = 1;
                                i2--;
                            }
                            map[i2, z2] = 3;
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
                            map[z1, i2] = 4;
                            i2--;
                            while (true)
                            {
                                if (i2 == Yline[0])
                                {
                                    break;
                                }
                                map[z1, i2] = 1;
                                i2--;
                            }
                            map[z1, i2] = 3;
                        }
                        if (r == 1 || r == 3 || r == 4 || r == 6)
                        {
                            z2 = Random.Range(room_y[1, 0, 2], room_y[1, 1, 2] + 1);
                            i2 = room_x[2, 0, 1];
                            map[i2, z2] = 4;
                            i2--;
                            while (true)
                            {
                                if (i2 == Xline[1])
                                {
                                    break;
                                }
                                map[i2, z2] = 1;
                                i2--;
                            }
                            map[i2, z2] = 3;
                        }
                        if (r == 2 || r == 4 || r == 5 || r == 6)
                        {
                            z1 = Random.Range(room_x[2, 0, 1], room_x[2, 1, 1] + 1);
                            i2 = room_y[1, 1, 2];
                            map[z1, i2] = 4;
                            i2++;
                            while (true)
                            {
                                if (i2 == Yline[1])
                                {
                                    break;
                                }
                                map[z1, i2] = 1;
                                i2++;
                            }
                            map[z1, i2] = 3;
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
                            map[i2, z2] = 4;
                            i2++;
                            while (true)
                            {
                                if (i2 == Xline[0])
                                {
                                    break;
                                }
                                map[i2, z2] = 1;
                                i2++;
                            }
                            map[i2, z2] = 3;
                        }
                        if (r == 0 || r == 2)
                        {
                            z1 = Random.Range(room_x[0, 0, 2], room_x[0, 1, 2] + 1);
                            i2 = room_y[2, 0, 0];
                            map[z1, i2] = 4;
                            i2--;
                            while (true)
                            {
                                if (i2 == Yline[1])
                                {
                                    break;
                                }
                                map[z1, i2] = 1;
                                i2--;
                            }
                            map[z1, i2] = 3;
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
                            map[i2, z2] = 4;
                            i2--;
                            while (true)
                            {
                                if (i2 == Xline[0])
                                {
                                    break;
                                }
                                map[i2, z2] = 1;
                                i2--;
                            }
                            map[i2, z2] = 3;
                        }
                        if (r == 1 || r == 3 || r == 4 || r == 6)
                        {
                            z1 = Random.Range(room_x[1, 0, 2], room_x[1, 1, 2] + 1);
                            i2 = room_y[2, 0, 1];
                            map[z1, i2] = 4;
                            i2--;
                            while (true)
                            {
                                if (i2 == Yline[1])
                                {
                                    break;
                                }
                                map[z1, i2] = 1;
                                i2--;
                            }
                            map[z1, i2] = 3;
                        }
                        if (r == 2 || r == 4 || r == 5 || r == 6)
                        {
                            z2 = Random.Range(room_y[2, 0, 1], room_y[2, 1, 1] + 1);
                            i2 = room_x[1, 1, 2];
                            map[i2, z2] = 4;
                            i2++;
                            while (true)
                            {
                                if (i2 == Xline[1])
                                {
                                    break;
                                }
                                map[i2, z2] = 1;    //変更
                                i2++;
                            }
                            map[i2, z2] = 3;       //変更
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
                            map[i2, z2] = 4;
                            i2--;
                            while (true)
                            {
                                if (i2 == Xline[1])
                                {
                                    break;
                                }
                                map[i2, z2] = 1;
                                i2--;
                            }
                            map[i2, z2] = 3;
                        }
                        if (r == 0 || r == 2)
                        {
                            z1 = Random.Range(room_x[2, 0, 2], room_x[2, 1, 2] + 1);
                            i2 = room_y[2, 0, 2];
                            map[z1, i2] = 4;
                            i2--;
                            while (true)
                            {
                                if (i2 == Yline[1])
                                {
                                    break;
                                }
                                map[z1, i2] = 1;
                                i2--;
                            }
                            map[z1, i2] = 3;
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
                map[Xline[i1], Yline[i2]] = 3;
            }
        }



        //部屋と通路をつなぐ

        i1 = 0;
        while (true)
        {
            if (map[Xline[0], 58 - i1] == 3 || i1 == 58)
            {
                break;
            }
            map[Xline[0], 58 - i1] = 0;
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[Xline[1], 58 - i1] == 3 || i1 == 58)
            {
                break;
            }
            map[Xline[1], 58 - i1] = 0;
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[Xline[0], i1] == 3 || i1 == 58)
            {
                break;
            }
            map[Xline[0], i1] = 0;
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[Xline[1], i1] == 3 || i1 == 58)
            {
                break;
            }
            map[Xline[1], i1] = 0;
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[i1, Yline[0]] == 3 || i1 == 58)
            {
                break;
            }
            map[i1, Yline[0]] = 0;
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[i1, Yline[1]] == 3 || i1 == 58)
            {
                break;
            }
            map[i1, Yline[1]] = 0;
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[58 - i1, Yline[0]] == 3 || i1 == 58)
            {
                break;
            }
            map[58 - i1, Yline[0]] = 0;
            i1++;
        }

        i1 = 0;
        while (true)
        {
            if (map[58 - i1, Yline[1]] == 3 || i1 == 58)
            {
                break;
            }
            map[58 - i1, Yline[1]] = 0;
            i1++;
        }

        
        


        for (int x = 0; x < 59; x++)
        {
            for (int y = 0; y < 59; y++)
            {
                if (map[x, y] == 0)
                {
                    Instantiate(wallObject, new Vector3(x, 0, y), Quaternion.identity);
                }
                /*壁以外色付け用
                if (map[x, y] == 1)
                {
                    Instantiate(wallObject1, new Vector3(x, 0, y), Quaternion.identity);
                }
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
        InstantiateInRoom(kaidan);

        //敵をランダムに配置
        enemynumber = Random.Range(5, 9);
        for (int i = 0; i < enemynumber; i++)
        {
            int a = Random.Range(0, 10);
            if(0 <= a && a < 4) {
                InstantiateInRoom(Enemy);
            }
            else if(4 <= a && a < 7)
            {
                InstantiateInRoom(Enemy2);
            }
            else if(7 <= a && a < 9)
            {
                InstantiateInRoom(Enemy2);
            }
            else
            {
                InstantiateInRoom(Enemy3);
            }
        }
        
    }

    private void InstantiateInRoom(GameObject obj)
    {
        do
        {
            i1 = Random.Range(1, 58);
            i2 = Random.Range(1, 58);
        } while (map[i1, i2] != 2);

        map[i1, i2] = 5;
        Instantiate(obj, new Vector3(i1, 0, i2), Quaternion.identity);
    }



    }

