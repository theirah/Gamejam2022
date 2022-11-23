using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChargedObjectComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void InitWithCharge(float chargePercentage);
}
