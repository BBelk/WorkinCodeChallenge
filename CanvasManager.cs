using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{

    public GameManager GameManager;
    public List<GameObject> allScreenObjects; 
    // 1 title, 2 pause, 3 gameplay, 4 gameOver
    public GameObject cursorRT;

    

    void Update(){
        var screenPoint = (Vector3)Input.mousePosition;
screenPoint.z = 10.0f; //distance of the plane from the camera
var newPosition = Camera.main.ScreenToWorldPoint(screenPoint);

    cursorRT.transform.position = newPosition;

    if (Input.GetKeyDown(KeyCode.Escape)){
            ButtonEscape();
    }

    
    }

    void Start(){
        GameManager = this.gameObject.GetComponent<GameManager>();
        OpenScreen(0);
    }

    public void ButtonPlay(){
        GameManager.StartPlay();
    }
    public void ButtonQuit(){
        GameManager.DoQuit();
    }
    public void ButtonMainMenu(){
        OpenScreen(0);
    }
    public void ButtonPause(){
        OpenScreen(1);
        GameManager.Pause();
    }

    public void ButtonEscape(){
        //escape/resume
        if(allScreenObjects[0].activeSelf || allScreenObjects[3].activeSelf){return;}
        if(allScreenObjects[1].activeSelf == true){
            GameManager.Resume();
            OpenScreen(2);
            return;
        }
        if(!allScreenObjects[1].activeSelf){
            // GameManager.But();
            ButtonPause();
        }
    }

    public void OpenScreen(int screenIndex){
        foreach(GameObject newObj in allScreenObjects){
            newObj.SetActive(false);
        }
        allScreenObjects[screenIndex].SetActive(true);
        if(screenIndex >= 3){
            SetScore(screenIndex);
        }
    }


    public TMP_Text winLoseText;
    public TMP_Text scoreText;
    public void SetScore(int winLoseIndex){
        winLoseText.text = "YOU WON!";
        if(winLoseIndex == 4){
            winLoseText.text = "YOU LOSE!";
        }
    }


}
