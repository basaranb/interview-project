using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Glass : MonoBehaviour
{
    private float speed;
    private Color glassColor;
    public Color GlassColor
    {
        get
        {
            return glassColor;
        }
        set
        {
            glassColor = value;
        }
    }
    private bool isActive = false;

    void Start()
    {
        Utilities.SetTransparentColor(0.4f, this.gameObject, glassColor);
    }
    public void Init(float speed, float lifeTime)
    {
        this.speed = speed;
        isActive = true;
        Invoke(nameof(Delete), lifeTime);

    }
    private void FixedUpdate()
    {
        if (isActive)
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);
    }
    private void Delete() => Destroy(gameObject);
}
