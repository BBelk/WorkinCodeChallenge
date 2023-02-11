using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CanvasManager CanvasManager;
    public MapController MapController;

    void Start(){
        CanvasManager = this.gameObject.GetComponent<CanvasManager>();
        MapController = this.gameObject.GetComponent<MapController>();
    }

    public void StartPlay(){
        CanvasManager.OpenScreen(2);
    }

    public void DoQuit(){
        Application.Quit();
    }

    public void GameOver(bool winTrueLoseFalse){
        var winLoseIndex = 3;
        if(!winTrueLoseFalse){
            winLoseIndex = 4;
        }
        CanvasManager.OpenScreen(winLoseIndex);
    }

    public void PlayerHit(){
        
    }
}
