using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ProgressObject:MonoBehaviour
{
    public RectTransform screwImage;   // Obraz śrubki (do rotacji)
    public Slider progressBar;         // Pasek postępu (opcjonalnie widoczny)

    public int maxProgress = 5;      // Maksymalna wartość postępu
    private int currentProgress = 0;   // Aktualny postęp

    /// <summary>
    /// Resetuje stan obiektu (rotację i pasek postępu).
    /// </summary>
    public void ResetProgress()
    {
        currentProgress = 0;
        UpdateProgressBar();
        UpdateScrewRotation();
        if (screwImage != null)
        {
            screwImage.gameObject.SetActive(true);
            progressBar.maxValue = maxProgress;
        }
    }

    /// <summary>
    /// Aktualizuje progres o jedną jednostkę. Zwraca true, jeśli osiągnięto maksymalny progres.
    /// </summary>
    public bool IncreaseProgress()
    {
        currentProgress++;
        UpdateProgressBar();
        UpdateScrewRotation();
        return currentProgress >= maxProgress;
    }

    /// <summary>
    /// Ustawia maksymalny progres dla obiektu.
    /// </summary>
    public void SetMaxProgress(int maxProgress)
    {
        this.maxProgress = maxProgress;
        if (progressBar != null)
        {
            progressBar.maxValue = maxProgress;
        }
    }

    /// <summary>
    /// Aktualizuje pasek postępu na podstawie bieżącego stanu.
    /// </summary>
    private void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            progressBar.value = currentProgress;
        }
    }

    /// <summary>
    /// Obraca obraz śrubki na podstawie bieżącego stanu progresu.
    /// </summary>
    private void UpdateScrewRotation()
    {
        if (screwImage != null)
        {
            float progressPercent = (float)currentProgress / maxProgress;
            float rotationAngle = Mathf.Lerp(0, 360, progressPercent);
            screwImage.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
    }

    /// <summary>
    /// Ustawia widoczność obiektu (śrubki i paska).
    /// </summary>
    public void SetVisibility(bool isVisible)
    {
        
        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(isVisible);
        }
    }
}
