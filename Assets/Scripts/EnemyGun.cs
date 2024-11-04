using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{

    public GameObject EnemyBulletGo; //this is our enemy bullet prefab

    // Start is called before the first frame update
    void Start()
    {
        //fire en enemy bullet after one second
        Invoke("FireEnemyBullet", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function to fire an enemy bullet
    void FireEnemyBullet(){
        //get a reference to the players ship
        GameObject playerShip = GameObject.Find("PlayerGO");

        if (playerShip != null){ //if the player is not dead
            //instantiate an enemy bullet
            GameObject bullet = (GameObject)Instantiate (EnemyBulletGo);

            //set the bullets initial position
            bullet.transform.position = transform.position;
            
            //compute the bullets direction toward the players ship
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            //set the bullets direction
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
