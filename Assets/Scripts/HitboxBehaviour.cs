using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxBehaviour : MonoBehaviour
{
    public int hitboxValue; //price value
    public int finalFruitValue; //cherry = 0, lemon = 1 etc
    private int fbn; //spriteValue from script
    [SerializeField] private GameObject myFruit;

    public void SetHitboxValues()
    {
        fbn = myFruit.GetComponent<FruitBehaviourNEW>().spriteValue;
        finalFruitValue = fbn;
        if (fbn >=0 && fbn < 4)
        {
            hitboxValue = 1;
        }
        if (fbn == 4 || fbn == 5)
        {
            hitboxValue = 2;
        }
        if (fbn == 6)
        {
            hitboxValue = 3;
        }
        if (fbn >= 7)
        {
            hitboxValue = 4;
        }
        
    }
}
