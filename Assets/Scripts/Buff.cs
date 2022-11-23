using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int buffId;
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();//access the player
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            if (player != null)
            {
                if (buffId == 0)
                {
                    player.TripleShotOn();//enable triple shot
                }
                else if (buffId == 1)
                {
                    player.SpeedBoostOn();//enable speed boost
                }
                else if (buffId == 2)
                {
                    player.ShieldOn();//enable shield
                }
            }
            Destroy(this.gameObject);
        }

    }
}
