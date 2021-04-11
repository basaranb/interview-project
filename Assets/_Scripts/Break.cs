using UnityEngine;
using System.Collections;
public class Break : MonoBehaviour
{
    public GameObject brokenObject;
    public GameManager GameManager;
    public UIManager UIManager;

    void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        UIManager = FindObjectOfType<UIManager>();
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ball" && collision.gameObject.GetComponent<Ball>().BallColor == this.gameObject.GetComponent<Glass>().GlassColor)
        {
            Destroy(gameObject);
            GameObject brokenGlass = Instantiate(brokenObject, transform.position, transform.rotation);
            brokenGlass.GetComponent<Explode>().ExplosionPos = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        }
        else
        {
            collision.gameObject.GetComponent<Ball>().Delete();
            GameManager.Pause();
            UIManager.Loose();

        }
    }
}
