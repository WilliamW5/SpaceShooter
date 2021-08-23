using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{   
    // speed variable of 8
    [SerializeField]
    private float _speed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // translate laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        // if laser position is greater than 8 on y - destroy object
        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }
    }
}
