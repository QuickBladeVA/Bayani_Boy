using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Character", menuName = "CreateCharacter")]

public class Character : ScriptableObject
{
    [Header("Character Script")]
    [Header("Stat")]
    [Tooltip("The character Health determines in or out of the battle")]
    public int hp;
    [Tooltip("The character Attack determines how much they can deal in battle")]
    public int atk;
    [Tooltip("The character Speed determines who goes first in battle")]
    public int spd;
}
