using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetButtonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject eventManagerObj;
    [SerializeField] private int setBet = 5;
    [SerializeField] private GameObject fruitBehaviourObj;
    private FruitBehaviourNEW fruitBehaviourComponent;
    private LineCheckScript eventManagerLineComponent;

    [SerializeField] private BetButtonBehaviour[] otherButtonArr; //array

    private Image betButtonImage;
    private Button betButtonBtn;
    public Sprite greenSprite, graySprite;
    public int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        betButtonImage = GetComponent<Image>();
        betButtonBtn = GetComponent<Button>();
        betButtonBtn.onClick.AddListener(TaskOnClick);
        fruitBehaviourComponent = fruitBehaviourObj.GetComponent<FruitBehaviourNEW>();
        eventManagerLineComponent = eventManagerObj.GetComponent<LineCheckScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // jucatorul nu poate interactiona cu butoanele daca invartirea este in desfasurare
        if (fruitBehaviourComponent.GlobalSpin == 0)
        {
            betButtonBtn.interactable = true;
        }
        else
        {
            betButtonBtn.interactable = false;
        }
        if (state == 0)
        {
            betButtonImage.sprite = graySprite;
        }
    }

    // actualizeaza butonul
    void TaskOnClick()
    {
        if (state == 0)
        {
            betButtonImage.sprite = greenSprite;
            state = 1;
            eventManagerLineComponent.bet = setBet;

            otherButtonArr[0].state = 0;
            otherButtonArr[1].state = 0;
            otherButtonArr[2].state = 0;
            otherButtonArr[3].state = 0;
        }
    }

    
}
