using TMPro;
using UnityEngine;

public class NumberUpdater : MonoBehaviour
{
    public CenterController CenterController;
    public TMP_Text numberText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numberText.text = "Level    : " + CenterController.level + "\nHealth   : " + CenterController.health + "\nEnergy  : " + CenterController.energy;
    }
}
