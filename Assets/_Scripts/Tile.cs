using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Tile : MonoBehaviour
{
    [SerializeField] float durationAnim = 0.33f;
    private new Rigidbody rigidbody = null;
    private new Renderer renderer = null;
    private float speed;
    private bool isActive = false;
    private SpawnManager tileMgr;
    private Color tileColor;
    public Color TileColor
    {
        get
        {
            return tileColor;
        }
        set
        {
            tileColor = value;
        }
    }
    public void Init(SpawnManager tileMgr, float speed, float lifeTime)
    {
        this.tileMgr = tileMgr;
        this.speed = speed;
        isActive = true;
        Invoke(nameof(Delete), lifeTime);
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }
    }
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }
    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", tileColor);
        renderer.material.DOFade(1.0f, durationAnim);
    }
    private void FixedUpdate()
    {
        if (isActive)
            rigidbody.velocity = new Vector3(0, 0, -speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isActive && collision.gameObject.GetComponent<Ball>())
            renderer.material.DOFade(0.0f, durationAnim);
    }
    private void Delete() => Destroy(gameObject);
}
