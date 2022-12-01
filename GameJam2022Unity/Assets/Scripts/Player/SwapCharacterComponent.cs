using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacterComponent : MonoBehaviour, IPauseable
{
    AudioManager audioManager;

    [SerializeField]
    List<GameObject> mCharacters = new List<GameObject>();
    [SerializeField]
    List<GameObject> mShadows = new List<GameObject>();

    public int mCurrIndex { get; protected set; }
    // Start is called before the first frame update
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if(mCharacters.Count < 1)
        {
            Debug.LogWarning("Not enough characters to swap between! Num: " + mCharacters.Count);
        }
        if(mCharacters.Count !=mShadows.Count)
        {
            Debug.LogWarning("Characters Count (" + mCharacters.Count + ") does not match Shadows Count (" + mShadows.Count + ")");
        }
        mCurrIndex = 0;
        mCharacters[mCurrIndex].SetActive(true);
        mShadows[mCurrIndex].SetActive(true);
        for(int i=1; i<mCharacters.Count; ++i)
        {
            mCharacters[i].SetActive(false);
            mShadows[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //mCurrIndex 0 is Red, 1 is wolf

        if(Input.GetButtonDown("SwapCharacter"))
        {
            GameObject oldChar = mCharacters[mCurrIndex];
            oldChar.SetActive(false);
            GameObject oldShadow = mShadows[mCurrIndex];
            oldShadow.SetActive(false);
            ++mCurrIndex;
            if (mCurrIndex >= mCharacters.Count) mCurrIndex = 0;
            GameObject newChar = mCharacters[mCurrIndex];
            newChar.SetActive(true);
            GameObject newShadow = mShadows[mCurrIndex];
            newShadow.SetActive(true);

            CharacterController2D oldCharacterController = oldChar.GetComponent<CharacterController2D>();
            CharacterController2D newCharacterController = newChar.GetComponent<CharacterController2D>();
            if (oldCharacterController && newCharacterController)
            {
                newCharacterController.CopyStatus(oldCharacterController);
            }
            //Plays appropriate music fade and character swap sound effect.
            if (mCurrIndex == 0)
                audioManager.SwitchToRed();
            else if (mCurrIndex == 1)
                audioManager.SwitchToWolf();
        }
    }
}
