using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections; // Potrzebne dla Coroutine

public class QuickTimeEventGame : MonoBehaviour
{
    public TMP_Text keyDisplay;       // Wyświetlanie klawisza (TextMeshPro)
    public Slider progressBar;        // Pasek postępu
    public TMP_Text winMessage;       // Komunikat o wygranej (TextMeshPro)

    private string[] keyPool = { "A", "S", "D", "F" }; // Pula klawiszy
    private string currentKey;        // Aktualny klawisz
    private int progress = 0;         // Aktualny postęp
    private int maxProgress = 10;     // Maksymalny postęp

    public float keyDisplayDelay = 1.0f; // Czas opóźnienia wyświetlenia klawisza
    public float reactionTime = 3.0f;   // Czas na reakcję gracza

    private Coroutine currentCoroutine; // Referencja do aktywnej korutyny

    void Start()
    {
        winMessage.gameObject.SetActive(false);
        progressBar.value = 0;
        progressBar.maxValue = maxProgress;

        StartNewKeyCycle();
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
        // Rozpocznij cykl: wylosowanie klawisza i uruchomienie korutyny
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
            progress++;
            progressBar.value = progress;

            if (progress >= maxProgress)
            {
                WinGame();
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
        progress = 0;
        progressBar.value = progress;
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
    }
}
