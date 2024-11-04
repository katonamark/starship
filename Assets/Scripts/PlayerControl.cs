using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGO; //reference to our game manager

    public GameObject PlayerBulletGO; //This is our players bullet prefab
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject ExplosionGO; //this is our explosion prefab

    //Reference to the lives ui text
    public Text LivesUIText;

    const int MaxLives = 3; //Maximum player lives
    int Lives; //Current player lives

    public float speed;

    public void Init()
    {
        Lives = MaxLives;

        //Update the lives ui text
        LivesUIText.text = Lives.ToString();

        //reset the players postion to the center of the screen
        transform.position = new Vector2(0,0); 

        //set this player game object to active
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //fire bullets when the spacebar is pressed
        if(Input.GetKeyDown("space")){

            //Play the laser sound effect
            GetComponent<AudioSource>().Play();

            //instantiate the first bullet
            GameObject bullet01 = (GameObject)Instantiate (PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position; //set the bullet initial position
            
            //instantiate the seconf bullet
            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position; //set the bullet initial position

        }

        float x = Input.GetAxisRaw("Horizontal"); //the value will be -1, 0 or 1 (for left, no input, and right)
        float y = Input.GetAxisRaw("Vertical"); //the value will be -1, 0 or 1 (for down, no input, and up)

        //now based on the input we conpute a direction vector, and we normalize it to get a unit vector
        Vector2 direction = new Vector2 (x, y).normalized;

        //now we call the function that computes and sets the players position
        Move(direction);


    }

    void Move(Vector2 direction){
        //find the screen limits to the players movement (left, right, top and bottom edges of the screen)
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0)); // this is the bottom-left point corner of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1)); // this is the top-right point corner of the screen

        max.x = max.x - 0.225f; //subtract the player sprite half width
        min.x = min.x + 0.225f; //add the player sprite half width

        max.y = max.y - 0.285f; //subtract the player sprite half height
        min.y = min.y + 0.285f; //add the player sprite half height

        //Get the players current position

        Vector2 pos = transform.position;

        //Calculate the new position

        pos += direction * speed * Time.deltaTime;

        // Make sure the new position is not outside the screen

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //Update the players position

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D col){
        //detect collision of the player ship with an enemy ship, or with an enemy bullet
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag")){
            PlayExplosion();

            Lives--; //substract one live
            LivesUIText.text = Lives.ToString(); // update lives ui text

            if (Lives == 0) //if our player is dead
            {
                //change game manager state to game over state
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

                //Destroy(gameObject); // Destroy the players ship
                //hide the players ship
                gameObject.SetActive(false);
            }
        }
    }

    //function to intantiate an explosion
    void PlayExplosion(){
        GameObject explosion = (GameObject)Instantiate (ExplosionGO);

        //set the position of the explosion
        explosion.transform.position = transform.position;
    }

    


}
