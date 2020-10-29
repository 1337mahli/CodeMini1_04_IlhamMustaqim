using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float jumpforce = 15.0f;
    float speed = 10.0f;
    float minlimit = -10f;
    float maxlimit = 10f;
    float maxPlaneZ = 20f;
    float minPlaneZ = -20f;
    float maxPlaneX = 5f;
    float minPlaneX = -5f;
    float gravitymoldifier = 5.5f;
    bool Ground;
    Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= gravitymoldifier;

    }

    // Update is called once per frame
    void Update()
    {
        //declare and Init variables to reference to User Interaction inputs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        //Move player (GameObject) according to user interaction
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        //prevent player to go out z axis
        if (Ground == true)
        {
            if (transform.position.z < minlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minlimit);
            }

            if (transform.position.z > maxlimit && transform.position.x > maxPlaneX)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxlimit);
            }

            if (transform.position.z > maxlimit && transform.position.x < minPlaneX)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxlimit);
            }

            //prevent player to go out x axis
            if (transform.position.x < minlimit)
            {
                transform.position = new Vector3(minlimit, transform.position.y, transform.position.z);
            }

            if (transform.position.x > maxlimit)
            {
                transform.position = new Vector3(maxlimit, transform.position.y, transform.position.z);
            }
            
        }

        if (Ground == false)
        {
            Debug.Log("PlaneB");
            if (transform.position.x < minPlaneX)
            {
                transform.position = new Vector3(minPlaneX, transform.position.y, transform.position.z);
            }

            if (transform.position.x > maxPlaneX)
            {
                transform.position = new Vector3(maxPlaneX, transform.position.y, transform.position.z);
            }

            if (transform.position.z < minlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minlimit);
            }

            else if(transform.position.z > maxlimit && transform.position.z > maxPlaneZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxPlaneZ);
            }
        }

        

        jumpPlayer();

    }

    private void jumpPlayer()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlaneA")) ;
        {
            Debug.Log("Plane A");
            Ground = true;
        }

        if (collision.gameObject.CompareTag("PlaneB")) ;
        {
            Debug.Log("Plane B");
            Ground = false;
        }
    }


}
