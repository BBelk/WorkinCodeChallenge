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



  void Update()
  {
    var screenPoint = (Vector3)Input.mousePosition;
    screenPoint.z = 10.0f;
    var newPosition = Camera.main.ScreenToWorldPoint(screenPoint);

    cursorRT.transform.position = newPosition;

    if (Input.GetKeyDown(KeyCode.Escape))
    {
      ButtonEscape();
    }
  }

  void Start()
  {
    GameManager = this.gameObject.GetComponent<GameManager>();
    OpenScreen(0);
  }

  public void ButtonPlay()
  {
    GameManager.StartPlay();
    timerText.text = "00:00";
    currentTime = 0f;
    StartTimer();
  }
  public void ButtonQuit()
  {
    GameManager.DoQuit();
  }
  public void ButtonMainMenu()
  {
    StopTimer();
    OpenScreen(0);
    GameManager.ResetAll();
  }
  public void ButtonPause()
  {
    OpenScreen(1);
    GameManager.Pause();
    doTime = false;
  }

  public void ButtonResume()
  {
    StartTimer();
    OpenScreen(2);
  }

  public void ButtonEscape()
  {
    //escape/resume
    if (allScreenObjects[0].activeSelf || allScreenObjects[3].activeSelf) { return; }
    if (allScreenObjects[1].activeSelf == true)
    {
      GameManager.Resume();
      ButtonResume();
      return;
    }
    if (!allScreenObjects[1].activeSelf)
    {
      ButtonPause();
    }
  }

  public void OpenScreen(int screenIndex)
  {
    foreach (GameObject newObj in allScreenObjects)
    {
      newObj.SetActive(false);
    }
    allScreenObjects[screenIndex].SetActive(true);
    if (screenIndex >= 3)
    {
      SetScore(screenIndex);
    }
  }


  public TMP_Text winLoseText;
  public TMP_Text scoreText;
  public void SetScore(int winLoseIndex)
  {
    StopTimer();
    winLoseText.text = "YOU WON!";
    float minutes = Mathf.FloorToInt(currentTime / 60);
    float seconds = Mathf.FloorToInt(currentTime % 60);
    var getTime = string.Format("{0:00}:{1:00}", minutes, seconds);
    var deathCount = GameManager.PlayerController.deathCount;
    var finalScore = (1000 - currentTime) + (deathCount * -100);
    if (finalScore < 0) { finalScore = 0; }
    scoreText.text = "Score\nTotal Time: " + getTime + "\nTotal Deaths: " + deathCount + "\n\nTotal Score: " + finalScore;
    // if(winLoseIndex == 4){
    //     winLoseText.text = "YOU LOSE!";
    // }
  }

  public TMP_Text timerText;
  public float currentTime = 0f;
  public Coroutine timerCoroutine;
  public bool doTime;
  public void StartTimer()
  {
    doTime = true;
    timerCoroutine = StartCoroutine(TimerCo());
  }

  public void StopTimer()
  {
    doTime = false;
    if (timerCoroutine != null)
    {
      StopCoroutine(timerCoroutine);
    }
  }

  public IEnumerator TimerCo()
  {
    while (doTime)
    {
      yield return new WaitForSeconds(1f);
      currentTime += 1f;
      float minutes = Mathf.FloorToInt(currentTime / 60);
      float seconds = Mathf.FloorToInt(currentTime % 60);
      timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
  }


}
