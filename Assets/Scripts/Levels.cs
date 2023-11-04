using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Levels : MonoBehaviour
{
    public int currentLevel;

    [SerializeField] GameObject Comb;
    [SerializeField] GameObject Scissors;
    [SerializeField] GameObject Brush;
    [SerializeField] GameObject HairClipper;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject LevelCompleteMenu;
    [SerializeField] GameObject HairSaloon;

    [SerializeField] GameObject[] LevelLockedImages;
    [SerializeField] GameObject[] LevelCompletedTexts;

    [SerializeField] Button[] LevelButtons;

    [SerializeField] AudioSource GameOverSound;
    [SerializeField] AudioSource LevelCompleteSound;
    [SerializeField] AudioSource MainMenuSong;

    public int levelNumber;

    bool isHairPainted;

    void Start(){
       if(PlayerPrefs.GetInt("LevelNumber") == 0){
            PlayerPrefs.SetInt("LevelNumber",1);
        }
        levelNumber = PlayerPrefs.GetInt("LevelNumber");
    }

    void Update(){
      
            for(int i=0;i<levelNumber;i++){
            LevelLockedImages[i].SetActive(false);
            LevelCompletedTexts[i].SetActive(true);
            LevelButtons[i].enabled = true;
            LevelButtons[i].GetComponent<Animator>().enabled = true;
        }
        
        
    }

 

    public void ChooseLevel(){
        
        string CurrentLevelButtonName = EventSystem.current.currentSelectedGameObject.name;
        currentLevel = Int32.Parse(LastNLetters(CurrentLevelButtonName,2));
        SelectLevel(currentLevel);
    }

     public void SelectLevel(int currentLevel){
        LevelCompleteSound.Stop();
        GameOverSound.Stop();
        switch(currentLevel) 
        {
            case 1:
            GetComponent<Menu>().PlayTutorial();
            break;

            case 2:
            GetComponent<Menu>().ShowIndications(2, "Nick", 1, 50, 20, 4); 
            break;

            case 3:
            GetComponent<Menu>().ShowIndications(3, "Gai", 1, 70, 21, 4); 
            break;

            case 4:    
            GetComponent<Menu>().ShowIndications(4, "Joe", 2, 90, 22, 4);    
            break;

            case 5:
            GetComponent<Menu>().ShowIndications(5, "Nick", 2, 110, 23, 4); 
            break;

            case 6:
            GetComponent<Menu>().ShowIndications(6, "Gai", 3, 130, 24, 4); 
            break;

            case 7:
            GetComponent<Menu>().ShowIndications(7, "Joe", 3, 150, 25, 4); 
            break;

            case 8:
            GetComponent<Menu>().ShowIndications(8, "Nick", 4, 170, 26, 4); 
            break;

            case 9:
            GetComponent<Menu>().ShowIndications(9, "Gai", 4, 190, 27, 4);
            break;

            case 10:
            GetComponent<Menu>().ShowIndications(10, "Joe", 5, 210, 28, 4);
            break;

            case 11:
            GetComponent<Menu>().ShowIndications(11, "Nick", 5, 230, 29, 4);
            break;

            case 12:
            GetComponent<Menu>().ShowIndications(12, "Gai", 6, 250, 30, 4);                 
            break;

            case 13:
            GetComponent<Menu>().ShowIndications(13, "Joe", 6, 270, 31, 4);
            break;

            case 14:
            GetComponent<Menu>().ShowIndications(14, "Nick", 7, 290, 32, 4);
            break;

            case 15:
            GetComponent<Menu>().ShowIndications(15, "Gai", 7, 310, 33, 4); 
            break;

           
        }

    }

     public void RestartLevel(){
        GetComponent<Menu>().isLevelRestarted = true;
        SelectLevel(currentLevel);
        PauseMenu.SetActive(false);
        if(MainMenuSong.isPlaying){
            StartCoroutine(GetComponent<AudioFade>().FadeOut(MainMenuSong,2f));
            Debug.Log("pause menu");
        }
        
        GameOverMenu.SetActive(false);
        LevelCompleteMenu.SetActive(false);
        HairSaloon.SetActive(false);
        
        
     
    }

   public void NextLevel(){
       
      HairSaloon.SetActive(false);
      LevelCompleteMenu.SetActive(false);
       if(currentLevel<15){
           currentLevel++;
       }
       SelectLevel(currentLevel);
    }

     public void PreviousLevel(){
         LevelCompleteMenu.SetActive(false);
         HairSaloon.SetActive(false);
         if(currentLevel>2){
           currentLevel--;
           SelectLevel(currentLevel);
        }else{
            GetComponent<Menu>().PlayTutorial();
        }
       
    }

    string LastNLetters(string str, int n){
      return str.Substring(str.Length-n);

    }

}
