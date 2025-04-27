using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlackjackGameManager : MonoBehaviour
{
    private GameObject[] cardPrefabs;
    public Transform playerCardSpawn;
    public Transform dealerCardSpawn;

    //Card offset (set in inspector X value change) or just 1 here i guess
    public Vector3 cardOffset = new Vector3(1.0f, 0, 0);


    private List<Card> playerCards = new List<Card>();
    private List<Card> dealerCards = new List<Card>();

    // Audio
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioSource audioSource;

    // Canvas
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI dealerScoreText;
    public TextMeshProUGUI outcomeText;

    // Check player turn
    private bool playerTurn = false;

    void Start()
    {
        cardPrefabs = Resources.LoadAll<GameObject>("Cards");
        if (cardPrefabs.Length == 0)
            Debug.LogError("No card prefabs found in Resources/Cards!");
    }

    public void OnStartButtonPressed()
    {
        Debug.Log("Start Button Pressed on instance: " + this.GetInstanceID());
        ResetGame();
        if (playerScoreText != null)
            playerScoreText.text = "Player: 0";
        if (dealerScoreText != null)
            dealerScoreText.text = "Dealer: ?";
        if (outcomeText != null)
            outcomeText.text = "";

        // Spawn card
        SpawnCardForPlayer();
        SpawnCardForPlayer();

        // Spawn card dealer
        SpawnCardForDealer(false);
        SpawnCardForDealer(false);
        
        UpdatePlayerScore();
        playerTurn = true;
        Debug.Log("Game started. It's the player's turn.");
    }

    public void OnHitButtonPressed()
    {
        Debug.Log("Hit Button Pressed");
        SpawnCardForPlayer();
        UpdatePlayerScore();
        int playerTotal = CalculateHandValue(playerCards);
        Debug.Log(playerTotal);
        if (playerTotal > 21)
        {
            Debug.Log("Player busts");
            EndPlayerTurn();
        }
    }

    public void OnStandButtonPressed()
    {
        Debug.Log("Stand Button Pressed");
        EndPlayerTurn();
    }

    private void EndPlayerTurn()
    {
        Debug.Log("Ending player turn.");
        playerTurn = false;
        foreach (Card card in dealerCards)
        {
            card.SetFaceDown(false);
        }
        UpdateDealerScore();
        StartCoroutine(DealerTurn());
    }

    private IEnumerator DealerTurn()
    {
        Debug.Log("Dealer turn started.");
        
        //TIME DELAY TEST -- CHECK LATER
        yield return new WaitForSeconds(1f);

        int dealerTotal = CalculateHandValue(dealerCards);
        Debug.Log("Dealer starting total: " + dealerTotal);
        UpdateDealerScore();
        while (dealerTotal < 17)
        {
            SpawnCardForDealer(true);
            yield return new WaitForSeconds(1f);
            dealerTotal = CalculateHandValue(dealerCards);
            Debug.Log("Dealer new total: " + dealerTotal);
            UpdateDealerScore();
        }
        int playerTotal = CalculateHandValue(playerCards);
        string outcome = "";
        if (playerTotal > 21)
        {
            outcome = "Lose";
        }
        else if (dealerTotal > 21)
        {
            outcome = "Win";
        }
        else if (playerTotal > dealerTotal)
        {
            outcome = "Win";
        }
        else if (playerTotal < dealerTotal)
        {
            outcome = "Lose";
        }
        else
        {
            outcome = "Push";
        }
        Debug.Log("Game outcome: " + outcome);
        if (outcomeText != null)
            outcomeText.text = outcome;
        PlayOutcomeSound(outcome);
    }

    private void PlayOutcomeSound(string outcome)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource not set/something wrong line 145");
            return;
        }
        if (outcome == "Win")
        {
            audioSource.PlayOneShot(winSound);
        }
        else if (outcome == "Lose")
        {
            audioSource.PlayOneShot(loseSound);
        }
        else
        {
            Debug.Log("Push = no sound (tie)");
        }
    }

    private void SpawnCardForPlayer()
    {
        GameObject cardObj = InstantiateRandomCard(playerCardSpawn, playerCards.Count);
        Card card = cardObj.GetComponent<Card>();
        if (card != null)
        {
            playerCards.Add(card);
        }
    }
    
    //Fixed flipping, change rotation of empty to 180
    private void SpawnCardForDealer(bool flipImmediately)
    {
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
        GameObject cardObj = Instantiate(cardPrefabs[randomIndex], spawnPosition, spawnPoint.rotation);
        return cardObj;
    }

    private int GetCardValue(Card card)
    {
        if (card.rank == "Ace")
            return 1;
        if (card.rank == "King" || card.rank == "Queen" || card.rank == "Jack")
            return 10;
        int value;
        if (int.TryParse(card.rank, out value))
            return value;
        return 0;
    }

    private int CalculateHandValue(List<Card> cards)
    {
        int total = 0;
        int aceCount = 0;
        foreach (Card card in cards)
        {
            int cardVal = GetCardValue(card);
            total += cardVal;
            if (card.rank == "Ace")
                aceCount++;
        }
        while (aceCount > 0 && total + 10 <= 21)
        {
            total += 10;
            aceCount--;
        }
        return total;
    }

    private void UpdatePlayerScore()
    {
        int total = CalculateHandValue(playerCards);
        if (playerScoreText != null)
            playerScoreText.text = "Player: " + total;
    }

    private void UpdateDealerScore()
    {
        int total = CalculateHandValue(dealerCards);
        if (dealerScoreText != null)
            dealerScoreText.text = "Dealer: " + total;
    }

    private void ResetGame()
    {
        Debug.Log("Resetting game state.");
        foreach (Card card in playerCards)
        {
            if (card != null)
                Destroy(card.gameObject);
        }
        foreach (Card card in dealerCards)
        {
            if (card != null)
                Destroy(card.gameObject);
        }
        playerCards.Clear();
        dealerCards.Clear();
    }
}
