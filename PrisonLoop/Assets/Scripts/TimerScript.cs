using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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
    
    private float TimeOffeset = 480.0f;
    public float CurrentTime { get; set; }
    private int CurrentEventIndex = 0;
    public static Action OnSceneChange;
    [System.Serializable]
    public struct TimetableEvent
    {
        public string SceneName;
        public float EventTime;
    }
    private void Start()
    {
        CurrentTime = startTime;
        if (timerText == null)
        {
            Debug.LogError("Brak przypisanego TextMeshPro!");
        }
        timetable.Sort((a, b) => a.EventTime.CompareTo(b.EventTime));
    }

    private void Update()
    {
        //Change Scene
        CurrentTime += Time.deltaTime * timerSpeed;
        if (CurrentEventIndex < timetable.Count && CurrentTime >= timetable[CurrentEventIndex].EventTime)
        {
            if (GameManager.Instance.IsSceneWorkDone)
            {
                SceneManager.LoadScene(timetable[CurrentEventIndex].SceneName);
                
                CurrentEventIndex++;
            }
            else
            {
                //SceneManager.LoadScene(""); TODO
            }
            OnSceneChange?.Invoke();
            
        }
        
        UpdateTimerText();
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