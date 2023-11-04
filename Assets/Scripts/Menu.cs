using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Menu : MonoBehaviour
{
   
    
    public GameObject currentTool;
    [SerializeField] GameObject MuteButton;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject HairSaloon;
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject PauseImage;
    [SerializeField] GameObject LevelsMenu;
    [SerializeField] GameObject LevelCompleteMenu;
    [SerializeField] GameObject Fill;
    [SerializeField] GameObject Client;
    [SerializeField] GameObject ClientBody;
    [SerializeField] GameObject PreviousLevelButton;
    [SerializeField] GameObject NextLevelButton;
    [SerializeField] GameObject Countdown;
    [SerializeField] GameObject PauseButton;
    [SerializeField] GameObject IndicationsPanel;
    [SerializeField] GameObject IndicationsLevelNumber;
    [SerializeField] GameObject Hair;
    [SerializeField] GameObject Mouth;
    [SerializeField] GameObject HairPanel;
    [SerializeField] GameObject JoeBeard;
    [SerializeField] GameObject Spray;
    [SerializeField] GameObject IndicationsClientNameText;
    [SerializeField] GameObject TutorialText;
    [SerializeField] GameObject BlueComb;
    [SerializeField] GameObject Arrow1;
    [SerializeField] GameObject Arrow2;
    [SerializeField] GameObject TutorialBackground;
    [SerializeField] GameObject TutorialBackgroundToolbar;
    [SerializeField] GameObject Finger;
    [SerializeField] GameObject Scissors;
    [SerializeField] GameObject ScissorsBackground;
    [SerializeField] GameObject BluecombBackground;
    [SerializeField] GameObject[] currentTools;
    public GameObject[] Tools;

    bool isPaused = false;
    bool shouldToolSoundBePlaying = true;
    public bool isPlaying;
    public bool isPlayingLevel;
    public bool isTutorialPlaying;
    public bool isHairPainted;
    public bool isSecondToolSelected;
    public bool isLevelRestarted;
    [SerializeField] bool Sound;
    [SerializeField] bool isNotFirstTimePlaying;

    public int totalDistance;
    int currentNumberOfTools;
    int currentToolIndex=0;
    int minDistance = 0;
    int[] currentDistances;
    int[] Distances;

    float time;

    public String CurrentClientName;
    public String ClientName;
    public String currentClientName;
    String[] nonCuttingTools = {"powder", "brush", "green comb", "hairdryer", "palette","spray","powder", "brush", "green comb", "hairdryer", "palette","spray","powder", "brush", "green comb", "hairdryer", "palette","spray"};
    String[] CuttingTools = {"scissors", "hairclipper"};
    String[] ToolsWithCuttingTools;
    String[] DistancesWithCuttingToolsDistances;
    public String[] Names={"Gyűjtő Róbert", "Gyűjtő Róbert", "Gyűjtő Róbert", "Bakó Norbert","Bakó Norbert", "Bakó Norbert", "Keresztes Mátyás", "Keresztes Mátyás", "Kristóf", "Kristóf",  "Tomika", "Tomika","Tomika", "Tibor", "Tibor", "Tibor", "Márk", "Robika", "Lőrincz Zsolt", "Lőrincz Zsolt", "Iuli", "Iuli", "Alex", "Alex", "Kristóf", "Cristi", "Ionuț", "Adi", "Salzimer", "Salzimer", "Chirvaș Adrian", "Mamaia Beat", "Demeter Ákos himself"};

    [SerializeField] Sprite MuteButtonImage;
    [SerializeField] Sprite UnMuteButtonImage;
    [SerializeField] Sprite SprayIconBlack;
    [SerializeField] Sprite SprayIconWhite;
    [SerializeField] Sprite SprayIconGreen;
    [SerializeField] Sprite[] NumberSprites;

    [SerializeField] Sprite NickFace;
    [SerializeField] Sprite NickMouth;
    [SerializeField] Sprite NickMouthHappy;
    [SerializeField] Sprite NickBlack;
    public  Sprite[] NickHairs;

    [SerializeField] Sprite GaiFace;
    [SerializeField] Sprite GaiWhite;
    public  Sprite[] GaiHairs;

    [SerializeField] Sprite JoeFace;
    [SerializeField] Sprite JoeGreen;
    [SerializeField] Sprite JoeMouth;
    [SerializeField] Sprite JoeMouthHappy;
    public  Sprite[] JoeHairs;

    [SerializeField] Color NickColor;
    [SerializeField] Color NickColorBlack;
    [SerializeField] Color NickHairColorBrown;
    [SerializeField] Color NickHairColorPink;
    
    [SerializeField] Color GaiColor;
    [SerializeField] Color GaiColorWhite;
    [SerializeField] Color GaiHairColorBrown;
    [SerializeField] Color GaiHairColorPurple;

    [SerializeField] Color JoeColor;
    [SerializeField] Color JoeColorGreen;
    [SerializeField] Color JoeHairColorBlack;
    [SerializeField] Color JoeHairColorBlue;

    [SerializeField] UnityEngine.UI.Slider slider;

    [SerializeField] AudioSource MainMenuSong;
    [SerializeField] AudioSource InGameSong;
    [SerializeField] AudioSource ToolCompleteSound;
    [SerializeField] AudioSource LevelCompleteSound;

    [SerializeField] AudioSource BeepSound;

    [SerializeField] Text happyText;

    void Awake(){
        Names = new String[]{"Gyűjtő Róbert", "Gyűjtő Róbert", "Gyűjtő Róbert", "Bakó Norbert","Bakó Norbert", "Bakó Norbert", "Keresztes Mátyás", "Keresztes Mátyás", "Kristóf", "Kristóf",  "Tomika", "Tomika","Tomika", "Tibor", "Tibor", "Tibor", "Márk", "Robika", "Lőrincz Zsolt", "Lőrincz Zsolt", "Iuli", "Iuli", "Alex", "Alex", "Kristóf", "Cristi", "Ionuț", "Adi", "Salzimer", "Salzimer", "Chirvaș Adrian", "Mamaia Beat", "Demeter Ákos himself"};
    }


    void Start(){
        isNotFirstTimePlaying = Convert.ToBoolean(PlayerPrefs.GetInt("IsNotFirstTimePlaying"));
        StartCoroutine(GetComponent<AudioFade>().FadeIn(MainMenuSong,1));
        
        if(!isNotFirstTimePlaying){
            PlayerPrefs.SetInt("Sound",1);
            PlayerPrefs.SetInt("IsNotFirstTimePlaying",1);
        }

        Sound = Convert.ToBoolean(PlayerPrefs.GetInt("Sound"));
        if (Sound)
		{
			
            AudioListener.volume = 1;
            MuteButton.GetComponent<Image>().sprite = MuteButtonImage;
		}
        else{
           
            AudioListener.volume = 0;
            MuteButton.GetComponent<Image>().sprite = UnMuteButtonImage;
        
        }
        
    }

    void Update(){
        if(isPaused){
            for(int i = 0; i <Tools.Length;i++){
            Tools[i].GetComponent<Animator>().enabled = false;
            }
        }
        if(isPlaying){
            if(currentToolIndex != currentTools.Length){
                slider.value = totalDistance;
                currentTool = currentTools[currentToolIndex];
                

                for(int i = 0; i <Tools.Length;i++){
                    if(Tools[i]!=currentTools[currentToolIndex]){
                        Tools[i].GetComponent<Animator>().enabled = false;
                        if(GameObject.Find(Tools[i].name + "Background")){
                        GameObject.Find(Tools[i].name + "Background").GetComponent<Image>().enabled = false;
                        }
                    }
                    else{
                        if(Tools[i].tag != "dragged" && !isPaused){
                        Tools[i].GetComponent<Animator>().enabled = true;
                        GameObject.Find(Tools[i].name + "Background").GetComponent<Image>().enabled = true;
                        }
                    }
                }

                if(totalDistance == minDistance){
                    currentTool.GetComponent<Animator>().enabled = false;
                    if(currentTool.name == "palette"){
                        isHairPainted = true;
                        switch(CurrentClientName){
                            case "Nick":
                            Hair.GetComponent<Image>().color = NickHairColorPink;
                            break;

                            case "Gai":
                            Hair.GetComponent<Image>().color = GaiHairColorPurple;
                            break;

                            case "Joe":
                            Hair.GetComponent<Image>().color = JoeHairColorBlue;
                            JoeBeard.GetComponent<Image>().color = JoeHairColorBlue;
                            break;
                        }
                        
                    }
                    if(currentTool.name == "spray"){
                        switch(CurrentClientName){
                            case "Nick":
                            ClientBody.GetComponent<Image>().color = NickColorBlack;
                            Client.GetComponent<Image>().sprite = NickBlack;
                            break;

                            case "Gai":
                            ClientBody.GetComponent<Image>().color = GaiColorWhite;
                            Client.GetComponent<Image>().sprite = GaiWhite;
                            break;

                            case "Joe":
                            ClientBody.GetComponent<Image>().color = JoeColorGreen;
                            Client.GetComponent<Image>().sprite = JoeGreen;
                            break;
                        }
                    }
                    currentToolIndex++;
                    ToolCompleteSound.Play();
                    if(currentDistances.Length-1>=currentToolIndex){
                    minDistance = totalDistance - currentDistances[currentToolIndex];
                    }
                }
            }
            else{
                isPlaying = false;
                if(!GetComponent<Countdown>().timerIsRunningIndicationsTutorial){
                LevelCompletedFunction();
                
                }
                }
        }

       

        if(isTutorialPlaying){
             
            slider.value = totalDistance;
            if(totalDistance == 120){
                if(!ToolCompleteSound.isPlaying && shouldToolSoundBePlaying){
                ToolCompleteSound.Play();
                shouldToolSoundBePlaying = false;
                }

                 for(int i = 0; i <Tools.Length;i++){
                    if(Tools[i]!=Scissors){
                        Tools[i].GetComponent<Animator>().enabled = false;
                        Tools[i].GetComponent<Image>().raycastTarget = false;
                    }
                    else{
                        Tools[i].GetComponent<Animator>().enabled = true;
                        Tools[i].GetComponent<Image>().raycastTarget = true;
                    }
                }

                BlueComb.GetComponent<Animator>().enabled = false;
                currentTool = Scissors;
                if(!isSecondToolSelected){
                ScissorsBackground.GetComponent<Image>().enabled = true;
                BluecombBackground.GetComponent<Image>().enabled = false;
                Arrow2.SetActive(true);
                }
                Arrow1.SetActive(false);
                Scissors.GetComponent<Animator>().enabled = true;
                
            }
            if(totalDistance == 0){
                ToolCompleteSound.Play();
                Finger.SetActive(false);
                isTutorialPlaying = false;
                Scissors.GetComponent<Animator>().enabled = false;
                if(!GetComponent<Countdown>().timerIsRunningIndicationsTutorial){
                LevelCompletedFunction();
                
                }
            }

        }

    }

    public void Play(){
        if(isNotFirstTimePlaying){
        MainMenu.SetActive(false);
        LevelsMenu.SetActive(true);
        } else {
            PlayTutorial();
        }
    }

    public void ShowIndications(int levelNumber, String ClientNamePresent, int numberOfTools, int distance, float timePresent, float timeIndications){
        
        
        MainMenuSong.Pause();
        StartCoroutine(PlaySoundEvery(1,3,1));
        TutorialText.SetActive(false);
        if(!isLevelRestarted){
        currentClientName = Names[UnityEngine.Random.Range(0,Names.Length-1)];
        IndicationsClientNameText.GetComponent<Text>().text = currentClientName;
        }else{
            IndicationsClientNameText.GetComponent<Text>().text = currentClientName;
        }
        IndicationsPanel.SetActive(true);
        LevelsMenu.SetActive(false);
        this.GetComponent<Countdown>().timerIsRunningIndications = true;
        GetComponent<Countdown>().timeRemainingIndications = timeIndications;
        IndicationsLevelNumber.GetComponent<Image>().sprite = NumberSprites[levelNumber-1];
        if(levelNumber < 10){
            IndicationsLevelNumber.GetComponent<RectTransform>().sizeDelta = new Vector2 (80, 160);
        }
        else{
            IndicationsLevelNumber.GetComponent<RectTransform>().sizeDelta = new Vector2 (160, 160);
        }
        ClientName = ClientNamePresent;
        Distances = randomListofNnumbersthatSumUpToN(numberOfTools,distance);
        time = timePresent;
        String[] ToolsWithoutCuttingTools = randomizeNElements(numberOfTools, nonCuttingTools);
        List<string> ToolsWithoutCuttingToolsList = ToolsWithoutCuttingTools.OfType<string>().ToList();
        ToolsWithoutCuttingToolsList.Add(CuttingTools[UnityEngine.Random.Range(0, 2)]);
        ToolsWithCuttingTools = ToolsWithoutCuttingToolsList.ToArray();
        currentNumberOfTools = numberOfTools;

        List<int> DistancesWithoutCuttingToolsDistancesList = Distances.ToList();
        if(ToolsWithCuttingTools[ToolsWithCuttingTools.Length-1]=="hairclipper" || ToolsWithCuttingTools[ToolsWithCuttingTools.Length-1]=="scissors" ){
            
            if(ClientNamePresent == "Nick"){
                DistancesWithoutCuttingToolsDistancesList.Add(110);
            }
            else if(ClientNamePresent == "Gai"){
                DistancesWithoutCuttingToolsDistancesList.Add(120);
            }
            else if(ClientNamePresent == "Joe"){
                DistancesWithoutCuttingToolsDistancesList.Add(150);
            }
        }
        

        Distances = DistancesWithoutCuttingToolsDistancesList.ToArray();

       
    }

    public void StartGame(){
        isLevelRestarted = false;
        isPaused = false;
        isPlayingLevel = true;
        for(int i=0;i< Tools.Length;i++){
        Tools[i].GetComponent<Image>().raycastTarget = true;
        }
        PlayInGameMusic();
        Countdown.SetActive(true);
        PauseButton.SetActive(true);
        currentToolIndex=0;
        HairSaloon.SetActive(true);
        IndicationsPanel.SetActive(false);
        isPlaying = true;
        this.GetComponent<Countdown>().timerIsRunning = true;
        GetComponent<Countdown>().timeRemaining = time;
        List<GameObject> currentToolsList = new List<GameObject>();
        for(int i=0;i<ToolsWithCuttingTools.Length;i++){
            currentToolsList.Add(GameObject.Find(ToolsWithCuttingTools[i]));
        }

        currentTools = currentToolsList.ToArray();
        currentDistances = Distances;
        currentNumberOfTools = ToolsWithCuttingTools.Length;
        slider.maxValue = Distances.Sum();
        totalDistance = Distances.Sum();
        slider.value = slider.maxValue;
        CurrentClientName = ClientName;
        LevelCompleteMenu.SetActive(false);
        PauseImage.SetActive(false);
        Fill.SetActive(true);
      

        switch(ClientName) 
            {
                case "Nick":
                Hair.GetComponent<Image>().sprite = NickHairs[NickHairs.Length-1];
                Hair.GetComponent<Image>().color = NickHairColorBrown;
                Client.GetComponent<Image>().sprite = NickFace;
                ClientBody.GetComponent<Image>().color = NickColor;
                Client.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,720);
                Client.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 500);
                ClientBody.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,395);
                Hair.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,920);
                Hair.GetComponent<RectTransform>().sizeDelta = new Vector2(435,220);
                HairPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,1000); 
                HairPanel.GetComponent<RectTransform>().sizeDelta = new Vector2 (650,350);
                Mouth.GetComponent<RectTransform>().sizeDelta = new Vector2(80,20);
                Mouth.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,680);
                JoeBeard.SetActive(false);
                Mouth.SetActive(true);
                Mouth.GetComponent<Image>().sprite = NickMouth; 
                Spray.GetComponent<Image>().sprite = SprayIconBlack; 
                break;

                case "Gai":
                Hair.GetComponent<Image>().sprite = GaiHairs[GaiHairs.Length-1];
                Hair.GetComponent<Image>().color = GaiHairColorBrown;
                Client.GetComponent<Image>().sprite = GaiFace;
                ClientBody.GetComponent<Image>().color = GaiColor;
                Mouth.SetActive(false);
                JoeBeard.SetActive(false);
                Client.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,720);
                Client.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 500);
                ClientBody.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,365);
                Hair.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,950);
                Hair.GetComponent<RectTransform>().sizeDelta = new Vector2(435,220);
                HairPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,1000); 
                HairPanel.GetComponent<RectTransform>().sizeDelta = new Vector2 (550,320);
                Spray.GetComponent<Image>().sprite = SprayIconWhite;
                break;

                case "Joe":
                Hair.GetComponent<Image>().sprite = JoeHairs[JoeHairs.Length-1];
                Hair.GetComponent<Image>().color = JoeHairColorBlack;
                Client.GetComponent<Image>().sprite = JoeFace;
                ClientBody.GetComponent<Image>().color = JoeColor;
                Client.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,728);
                Client.GetComponent<RectTransform>().sizeDelta = new Vector2(330, 530);
                ClientBody.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,360);
                Hair.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,912);
                Hair.GetComponent<RectTransform>().sizeDelta = new Vector2 (500,300);
                HairPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,955); 
                HairPanel.GetComponent<RectTransform>().sizeDelta = new Vector2 (641,415);
                Mouth.GetComponent<RectTransform>().sizeDelta = new Vector2(95,27);
                Mouth.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,650);
                JoeBeard.SetActive(true);
                Mouth.SetActive(true);
                Mouth.GetComponent<Image>().sprite = JoeMouth;
                Spray.GetComponent<Image>().sprite = SprayIconGreen;
                break;
            }
        minDistance = totalDistance - currentDistances[currentToolIndex]; 
    }

    public void PlayTutorial(){
        currentClientName = Names[UnityEngine.Random.Range(0,Names.Length-1)];
        IndicationsClientNameText.GetComponent<Text>().text = currentClientName;
        LevelCompleteSound.Stop();
        shouldToolSoundBePlaying = true;
        StartCoroutine(PlaySoundEvery(1,3,1));
        GetComponent<Levels>().currentLevel = 1;
        MainMenuSong.Pause();
        this.GetComponent<Countdown>().timerIsRunningIndicationsTutorial = true;
        GetComponent<Countdown>().timeRemainingIndicationsTutorial = 4;
        IndicationsPanel.SetActive(true);
        TutorialText.SetActive(true);
        LevelsMenu.SetActive(false);
        IndicationsLevelNumber.GetComponent<Image>().sprite = NumberSprites[0];
        IndicationsLevelNumber.GetComponent<RectTransform>().sizeDelta = new Vector2 (80, 160);
        LevelCompleteMenu.SetActive(false);
        HairSaloon.SetActive(false);
        PauseImage.SetActive(false);
        MainMenu.SetActive(false);
    }

    public void StartGameTutorial(){
        BluecombBackground.GetComponent<Image>().enabled = true;
        PlayInGameMusic();
        Countdown.SetActive(false);
        PauseButton.SetActive(false);
        IndicationsPanel.SetActive(false);
        HairSaloon.SetActive(true);
        Hair.GetComponent<Image>().sprite = NickHairs[NickHairs.Length-1];
        Hair.GetComponent<Image>().color = NickHairColorBrown;
        Client.GetComponent<Image>().sprite = NickFace;
        ClientBody.GetComponent<Image>().color = NickColor;
        Client.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,720);
        Client.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 500);
        ClientBody.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,395);
        Hair.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,920);
        Hair.GetComponent<RectTransform>().sizeDelta = new Vector2(435,220);
        HairPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,1000); 
        HairPanel.GetComponent<RectTransform>().sizeDelta = new Vector2 (650,350);
        Mouth.GetComponent<RectTransform>().sizeDelta = new Vector2(80,20);
        Mouth.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,680);
        JoeBeard.SetActive(false);
        Mouth.SetActive(true);
        Mouth.GetComponent<Image>().sprite = NickMouth; 
        Spray.GetComponent<Image>().sprite = SprayIconBlack;
        Arrow1.SetActive(true);
        currentTool = BlueComb;
        totalDistance = 170;
        ClientName = "Nick";
        CurrentClientName = "Nick";
        GetComponent<Levels>().currentLevel = 1;
        slider.maxValue = 170;
        slider.value = 170;
        Fill.SetActive(true);
        isTutorialPlaying = true;
         for(int i = 0; i <Tools.Length;i++){
                    if(Tools[i]!=BlueComb){
                        Tools[i].GetComponent<Animator>().enabled = false;
                        Tools[i].GetComponent<Image>().raycastTarget = false;
                    }
                    else{
                        Tools[i].GetComponent<Animator>().enabled = true;
                        Tools[i].GetComponent<Image>().raycastTarget = true;
                    }
        }
       
    }
    
    public void Mute(){
        if(Sound){
        AudioListener.volume = 0;
        PlayerPrefs.SetInt("Sound",0);
        Sound = false;
        MuteButton.GetComponent<Image>().sprite = UnMuteButtonImage;
        }
        else{
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Sound",1);
            Sound = true;
            MuteButton.GetComponent<Image>().sprite = MuteButtonImage;
        }
    }

     public void Quit(){
        Application.Quit();
       
    }

    public void MainMenuFunction(){
        Time.timeScale = 1;
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

    void LevelCompletedFunction(){
        
        GameObject.Find(currentTool.name + "Background").GetComponent<Image>().enabled = false;
        happyText.text = currentClientName + " is happy!";
        isPlayingLevel = false;
        InGameSong.Pause();
        LevelCompleteSound.Play();
        switch(ClientName) 
            {
                case "Nick":
                Mouth.GetComponent<Image>().sprite = NickMouthHappy;
                Mouth.GetComponent<RectTransform>().sizeDelta = new Vector2(105,30);
                break;

                case "Joe":
                Mouth.GetComponent<Image>().sprite = JoeMouthHappy;
                Mouth.GetComponent<RectTransform>().sizeDelta = new Vector2(105,30);
                break;

            }
                
        
        Fill.SetActive(false);
        isPlaying = false;
        isPaused = true;
        LevelCompleteMenu.SetActive(true);
        PauseImage.SetActive(true);
        if(GetComponent<Levels>().currentLevel+1>GetComponent<Levels>().levelNumber){
            if(GetComponent<Levels>().levelNumber<15){
                GetComponent<Levels>().levelNumber = GetComponent<Levels>().currentLevel + 1;
                PlayerPrefs.SetInt("LevelNumber",GetComponent<Levels>().levelNumber);
            }

        }

        if(GetComponent<Levels>().currentLevel > 1){
            PreviousLevelButton.SetActive(true);
        }
        else{
            PreviousLevelButton.SetActive(false);
        }

        if(GetComponent<Levels>().currentLevel < 15){
            NextLevelButton.SetActive(true);
        }
        else{
            NextLevelButton.SetActive(false);
        }
    }

  

    public void BackFromOptions(){
        OptionsMenu.SetActive(false);
        if(isPlaying){

            HairSaloon.SetActive(true);
            PauseMenu.SetActive(true);
        }
        else{
            MainMenu.SetActive(true);
        }
    }

    public void Pause(){
        isPlayingLevel = false;
        InGameSong.Pause();
        PlayMenuMusic();
        isPaused = true;
        PauseMenu.SetActive(true);
        PauseImage.SetActive(true);
        GetComponent<Countdown>().timerIsRunning = false;
         for(int i=0;i<Tools.Length;i++){
            Tools[i].GetComponent<Animator>().enabled = false;
        }

    }

     public void Resume(){
        isPlayingLevel = true;
        MainMenuSong.Pause();
        PlayInGameMusic();
        isPaused = false;
        PauseMenu.SetActive(false);
        PauseImage.SetActive(false);
        GetComponent<Countdown>().timerIsRunning = true;
    }

     private void OnApplicationPause(bool pause)
	{
		if ( isPlayingLevel )
		{
			Pause();
		}
	}

    private void OnApplicationFocus(bool pause)
	{
		if (isPlayingLevel)
		{
			Pause();
		}
	}

    void PlayMenuMusic(){
        StartCoroutine(GetComponent<AudioFade>().FadeIn(MainMenuSong,2f));
    }

    void PlayInGameMusic(){
        StartCoroutine(GetComponent<AudioFade>().FadeIn(InGameSong,2f));
    }


      IEnumerator PlaySoundEvery(float t, int times, float delay){
        yield return new WaitForSeconds(delay);
    for(int i=0;i<times;i++){
        BeepSound.Play();
        yield return new WaitForSeconds(t);
    }
      }

    String[] randomizeNElements(int n, string[] elements){
        string[] firstNRandomizedElements = {"a","a"};
	    while(doesContainAdjacentElements(firstNRandomizedElements)){
	    string[] randomizedElements = elements.OrderBy(x => new System.Random().Next()).ToArray();;
	    firstNRandomizedElements = new string[n];
	    for(int i=0;i<n;i++){
		    firstNRandomizedElements[i] = randomizedElements[i];
	    }
        }
	return firstNRandomizedElements;
    }

    bool doesContainAdjacentElements(string[] elements){
	for(int i=0;i<elements.Length-1;i++){
		if(elements[i]==elements[i+1]){
			return true;
		}
		
	}
	    return false;
    }

     int[] randomListofNnumbersthatSumUpToN(int m, int n)
{


	int [] arr = new int[m];
	

	for (int i = 0; i < n; i++)
	{

		System.Random rnd = new System.Random();

		arr[rnd.Next(0, n) % m]++;
	}
	
	return arr;

	
}

}