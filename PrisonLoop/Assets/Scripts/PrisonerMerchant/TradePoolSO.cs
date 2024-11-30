using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TradePoolSO", menuName = "Scriptable Objects/Trade Pool")]
public class TradePoolSO : ScriptableObject
{
    [System.Serializable]
    public class TradeOption
    {
        public ItemSO offeredItem; // Przedmiot oferowany
        [Range(0f, 1f)] public float probability; // Prawdopodobieństwo pojawienia się tego przedmiotu
    }

    public ItemSO desiredItem; // Przedmiot wymagany od gracza
    public List<TradeOption> options; // Pula przedmiotów, które mogą być oferowane w zamian

    /// <summary>
    /// Losuje TradeOffer na podstawie zdefiniowanych prawdopodobieństw.
    /// </summary>
    /// <returns>Zwraca wygenerowaną ofertę handlową lub null, jeśli brak możliwych opcji.</returns>
    public TradeOffer GetRandomOffer()
    {
        float totalProbability = 0f;

        // Obliczamy sumę prawdopodobieństw
        foreach (var option in options)
        {
            totalProbability += option.probability;
        }

        if (totalProbability <= 0)
        {
            Debug.LogWarning("Total probability is zero or negative. No valid options to choose from.");
            return null;
        }

        // Generujemy losową wartość w zakresie [0, totalProbability]
        float randomPoint = Random.Range(0f, totalProbability);

        // Szukamy pasującego przedmiotu na podstawie losowego punktu
        float cumulativeProbability = 0f;
        foreach (var option in options)
        {
            cumulativeProbability += option.probability;
            if (randomPoint <= cumulativeProbability)
            {
                // Tworzymy nową ofertę handlową
                return new TradeOffer
                {
                    desiredItem = this.desiredItem,
                    offeredItem = option.offeredItem
                };
            }
        }

        // Jeśli coś poszło nie tak, zwracamy null (nie powinno się zdarzyć)
        Debug.LogError("Failed to select a valid trade offer from the pool.");
        return null;
    }
}
