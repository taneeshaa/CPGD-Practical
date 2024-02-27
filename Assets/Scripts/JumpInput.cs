using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
        }

#elif UNITY_ANDROID || UNITY_IOS
        if(Input.touchCount > 0)
        {
            //Debug.Log($"Touch detected {Input.touchCount}");
            Touch primaryTouch = Input.GetTouch(0);
            switch (primaryTouch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("Touch Began");
                    break;
                case TouchPhase.Moved:
                    Debug.Log("Touch Moved");
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Touch Ended");
                    break;
                case TouchPhase.Stationary:
                    break;
                default:
                    break;
            }
        }
#endif
    }
}
