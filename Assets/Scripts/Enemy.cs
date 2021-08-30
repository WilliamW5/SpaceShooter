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

    [SerializeField]
    private GameObject _enemyLaserPrefab;
    private float _fireRate = 3.0f;
    private float _canFire = -1f;
    private bool _stopFiring = false;

    private Laser _laser;

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
        CalculateMovement();
        EnemyFire();

    }

    public void CalculateMovement()
    {
        // move down 4m per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // if bottom of screen
        // respawn at top
        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _laser = other.gameObject.GetComponent<Laser>();
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
            EnemyDestroyedRoutine(other);
        }

        // if other is laser
        // Destroy Enemy
        // Destroy Laser
        if(other.CompareTag("Laser") && _laser._isEnemyLaser == false)
        {
            Destroy(other.gameObject);
            // Add 10 to score
            if (_player != null)
            {
                _player.AddScore(10);
            }
            EnemyDestroyedRoutine(other);
        }

    }

    private void EnemyFire()
    {
        if (_stopFiring == false)
        {
            if (Time.time > _canFire)
            {
                _fireRate = Random.Range(3f, 7f);
                _canFire = Time.time + _fireRate;
                GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
                // Debug.Break() immediately pauses Unity after spawning object
                // Debug.Break();
                Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers.Length; i++)
                {
                    lasers[i].AssignEnemyLaser();
                }
            }
        }
    }

    private void EnemyDestroyedRoutine(Collider2D other)
    {
        _enemyExplosionAnimation.SetTrigger("OnEnemyDeath");
        _speed = 0;
        _stopFiring = true;
        _audioSource.Play();
        // Deleted the collider so the animation is the only thing present
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 2.7f);
    }
}
