using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int lastTime;

    [SerializeField] private GameObject pacMan;
    private Tweener tweener;

    const float moveWait = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        lastTime = 0;
        // Debug.Log(lastTime);

        tweener = GetComponent<Tweener>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // I wrote a UpdateFrame.cs to change the Application.targetFrameRate in order to check the frame-rate independent movement

        // I first used timer += Time.fixedDeltaTime, but the movement still become obviously slower when FPS =< 5
        // Therefore, I decided to use Time.unscaledTime, which is connected to system clock and never change due to FPS

        if (Time.unscaledTime - lastTime >= 1)
        {
            lastTime = lastTime + 1;
            // Debug.Log(lastTime);
        }

        if (lastTime == 1)
        {
            tweener.AddTween(pacMan.transform, pacMan.transform.position, new Vector3(0.3f, -0.3f, -0.1f), 2.0f);
        }

        if (lastTime > 1)
        {
            if (lastTime % moveWait == 0)
            {
                if (pacMan.transform.position == new Vector3(0.3f, -0.3f, -0.1f))
                {
                    tweener.AddTween(pacMan.transform, pacMan.transform.position, new Vector3(1.8f, -0.3f, -0.1f), 2.0f);
                }
                if (pacMan.transform.position == new Vector3(1.8f, -0.3f, -0.1f))
                {
                    tweener.AddTween(pacMan.transform, pacMan.transform.position, new Vector3(1.8f, -1.5f, -0.1f), 2.0f);
                }
                if (pacMan.transform.position == new Vector3(1.8f, -1.5f, -0.1f))
                {
                    tweener.AddTween(pacMan.transform, pacMan.transform.position, new Vector3(0.3f, -1.5f, -0.1f), 2.0f);
                }
                if (pacMan.transform.position == new Vector3(0.3f, -1.5f, -0.1f))
                {
                    tweener.AddTween(pacMan.transform, pacMan.transform.position, new Vector3(0.3f, -0.3f, -0.1f), 2.0f);
                }
            }
        }
    }
}
