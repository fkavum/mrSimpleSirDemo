using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    [Header("Texts")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText; //for the final screen.

    [Header("UI panels")]
    public GameObject startMenuUI;
    public GameObject winMenuUI;
    public GameObject ingameUI;

    private void Start()
    {
        CloseAllUI();
        startMenuUI.SetActive(true);
        GameState.ChangeState(GameStateEnum.GameStart);
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void OpenWinMenuUI()
    {
        finalScoreText.text = scoreText.text; //hehe
        CloseAllUI();
        winMenuUI.SetActive(true);
        GameState.ChangeState(GameStateEnum.EndGame);
    }

    public void OpenLoseMenuUI()
    {
        CloseAllUI();
        winMenuUI.SetActive(true);
        GameState.ChangeState(GameStateEnum.EndGame);
    }

    /// <summary>
    /// When you click start button this function activate
    /// </summary>
    public void StartLevelButton()
    {
        CloseAllUI();
        ingameUI.SetActive(true);
        GameState.ChangeState(GameStateEnum.InGame);
    }
    /// <summary>
    /// When you click restart button this function activate.
    /// </summary>
    public void RetryLevelButton()
    {
        CloseAllUI();
        ingameUI.SetActive(true);
        GameState.ChangeState(GameStateEnum.InGame);
        LevelManager.Instance.InitializeLevel();  //Reinitialize level.
    }

    private void CloseAllUI()
    {
        startMenuUI.SetActive(false);
        winMenuUI.SetActive(false);
        ingameUI.SetActive(false);
    }
}
