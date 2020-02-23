using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(menuName = "PluggableAI/PnjStats")]
public class PnjStats : ScriptableObject
{

    public float moveSpeed ;
    public float lookRange ;
    public float lookSphereCastRadius ;

    public float attackRange ;
    public float attackRate ;
    
    public int attackDamage ;

    public GameObject prefabs_Impact;

    public AudioClip DeathSound;
    public AudioClip ShootSound;

    public float MaxHealh = 200;
   
}
