using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GambleBehaviour : MonoBehaviour
{
    [SerializeField] private Button spinButton, incasareButton, gambleButton, heartButton, spadesButton;
    private Image spinButtonImage, incasareButtonImage, gambleButtonImage, heartButtonImage, spadesButtonImage;
    [SerializeField] private Image gambleBackgroundImage;

    [SerializeField] private FruitBehaviourNEW[] myFruitArr;
    private LineCheckScript lineCheckScript;

    [SerializeField] private Text balanceText;
    [SerializeField] private Text winText;
    [SerializeField] private Text gambleAmountText;
    [SerializeField] private Text gambleToWinText;

    [SerializeField] private GameObject mainCardObj;
    private Animator mainCardAnim;
    private SpriteRenderer mainCardSprite;

    private UnityEvent dublajeEventTrig;
    private int cardSelect;
    private int randomCardValue;
    private int x;
    [SerializeField] Sprite heartCardSprite;
    [SerializeField] Sprite spearsCardSprite;
    [SerializeField] private SpriteRenderer gambleWinText;
    [SerializeField] private SpriteRenderer[] historyCardArr;


    // Start is called before the first frame update
    void Start()
    {
        spinButtonImage = spinButton.GetComponent<Image>();
        incasareButtonImage = incasareButton.GetComponent<Image>();
        gambleButtonImage = gambleButton.GetComponent<Image>();
        heartButtonImage = heartButton.GetComponent<Image>();
        spadesButtonImage = spadesButton.GetComponent<Image>();
        lineCheckScript = GetComponent<LineCheckScript>();

        incasareButton.onClick.AddListener(incasareClickEvent);
        gambleButton.onClick.AddListener(gambleClickEvent);
        heartButton.onClick.AddListener(heartClickEvent);
        spadesButton.onClick.AddListener(spadesClickEvent);


        if (dublajeEventTrig == null)
            dublajeEventTrig = new UnityEvent();
        dublajeEventTrig.AddListener(DublajeEvent);

        mainCardAnim = mainCardObj.GetComponent<Animator>();
        mainCardSprite = mainCardObj.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // incasarea castigului dupa un dublaj
        if (Input.GetKey("space") && myFruitArr[13].GlobalSpin == 2)
            incasareClickEvent();
    }

    public void GambleEvent()
    {
        //daca valoarea castigata e mai mare decat 0, jucatorul poate incasa castigul sau opta spre dublarea sumei
        if (lineCheckScript.win > 0)
        {
            disableButton(spinButton, spinButtonImage);
            enableButton(incasareButton, incasareButtonImage);
            enableButton(gambleButton, gambleButtonImage);
        }
        else //altfel se opreste evenimentul de spin si se reia jocul
        {
            for (int i = 0; i <= 19; i++)
            {
                myFruitArr[i].GlobalSpin = 0;
            }
        }
        
    }

    private void disableButton(Button btn, Image btnImage)
    {
        btn.interactable = false;
        btnImage.enabled = false;
    }   
    
    private void enableButton(Button btn, Image btnImage)
    {
        btn.interactable = true;
        btnImage.enabled = true;
    }

    // logica din spatele butonului de incasare
    private void incasareClickEvent()
    {
        lineCheckScript.balance += lineCheckScript.win;
        balanceText.text = lineCheckScript.balance + " RON";

        for (int i = 0; i <= 19; i++)
        {
            myFruitArr[i].GlobalSpin = 0;
        }

        for (int i = 0; i <= 4; i++)
        {
            historyCardArr[i].enabled = false;
        }
        
        gambleBackgroundImage.enabled = false;
        gambleAmountText.enabled = false;
        gambleToWinText.enabled = false;
        enableButton(spinButton, spinButtonImage);
        disableButton(incasareButton, incasareButtonImage);
        disableButton(gambleButton, gambleButtonImage);
        disableButton(heartButton, heartButtonImage);
        disableButton(spadesButton, spadesButtonImage);

        mainCardSprite.enabled = false;
        mainCardAnim.enabled = false;

    }

    // logica din spatele butonului de dublare
    private void gambleClickEvent()
    {
        disableButton(gambleButton, gambleButtonImage);
        enableButton(heartButton, heartButtonImage);
        enableButton(spadesButton, spadesButtonImage);
        gambleBackgroundImage.enabled = true;
        gambleAmountText.enabled = true;
        gambleToWinText.enabled = true;
        gambleAmountText.text = lineCheckScript.win + "RON";
        gambleToWinText.text = (lineCheckScript.win * 2) + "RON";

        for (int i = 0; i <= 4; i++)
        {
            historyCardArr[i].enabled = true;
        }

        // se declanseaza evenimentul de dublare
        dublajeEventTrig.Invoke();

    }

    // 
    public void DublajeEvent()
    {
        mainCardSprite.enabled = true;
        mainCardAnim.enabled = true;

    }

    //cand se alege inima rosie/neagra, cartea din centru primeste o valoare aleatorie
    //in functie de alegerea jucatorului, acesta poate sa isi dubleze suma sau sa o piarda
    private void heartClickEvent()
    {
        randomCardValue = Random.Range(0, 2);
        cardSelect = 1;
        setCardSprite(randomCardValue);

        if (cardSelect == randomCardValue)
        {
            lineCheckScript.win *= 2;
            winText.text = lineCheckScript.win + " RON";
            StartCoroutine(wait1SecondDublaj());
        }
        else
        {
            lineCheckScript.win = 0;
            winText.text = lineCheckScript.win + " RON";
            StartCoroutine(wait1SecondIncasare());
        }
    }

    private void spadesClickEvent()
    {
        randomCardValue = Random.Range(0, 2);
        cardSelect = 0;
        setCardSprite(randomCardValue);

        if (cardSelect == randomCardValue)
        {
            lineCheckScript.win *= 2;
            winText.text = lineCheckScript.win + " RON";
            StartCoroutine(wait1SecondDublaj());
            
        }
        else
        {
            lineCheckScript.win = 0;
            winText.text = lineCheckScript.win + " RON";
            StartCoroutine(wait1SecondIncasare());
        }
    }

    //se ocupa de logica afisarii cartilor (cea principala si cele din istoric)
    private void setCardSprite(int value)
    {
        for (int i = 4; i >=1; i--)
        {
            historyCardArr[i].sprite = historyCardArr[i - 1].sprite;
        }

        mainCardAnim.enabled = false;
        if (value == 1)
        {
            mainCardSprite.sprite = heartCardSprite;
            historyCardArr[0].sprite = heartCardSprite;
        }
        else
        {
            mainCardSprite.sprite = spearsCardSprite;
            historyCardArr[0].sprite = spearsCardSprite;
        }
    }

    //asteapta o secunda intre evenimente
    IEnumerator wait1SecondDublaj()
    {
        heartButton.interactable = false;
        spadesButton.interactable = false;
        gambleWinText.enabled = true;
        yield return new WaitForSeconds(1);
        gambleWinText.enabled = false;
        gambleAmountText.text = lineCheckScript.win + "RON";
        gambleToWinText.text = (lineCheckScript.win * 2) + "RON";
        heartButton.interactable = true;
        spadesButton.interactable = true;
        DublajeEvent();
    }

    IEnumerator wait1SecondIncasare()
    {
        yield return new WaitForSeconds(1);
        incasareClickEvent();
    }
}
