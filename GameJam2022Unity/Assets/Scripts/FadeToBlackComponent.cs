using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeToBlackComponent : MonoBehaviour
{
    [SerializeField]
    Image imageToFade;

    protected bool mFading;
    protected float mFadeLengthSecs;
    protected UnityEvent mCallback;
    // Start is called before the first frame update
    void Start()
    {
        mFading = false;
        mFadeLengthSecs = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (mFading)
        {
            Color currColor = imageToFade.color;
            if (currColor.a >= 1) return;
            float fadePerSec = 1.0f / mFadeLengthSecs;
            float fadeAmount = Time.unscaledDeltaTime * fadePerSec;
            Color newColor = new Color(currColor.r, currColor.g, currColor.b, currColor.a + fadeAmount);
            if (newColor.a >= 1)
            {
                newColor.a = 1;
            }
            imageToFade.color = newColor;
            if (newColor.a >= 1)
            {
                if (mCallback != null) mCallback.Invoke();
                mFading = false;
            }
        }
    }

    public void BeginFadeToBlack(float fadeLengthSecs, UnityEvent callback)
    {
        mFading = true;
        mFadeLengthSecs = fadeLengthSecs;
        mCallback = callback;
    }
}
