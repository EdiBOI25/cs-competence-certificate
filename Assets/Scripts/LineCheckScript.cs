using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LineCheckScript : MonoBehaviour
{
    public HitboxBehaviour[] hb;
    private int[,] hbm = new int [6,4]; //Hitbox Value 2D Array
    private int[,] fvm = new int [6,4]; //Fruit Value 2D Array

    [SerializeField] private Text balanceText;
    [SerializeField] private Text winText;
    public float balance = 5000;
    public float bet = 100;
    public float win = 0;

    [SerializeField] private SpriteRenderer[] linesSpriteArr;

    [SerializeField] private Animator[] fireAnimArr;
    [SerializeField] private SpriteRenderer[] fireSpriteArr;

    private int[] scatterArray = new int[6];

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }

    //actualizare balanta de pe ecran
    public void UpdateBalance()
    {
        balance -= bet;
        balanceText.text = balance + " RON";
    }

    public void TestEvent()
    {
        GetValues();
        LineCheck();
        winText.text = win + " RON";
        //balanceText.text = balance + " RON";
    }

    //logica sumei castigate
    private void Win(int winType, int HbmValue)
    {
        if (winType == 3) //linia merge pana la col 3
        {
            if (HbmValue == 1) //tier1
            {
                //balance += bet * 4;
                win += bet * 4;
            }
            else if (HbmValue == 2) //tier2
            {
                win += bet * 10;
                //balance += bet * 10;
            }
            else if (HbmValue == 3) //septari
            {
                //balance += bet * 20;
                win += bet * 20;
            }
        }

        if (winType == 4) //linia merge pana la col 4
        {
            if (HbmValue == 1) //tier1
            {
                //balance += bet * 10;
                win += bet * 10;
            }
            else if (HbmValue == 2) //tier2
            {
                //balance += bet * 40;
                win += bet * 40;
            }
            else if (HbmValue == 3) //septari
            {
                //balance += bet * 200;
                win += bet * 200;
            }
        }

        if (winType == 5) //linia merge pana la col 5
        {
            if (HbmValue == 1) //tier1
            {
                //balance += bet * 40;
                win += bet * 40;
            }
            else if (HbmValue == 2) //tier2
            {
                //balance += bet * 100;
                win += bet * 100;
            }
            else if (HbmValue == 3) //septari
            {
                //balance += bet * 1000;
                win += bet * 1000;
            }
        }
    }

    //verificare linii
    private void LineCheck()
    {
        if (fvm[1, 1] == fvm[2, 1] && fvm[1, 1] == fvm[3,1]) //linia 1
        {
            linesSpriteArr[0].enabled = true;
            ActivateFire(fireSpriteArr[0], fireAnimArr[0]);
            ActivateFire(fireSpriteArr[3], fireAnimArr[3]);
            ActivateFire(fireSpriteArr[6], fireAnimArr[6]);

            if (fvm[1,1] == fvm[4,1])
            {
                ActivateFire(fireSpriteArr[9], fireAnimArr[9]);
                if (fvm[1,1] == fvm[5,1])
                {
                    ActivateFire(fireSpriteArr[12], fireAnimArr[12]);
                    //creste balanta in functie de hbm[1,1] 5
                    Win(5, hbm[1, 1]);
                }
                else
                {
                    //creste balanta in functie de hbm[1,1] 4
                    Win(4, hbm[1, 1]);
                }
            }
            else
            {
                //creste balanta in functie de hbm[1,1] 3
                Win(3, hbm[1, 1]);
            }
        }

        if (fvm[1, 2] == fvm[2, 2] && fvm[1, 2] == fvm[3, 2]) // linia 2
        {
            linesSpriteArr[1].enabled = true;
            ActivateFire(fireSpriteArr[1], fireAnimArr[1]);
            ActivateFire(fireSpriteArr[4], fireAnimArr[4]);
            ActivateFire(fireSpriteArr[7], fireAnimArr[7]);
            if (fvm[1, 2] == fvm[4, 2])
            {
                ActivateFire(fireSpriteArr[10], fireAnimArr[10]);
                if (fvm[1, 2] == fvm[5, 2])
                {
                    ActivateFire(fireSpriteArr[13], fireAnimArr[13]);
                    //creste balanta in functie de hbm[1,2] 5
                    Win(5, hbm[1, 2]);
                }
                else
                {
                    //creste balanta in functie de hbm[1,2] 4
                    Win(4, hbm[1, 2]);
                }
            }
            else
            {
                //creste balanta in functie de hbm[1,2] 3
                Win(3, hbm[1, 2]);
            }
        }

        if (fvm[1, 3] == fvm[2, 3] && fvm[1, 3] == fvm[3, 3]) // linia 3
        {
            linesSpriteArr[2].enabled = true;
            ActivateFire(fireSpriteArr[2], fireAnimArr[2]);
            ActivateFire(fireSpriteArr[5], fireAnimArr[5]);
            ActivateFire(fireSpriteArr[8], fireAnimArr[8]);
            if (fvm[1, 3] == fvm[4, 3])
            {
                ActivateFire(fireSpriteArr[11], fireAnimArr[11]);
                if (fvm[1, 3] == fvm[5, 3])
                {
                    ActivateFire(fireSpriteArr[14], fireAnimArr[14]);
                    //creste balanta in functie de hbm[1,2] 5
                    Win(5, hbm[1, 3]);
                }
                else
                {
                    //creste balanta in functie de hbm[1,2] 4
                    Win(4, hbm[1, 3]);
                }
            }
            else
            {
                //creste balanta in functie de hbm[1,2] 3
                Win(3, hbm[1, 3]);
            }
        }

        if (fvm[1, 1] == fvm[2, 2] && fvm[1, 1] == fvm[3, 3]) // linia 4
        {
            linesSpriteArr[3].enabled = true;
            ActivateFire(fireSpriteArr[0], fireAnimArr[0]);
            ActivateFire(fireSpriteArr[4], fireAnimArr[4]);
            ActivateFire(fireSpriteArr[8], fireAnimArr[8]);
            if (fvm[1, 1] == fvm[4, 2])
            {
                ActivateFire(fireSpriteArr[10], fireAnimArr[10]);
                if (fvm[1, 1] == fvm[5, 1])
                {
                    ActivateFire(fireSpriteArr[12], fireAnimArr[12]);
                    //creste balanta in functie de hbm[1,2] 5
                    Win(5, hbm[1, 1]);
                }
                else
                {
                    //creste balanta in functie de hbm[1,2] 4
                    Win(4, hbm[1, 1]);
                }
            }
            else
            {
                //creste balanta in functie de hbm[1,2] 3
                Win(3, hbm[1, 1]);
            }
        }

        if (fvm[1, 3] == fvm[2, 2] && fvm[1, 3] == fvm[3, 1]) // linia 5
        {
            linesSpriteArr[4].enabled = true;
            ActivateFire(fireSpriteArr[2], fireAnimArr[2]);
            ActivateFire(fireSpriteArr[4], fireAnimArr[4]);
            ActivateFire(fireSpriteArr[6], fireAnimArr[6]);
            if (fvm[1, 3] == fvm[4, 2])
            {
                ActivateFire(fireSpriteArr[10], fireAnimArr[10]);
                if (fvm[1, 3] == fvm[5, 3])
                {
                    ActivateFire(fireSpriteArr[14], fireAnimArr[14]);
                    //creste balanta in functie de hbm[1,2] 5
                    Win(5, hbm[1, 3]);
                }
                else
                {
                    //creste balanta in functie de hbm[1,2] 4
                    Win(4, hbm[1, 3]);
                }
            }
            else
            {
                //creste balanta in functie de hbm[1,2] 3
                Win(3, hbm[1, 3]);
            }
        }

        //Debug.Log(fvm[1, 1] + " " + fvm[2, 1] + " " + fvm[3, 1] + " " + fvm[4, 1] + " " + fvm[5, 1]);
        //Debug.Log(fvm[1, 2] + " " + fvm[2, 2] + " " + fvm[3, 2] + " " + fvm[4, 2] + " " + fvm[5, 2]);
        //Debug.Log(fvm[1, 3] + " " + fvm[2, 3] + " " + fvm[3, 3] + " " + fvm[4, 3] + " " + fvm[5, 3]);
    }

    //preia valorile fiecarui hitbox
    private void GetValues()
    {
        win = 0;
        int k = 0;
        int scatterNumber = 0;
        for (int i = 1; i <= 5; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                hbm[i, j] = hb[k].hitboxValue;
                fvm[i, j] = hb[k].finalFruitValue;
                k++;
                //verificare scatter
                if (fvm[i,j] == 7)
                {   //pozitia focului ce trebuie activat
                    if (i == 1 && j == 1) scatterArray[scatterNumber] = 0;
                    if (i == 1 && j == 2) scatterArray[scatterNumber] = 1;
                    if (i == 1 && j == 3) scatterArray[scatterNumber] = 2;
                    if (i == 2 && j == 1) scatterArray[scatterNumber] = 3;
                    if (i == 2 && j == 2) scatterArray[scatterNumber] = 4;
                    if (i == 2 && j == 3) scatterArray[scatterNumber] = 5;
                    if (i == 3 && j == 1) scatterArray[scatterNumber] = 6;
                    if (i == 3 && j == 2) scatterArray[scatterNumber] = 7;
                    if (i == 3 && j == 3) scatterArray[scatterNumber] = 8;
                    if (i == 4 && j == 1) scatterArray[scatterNumber] = 9;
                    if (i == 4 && j == 2) scatterArray[scatterNumber] = 10;
                    if (i == 4 && j == 3) scatterArray[scatterNumber] = 11;
                    if (i == 5 && j == 1) scatterArray[scatterNumber] = 12;
                    if (i == 5 && j == 2) scatterArray[scatterNumber] = 13;
                    if (i == 5 && j == 3) scatterArray[scatterNumber] = 14;
                    scatterNumber++;
                }
            }
        }
        
        if (scatterNumber == 3)
        {
            ActivateFire(fireSpriteArr[scatterArray[0]], fireAnimArr[scatterArray[0]]);
            ActivateFire(fireSpriteArr[scatterArray[1]], fireAnimArr[scatterArray[1]]);
            ActivateFire(fireSpriteArr[scatterArray[2]], fireAnimArr[scatterArray[2]]);
            //balance += bet * 2;
            win += bet * 2;
            //for (1 -> 3) animatie pe fiecare scatterArray[x,y]
        }
        else if (scatterNumber == 4)
        {
            ActivateFire(fireSpriteArr[scatterArray[0]], fireAnimArr[scatterArray[0]]);
            ActivateFire(fireSpriteArr[scatterArray[1]], fireAnimArr[scatterArray[1]]);
            ActivateFire(fireSpriteArr[scatterArray[2]], fireAnimArr[scatterArray[2]]);
            ActivateFire(fireSpriteArr[scatterArray[3]], fireAnimArr[scatterArray[3]]);
            //balance += bet * 10;
            win += bet * 10;
        }
        else if (scatterNumber == 5)
        {
            ActivateFire(fireSpriteArr[scatterArray[0]], fireAnimArr[scatterArray[0]]);
            ActivateFire(fireSpriteArr[scatterArray[1]], fireAnimArr[scatterArray[1]]);
            ActivateFire(fireSpriteArr[scatterArray[2]], fireAnimArr[scatterArray[2]]);
            ActivateFire(fireSpriteArr[scatterArray[3]], fireAnimArr[scatterArray[3]]);
            ActivateFire(fireSpriteArr[scatterArray[4]], fireAnimArr[scatterArray[4]]);
            //balance += bet * 50;
            win += bet * 50;
        }

        
    }

    //declanseaza animatia de foc
    private void ActivateFire(SpriteRenderer sr, Animator anim)
    {
        sr.enabled = true;
        anim.enabled = true;
    }
}
