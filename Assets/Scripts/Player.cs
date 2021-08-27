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
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        // Take the current position = new Position(0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
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
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

    }
}
