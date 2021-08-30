using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{   
    [SerializeField]
    private float _speed = 8.0f;
    public bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        // translate laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        // if laser position is greater than 8 on y - destroy object
        if (transform.position.y > 8f)
        {
            // if object has parent, destory parent too
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        // translate laser up
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // if laser position is greater than 8 on y - destroy object
        if (transform.position.y < -8f)
        {
            // if object has parent, destory parent too
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.Damage();
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }
}
