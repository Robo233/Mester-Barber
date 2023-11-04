using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour,  IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] GameObject SceneLoader;
    [SerializeField] GameObject Client;
    [SerializeField] GameObject Hair;
    [SerializeField] GameObject Arrow1;
    [SerializeField] GameObject Arrow2;
    [SerializeField] GameObject Finger;

    RectTransform rectTransform;

    [SerializeField] RectTransform hairArea;

    [SerializeField] Canvas canvas;

    CanvasGroup canvasGroup;

    public string currentTool;

    bool isHairPainted;

    Vector3 toolPosition;

    [SerializeField] UnityEngine.UI.Slider slider;

    [SerializeField] ParticleSystem sprayEffect;
    [SerializeField] ParticleSystem powderEffect;
    ParticleSystem.MainModule sprayEffectMain;

    [SerializeField] Color NickSprayColor;
    [SerializeField] Color GaiSprayColor;
    [SerializeField] Color JoeSprayColor;

    [SerializeField] Vector3 sprayEffectPostionNick;
    [SerializeField] Vector3 sprayEffectPostionGai;
    [SerializeField] Vector3 sprayEffectPostionJoe;

      private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        toolPosition = transform.localPosition;
        sprayEffectMain = sprayEffect.main;
    }

    void Update(){

        isHairPainted = SceneLoader.GetComponent<Menu>().isHairPainted;
    }

    public void OnBeginDrag(PointerEventData eventData){
           switch(SceneLoader.GetComponent<Menu>().CurrentClientName) 
            {
                case "Nick":
                    sprayEffect.transform.position = sprayEffectPostionNick;
                    sprayEffectMain.startColor = NickSprayColor;
                    
                break;

                case "Gai":
                    sprayEffect.transform.position = sprayEffectPostionGai;
                    sprayEffectMain.startColor = GaiSprayColor;
                break;

                case "Joe":
                    sprayEffect.transform.position = sprayEffectPostionJoe;
                    sprayEffectMain.startColor = JoeSprayColor;
                break;
            }
       
        transform.localScale = new Vector3(1,1,1);
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        GetComponent<Animator>().enabled = false;
        tag = "dragged";
        if(SceneLoader.GetComponent<Menu>().isTutorialPlaying && gameObject.name == "blue comb"){
            Arrow1.SetActive(false);
            Finger.SetActive(true);
        }
        if(SceneLoader.GetComponent<Menu>().isTutorialPlaying && gameObject.name == "scissors"){
            SceneLoader.GetComponent<Menu>().isSecondToolSelected = true;
            Arrow2.SetActive(false);
            Finger.SetActive(true);
        }
    }

    public void OnEndDrag(PointerEventData eventData){
         if(gameObject.name=="spray"){
                sprayEffect.Stop();
            }
         if(gameObject.name=="powder"){
                powderEffect.Stop();
            }
        transform.localScale = new Vector3(1,1,1);
        gameObject.GetComponent<AudioSource>().Stop();
        if(gameObject.name == "scissors" || gameObject.name == "blue comb"){
        gameObject.GetComponent<Animator>().enabled = true;
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        transform.localPosition =  toolPosition;
        GetComponent<Animator>().enabled = true;
        tag = "Untagged";
        
        if(SceneLoader.GetComponent<Menu>().isTutorialPlaying && gameObject.name == "blue comb"){
            Arrow1.SetActive(true);
            Finger.SetActive(false);
        }
         if(SceneLoader.GetComponent<Menu>().isTutorialPlaying && gameObject.name == "scissors"){
            Arrow2.SetActive(true);
            Finger.SetActive(false);
        }

    }

     public void OnDrag(PointerEventData eventData){
        if(SceneLoader.GetComponent<Menu>().isPlaying || SceneLoader.GetComponent<Menu>().isTutorialPlaying){
        
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        

        if(doOverlap2(this.gameObject, hairArea.gameObject)){

            if(gameObject.name=="spray"){
                sprayEffect.Play();
            }
            if(gameObject.name=="powder"){
                powderEffect.Play();
            }   

            if(SceneLoader.GetComponent<Menu>().currentTool == gameObject){
                SceneLoader.GetComponent<Menu>().totalDistance--;
                if(!gameObject.GetComponent<AudioSource>().isPlaying){
                gameObject.GetComponent<AudioSource>().Play();
                }
               

            if(SceneLoader.GetComponent<Menu>().totalDistance%10==0 && (gameObject.name=="scissors" || gameObject.name=="hairclipper" )){
                
                switch(SceneLoader.GetComponent<Menu>().CurrentClientName) 
            {
                case "Nick":
                    Hair.GetComponent<Image>().sprite = SceneLoader.GetComponent<Menu>().NickHairs[SceneLoader.GetComponent<Menu>().totalDistance/10];
                    sprayEffectMain.startColor = NickSprayColor;
                    
                break;

                case "Gai":
                    Hair.GetComponent<Image>().sprite = SceneLoader.GetComponent<Menu>().GaiHairs[SceneLoader.GetComponent<Menu>().totalDistance/10];
                    sprayEffectMain.startColor = GaiSprayColor;
                break;

                case "Joe":
                    Hair.GetComponent<Image>().sprite = SceneLoader.GetComponent<Menu>().JoeHairs[SceneLoader.GetComponent<Menu>().totalDistance/10];
                    sprayEffectMain.startColor = JoeSprayColor;
                break;
            }
            
            }
           
        }
    }
    else{
        if(gameObject.GetComponent<AudioSource>().isPlaying){
                gameObject.GetComponent<AudioSource>().Stop();
                }
    }
        }
     }

     bool doOverlap2(GameObject GameObject1, GameObject GameObject2)
     {
        if((GameObject1.transform.localPosition.y>(GameObject2.transform.position.y-(GameObject2.GetComponent<RectTransform>().rect.height))) && (GameObject1.transform.localPosition.y<(GameObject2.transform.position.y)) ){
            return true;
        }
        return false;
     }

    /*
       bool doOverlap(GameObject GameObject1, GameObject GameObject2)
    {
        float GameObject1Height = GameObject1.GetComponent<RectTransform>().rect.height;   
        float GameObject1Width = GameObject1.GetComponent<RectTransform>().rect.width;
        float GameObject2Height = GameObject2.GetComponent<RectTransform>().rect.height;   
        float GameObject2Width = GameObject2.GetComponent<RectTransform>().rect.width;

        Debug.Log(GameObject1Height);
        Debug.Log(GameObject1Width);
        Debug.Log(GameObject2Height);
        Debug.Log(GameObject2Width);
        Debug.Log(GameObject1.transform.localPosition.x);
        Debug.Log(GameObject1.transform.localPosition.y);
        Debug.Log(GameObject2.transform.localPosition.x);
        Debug.Log(GameObject2.transform.localPosition.y);

        float l1x = GameObject1.transform.localPosition.x - GameObject1Width/4;
        float l1y = GameObject1.transform.localPosition.y + GameObject1Height/4;
        float l2x = GameObject2.transform.position.x - GameObject2Width/4;
        float l2y = GameObject2.transform.position.y + GameObject2Height/4;
        float r1x = GameObject1.transform.localPosition.x + GameObject1Width/4;
        float r1y = GameObject1.transform.localPosition.y - GameObject1Height/4;
        float r2x = GameObject2.transform.position.x + GameObject2Width/4;
        float r2y = GameObject2.transform.position.y - GameObject2Height/4;
        
        if (l1x == r1x || l1y == r1y || r2x == l2x || l2y == r2y)
        {
            return false;
        }
       
        if (l1x > r2x || l2x > r1x)
        {
            return false;
        }
 
        if (r1y > l2y || r2y > l1y)
        {
            return false;
        }
        return true;
    }
    */
   
}
