using UnityEngine;
using System.Collections.Generic;

public class PrisonerMerchant : MonoBehaviour, IInteractable
{
    public List<TradePoolSO> tradePools; // Lista dostępnych TradePoolSO
    private TradeOffer tradeOffer;      // Wygenerowana oferta handlowa
    public TradeUI tradeUI;            // UI do wyświetlania oferty
    private SpriteRenderer spriteRenderer;
    private bool boughtOut;            // Flaga informująca, czy transakcja została zakończona

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Losujemy TradePool z dostępnych opcji
        if (tradePools == null || tradePools.Count == 0)
        {
            Debug.LogError("No trade pools assigned to the merchant!");
            return;
        }

        // Losuj jeden z TradePoolSO
        TradePoolSO selectedPool = GetRandomTradePool();

        // Generujemy ofertę handlową
        tradeOffer = selectedPool?.GetRandomOffer();
        if (tradeOffer == null)
        {
            Debug.LogWarning("No valid trade offer could be generated!");
            return;
        }

        // Konfigurujemy UI oferty
        tradeUI.SetUp(tradeOffer);
    }

    void Start()
    {
        // Wyświetlamy UI oferty na początku
        tradeUI.ShowUI();
    }

    public void Interact()
    {
        if (boughtOut)
        {
            spriteRenderer.color = Color.red;
            Debug.Log("Bought out");
            return;
        }

        spriteRenderer.color = Color.green;

        // Sprawdź ekwipunek gracza
        PlayerEq playerEq = GameManager.Instance.PlayerEq;

        if (playerEq.ContainsItem(tradeOffer.desiredItem.Type))
        {
            // Usuń przedmiot i dodaj nowy, jeśli wymiana się udała
            if (playerEq.removeItem(tradeOffer.desiredItem) &&
                playerEq.addItem(tradeOffer.offeredItem))
            {
                boughtOut = true;
                tradeUI.HideUI();
            }
        }
    }

    public void OnPlayerEnter()
    {
        spriteRenderer.color = Color.yellow; // Sygnalizacja wejścia w interakcję
    }

    public void OnPlayerExit()
    {
        spriteRenderer.color = Color.white; // Powrót do normalnego wyglądu
    }

    /// <summary>
    /// Losuje jeden TradePoolSO z listy.
    /// </summary>
    /// <returns>Zwraca losowo wybrany TradePoolSO.</returns>
    private TradePoolSO GetRandomTradePool()
    {
        int randomIndex = Random.Range(0, tradePools.Count);
        return tradePools[randomIndex];
    }
}
