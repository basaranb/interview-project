using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float magnitudeCol, radius, power, upwards;
    private float lifeTime = 5;
    private Vector3 explosionPos = new Vector3(1, 1, 1);
    public Vector3 ExplosionPos
    {
        get
        {
            return explosionPos;
        }

        set
        {
            explosionPos = value;
        }
    }

    void Start()
    {
        Debug.Log("Explosion Pos: " + explosionPos);
        // Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Transform child in transform)
        {
            print("Foreach loop: " + child);
            if (child.GetComponent<Rigidbody>())
            {
                child.GetComponent<Rigidbody>().AddExplosionForce(200, explosionPos, 5);
            }

        }
        Invoke(nameof(Delete), lifeTime);
    }


    private void Delete() => Destroy(gameObject);
}
