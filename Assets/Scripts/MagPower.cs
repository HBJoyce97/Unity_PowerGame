using UnityEngine;
using System.Collections;

public class MagPower : MonoBehaviour
{

    GameObject metalcrate, magchar; // Refers to gameobject being dragged

    public Vector3 GOcenter; // Gameobject center
    public Vector3 clickposition; // Click position
    public Vector3 offset; // Vector between mouse click and object center
    public Vector3 newGOcenter; // New center of gameobject
    public string msg;
    RaycastHit hit; // Store hit object information
    public float minDistance; // Minimum distance 

    private bool draggingMode = false; //Dragging mode either we are dragging (true) or we're not (false)
    public float distance; // Variable ysed to calculate distance between player and crate
    Vector3 p, c;

    // Use this for initialization
    void Start()
    {
        metalcrate = this.gameObject; //.Find("MetalCrate");  //Sets metalcrate to the 'MetalCrate' object
        magchar = GameObject.Find("MagChar"); // Sets magchar to the 'MagChar' object
        draggingMode = false; // Sets up dragging mode
        minDistance = 7; // Set the minimuum required distance within which the crates can be moved

    }

    // Update is called once per frame
    void Update()
    {
        if (magchar.activeSelf) // Only usable when the 'MagChar' is active
        {
            p = magchar.transform.position;                
            c = metalcrate.transform.position;            
            distance = Vector3.Distance(p, c);             
            if (distance <= minDistance)  // If distance to crate is less than or equal to the minium required distance the player can move the crate          
            {
                if (Input.GetMouseButtonDown(0) && !draggingMode) // First frame when left mouse is clicked 
                {

                    RaycastHit2D hit2d = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                    if (hit2d)
                    {
                        metalcrate = hit2d.collider.gameObject;
                        GOcenter = metalcrate.transform.position;
                        clickposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        offset = clickposition - GOcenter;
                        metalcrate.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                        draggingMode = true; // Set dragging mode to false

                    }
                }

                if (Input.GetMouseButton(0)) // Every frame when left mouse is held
                {
                    if (draggingMode) // If the crate is being dragged
                    {
                        clickposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        newGOcenter = clickposition - offset;
                        metalcrate.transform.position = new Vector3(newGOcenter.x, newGOcenter.y, newGOcenter.z);

                    }
                }

                if (!Input.GetMouseButton(0)) // When left mouse is released
                {
                    if (!Input.GetMouseButtonDown(1) && draggingMode) // If the right button is not being held down and we are dragging the crate, thenn let it go into free-fall
                    {
                        metalcrate.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; // Set no movement constraints on the crate
                        draggingMode = false; // We are not dragging the crate
                    }
                }

                if (Input.GetMouseButtonDown(1)) // First frame when right mouse is clicked
                {
                    metalcrate.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition; // Freeze the crate at its currrent position
                    draggingMode = false; // Switch drag mode off
                }


            }
        }


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            metalcrate.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; // Set no movement constraints on the crate
            draggingMode = false; // We are not dragging the crate
        }
    }
}