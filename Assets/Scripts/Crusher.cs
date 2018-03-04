using UnityEngine;
using System.Collections;

public class Crusher : MonoBehaviour {
    [SerializeField]
    Transform spawnPoint;

    public bool interact = false;
    public GameObject crusher; // crusher gameobject
    public Transform lineStart, lineEnd; // Raycast Line start and end

	// Use this for initialization
	void Start () {

        crusher = GameObject.Find("Crusher"); // Sets crusher to the 'Crusher' object
        crusher.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

    }

	// Update is called once per frame
	void Update () {

        Raycasting();
    }

    void Raycasting()
    {
        Debug.DrawLine(lineStart.position, lineEnd.position, Color.green); // Draw the Raycast line for the Crusher

        if (Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Player"))) // If the raycast comes in contact with any object that has the layer "Player"...
        {
            crusher.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; // Unfreeze all constraints
            crusher.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX; // Freeze X position
            crusher.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; // Freeze Z rotation
            interact = true; 
        }
        else
        {
            interact = false;
        }
    }
        void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player")) // If the Crusher collides with an object that has the tag "Player"
        {
            crusher.transform.position = new Vector2(0.494f, 1.926f); // Reset position of crusher
            crusher.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; // Freeze all positions/rotations
            col.transform.position = spawnPoint.transform.position; // Set the position of the object with "Player" to the position of the spawn point
        }
    }
}
