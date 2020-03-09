using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class CardParser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Dictionary<string, Card.CardInfo> ReadMasterSpreadsheet(TextAsset spreadsheet)
    {
        Dictionary<string, Card.CardInfo> dictionaryOfCards = new Dictionary<string, Card.CardInfo>();
        var stringToRead = new StringReader(spreadsheet.text);
        stringToRead.ReadLine();
        while (stringToRead.Peek() != -1)
        {
            stringToRead.ReadLine();
            var unparsedCardInfo = stringToRead.ReadLine().Split(',');
            unparsedCardInfo[6].Trim('(');
            unparsedCardInfo[9].Trim(')');
            var finalCardInfo = new Card.CardInfo();
            //dictionaryOfCards.Add(unparsedCardInfo[0], new Card.CardInfo());
            finalCardInfo.cardName = unparsedCardInfo[0]; //set name
            finalCardInfo.type = Card.Parse(unparsedCardInfo[1]); //set type
            finalCardInfo.value = int.Parse(unparsedCardInfo[2]); // set value
            finalCardInfo.isPlayable = bool.Parse(unparsedCardInfo[3]); // set isPlayable
            finalCardInfo.text = unparsedCardInfo[4]; // set text ***Does not take commas into account
            finalCardInfo.art = Resources.Load<Sprite>(unparsedCardInfo[5]); //set art

            finalCardInfo.buyCost = new Card.VictoryPoints();
            finalCardInfo.buyCost.calmPoints = int.Parse(unparsedCardInfo[6]);
            finalCardInfo.buyCost.bubblyPoints = int.Parse(unparsedCardInfo[7]);
            finalCardInfo.buyCost.hypePoints = int.Parse(unparsedCardInfo[8]);
            finalCardInfo.buyCost.totalPoints = int.Parse(unparsedCardInfo[9]); //set buyCost

            dictionaryOfCards.Add(finalCardInfo.cardName, finalCardInfo); // add to dictionary

            if (stringToRead.Peek() == -1) break;
        }

        stringToRead.Close();
        return dictionaryOfCards;


    }
}
