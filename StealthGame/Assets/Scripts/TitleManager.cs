using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //  Startボタンが押された際の処理
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    //  Quitボタンが押された際の処理
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
