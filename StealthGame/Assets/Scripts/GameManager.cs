using TMPro;
using UnityEngine;

public enum GameState
{
    Playing,
    GameOver,
    GameClear
}

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameclearUI;
    [SerializeField] TextMeshProUGUI gameTimeText;

    private float gameTime = 0.0f;
    private GameState gameState = GameState.Playing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //  GameOver / GameClearを非表示にする
        gameOverUI.SetActive(false);
        gameclearUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        gameTimeText.text = "Time: " + gameTime.ToString("F1");
    }

    //  GameOverに移行
    public void GameOver()
    {
        //  重複呼び出しをしないように判定する
        if (gameState != GameState.GameOver)
        {
            gameState = GameState.GameOver;     //  GameOverに変更
            gameOverUI.SetActive(true);         //  GameOverUIを表示
        }
    }

    //  GameClearに移行
    public void GameClear()
    {
        //  重複呼び出しをしないように判定する
        if (gameState != GameState.GameClear)
        {
            gameState = GameState.GameClear;    //  GameClearに変更
            gameclearUI.SetActive(true);        //  GameClearUIを表示
        }
    }
}
