using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    public void SendScoreToLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore", // Nombre del estadístico en la tabla de clasificación
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateScoreSuccess, OnUpdateScoreError);
    }

    private void OnUpdateScoreSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Puntaje actualizado exitosamente.");
    }

    private void OnUpdateScoreError(PlayFabError error)
    {
        Debug.LogError("Error al actualizar el puntaje: " + error.GenerateErrorReport());
    }
}
