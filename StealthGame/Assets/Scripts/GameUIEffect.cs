using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIEffect : MonoBehaviour
{
    [SerializeField] private Image backImage;
    [SerializeField] private Image uiImage;
    [SerializeField] private float fadeTime;
    [SerializeField] private string nextSceneName;
    private float backTargetAlpha = 0.0f;
    private float uiTargetAlpha = 0.0f;
    private float elapsed = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backTargetAlpha = backImage.color.a;
        uiTargetAlpha = uiImage.color.a;

        ApplyFade(0);

        //  ジングル再生
        StartCoroutine(AudioPlayEndWait());
    }

    // Update is called once per frame
    void Update()
    {
        //  経過時間を算出
        elapsed += Time.deltaTime;
        var rate = Mathf.Clamp01(elapsed / fadeTime);   //  [elapsed / fadeTime]を0.0～1.0の範囲にクランプする

        // フェード処理
        ApplyFade(rate);
    }

    //  フェード処理
    private void ApplyFade(float rate)
    {
        //  各ImageのAlpha値を計算
        var backAlpha = Mathf.Lerp(0, backTargetAlpha, rate);
        var uiAlpha = Mathf.Lerp(0, uiTargetAlpha, rate);

        //  Imageに反映(背景)
        var backColor = backImage.color;
        backColor.a = backAlpha;
        backImage.color = backColor;

        //  Imageに反映(画像)
        var uiColor = uiImage.color;
        uiColor.a = uiAlpha;
        uiImage.color = uiColor;
    }

    private IEnumerator AudioPlayEndWait()
    {
        //  ジングル再生
        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        //  ジングル再生終了待ち
        yield return new WaitForSeconds(audioSource.clip.length);

        //  次シーンの読み込み
        SceneManager.LoadScene(nextSceneName);
    }
}
