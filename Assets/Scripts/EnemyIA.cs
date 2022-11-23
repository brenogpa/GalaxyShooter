using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _speed = 3.5f;

    private float _canFire = 2.0f;
    [SerializeField]
    private float _fireRate = 8.0f;

    [SerializeField]
    private AudioClip _clip;

    private UIManager _uiManager;
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.40f)
        {
            transform.position = new Vector3(Random.Range(-8.18f, 8.18f), 6.0f, 0);
        }
        Shoot();
    }
    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();

            Instantiate(_laserPrefab, transform.position + new Vector3(0, -0.88f, 0), Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);

        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
        
    }
}
