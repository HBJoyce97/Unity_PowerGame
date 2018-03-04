using UnityEngine;
using System.Collections;

public class PlayerSwitch : MonoBehaviour
{
    GameObject timechar, magchar; // Different player objects
    int selectchar; // Will be used to switch characters

    // Use this for initialization
    void Start()
    {
        selectchar = 1; // Default value
        timechar = GameObject.Find("TimeChar"); // Sets timechar to the 'TimeChar' object
        magchar = GameObject.Find("MagChar"); // Sets magchar to the 'MagChar' object
        magchar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Change"))
        { // Checks if the "Change" button (Left shift) has been pressed down

            if (selectchar == 1)
            {
                selectchar = 2;
            }
            else if (selectchar == 2)
            {
                selectchar = 1;
            }



            if (selectchar == 1)
            {
                magchar.SetActive(false); // Deactivates the 'MagChar' GameObject
                timechar.transform.position = magchar.transform.position;
                timechar.SetActive(true); // Activates the 'TimeChar' GameObject
            }
            else if (selectchar == 2)
            {

                timechar.SetActive(false); // Deactivates the 'TimeChar' GameObject
                magchar.transform.position = timechar.transform.position;
                magchar.SetActive(true); // Activates the 'MagChar' GameObject
            }
        }
    }
}
