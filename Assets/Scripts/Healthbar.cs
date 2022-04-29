using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health PlayerHealth;
    [SerializeField] private Image TotalHealthbar;
    [SerializeField] private Image CurrentHealthbar;



    private void Start()
    {
        TotalHealthbar.fillAmount = PlayerHealth.CurrentHealth / 10;
    }

    private void Update()
    {
        CurrentHealthbar.fillAmount = PlayerHealth.CurrentHealth / 10;
    }
}