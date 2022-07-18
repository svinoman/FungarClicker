using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    public static bool isPlaying = false;
    [SerializeField] private GameObject menu;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _bestScoreText;
    private bool _isMenuActive = true;
    private bool _fadeOutFix;
    private float _scoreTimer;
    private void Update()
    {
        CheckIfGameHasStarted();
        CheckIfMenuBoolIsTrue();
        ScoreTimer();
        ScoreSystem();
        if (menu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MenuDisable"))
        {
            _isMenuActive = false;
        }
    }
    public void ToggleMenu()
    {
        if (!_isMenuActive)
        {
            _fadeOutFix = false;
            _isMenuActive = true;
        }
        else
        {
            if (!_fadeOutFix)
            {
                menu.GetComponent<Animator>().Play("MenuFadeOut");
                _fadeOutFix = true;
            }
        }
    }
    private void CheckIfMenuBoolIsTrue()
    {
        if (!_isMenuActive)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }
    private void CheckIfGameHasStarted()
    {
        if (isPlaying)
        {
            if (!_fadeOutFix)
            {
                menu.GetComponent<Animator>().Play("MenuFadeOut");
                _fadeOutFix = true;
            }
        }
        else
        {
            _fadeOutFix = false;
            _isMenuActive = true;
        }

    }
    private void ScoreTimer()
    {
        if(isPlaying)
        {
            _scoreTimer += Time.deltaTime;
            if (_scoreTimer >= 1)
            {
                GribusAI.score++;
                _scoreTimer = 0;
            }
        }
    }
    private void ScoreSystem()
    {
        _scoreText.text = ("Score: " + GribusAI.score);
        if(!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }
        _bestScoreText.text = ("Best Score: " + PlayerPrefs.GetInt("BestScore"));
    }

    public void Play()
    {
        GameObject.FindGameObjectWithTag("GribusManager").GetComponent<GribusSpawner>().clearLevel = true;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
