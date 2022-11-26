using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandlerComponent : MonoBehaviour
{
    [SerializeField]
    Weapon mEquippedWeapon;


    protected float mCurrChargeTime;
    protected float timeBeforeNextUse = 0;

    // Update is called once per frame
    void Update()
    {
        timeBeforeNextUse -= Time.deltaTime;
        if (timeBeforeNextUse < 0)
        {
            timeBeforeNextUse = 0;
        }
        if (timeBeforeNextUse <=0)
        {
            if (mEquippedWeapon.isChargeWeapon)
            {
                if (Input.GetButton("Fire"))
                {
                    AddCharge(Time.deltaTime);
                }
                if (Input.GetButtonUp("Fire"))
                {
                    GameObject go = FireWeapon(mCurrChargeTime);
                    ChargedObjectComponent chargedObjectComponent = go.GetComponent<ChargedObjectComponent>();
                    if (chargedObjectComponent)
                    {
                        chargedObjectComponent.InitWithCharge(mCurrChargeTime / mEquippedWeapon.maxChargeTime);
                    }
                    mCurrChargeTime = 0;
                }
            }
            else
            {
                if (Input.GetButton("Fire"))
                {
                    FireWeapon(0);
                }
            }
        }
    }

    GameObject FireWeapon(float charge)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z; // distance between camera and grid, whose position is at 0
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        GameObject go = mEquippedWeapon.Fire(gameObject.transform.position, targetPosition, charge);
        timeBeforeNextUse = mEquippedWeapon.cooldown;
        return go;
    }

    public void AddCharge(float chargeAmt)
    {
        mCurrChargeTime += chargeAmt;
        if (mCurrChargeTime > mEquippedWeapon.maxChargeTime)
        {
            mCurrChargeTime = mEquippedWeapon.maxChargeTime;
        }
    }

    public void CancelCharging()
    {
        mCurrChargeTime = 0;
    }

}
