using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    GameObject scoreUITextGO; //Reference to the text UI game object
    public GameObject ExplosionGO; //this is our explosion prefab 
    float speed; //for the enemy speed

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f; //set speed

        //get the score text UI
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        //Get the enemy current position
        Vector2 position = transform.position;

        //Compute the enemy new position
        position = new Vector2(position.x, position.y-speed * Time.deltaTime);

        //Update the enemy position
        transform.position = position;

        //This is the bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

        //if the enemy went outside the screen on the bottom, then destroy the enemy
        if (transform.position.y < min.y){
            Destroy(gameObject);

        }

    }

    void OnTriggerEnter2D(Collider2D col){
        //detect collision of the enemy ship with the players ship, or with a players bullet
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag")){
            PlayExplosion();

            //add 100 points to the score
            scoreUITextGO.GetComponent<GameScore>().Score += 100;

            //destroy this enemy ship
            Destroy(gameObject);
        }
    }

    //function to instantiate an explosion
    void PlayExplosion(){
        GameObject explosion = (GameObject)Instantiate (ExplosionGO);
        
        //set the position of the explosion
        explosion.transform.position = transform.position;
    }

}
