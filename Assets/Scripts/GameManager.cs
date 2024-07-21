using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region 
    private static CameraFollower instance;
    public static CameraFollower Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<CameraFollower>();
            }
            return instance;
        }
    }
    #endregion 
    [Header("Stuff")]
    [SerializeField] private int requiredNumber = 20;
    [SerializeField] private TextMeshProUGUI trashCountUI;
    [SerializeField] private TextMeshProUGUI timerUI;
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private float duration = 120f;
    [SerializeField] private Player player;
    private float currentTime;
    private bool gameOver;
    private void Start()
    {
        currentTime = Time.time + duration;
    }
    private void Update()
    {
        timerUI.text = $"Timer: {(int)(currentTime - Time.time)}";
        trashCountUI.text = $"Trash Collected: {player.trashCollected} / {requiredNumber}";

        if (!gameOver && Time.time >= currentTime)
        {
            gameOver = true;
            if (player.trashCollected >= requiredNumber)
            {
                player.Fat();
            }
            else
            {
                player.Starve();
            }
            EndGame();
        }
    }
    private void EndGame()
    {
        
    }
}
