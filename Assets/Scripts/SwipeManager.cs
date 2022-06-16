using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static bool tap, swipe_left, swipe_right, swipe_up, swipe_down;
    public bool isDrag = false;
    private Vector2 start_touch, swipe_delta;

    private void Update()
    {
        tap = swipe_down = swipe_up = swipe_left = swipe_right = false;
        #region Standalone Inputs
        if(Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDrag = true;
            start_touch = Input.mousePosition; 
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        if(Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDrag = true;
                start_touch = Input.touches[0].position;
            }
            else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDrag = false;
                Reset();
            }
        }
        #endregion

        swipe_delta = Vector2.zero;
        if (isDrag == true)
        {
            if (Input.touches.Length < 0)
                swipe_delta = Input.touches[0].position - start_touch;
            else if (Input.GetMouseButton(0))
            {
                swipe_delta = (Vector2)Input.mousePosition - start_touch;
            }
        }

        if(swipe_delta.magnitude >100)
        {
            float x = swipe_delta.x;
            float y = swipe_delta.y;
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                    swipe_left = true;
                else
                    swipe_right = true;
            }
            else
            {
                if (y < 0)
                    swipe_down = true;
                else
                    swipe_up = true;
            }
            Reset();
        }

    }
    private void Reset()
    {
        start_touch = swipe_delta = Vector2.zero;
        isDrag = false;
    }
}
