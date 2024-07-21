using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region 
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
            }
            return instance;
        }
    }
    #endregion 
    [Header("Stuff")]
    [SerializeField] private int requiredNumber = 20;
    [SerializeField] private TextMeshProUGUI trashCountUI;
    [SerializeField] private TextMeshProUGUI timerUI;
    [SerializeField] private GameObject restartPanelWin;
    [SerializeField] private GameObject restartPanelLose;
    [SerializeField] private float duration = 120f;
    [SerializeField] private Player player;
    private float currentTime;
    private bool gameOver;
    private void Start()
    {
        currentTime = Time.time + duration;
        player = FindAnyObjectByType<Player>();
    }
    private void Update()
    {
        timerUI.text = $"Timer: {Mathf.Clamp((int)(currentTime - Time.time), 0, currentTime)}";
        trashCountUI.text = $"Trash: {player.trashCollected} / {requiredNumber}";

        if (!gameOver && Time.time >= currentTime)
        { 
            if (player.trashCollected >= requiredNumber)
            {
                player.Fat();
                WinGame();
            }
            else
            {
                player.Starve();
                EndGame();
            }
            
        }
    }
    private IEnumerator StartEndGame()
    {
        yield return new WaitForSeconds(2f);
        restartPanelLose.SetActive(true);
    }
    public void EndGame()
    {
        gameOver = true;
        StartCoroutine(StartEndGame());
    }
    private IEnumerator StartWinGame()
    {
        yield return new WaitForSeconds(2f);
        restartPanelWin.SetActive(true);
    }
    public void WinGame()
    {
        gameOver = true;
        StartCoroutine(StartWinGame());
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
