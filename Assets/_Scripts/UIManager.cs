using DG.Tweening;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    [SerializeField] Canvas MainMenu, PauseMenu, RetryMenu, GamePlayMenu;
    public GameManager GameManager { get; private set; }
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
        GamePlayMenu.transform.DOMoveX(0, 1f).SetEase(Ease.InBack);
        GameManager.StartGame();
    }
    public void Home()
    {
        GameManager.Home();
        MainMenu.transform.DOMoveX(0, 1.0f).SetEase(Ease.InBack);
        GamePlayMenu.transform.DOMoveX(150, 1f).SetEase(Ease.InBack);

    }
    public void Pause()
    {

        GamePlayMenu.transform.DOMoveX(150, 1f).SetEase(Ease.InBack);
        PauseMenu.transform.DOMoveY(0, 1f).SetEase(Ease.InBack).OnComplete(GameManager.Pause);

    }
    public void Continue()
    {
        PauseMenu.transform.DOMoveY(-150, 1f).SetEase(Ease.InBack);
        GamePlayMenu.transform.DOMoveX(0, 1f).SetEase(Ease.InBack);
        GameManager.Continue();
    }
    public void Retry()
    {
        MainMenu.transform.position = new Vector3(-150, -11, 100);
        GamePlayMenu.transform.position = new Vector3(0, 0, 100);
        RetryMenu.transform.DOMoveX(-150, 1f).SetEase(Ease.InBack).OnComplete(GameManager.StartGame);

    }

    public void Loose()
    {
        RetryMenu.transform.DOMoveX(0, 1f).SetEase(Ease.InBack).OnComplete(GameManager.Pause);
    }


}
