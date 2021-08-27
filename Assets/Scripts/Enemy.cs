using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move down 4m per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // if bottom of screen
        // respawn at top

        if (transform.position.y  < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // if other is player
        // Destroy us
        // Damage Player
        if (other.CompareTag("Player"))
        {
            // Way to deal with error handling... Searches is the object is player or not
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {   
                // damage player
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        // if other is laser
        // Destroy us
        // Destroy Laser
        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }
}
