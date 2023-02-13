using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public CanvasManager CanvasManager;
  public MapController MapController;
  public PlayerController PlayerController;
  public BossScript BossScript;

  void Start()
  {
    CanvasManager = this.gameObject.GetComponent<CanvasManager>();
    MapController = this.gameObject.GetComponent<MapController>();
  }

  public void StartPlay()
  {
    PlayerController.StartPlayer();
    CanvasManager.OpenScreen(2);
    BossScript.ResetBoss();
    MapController.Invoke("StartGame", 1.0f);
  }

  public void DoQuit()
  {
    Application.Quit();
  }

  public void GameOver(bool winTrueLoseFalse)
  {
    var winLoseIndex = 3;
    if (!winTrueLoseFalse)
    {
      winLoseIndex = 4;
    }
    CanvasManager.OpenScreen(winLoseIndex);
  }

  public void PlayerHit()
  {
    PlayerController.PlayerHit();
  }

  public void Pause()
  {
    PlayerController.PausePlayer();
    MapController.PauseMeteors();
    BossScript.PauseBoss();
  }

  public void Resume()
  {
    PlayerController.Resume();
    MapController.Resume();
    BossScript.Resume();
  }

  public void ResetAll()
  {
    PlayerController.ResetAll();
    MapController.ResetAll();
  }
}
