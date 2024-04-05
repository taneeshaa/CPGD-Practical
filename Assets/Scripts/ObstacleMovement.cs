using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 2f;
    private float timer = 0f;
    public float interval = 9f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer < interval)
        {
            gameObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
