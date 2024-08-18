using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sequence", menuName = "Entity/Create Sequence")]
public class Sequence : ScriptableObject
{
    public List<Move> moveList= new List<Move>();
}