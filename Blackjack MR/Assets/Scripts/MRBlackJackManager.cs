using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRBlackJackManager : MonoBehaviour
{
    private GameObject[] cardPrefabs;
    private Transform playerCardSpawn;
    private Transform dealerCardSpawn;
    public Vector3 cardOffset = new Vector3(1.0f, 0, 0);
    private List<Card> playerCards = new List<Card>();
    private List<Card> dealerCards = new List<Card>();
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioSource audioSource;
    private bool playerTurn = false;

    void Start()
    {
        cardPrefabs = Resources.LoadAll<GameObject>("Cards");
        if (cardPrefabs.Length == 0)
            Debug.LogError("No card prefabs found in Resources/Cards!");
    }

    public void BeginGame()
    {
        GameObject playerObj = GameObject.Find("PlayerCard");
        GameObject dealerObj = GameObject.Find("AICard");
        if (playerObj == null || dealerObj == null)
            return;

        playerCardSpawn = playerObj.transform;
        dealerCardSpawn = dealerObj.transform;
        OnStartButtonPressed();
    }

    public void OnStartButtonPressed()
    {
        ResetGame();
        SpawnCardForPlayer();
        SpawnCardForPlayer();
        SpawnCardForDealer(false);
        SpawnCardForDealer(false);
        playerTurn = true;
    }

    public void OnHitButtonPressed()
    {
        if (!playerTurn)
            return;

        SpawnCardForPlayer();
        int playerTotal = CalculateHandValue(playerCards);
        if (playerTotal > 21)
            EndPlayerTurn();
    }

    public void OnStandButtonPressed()
    {
        if (!playerTurn)
            return;
        EndPlayerTurn();
    }

    private void EndPlayerTurn()
    {
        playerTurn = false;
        foreach (Card card in dealerCards)
            card.SetFaceDown(false);
        StartCoroutine(DealerTurn());
    }

    private IEnumerator DealerTurn()
    {
        yield return new WaitForSeconds(1f);
        int dealerTotal = CalculateHandValue(dealerCards);
        while (dealerTotal < 17)
        {
            SpawnCardForDealer(true);
            yield return new WaitForSeconds(1f);
            dealerTotal = CalculateHandValue(dealerCards);
        }
        int playerTotal = CalculateHandValue(playerCards);
        string outcome;
        if (playerTotal > 21)
            outcome = "Lose";
        else if (dealerTotal > 21)
            outcome = "Win";
        else if (playerTotal > dealerTotal)
            outcome = "Win";
        else if (playerTotal < dealerTotal)
            outcome = "Lose";
        else
            outcome = "Push";
        PlayOutcomeSound(outcome);
    }

    private void PlayOutcomeSound(string outcome)
    {
        if (audioSource == null)
            return;
        if (outcome == "Win")
            audioSource.PlayOneShot(winSound);
        else if (outcome == "Lose")
            audioSource.PlayOneShot(loseSound);
    }

    private void SpawnCardForPlayer()
    {
        if (playerCardSpawn == null)
            return;
        GameObject cardObj = InstantiateRandomCard(playerCardSpawn, playerCards.Count);
        Card card = cardObj.GetComponent<Card>();
        if (card != null)
            playerCards.Add(card);
    }

    private void SpawnCardForDealer(bool flipImmediately)
    {
        if (dealerCardSpawn == null)
            return;
        GameObject cardObj = InstantiateRandomCard(dealerCardSpawn, dealerCards.Count);
        Card card = cardObj.GetComponent<Card>();
        if (card != null)
        {
            dealerCards.Add(card);
            card.SetFaceDown(!flipImmediately);
        }
    }

    private GameObject InstantiateRandomCard(Transform spawnPoint, int cardIndex)
    {
        int randomIndex = Random.Range(0, cardPrefabs.Length);
        Vector3 spawnPosition = spawnPoint.position + cardOffset * cardIndex;
        return Instantiate(cardPrefabs[randomIndex], spawnPosition, spawnPoint.rotation);
    }

    private int GetCardValue(Card card)
    {
        if (card.rank == "Ace") return 1;
        if (card.rank == "King" || card.rank == "Queen" || card.rank == "Jack") return 10;
        int value;
        return int.TryParse(card.rank, out value) ? value : 0;
    }

    private int CalculateHandValue(List<Card> cards)
    {
        int total = 0, aceCount = 0;
        foreach (Card card in cards)
        {
            int cardVal = GetCardValue(card);
            total += cardVal;
            if (card.rank == "Ace") aceCount++;
        }
        while (aceCount > 0 && total + 10 <= 21)
        {
            total += 10;
            aceCount--;
        }
        return total;
    }

    private void ResetGame()
    {
        foreach (Card card in playerCards)
            if (card != null) Destroy(card.gameObject);
        foreach (Card card in dealerCards)
            if (card != null) Destroy(card.gameObject);
        playerCards.Clear();
        dealerCards.Clear();
    }
}
