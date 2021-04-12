using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool IsGameStarted { get; private set; }
    public SpawnManager SpawnManager;
    public UIManager UIManager;
    public Ball Ball;
    private AudioSource BackMusic;
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {

        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        SpawnManager = FindObjectOfType<SpawnManager>();
        UIManager = FindObjectOfType<UIManager>();
        BackMusic = GetComponent<AudioSource>();
    }
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    void Start()
    {
        IsGameStarted = false;
    }
    public void StartGame()
    {
        ClearCurrentObjects();
        IsGameStarted = true;
        Ball.Init(this);
        SpawnManager.Init();
        Music("play");
    }
    private void ClearCurrentObjects()
    {
        SpawnManager.TileParent.gameObject.SetActive(true);
        foreach (Transform child in SpawnManager.TileParent.gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        SpawnManager.GlassParent.gameObject.SetActive(true);
        foreach (Transform child in SpawnManager.GlassParent.gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void Pause()
    {
        if (Time.timeScale == 1f)
            Time.timeScale = 0f;
        Music("pause");
    }
    public void Continue()
    {
        if (Time.timeScale != 1f)
            Time.timeScale = 1f;
        Music("unpause");
    }
    public void Lose()
    {
        SpawnManager.StopSpwning();
        SpawnManager.TileParent.gameObject.SetActive(false);
        SpawnManager.GlassParent.gameObject.SetActive(false);
        Ball.Disable();
        UIManager.BringLose();
        Music("stop");
    }
    private void Music(string command)
    {
        switch (command)
        {
            case "play":
                BackMusic.Play(0);
                break;
            case "stop":
                BackMusic.Stop();
                break;
            case "pause":
                BackMusic.Pause();
                break;
            case "unpause":
                BackMusic.UnPause();
                break;
            default:
                break;
        }
    }
}
