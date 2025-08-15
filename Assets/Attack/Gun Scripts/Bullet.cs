using System.ComponentModel;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3f;

    
    void Awake()
    {
        Destroy(gameObject,life);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
            
    }
}
