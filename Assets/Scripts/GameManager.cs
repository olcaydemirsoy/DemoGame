using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [Header("Diamond Object Settings")]
    [Tooltip("Elmas objesi.")] public GameObject Diamond;
    [Tooltip("Puan toplam� g�stergesi.")] public int score;
    [Tooltip("Her bir elmas al�nd���nda ka� puan eklenir?")] public int scoreHitPoint;
    [Tooltip("En fazla ka� puan al�nabilir?")] public int maxScorePoint;
    [Tooltip("Puan g�steren text objesi")] public TextMeshProUGUI scroreText;
    [Tooltip("Ayn� anda ka� elmas olabilir?")] public int maxDiamondCount;

    [Header("Game Start/End")]
    [Tooltip("Oyun Aktif/Pasif kontrol�")] public bool gameActive = false;
    public GameObject startPanel;
    public GameObject endPanel;
    [Tooltip("Oyun sonu g�r�nen puan")] public TextMeshProUGUI gameEndScore;
    [Tooltip("Oyun en fazla ne kadar s�rebilir?")] public float gameTime;
    public TextMeshProUGUI gameTimeText;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {

            Destroy(gameManager.gameObject);
            gameManager = this;

        }

        DontDestroyOnLoad(this.gameObject);

    }

    private void Update()
    {

        GameActions();
    }

    private void GameActions()
    {
        if (score == maxScorePoint || gameTime < 0)
        {
            gameActive = false;
            endPanel.SetActive(true);
            gameEndScore.text = "Score : " + score.ToString();
        }
        if (gameActive)
        {
            gameTime -= Time.deltaTime;
            gameTimeText.text = ((int)gameTime).ToString();
            DiamondControl();
        }
    }

    private void DiamondControl()
    {
        GameObject[] diamonds = GameObject.FindGameObjectsWithTag("Diamond");

        if (diamonds.Length < maxDiamondCount)
        {
            GameObject goDiamond = Instantiate(Diamond, ReturnDiamondPosition(), Quaternion.identity);
            goDiamond.transform.parent = GameObject.Find("Start Diamond").transform;
            goDiamond.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        }
    }

    public Vector3 ReturnDiamondPosition()
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(44f, 50f), 1.24f, UnityEngine.Random.Range(30f, 48f));
        return position;
    }


    public void StartGame()
    {
        startPanel.SetActive(false);
        gameActive = true;
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
