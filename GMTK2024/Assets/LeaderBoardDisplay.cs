using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardDisplay : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardEntryPrefab; // Prefab para una entrada de la tabla de clasificaci�n
    [SerializeField] private Transform leaderboardParent; // Donde se instancian las entradas del leaderboard

    public void FetchAndDisplayLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScore", // Nombre del estad�stico en la tabla de clasificaci�n
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardError);
    }

    private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        // Limpia entradas anteriores
        foreach (Transform child in leaderboardParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var entry in result.Leaderboard)
        {
            GameObject entryObject = Instantiate(leaderboardEntryPrefab, leaderboardParent);
            TextMeshProUGUI positionText = entryObject.transform.Find("PositionText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = entryObject.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI playerNameText = entryObject.transform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>();

            positionText.text = (entry.Position + 1).ToString(); // Posici�n en la lista (1-indexed)
            scoreText.text = entry.StatValue.ToString(); // Puntaje del jugador
            playerNameText.text = entry.DisplayName; // Nombre del jugador
        }
    }

    private void OnGetLeaderboardError(PlayFabError error)
    {
        Debug.LogError("Error al obtener el leaderboard: " + error.GenerateErrorReport());
    }
}
