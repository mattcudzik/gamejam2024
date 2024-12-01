using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Object = System.Object;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timerSpeed = 1.0f;
    //Base start time = 8:00 -> 640 Minutes
    [SerializeField] private float startTime = 0.0f;
    //Time in minutes
    //Start time = 8:00 -> 640 Minutes
    [SerializeField] private List<TimetableEvent> timetable = new List<TimetableEvent>();
    //Timetable:
    //08:00 - Apel -> 0
    //08:30 - Posiłek -> 30
    //11:00 - Praca -> 180
    //14:00 - Czas wolny -> 360
    //20:00 - Posiłek -> 720
    //21:00 - Sen  -> 780

    private float timerWait = 5f;
    private float timerWaitStart = 0f;
    private bool isTimerWait = false;
    private float TimeOffeset = 480.0f;
    private bool ParityDay = false;
    private bool Slowed = false;
    private float SlowDown = 0f;
    public float CurrentTime { get; set; }
    private int CurrentEventIndex = 0;
    public string myReason = "";
    public static Action OnSceneChange;
    [System.Serializable]
    public struct TimetableEvent
    {
        public string SceneName;
        public float EventTime;
    }
    private void Start()
    {
        RollCallSlowDown(0.25f);
        SlowDown += 2;
        CurrentTime = startTime;
        if (timerText == null)
        {
            Debug.LogError("Brak przypisanego TextMeshPro!");
        }
        timetable.Sort((a, b) => a.EventTime.CompareTo(b.EventTime));
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "RollCall" && !Slowed)
        {
            RollCallSlowDown();
        }
        else if(Slowed)
        {
            NormalSpeed();
        }

        CurrentTime += Time.deltaTime * timerSpeed;
        Debug.Log("Stan pracy");
        Debug.Log(GameManager.Instance.IsSceneWorkDone);
        if (!isTimerWait)
        {
            if (CurrentEventIndex < timetable.Count && CurrentTime >= timetable[CurrentEventIndex].EventTime)
            {
                
                if (GameManager.Instance.IsSceneWorkDone)
                {
                    if (ParityDay == true && timetable[CurrentEventIndex].SceneName == "Work")
                    {
                        SceneManager.LoadScene("Laundry");
                    }
                    else
                    {
                        SceneManager.LoadScene(timetable[CurrentEventIndex].SceneName);
                    }

                    
                    CurrentEventIndex++;
                    if (CurrentEventIndex == timetable.Count) NextDay();
                }
                else
                {
                    StartDelay("because you have not completed the task");
                }
                OnSceneChange?.Invoke();
            
            }
            UpdateTimerText();
        }
        else
        {
            if (CurrentTime - timerWaitStart >= timerWait)
            {
                NextDay();
            }
        }
    }

    public void StartDelay(string reason)
    {
        myReason=reason;
        SceneManager.LoadScene("Delay");
        isTimerWait = true;
        timerWaitStart = CurrentTime;
    }

    private void RollCallSlowDown(float factor=0.5f)
    {
        SlowDown += 1/factor;
        timerSpeed *= factor;
        Slowed = true;
    }
    private void NormalSpeed()
    {
        
        timerSpeed *= SlowDown;
        SlowDown = 0;
        Slowed = false;
    }
    public void NextDay()
    {
        if (GameManager.Instance.NextDay())
        {
            SceneManager.LoadScene("RollCall");
            CurrentEventIndex=0;
            CurrentTime = startTime;
            isTimerWait = false;
        }
        else
        {
            SceneManager.LoadScene("GameOver");
            GameManager.Instance.UIActive(false);
        }
        
    }
    private void UpdateTimerText()
    {
        // Formatowanie czasu na minuty:sekundy
        int hours = Mathf.FloorToInt((CurrentTime+TimeOffeset) / 60);
        int minutes = Mathf.FloorToInt((CurrentTime+TimeOffeset) % 60);

        // Ustawienie tekstu
        timerText.text = string.Format("{00:00}:{01:00}", hours, minutes);
    }
}