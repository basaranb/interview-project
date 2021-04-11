using System.Collections;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    public bool forMenuAnimation;
    [SerializeField] Tile tilePrefab = null;
    [SerializeField] Glass glassPrefab;
    [SerializeField] Transform tileParent = null;
    [SerializeField] float speedOfTiles = 14.0f;
    [SerializeField] float lifeOfTiles = 5.0f;
    [SerializeField] float spawnDelay = 1.0f;
    private Color yellow = new Color32(246, 223, 14, 255);
    private Color purple = new Color32(31, 18, 31, 255);
    private Color pink = new Color32(255, 0, 128, 255);
    private Color[] tileColors;
    private static SpawnManager _instance;
    public static SpawnManager Instance { get { return _instance; } }
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
        tileColors = new Color[] { yellow, purple, pink };
        if (forMenuAnimation)
        {
            StartCoroutine(Spawner());
        }
    }
    public void Init()
    {
        InitStartTiles();
        StartCoroutine(Spawner());
    }
    private void InitStartTiles()
    {
        for (int i = 0; i < tileParent.childCount; i++)
        {
            var child = tileParent.GetChild(i);
            if (child.GetComponent<Tile>())
                child.GetComponent<Tile>().Init(this, speedOfTiles, lifeOfTiles);
        }
    }
    private void SpawnGlass()
    {
        int[] xPos = { -3, 0, 3 };
        var indx = Random.Range(0, xPos.Length);
        var glass = Instantiate(glassPrefab, new Vector3(xPos[indx], 9f, 68), Quaternion.Euler(new Vector3(0, 90, 0)));
        glass.Init(speedOfTiles, lifeOfTiles);
        glass.GlassColor = tileColors[Random.Range(0, tileColors.Length)];
    }
    private void Spawn3Tiles()
    {
        Utilities.RandomizeArray(tileColors);
        for (int i = -1; i < tileColors.Length - 1; i++)
        {
            var tile = Instantiate(tilePrefab, new Vector3(i * 3, 5.95f, 68), Quaternion.identity);
            tile.Init(this, speedOfTiles, lifeOfTiles);
            tile.transform.SetParent(tileParent);
            tile.TileColor = tileColors[i + 1];
        }
    }
    private IEnumerator Spawner()
    {
        while (true)
        {
            Spawn3Tiles();
            if (Random.Range(0, 1) == 0 && !forMenuAnimation)
            {
                yield return new WaitForSeconds(spawnDelay / 2);
                SpawnGlass();
                yield return new WaitForSeconds(spawnDelay / 2);
            }
            else
            {
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
