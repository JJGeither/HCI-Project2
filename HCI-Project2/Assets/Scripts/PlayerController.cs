using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameObject ShellObject;
    public float groundRayLength;


    private bool isInShell;
    private GameObject ActiveObject;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        isInShell = false;
        isGrounded = true;
        ShellObject.SetActive(false);
        ActiveObject = PlayerObject;
    }

    public bool GetIsGrounded() { return isGrounded; }

    void UpdateShell()
    {


        // Check if 'J' key is pressed and transform the object
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isInShell)
            {
                // Transform this object to match the targetObject
                ShellObject.SetActive(true);
                ShellObject.transform.position = PlayerObject.transform.position;
                ShellObject.GetComponent<Rigidbody>().velocity = PlayerObject.GetComponent<Rigidbody>().velocity * 2;
                PlayerObject.gameObject.SetActive(false);
                ActiveObject = ShellObject;

            } else
            {
                // Transform this object to match the targetObject
                PlayerObject.SetActive(true);
                PlayerObject.transform.position = ShellObject.transform.position;
                PlayerObject.GetComponent<Rigidbody>().velocity = ShellObject.GetComponent<Rigidbody>().velocity / 2;
                ShellObject.gameObject.SetActive(false);
                ActiveObject = PlayerObject;
            }
            isInShell = !isInShell;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShell();
        // Bit shift the index of the layer (3) to get a bit mask
        int layerMask = 1 << 3;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ActiveObject.transform.position, ActiveObject.transform.TransformDirection(Vector3.down), out hit, groundRayLength, layerMask))
            isGrounded = true;
        else
             isGrounded = false;
        
    }
}
