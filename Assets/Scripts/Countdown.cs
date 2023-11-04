using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float timeRemaining;
    public float timeRemainingIndications;
    public float timeRemainingIndicationsTutorial;

    public bool timerIsRunning = false;
    public bool timerIsRunningIndications = false;
    public bool timerIsRunningIndicationsTutorial = false;
    bool isHairPainted;

    [SerializeField] Text countDownText;
    [SerializeField] Text unhappyText;

    [SerializeField] Image countDownTextIndications;

    [SerializeField] GameObject PauseImage;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject Mouth;

    [SerializeField] Image Client;

    [SerializeField] Sprite NickMouthSad;
    [SerializeField] Sprite JoeMouthSad;

    [SerializeField] Sprite[] IndicationsCountDownNumbers;

    [SerializeField] AudioSource InGameSong;
    [SerializeField] AudioSource GameOverSound;

    void Update()
    {
        if(GetComponent<Menu>().isPlaying == true){
        if (timerIsRunning)
        {
            DisplayTime(timeRemaining);
            if (timeRemaining > 0 )
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                GameOver();
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        }

        if (timerIsRunningIndications)
        {
            DisplayTimeIndications(timeRemainingIndications);
            if (timeRemainingIndications > 0 )
            {
                timeRemainingIndications -= Time.deltaTime;
            }
            else
            {
                GetComponent<Menu>().StartGame();
                timeRemainingIndications = 0;
                timerIsRunningIndications = false;
            }
        }

        if (timerIsRunningIndicationsTutorial)
        {
            
            DisplayTimeIndications(timeRemainingIndicationsTutorial);
            if (timeRemainingIndicationsTutorial > 0 )
            {
               
                
                timeRemainingIndicationsTutorial -= Time.deltaTime;
                
            }
            else
            {
                GetComponent<Menu>().StartGameTutorial();
                timeRemainingIndicationsTutorial = 0;
                timerIsRunningIndicationsTutorial = false;
            }
        }

        isHairPainted = GetComponent<Menu>().isHairPainted;
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        if(seconds<10){
        countDownText.text = "0"+seconds.ToString();
        }else{
            countDownText.text = seconds.ToString();
        }
    }

     void DisplayTimeIndications(float timeToDisplay)
    { 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        countDownTextIndications.sprite = IndicationsCountDownNumbers[(int)seconds+1];
         
    }

   

    void GameOver(){
        GetComponent<Menu>().isPlayingLevel = false;
        unhappyText.text = GetComponent<Menu>().currentClientName + " is sad!";
        InGameSong.Pause();
        GameOverSound.Play();
        PauseImage.SetActive(true);
        GameOverMenu.SetActive(true);
        GetComponent<Menu>().isPlaying = false;
        for(int i=0;i<GetComponent<Menu>().Tools.Length;i++){
            GetComponent<Menu>().Tools[i].GetComponent<Animator>().enabled = false;
        }
       
        switch (GetComponent<Menu>().ClientName) {
            case "Nick":
            Mouth.GetComponent<Image>().sprite = NickMouthSad;
            Mouth.GetComponent<RectTransform>().sizeDelta = new Vector2(105,30);
            break;

            case "Joe":
            Mouth.GetComponent<Image>().sprite = JoeMouthSad;
            Mouth.GetComponent<RectTransform>().sizeDelta = new Vector2(105,30);
            break;

        }
    }

  

    string firstNLetters(string str, int maxLength){
        return str.Substring(0, Math.Min(str.Length, maxLength));
    }

}
