using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LevelManager : Singleton<LevelManager>
{
    private int currentScore = 0;
    public int winScore = 100;


    private CollectibleSpawner[] spawners;
    [HideInInspector]public Player player;

    private Matrix4x4 playerInitialPos; //transform,rotation,scale information. To use replace player to its initial place after restart. Im using TRS matrix for just a fantasy.
    
    /// <summary>
    /// Spawners, player and playerInitialPosition are defined.
    /// Level initialize.
    /// </summary>
    private void Start()
    {
        spawners = gameObject.GetComponentsInChildren<CollectibleSpawner>();
        player = gameObject.GetComponentInChildren<Player>();
        playerInitialPos = Matrix4x4.TRS(player.transform.localPosition, player.transform.localRotation, player.transform.localScale);
        InitializeLevel();
    }

    /// <summary>
    /// Set player position and rotation
    /// Reset score (also in the UI)
    /// and activate spawners.
    /// </summary>
    public void InitializeLevel()
    {

        player.transform.localPosition = playerInitialPos.MultiplyPoint(Vector3.zero);
        player.transform.rotation = playerInitialPos.rotation;

        foreach (var spawner in spawners)
        {
            spawner.ApplyInitialSpawn();
        }

        currentScore = 0;
        UIManager.Instance.SetScoreText(currentScore);
    }

    private void Update()
    {
        CheckWinCondition();
    }

    public void AddScore(int score)
    {
        UIManager.Instance.scoreText.transform.DOKill();
        currentScore += score;
        UIManager.Instance.scoreText.transform.DOScale(1.1f, 0.1f).OnComplete(() => {
            UIManager.Instance.scoreText.transform.localScale = Vector3.one;
        });
        UIManager.Instance.SetScoreText(currentScore);
    }

    private void CheckWinCondition()
    {
        if(currentScore >= winScore)
        {
            UIManager.Instance.OpenWinMenuUI();
        }
    }
}
