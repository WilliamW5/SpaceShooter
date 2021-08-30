using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public or private reference
    // data-type (int, float, bool, string)
    // every variable has a name
    // optional value assigned
    // [SerializeField] - allows the variable to still be private, but seen in the Unity editor
    [SerializeField]
    private float _speed = 3.5f; // _ means private
    [SerializeField]
    private float _speedBoostMod = 2f;
    [SerializeField]
    private float _fireRate = 0.5f;

    // sets the can fire less than Time.time
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;

    private bool _speedBoost = false;
    private bool _trippleShot = false;
    [SerializeField]
    private bool _isShieldActive = false;
    private bool _isDead = false;

    private SpawnManager _spawnManager;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripShotPrefab;

    [SerializeField]
    private GameObject shieldVisualizer;

    [SerializeField]
    private GameObject[] _engineDamage; // [0] - Right_Enginer    [1]- Left_Engine

    [SerializeField]
    public int _score = 0;

    [SerializeField]
    private AudioClip _laserAudio;
    private AudioSource _audioSource;

    private UIManager _uiManager;
    private GameManager _gameManager;

// Start is called before the first frame update
void Start()
    {
        // how to get access to the spawn manager script
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_gameManager.isCoOpMod == false)
        {
            // Take the current position = new Position(0, 0, 0)
            transform.position = new Vector3(0, 0, 0);
        }
        else
        {

        }

        if(_gameManager == null)
        {
            Debug.LogError("The GameManager(Player) is NULL.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource(Player) is NULL.");
        }

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        if( _uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();


        // if I hit space key, spawn or (initiate) gameObject
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        float _xMax = 11.26f;
        
        // Mathf.Clamp - sets the parameters (or Clamps) to the following parameter... y.position, -3.8f, and 0
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= _xMax)
        {
            // -_xMax allows the wrap around effect 
            transform.position = new Vector3(-_xMax, transform.position.y, 0);
        }
        else if (transform.position.x <= -_xMax)
        {
            // _xMax allows the wrap around effect 
            transform.position = new Vector3(_xMax, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        // Sets _canFire to the current time and fireRate, which allows the if statement above to be able to impliment at our fireRate
        _canFire = Time.time + _fireRate;                           // Quaternion.identity = default rotation

        if (_trippleShot == true)
        {
            Instantiate(_tripShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        }
        _audioSource.clip = _laserAudio;
        _audioSource.Play();

    }
    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            // Disable Shields
            shieldVisualizer.SetActive(false);
            return;
        }
        EngineDamageActive();

        _lives--;
        _uiManager.UpdateLives(_lives);

        // Check if dead
        // Destroy us
        if (_lives < 1)
        {
            _spawnManager.onPlayerDeath();
            // Communicate with Spawn Manager
            Destroy(this.gameObject);
        }
    }

    public void TrippleShotActive()
    {
        _trippleShot = true;
        StartCoroutine(TrippleShotPowerDown());
    }

    IEnumerator TrippleShotPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _trippleShot = false;
    }

    public void SpeedBoostActive()
    {
        _speedBoost = true;
        _speed *= _speedBoostMod;
        StartCoroutine(SpeedBoostPowerDown());
    }

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoost = false;
        _speed /= _speedBoostMod;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        shieldVisualizer.SetActive(true);    
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    private void EngineDamageActive()
    {
        if(_lives == 2)
        {
            _engineDamage[0].SetActive(true);
        }
        else
        {
            _engineDamage[1].SetActive(true);
        }
    }
}
