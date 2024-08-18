using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Entity/Create Character")]
public class Character : ScriptableObject
{
    public int hp;
    public int atk;
    public int spd;

}