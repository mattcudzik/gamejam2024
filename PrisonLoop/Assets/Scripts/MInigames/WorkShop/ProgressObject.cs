using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ProgressObject:MonoBehaviour
{
    public RectTransform screwImage;   // Obraz śrubki (do rotacji)
    public Slider progressBar;         // Pasek postępu (opcjonalnie widoczny)

    public int maxProgress = 5;      // Maksymalna wartość postępu
    private int currentProgress = 0;   // Aktualny postęp


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

 
    public bool IncreaseProgress()
    {
        currentProgress++;
        UpdateProgressBar();
        UpdateScrewRotation();
        return currentProgress >= maxProgress;
    }


    public void SetMaxProgress(int maxProgress)
    {
        this.maxProgress = maxProgress;
        if (progressBar != null)
        {
            progressBar.maxValue = maxProgress;
        }
    }

 
    private void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            progressBar.value = currentProgress;
        }
    }


    private void UpdateScrewRotation()
    {
        if (screwImage != null)
        {
            float progressPercent = (float)currentProgress / maxProgress;
            float rotationAngle = Mathf.Lerp(0, 360, progressPercent);
            screwImage.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
    }

    public void SetVisibility(bool isVisible)
    {
        
        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(isVisible);
        }
    }
}
