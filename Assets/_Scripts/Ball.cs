using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{

    [SerializeField] float durationAnim = 0.33f;
    [SerializeField] float speed = 10.0f;
    private new Rigidbody rigidbody;
    private new Renderer renderer;
    private bool _isInitialized = false;
    private GameManager gameMgr;
    private Touch touch;
    private float speedModifier = 0.03f;
    private Color ballColor;
    public Color BallColor
    {
        get
        {
            return ballColor;
        }
        set
        {
            ballColor = value;
        }
    }
    public void Init(GameManager gameMgr)
    {
        this.gameObject.SetActive(true);
        this.gameObject.transform.position = new Vector3(0, 6.6f, 12);
        this.gameMgr = gameMgr;
        rigidbody.isKinematic = false;
        Jump();
        _isInitialized = true;

    }
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();

    }
    private void Start()
    {
        renderer.material.SetColor("_BaseColor", Color.white);
        renderer.sharedMaterial.DOFade(1.0f, 0.0f);
    }
    private void Update()
    {
        if (_isInitialized && gameMgr.IsGameStarted)
            Move();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_isInitialized && gameMgr.IsGameStarted)
        {
            if (collision.gameObject.GetComponent<Tile>())
            {
                Jump();
                var newColor = collision.gameObject.GetComponent<Tile>().TileColor;
                gameObject.GetComponent<Renderer>().material.DOColor(newColor, 0.5f);
                ballColor = newColor;

            }

        }

        if (collision.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Collided with ground");
            gameMgr.Loose();
        }
    }


    private void Move()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x + touch.deltaPosition.x * speedModifier, -3.4f, 3.4f),
                    transform.position.y,
                    transform.position.z
                );
            }
        }
    }
    private void Jump()
    {

        rigidbody.velocity = new Vector3(0, speed, 0);
        Debug.Log("jumped with: " + rigidbody.velocity);

    }
    public void Disable()
    {
        renderer.sharedMaterial.DOFade(0.0f, durationAnim).OnComplete(Passifize);

    }
    private void Passifize()
    {
        this.gameObject.SetActive(false);
    }

}
