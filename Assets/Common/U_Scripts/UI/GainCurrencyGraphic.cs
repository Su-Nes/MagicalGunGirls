using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class GainCurrencyGraphic : MonoBehaviour
{
    private TMP_Text text;
    private bool fadeActive;
    private float currentCounterDelay;
    private int currentAmount;

    [SerializeField] private float fadeLerpValue, endFadeDelay, counterDelay, counterDelayIncreasePercentage;

    [SerializeField] private KeyCode debugPlayKey = KeyCode.L;


    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void AddCurrency(int from, int to)
    {
        fadeActive = true;
        currentAmount = from;
        currentCounterDelay = counterDelay;
        StartCoroutine(CounterSequence(to));
    }

    private void FixedUpdate()
    {
        text.color = Color.Lerp(text.color, fadeActive ? Color.white : Color.clear, fadeLerpValue);
    }

    private void Update()
    {
        if(Input.GetKeyDown(debugPlayKey))
            AddCurrency(currentAmount, currentAmount + 15);
    }

    private IEnumerator CounterSequence(int endNum)
    {
        while (currentAmount < endNum)
        {
            text.text = $"{currentAmount} MN";
            yield return new WaitForSeconds(currentCounterDelay);
        
            currentAmount++;
            currentCounterDelay *= counterDelayIncreasePercentage;
        }
        
        text.text = $"{endNum} MN";
        
        yield return new WaitForSeconds(endFadeDelay);
        fadeActive = false;
    }
}
