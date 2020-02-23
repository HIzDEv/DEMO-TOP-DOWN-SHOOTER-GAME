using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "MyPlayer/Ressources")]
public class PlayerRessources : ScriptableObject
{
    [Header("PREFABS")]
    public  GameObject GrenadePrefab;
    public GameObject Casingbullet; //Prefab balle vide
    public GameObject bullet; //Prefab balle
    public GameObject HealPs; //Heal PS

    [Header("Audio")]
    public  AudioClip ReloadingSound;
    public  AudioClip pickup;
    public AudioClip ShootingClip;
    public AudioClip healClip;
    public AudioClip FootStepsClip;

}
