using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum to specify what this damage source is
public enum DamageSources
{
    Player,
    Enemy,
    Hazard
}


public class Hitbox : MonoBehaviour
{
    //Class contains data about the attack, i.e. attacker, damage and so on
    public DamageSources damageType;
    public int damage;
    //Potential other details we could add: Knockback force, hitstun

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
