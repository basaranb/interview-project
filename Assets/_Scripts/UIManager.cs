using DG.Tweening;
using UnityEngine;
using System.Collections;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    [SerializeField] Canvas MainMenu, PauseMenu, RetryMenu, GamePlayMenu;
    public GameManager GameManager { get; private set; }
    private GameObject PlayBtn;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        GameManager = FindObjectOfType<GameManager>();
        PlayBtn = MainMenu.transform.Find("PlayBtn").gameObject;
        PlayBtnPulse();
    }
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    public void Play()
    {
        MainMenu.transform.DOMoveX(-150, 1.0f).SetEase(Ease.InBack);
        GamePlayMenu.transform.DOMoveX(0, 1f).SetEase(Ease.InBack).OnComplete(GameManager.StartGame);

    }
    public void Home()
    {
        MainMenu.transform.DOMoveX(0, 1.0f).SetEase(Ease.InBack);
        GamePlayMenu.transform.DOMoveX(150, 1f).SetEase(Ease.InBack);
        RetryMenu.transform.DOMoveX(-150, 1f).SetEase(Ease.InBack);
        PauseMenu.transform.DOMoveY(-150, 1f).SetEase(Ease.InBack);
        GameManager.Continue();
        PlayBtnPulse();
    }
    public void Pause()
    {

        GamePlayMenu.transform.DOMoveX(150, 1f).SetEase(Ease.InBack);
        PauseMenu.transform.DOMoveY(0, 1f).SetEase(Ease.InBack).OnComplete(GameManager.Pause);

    }
    public void Continue()
    {
        PauseMenu.transform.DOMoveY(-150, 1f).SetEase(Ease.OutSine);
        GamePlayMenu.transform.DOMoveX(0, 1f).SetEase(Ease.InBack);
        GameManager.Continue();
    }
    public void Retry()
    {
        GameManager.Continue();
        GamePlayMenu.transform.DOMoveX(0, 1f).SetEase(Ease.InBack);
        RetryMenu.transform.DOMoveX(-150, 1f).SetEase(Ease.InBack).OnComplete(GameManager.StartGame);

    }

    public void Loose()
    {
        GamePlayMenu.transform.DOMoveX(150, 1f).SetEase(Ease.OutSine);
        RetryMenu.transform.DOMoveX(0, 1f).SetEase(Ease.InBack).OnComplete(GameManager.Pause);

    }
    private void PlayBtnPulse()
    {
        PlayBtn.transform.DOScale(9f, 0.4f).SetLoops(-1, LoopType.Yoyo);
    }


}

