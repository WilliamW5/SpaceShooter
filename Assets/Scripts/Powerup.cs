using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        // When we leave screen, destroy this object
        this.gameObject.transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
        }

    }

    // onTriggerCollison
    // only collectable by the player (Hint: Use Tags)
    // on collected, destroy

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // handle to the component
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.TrippleShotActive();
            }
            Destroy(this.gameObject);
        }
        
    }

}
