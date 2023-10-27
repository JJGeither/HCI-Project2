using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject shellObject;

    // Start is called before the first frame update
    void Start()
    {
        shellObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
