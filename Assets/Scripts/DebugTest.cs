using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    public Fighter character;
    TextMeshProUGUI text;

    private void Start()
    {
        text= GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text =
            "Character Name: " + character.name +
            " HP: " + character.hp +
            " Action: " + character.state +
            " Stamina: " + character.stamina;

    }

}
