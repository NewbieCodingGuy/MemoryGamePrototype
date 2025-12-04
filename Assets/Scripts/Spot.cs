using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Spot : MonoBehaviour
{
    public int Id;
    public bool IsOccupied { get; private set; }
    public TMP_Text sequenceNumberTxt; 
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        HideSequenceNumber();
    }

    public void SetOccupied(bool occupied)
    {
        IsOccupied = occupied;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        
        sr.color = IsOccupied ? Color.gray : Color.white;
    }

    private void OnMouseUpAsButton()
    {
        InputHandler.NotifySpotClicked(this);
    }

    public void ShowSequenceNumber(int number)
    {
        if (sequenceNumberTxt == null) return;
        sequenceNumberTxt.text = number.ToString();
        sequenceNumberTxt.gameObject.SetActive(true);
    }

    public void HideSequenceNumber()
    {
        if (sequenceNumberTxt != null) sequenceNumberTxt.gameObject.SetActive(false);
    }

    public void ShowWrongFeedback()
    {
        StartCoroutine(Utility.FlashSprite(sr, Color.red, 0.4f));
    }
}
