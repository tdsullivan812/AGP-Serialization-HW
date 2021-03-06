﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public abstract class Card
{
    //Data Structures
    #region
    public struct CardInfo
    {
        public string cardName;
        public Vibes type;
        public int value;
        public bool isPlayable;
        public string text;
        public Sprite art;
        public VictoryPoints buyCost;

    }

    public struct VictoryPoints
    {
        public int calmPoints;
        public int bubblyPoints;
        public int hypePoints;
        public int totalPoints;
    }

    public enum Vibes : byte
    {
        Calm,
        Bubbly,
        Hype,
        Undefined
    }
    #endregion

    public GameObject cardGameObject;
    public CardInfo displayedInfo;
    //public DisplayedCard cardOnScreen;
    public abstract void Effect();

    public Card()
    {

    }

    public void InitializeCardGameObject()
    {
        cardGameObject = Object.Instantiate(Resources.Load<GameObject>("Cards/Basic Card"));
        cardGameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].text = displayedInfo.cardName;
        cardGameObject.GetComponentsInChildren<TextMeshProUGUI>()[1].text = displayedInfo.text;

        UnityEngine.UI.Image cardBackgroundImage = cardGameObject.GetComponent<UnityEngine.UI.Image>();
        cardGameObject.name = displayedInfo.cardName;

        switch (displayedInfo.type)
        {
            case (int)Card.Vibes.Calm:
                cardBackgroundImage.sprite = Resources.Load<Sprite>("Cards/Bubbly_Background");
                break;
            case (int)Card.Vibes.Bubbly:
                cardBackgroundImage.sprite = Resources.Load<Sprite>("Cards/Calm_Background");
                break;
            case (int)Card.Vibes.Hype:
                cardBackgroundImage.sprite = Resources.Load<Sprite>("Cards/Hype_Background");
                break;
            default:
                break;

        }

        cardGameObject.AddComponent<CardIdentifier>().whichCardIsThis = this;
    }

    public static Vibes Parse(string vibeString)
    {
        if (vibeString == "Vibes.Bubbly") return Vibes.Bubbly;
        else if (vibeString == "Vibes.Calm") return Vibes.Calm;
        else if (vibeString == "Vibes.Hype") return Vibes.Hype;
        else return Vibes.Undefined;
    }
}
