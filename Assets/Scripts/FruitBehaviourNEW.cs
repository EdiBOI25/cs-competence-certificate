using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
//535
//295
//55
//-185
public class FruitBehaviourNEW : MonoBehaviour
{
    [SerializeField] private UnityEvent LineCheckTrigger;
    [SerializeField] private UnityEvent HitboxTrigger;
    [SerializeField] private UnityEvent UpdateSoldTrigger;
    [SerializeField] private UnityEvent GambleTrigger;

    [SerializeField] private SpriteRenderer[] linesSpriteArr = new SpriteRenderer[5];
    [SerializeField] private SpriteRenderer[] fireSpriteArr;
    [SerializeField] private Animator[] fireAnimArr;

    public float timeToSpin = 1f;
    private float time;
    private float GlobalTime = 2.8f;

    private float spinSpeed = 98.36049f * 9.6f;
    //private float check20frames = 1f/3f;
    [SerializeField] float spinsPerSec = 1; //1 spin/s

    private bool isSpinning = false;
    public int GlobalSpin = 0;
    private Vector3 startPosition;

    public Button yourButton;

    public enum FruitName { cherry, lemon, orange, plum, grapes, watermelon, seven, scatter }
    public int spriteValue;

    private SpriteRenderer sprRend;
    public Sprite[] spriteArray;

    [SerializeField] public LineCheckScript lineCheckComponent;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        sprRend = GetComponent<SpriteRenderer>();
    }

    

    // Update is called once per frame
    void Update()
    {
        //initializare
        //rotire prin tasta spatiu
        if (Input.GetKey("space") && isSpinning == false && GlobalSpin == 0 && lineCheckComponent.balance >= lineCheckComponent.bet) //space pt a incepe spin-ul
        {   
            isSpinning = true;
            GlobalSpin = 1;
            time = timeToSpin;
            UpdateSoldTrigger.Invoke();

            for (int i = 0; i <= 4; i++)
            {
                linesSpriteArr[i].enabled = false;
            }

            for (int i = 0; i <= 14; i++)
            {
                fireAnimArr[i].enabled = false;
                fireSpriteArr[i].enabled = false;
            }
        }

        StartSpin();

        if (GlobalSpin == 1) //globalspin logic (reprezinta starea globala a jocului)
        {
            GlobalTime -= 1f / 60f;
        }
        if (GlobalTime <= 0f)
        {
            GlobalSpin = 2; //gamble time
            GambleTrigger.Invoke();
            GlobalTime = 2.8f;
        }

        GetspriteValue();
    }

    void TaskOnClick() //spin la UI Button
    {
        if (isSpinning == false && GlobalSpin == 0 && lineCheckComponent.balance >= lineCheckComponent.bet)
        {
            isSpinning = true;
            GlobalSpin = 1;
            time = timeToSpin;
            UpdateSoldTrigger.Invoke();

            for (int i = 0; i <= 4; i++)
            {
                linesSpriteArr[i].enabled = false;
            }

            for (int i = 0; i <= 14; i++)
            {
                fireAnimArr[i].enabled = false;
                fireSpriteArr[i].enabled = false;
            }
        }

    }

    void GetspriteValue() //in functie de valoarea random, simbolul ia un sprite
    {   //fiecare if reprezinta o probabilitate
        if (isSpinning == true && transform.position.y >= 300f)
        {
            int randomValue = Random.Range(1, 101);
            if (randomValue >= 0 && randomValue <= 16)
            {
                spriteValue = (int)FruitName.cherry;
            }
            if (randomValue > 16 && randomValue <= 32)
            {
                spriteValue = (int)FruitName.lemon;
            }
            if (randomValue > 32 && randomValue <= 48)
            {
                spriteValue = (int)FruitName.orange;
            }
            if (randomValue > 48 && randomValue <= 64)
            {
                spriteValue = (int)FruitName.plum;
            }
            if (randomValue > 64 && randomValue <= 76)
            {
                spriteValue = (int)FruitName.grapes;
            }
            if (randomValue > 76 && randomValue <= 88)
            {
                spriteValue = (int)FruitName.watermelon;
            }
            if (randomValue > 88 && randomValue <= 94)
            {
                spriteValue = (int)FruitName.seven;
            }
            if (randomValue > 94 && randomValue <= 102)
            {
                spriteValue = (int)FruitName.scatter;
            }
            SpriteChanger(spriteValue);
        }
    }

    void SpriteChanger(int spriteValue) //schimbare sprite in functie de valoarea din GetSpriteValue
    {
        if (spriteValue == 0)
        {
            sprRend.sprite = spriteArray[0];
        }
        if (spriteValue == 1)
        {
            sprRend.sprite = spriteArray[1];
        }
        if (spriteValue == 2)
        {
            sprRend.sprite = spriteArray[2];
        }
        if (spriteValue == 3)
        {
            sprRend.sprite = spriteArray[3];
        }
        if (spriteValue == 4)
        {
            sprRend.sprite = spriteArray[4];
        }
        if (spriteValue == 5)
        {
            sprRend.sprite = spriteArray[5];
        }
        if (spriteValue == 6)
        {
            sprRend.sprite = spriteArray[6];
        }
        if (spriteValue == 7)
        {
            sprRend.sprite = spriteArray[7];
        }
    }

    

    void StartSpin() //rotirea insasi a fructelor
    {
        if (isSpinning == true)
        {
            if (transform.position.y <= -425f)
            {
                transform.position += new Vector3(0, 960f, 0); //cand ajunge sub tablou trece sus
            }

            if (time <= 0f) //cand se "termina" spin-ul, simbolul cade pana in pozitia care trebuie
            {
                RepairPosition();
            }
            else
            {
                time -= 1f / 60f; //scade timpul si cad fructele (animatie)
                transform.position += new Vector3(0, -spinSpeed * 1f / 60f * spinsPerSec, 0);
            }
        }
    }

    void RepairPosition() //fructele cad pana unde trebuie
    {
        if (isSpinning == true)
        {
            transform.position += new Vector3(0, -spinSpeed * 1f / 60f * spinsPerSec, 0);
            HitboxTrigger.Invoke();
        }
        if (transform.position.y <= startPosition.y) //cand ajunge sub unde trebuie, se muta pe pozitia pe care trebuie
        {
            transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
            isSpinning = false;
            
            LineCheckTrigger.Invoke();
        }
    }
}
