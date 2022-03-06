using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Choice = Ink.Runtime.Choice;
using Story = Ink.Runtime.Story;
using Text = UnityEngine.UI.Text;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkJSON;
    public Text questionBox;
    public GameObject [] choiceButtons;
    public bool isTalking;
    public string currentKnot = "Default";

    public GameObject garfield;
    public GameObject triangle;

    public Vector2 left;
    //public Vector2 leftOffStage;
    public Vector2 right;
    //public Vector2 rightOffStage;
    public Vector2 resetPos;

    private static Story story;
    
    private static Choice choiceSelected;

    void Awake()
    {
        story = new Story(inkJSON.text);
        ChooseKnot("Default");
        
        UpdateUI();
        
    }

    void Update()
    {
        
    }

    string LoadStoryChunk()
    {
        string text = "";
        
        if (story.canContinue)
        {
            text = story.ContinueMaximally();
        }

        return text;
    }


    void UpdateUI()
    {
        questionBox.text = LoadStoryChunk();

        ResetActors();
        StageDirections();

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i <= (story.currentChoices.Count-1))
            {
                choiceButtons[i].SetActive(true);
            }
            else
            {
                choiceButtons[i].SetActive(false);
            }
        }
        
        foreach (Choice choice in story.currentChoices)
        {
            choiceButtons[choice.index].GetComponentInChildren<Text>().text = choice.text;
        }
        
    }


    public void ChoiceIndex0() // first choice
    {
        story.ChooseChoiceIndex(0);
        UpdateUI();
        //reset postions of actors
    }
    
    public void ChoiceIndex1() // second choice
    {
        story.ChooseChoiceIndex(1);
        UpdateUI();
        //reset postions of actors
    }
    
    public void ChoiceIndex2() // third choice
    {
        story.ChooseChoiceIndex(2);
        UpdateUI();
        //reset postions of actors
    }



    public void StageDirections()
    {
        List<string> tags = story.currentTags;
        List<string> list1 = new List<string>();
        List<string> list2 = new List<string>();

        if (tags.Count > 0)
        {
            int listCount = 1;
            foreach (string thing in tags)
            {
                if (listCount == 1)
                {
                    list1.Add(thing);
                }

                if (listCount == 2)
                {
                    list2.Add(thing);
                }
                
                if (thing == "end")
                {
                    listCount++;
                }
            }

            foreach (string stuff in list1)
            {
                Debug.Log(stuff);
            }
            
            foreach (string stuff2 in list2)
            {
                Debug.Log(stuff2);
            }
            Acting(list1);
            Acting(list2);
            
        }
        
    }

    private void Acting(List<string> list)
    {
        GameObject actor = null;
        Vector2 endPos = new Vector2();
        
        //character
        switch (list[0]) //actor
        {
            case "garf":
                actor = garfield;
                break;
            case "tri":
                actor = triangle;
                break;
        }

        switch (list[2]) //location
        {
            case "left":
                endPos = left;
                break;
            case "right":
                endPos = right;
                break;
        }
        
        switch (list[1]) //action
        {
            case "runs":
                actor.transform.position = endPos;
                break;
            case "starts":
                actor.transform.position = endPos;
                break;
                
        }

    }

    void ResetActors()
    {
        triangle.transform.position = resetPos;
        garfield.transform.position = resetPos;
    }
    
    
    private void ChooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        UpdateUI();
    }
    
    
    public void ChooseKnot(string knot)
    {
        story.ChoosePathString(knot);
        currentKnot = knot;
    }

    private void FinishDialogue()
    {
        Debug.Log("End");
    }
    
}
