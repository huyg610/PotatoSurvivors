using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AppStatus : MonoBehaviour
{
    private TextMeshProUGUI textStatus;
    public Slider health;
    void Start(){
        textStatus = GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        // Text Bar
        if(textStatus != null){
            textStatus.text = "Stats go here";
        }

        // Health Bar
        if(health != null) health.value = PlayerMovement.playerHealth/10;
    }
}

