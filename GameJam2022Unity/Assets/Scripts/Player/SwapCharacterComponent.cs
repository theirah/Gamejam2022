using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacterComponent : MonoBehaviour, IPauseable
{
    [SerializeField]
    List<GameObject> mCharacters = new List<GameObject>();

    protected int mCurrIndex;
    // Start is called before the first frame update
    void Awake()
    {
        if(mCharacters.Count < 1)
        {
            Debug.LogWarning("Not enough characters to swap between! Num: " + mCharacters.Count);
        }
        mCurrIndex = 0;
        mCharacters[mCurrIndex].SetActive(true);
        for(int i=1; i<mCharacters.Count; ++i)
        {
            mCharacters[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("SwapCharacter"))
        {
            GameObject oldChar = mCharacters[mCurrIndex];
            oldChar.SetActive(false);
            ++mCurrIndex;
            if (mCurrIndex >= mCharacters.Count) mCurrIndex = 0;
            GameObject newChar = mCharacters[mCurrIndex];
            newChar.SetActive(true);

            CharacterController2D oldCharacterController = oldChar.GetComponent<CharacterController2D>();
            CharacterController2D newCharacterController = newChar.GetComponent<CharacterController2D>();
            if (oldCharacterController && newCharacterController)
            {
                newCharacterController.CopyStatus(oldCharacterController);
            }

        }
    }
}
