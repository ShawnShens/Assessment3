using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFrame : MonoBehaviour
{
    public int targetFrameRate;

    // Start is called before the first frame update
    void Awake()
    {
        // Change the current FPS
        Application.targetFrameRate = targetFrameRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
