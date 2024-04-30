using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Genereaza random valoarea fructului, implicit ce fruct va aparea pe pozitia respectiva
public class FruitValue : MonoBehaviour
{
    public enum FruitName { cherry, lemon, orange, pulm, grapes, watermelon, seven, scatter }
    public int frValue = -25;
    void Start()
    {
        frValue = GetValue();
    }

    public int GetValue()
    {
        int randomValue = Random.Range(1, 101);
        int fruitValue = -25;

        if (randomValue >= 0 && randomValue <= 20)
        {
            fruitValue = (int)FruitName.cherry;
        }
        if (randomValue > 20 && randomValue <= 40)
        {
            fruitValue = (int)FruitName.lemon;
        }
        if (randomValue > 40 && randomValue <= 60)
        {
            fruitValue = (int)FruitName.orange;
        }
        if (randomValue > 60 && randomValue <= 80)
        {
            fruitValue = (int)FruitName.lemon;
        }
        if (randomValue > 80 && randomValue <= 87)
        {
            fruitValue = (int)FruitName.grapes;
        }
        if (randomValue > 87 && randomValue <= 95)
        {
            fruitValue = (int)FruitName.watermelon;
        }
        if (randomValue > 95 && randomValue <= 98)
        {
            fruitValue = (int)FruitName.seven;
        }
        if (randomValue > 98 && randomValue <= 102)
        {
            fruitValue = (int)FruitName.scatter;
        }

        return fruitValue;
    }
}
