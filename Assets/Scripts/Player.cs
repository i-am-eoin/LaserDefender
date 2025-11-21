using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6.0f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 20.0f;
    [SerializeField] float projectileFiringTime = 0.2f;
    [SerializeField] int playerHealth = 100;
    [SerializeField] AudioClip plrDeathSound;
    [SerializeField] [Range(0, 1)] float plrDeathSoundVolume = 0.75f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserSoundVolume = 0.25f;



    float xMin, xMax, yMin, yMax;
    
    IEnumerator fireCoroutine;

    void Start()
    {
        SetUpMoveBoundaries();
        fireCoroutine = FireContinuously();

    }
    public void Update()
    {
        Move();
        Fire();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        //Camera.main.transform.position = new Vector3(newXPos, newYPos, Camera.main.transform.position.z);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1")) {
           StartCoroutine(fireCoroutine);
        }
        if (Input.GetButtonUp("Fire1")) {
           StopCoroutine(fireCoroutine);
        }
    }
    
    IEnumerator FireContinuously()
    {
        while (true) {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);          
            laser.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
            yield return new WaitForSeconds(projectileFiringTime);
        }
    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        playerHealth -= damageDealer.GetDamage();
        damageDealer.Hit();    
        if (playerHealth <= 0) {            
            AudioSource.PlayClipAtPoint(plrDeathSound, Camera.main.transform.position, plrDeathSoundVolume);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) {
            return;
        }
        ProcessHit(damageDealer);
    }
}