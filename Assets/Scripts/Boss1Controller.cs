using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour
{
    public float speed = 0f;                   // Főgonosz sebessége
    public float shootingInterval = 2f;        // Lövések közötti időköz
    public int Lives = 12;
    private bool hasHit = false;
    public GameObject bulletPrefab;            // Lövedék prefabja
    public Transform bulletSpawnPoint;         // Lövedék kilövési pozíciója
    public Transform player; 
    GameObject scoreUITextGO;                  // Játékos pozíciója
    private bool movingRight = true;           // Kezdeti mozgásirány jobbra
    //private float timeSinceLastShot = 0f;      // Utolsó lövés óta eltelt idő

    void Start()
    {
        // Kezdetben véletlenszerűen állítja be az irányt
        movingRight = Random.value > 0.5f;
    }

    void Update()
    {
        Move();
        //Shoot();
    }

    // Főgonosz jobbra-balra mozgatása
    void Move()
    {
        // Jobbra vagy balra mozdul el az aktuális irány alapján
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        // Ha eléri a képernyő széleit, irányt vált
        if (transform.position.x >= 4.5f) // Jobb széle (változtatható a képernyő szélesség szerint)
        {
            movingRight = false;
        }
        else if (transform.position.x <= -4.5f) // Bal széle (változtatható)
        {
            movingRight = true;
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Detect collision of the enemy with the player's ship or bullets
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag01") || (col.tag == "PlayerBulletTag02"))
        {
            // Only add points if the enemy hasn't been hit yet
        
            if (!hasHit)
            {
                //PlayExplosion();

                // Add 100 points to the score
                scoreUITextGO.GetComponent<GameScore>().Score += 100;

                // Set the flag to true to prevent further score increases
                //hasHit = true;
                Lives-=1;

            }

            if(Lives==0) {
            
            Destroy(gameObject);
        }
    }
    }

    
}

