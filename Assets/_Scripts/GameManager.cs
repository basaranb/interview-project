using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int Score { get; private set; }
    public bool IsGameStarted { get; private set; }
    public SpawnManager SpawnManager { get; private set; }
    public Ball Ball;
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
        Score = 0;
    }
    public void StartGame()
    {

        IsGameStarted = true;
        Ball.Init(this);
        SpawnManager.Init();

    }
    public void Home()
    {
        Continue();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        if (Time.timeScale == 1f)
            Time.timeScale = 0f;
    }
    public void Continue()
    {
        if (Time.timeScale != 1f)
            Time.timeScale = 1f;
    }

    private void Update()
    {
        Debug.Log("Time: " + Time.timeScale);

    }
}
