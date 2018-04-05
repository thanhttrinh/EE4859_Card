using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// holds the refs to all the Text, Images on the card
public class OneCardManager : MonoBehaviour {

    public CardAsset cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
    public Text NameText;
    public Text ManaCostText;
    public Text DescriptionText;
    public Text HealthText;
    public Text AttackText;
    public Text RangeText;
    public Text MovementText;
    public Text CropSize;
    [Header("Image References")]
    public Image CardTopRibbonImage;
    public Image CardLowRibbonImage;
    public Image CardGraphicImage;
    public Image CardBodyImage;
    public Image CardFaceFrameImage;
    public Image CardFaceGlowImage;
    public Image CardBackGlowImage;
    //public Image RarityStoneImage;

    void Awake()
    {
        if (cardAsset != null)
            ReadCardFromAsset();
    }

    private bool canBePlayedNow = false;
    public bool CanBePlayedNow
    {
        get
        {
            return canBePlayedNow;
        }

        set
        {
            canBePlayedNow = value;

            CardFaceGlowImage.enabled = value;
        }
    }

    public void ReadCardFromAsset()
    {
        // universal actions for any Card
        // 1) apply tint
        /*
        if (cardAsset.CharacterAsset != null)
        {
            CardBodyImage.color = cardAsset.CharacterAsset.ClassCardTint;
            CardFaceFrameImage.color = cardAsset.CharacterAsset.ClassCardTint;
            CardTopRibbonImage.color = cardAsset.CharacterAsset.ClassRibbonsTint;
            CardLowRibbonImage.color = cardAsset.CharacterAsset.ClassRibbonsTint;
        }
        else
        {*/
            //CardBodyImage.color = GlobalSettings.Instance.CardBodyStandardColor;
            CardFaceFrameImage.color = Color.white;
            //CardTopRibbonImage.color = GlobalSettings.Instance.CardRibbonsStandardColor;
            //CardLowRibbonImage.color = GlobalSettings.Instance.CardRibbonsStandardColor;
        //}
        
        // 2) add card name
		NameText.text = cardAsset.ScriptName;
        // 3) add mana cost
        ManaCostText.text = cardAsset.ManaCost.ToString();
        // 4) add description
        DescriptionText.text = cardAsset.Description;
        // 5) Change the card graphic sprite
        CardGraphicImage.sprite = cardAsset.CardImage;

        if (cardAsset.TypeOfCard == TypesOfCards.Soldier)
        {
            // this is a soldier
            AttackText.text = cardAsset.Attack.ToString();
            HealthText.text = cardAsset.MaxHealth.ToString();
            RangeText.text = cardAsset.SoldierRange.ToString();
            MovementText.text = cardAsset.Movement.ToString();
        }

        if (cardAsset.TypeOfCard == TypesOfCards.Crop)
        {
            // this is a crop
            HealthText.text = cardAsset.CropHealth.ToString();
            CropSize.text = cardAsset.CropSize.ToString();
        }

        if (cardAsset.TypeOfCard == TypesOfCards.Spell)
        {
            // this is a spell
            RangeText.text = cardAsset.SpellRange.ToString();
        }

        if (PreviewManager != null)
        {
            // this is a card and not a preview
            // Preview GameObject will have OneCardManager as well, but PreviewManager should be null there
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.ReadCardFromAsset();
        }
        /*
        if (RarityStoneImage == null)
            Debug.Log("RarityStoneImage is null on object:" + gameObject.name);

        // NEW apply rarity color to a card 
        RarityStoneImage.color = RarityColors.Instance.ColorsDictionary[cardAsset.Rarity];
        */
    }

    /*
    public bool ValidMove(OneCardManager[,] board, int x1, int y1, int x2, int y2)
    {
        int deltaMoveX = Mathf.Abs(x1 - x2);
        int deltaMoveY = Mathf.Abs(y1 - y2);

        Debug.Log("deltaMoveX = " + deltaMoveX + ", deltaMoveY = " + deltaMoveY);

        //if moving on top of another soldier/crop
        if (board[x2, y2] != null)
        {
            Debug.Log("false");
            return false;
        }

        //if soldier moves over a crop


        //sBoard[x1,y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier
        //if (isAllySoldier)
        //{
        //if deltaMoveX and deltaMoveY is less than or equal to the soldiers movement
        //deltaMoveX <= board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement
        if ((deltaMoveX == 0 && deltaMoveY <= board[x1, y1].gameObject.GetComponent<OneCardManager>().cardAsset.Movement) || (deltaMoveX <= board[x1, y1].gameObject.GetComponent<OneCardManager>().cardAsset.Movement && deltaMoveY == 0))
        {
            Debug.Log("true & soldier has " + board[x1, y1].gameObject.GetComponent<OneCardManager>().cardAsset.Movement + "movement and deltaMoveX =" + deltaMoveX + "and deltaMoveY = " + deltaMoveY);
            return true;

        }
        //if player reaches to an enemy soldier at its max movement+1 and wanted to attack
        /*
        else if(deltaMoveX == sBoard[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement + 1)
        {
            deltaMoveX = sBoard[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement;
            Debug.Log("true in x movement");
            return true;
        }
        else if (deltaMoveY == sBoard[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement + 1)
        {
            deltaMoveY = sBoard[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement;
            Debug.Log("true in y movement");
            return true;
        }
        
        //}
        Debug.Log("false");
        return false;
    }
    */
}
