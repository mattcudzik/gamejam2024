using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class QuickTimeEventGame : MiniGameBase
{
    public TMP_Text keyDisplay;                  // Wyświetlanie klawisza (TextMeshPro)
    public TMP_Text winMessage;                  // Komunikat o wygranej (TextMeshPro)
    public List<ProgressObject> progressObjects; // Lista obiektów (śrubek z paskami)

    private string[] keyPool = { "A", "S", "D", "F" }; // Pula klawiszy
    private string currentKey;                   // Aktualny klawisz
    private int currentProgressObjectIndex = 0;  // Indeks aktualnie aktywnego obiektu
    public float keyDisplayDelay = 1.0f;         // Czas opóźnienia wyświetlenia klawisza
    public float reactionTime = 3.0f;            // Czas na reakcję gracza
    public float keyDisplayOffsetY = 140f;
    private Coroutine currentCoroutine;          // Referencja do aktywnej korutyny

    void Start()
    {
        winMessage.gameObject.SetActive(false);
        keyDisplay.GetComponent<RectTransform>().localPosition =
            progressObjects[0].GetComponent<RectTransform>().localPosition + new Vector3(0, keyDisplayOffsetY, 0);
        // Ukryj wszystkie obiekty poza pierwszym
        for (int i = 0; i < progressObjects.Count; i++)
        {
            progressObjects[i].ResetProgress();
            progressObjects[i].SetVisibility(i == 0);
        }

        StartNewKeyCycle(); // Rozpocznij grę
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(currentKey)) // Sprawdzanie, jeśli klawisz jest aktywny
        {
            foreach (var key in keyPool)
            {
                if (Input.GetKeyDown(key.ToLower())) // Sprawdzanie wciśniętego klawisza
                {
                    CheckKey(key);
                    break;
                }
            }
        }
    }

    void StartNewKeyCycle()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(HandleKeyCycle());
    }

    IEnumerator HandleKeyCycle()
    {
        keyDisplay.text = ""; // Ukryj klawisz
        currentKey = "";      // Ustaw brak aktywnego klawisza

        yield return new WaitForSeconds(keyDisplayDelay); // Odczekaj, zanim wyświetlisz klawisz

        currentKey = keyPool[Random.Range(0, keyPool.Length)];
        keyDisplay.text = currentKey; // Wyświetl klawisz

        yield return new WaitForSeconds(reactionTime); // Odczekaj czas reakcji gracza

        if (currentKey != "") // Jeśli klawisz nadal aktywny (brak reakcji gracza)
        {
            ResetProgress(); // Zresetuj progres
            StartNewKeyCycle(); // Rozpocznij od nowa
        }
    }

    void CheckKey(string pressedKey)
    {
        if (pressedKey == currentKey)
        {
            // Poprawny klawisz - zwiększ progres
            bool isComplete = progressObjects[currentProgressObjectIndex].IncreaseProgress();

            if (isComplete)
            {
                if (currentProgressObjectIndex == progressObjects.Count - 1)
                {
                    WinGame(); // Ostatni obiekt ukończony
                }
                else
                {
                    ActivateNextObject(); // Przejdź do kolejnego obiektu
                }
            }
            else
            {
                StartNewKeyCycle(); // Rozpocznij nowy cykl
            }
        }
        else
        {
            // Błędny klawisz - resetuj progres
            ResetProgress();
            StartNewKeyCycle();
        }
    }

    void ResetProgress()
    {
        progressObjects[currentProgressObjectIndex].ResetProgress();
    }

    void ActivateNextObject()
    {
        // Dezaktywuj obecny obiekt
        //progressObjects[currentProgressObjectIndex].SetVisibility(false);

        // Przejdź do następnego obiektu
        currentProgressObjectIndex++;
        progressObjects[currentProgressObjectIndex].SetVisibility(true);
        
        keyDisplay.GetComponent<RectTransform>().localPosition =
            progressObjects[currentProgressObjectIndex].GetComponent<RectTransform>().localPosition + new Vector3(0, 100, 0);

        
        StartNewKeyCycle(); // Rozpocznij cykl dla nowego obiektu
    }

    void WinGame()
    {
        // Wygrana - wyświetl komunikat i zatrzymaj dalsze akcje
        winMessage.gameObject.SetActive(true);
        keyDisplay.text = "";
        currentKey = ""; // Wyłącz aktywny klawisz
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine); // Zatrzymaj korutynę
        }
        
        onMiniGameEnd?.Invoke();
        Destroy(gameObject);
    }
}
