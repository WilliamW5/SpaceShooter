using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Player _player;

    private Animator _enemyExplosionAnimation;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL;");
        }
        _enemyExplosionAnimation = gameObject.GetComponent<Animator>();
        if (_enemyExplosionAnimation == null)
        {
            Debug.LogError("The EnemyExplosionAnimation is NULL;");
        }
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is player
        // Destroy Enemy
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
            _enemyExplosionAnimation.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.7f);
        }

        // if other is laser
        // Destroy Enemy
        // Destroy Laser
        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            // Add 10 to score
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _enemyExplosionAnimation.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.7f);
        }
    }
}
