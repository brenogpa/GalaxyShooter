using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool tripleShotAvailable = false;
    public bool speedBoostAvailable = false;
    public bool shieldAvailable = false;
    public int hp = 3;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _thrusterPrefab;
    [SerializeField]
    private GameObject[] _enginesPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    private void Start()
    {
           //posição atual = nova posição
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager != null)
        {
            _uiManager.UpdateHP(hp);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutine();
        }

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();

            if (tripleShotAvailable == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }

            _canFire = Time.time + _fireRate;
        }
    }
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (speedBoostAvailable == true)
        {
            transform.Translate(Vector3.right * _speed * 1.8f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.8f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }        

        //limite de movimentação dos eixos y,x
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
        else if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (shieldAvailable == true)
        {
            shieldAvailable = false;
            _shieldPrefab.SetActive(false);
            return;
        }
        
        hp -= 1;
        _uiManager.UpdateHP(hp);

        if (hp == 2)
        {
            _enginesPrefab[0].SetActive(true);
        }
        else if (hp == 1)
        {
            _enginesPrefab[1].SetActive(true);
        }

        if (hp < 1)
        {
            Destroy(this.gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
        }
    }

    public void TripleShotOn()
    {
        tripleShotAvailable = true;
        StartCoroutine(TripleShotDuration());
    }

    public void SpeedBoostOn()
    {
        speedBoostAvailable = true;
        _thrusterPrefab.SetActive(true);
        StartCoroutine(SpeedBoostDuration());
    }

    public void ShieldOn()
    {
        shieldAvailable = true;
        _shieldPrefab.SetActive(true);
        StartCoroutine(ShieldDuration());
    }

    public IEnumerator TripleShotDuration()
    {
        yield return new WaitForSeconds(10.0f);
        tripleShotAvailable = false;
    }

    public IEnumerator SpeedBoostDuration()
    {
        yield return new WaitForSeconds(7.0f);
        speedBoostAvailable = false;
        _thrusterPrefab.SetActive(false);
    }
    
    public IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(10.0f);
        shieldAvailable = false;
        _shieldPrefab.SetActive(false);
    }
}
