using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Ink.Parsed;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Choice = Ink.Runtime.Choice;
using Story = Ink.Runtime.Story;
using Text = UnityEngine.UI.Text;


public class DialogueManager : MonoBehaviour
{
    public TextAsset inkJSON;
    private static Story story;
    private static Choice choiceSelected;
    
    //this is because for some reason it's impossible to get the current knot unless it's a hardcoded call, sorry
    //use ChooseKnot("stringknot"); to call a know manually (shown in Awake())
    [SerializeField]
    private string lastManuallyChosenKnot = " ";
    
    [Header("UI Elements")]
    public Text textBox;
    public GameObject [] choiceButtons;
    
    
    [Header("Stage Directions")] //look at Acting() for more info
    public int numOfDirectionsPerActor;
    public GameObject [] actors;
    
    
    [Header("Actor Speed in Seconds")] //how long it takes an actor to cross the screen
    public float walkDuration; 
    public float runDuration;
    
    
    [Header("Stage Positions")] //look at Acting() for more info
    public Vector2 screenLeft;
    public Vector2 offScreenLeft;
    public Vector2 screenRight;
    public Vector2 offScreenRight;
    public Vector2 resetPos;
    
    void Awake()
    {
        story = new Story(inkJSON.text);
        ChooseKnot("Default");
    }
    
    void UpdateUI() // how the UI is updated, this is the leading function of the entire script
    {
        textBox.text = LoadStoryChunk();

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

    
    
    //********
    //These functions are design to be edited and added to as more functionality is needed
    
    public void ChoiceIndex0() // 1st choice (remember to assign to button)
    {
        story.ChooseChoiceIndex(0);
        UpdateUI();
    }
    
    public void ChoiceIndex1() // 2nd choice (remember to assign to button)
    {
        story.ChooseChoiceIndex(1);
        UpdateUI();
    }
    
    public void ChoiceIndex2() // 3rd choice (remember to assign to button)
    {
        story.ChooseChoiceIndex(2);
        UpdateUI();
    }
    
    //copy, paste, and change the number to add another choice
    
    //copy, paste, and have it call a specific node that you must dictate using ChooseKnot("stringknot");

    
    
    //this function handles how to display and move an actor on the screen 
    //KEEP IN MIND THE STAGE DIRECTIONS PER ACTOR AND UPDATE THE PUBLIC INT IN THE EDITOR ACCORDINGLY!!!!!!!!!!!!!!!!!!
    private void Acting(List<string> list) //continue to edit/hardcode these actions as more directions/actors are needed/added
    {
        GameObject actor = null;
        Vector2 endPos = new Vector2();
        
        switch (list[0]) //Actor
        {
            case "garf":
                actor = actors[0]; 
                break;
            case "tri":
                actor = actors[1];
                break;
        }

        switch (list[2]) //Ending Location
        {
            case "left":
                endPos = screenLeft;
                break;
            case "right":
                endPos = screenRight;
                break;
        }
        
        switch (list[1]) //Action / Starting Location
        {
            case "runsRightTo": //starts right and runs to the endPos
                actor.transform.position = offScreenRight;
                actor.transform.DOMove(endPos, runDuration);
                break;
            case "runsLeftTo": //starts left and runs to the endPos
                actor.transform.position = offScreenLeft;
                actor.transform.DOMove(endPos, runDuration);
                break;
            case "starts": //begins the scene in specified position
                actor.transform.position = endPos;
                break;
        }
    }
    //********
    
    
    
    
    
    //Heavy Lifting Functions_____________________________________________________________________________

    string LoadStoryChunk() //loads the text of that specific Knot or "Chunk"
    {
        string text = "";
        
        if (story.canContinue)
        {
            text = story.ContinueMaximally();
        }
        
        return text;
    }
    

    public void StageDirections()
    {
        List<string> tags = story.currentTags;
        List<List<string>> Directions = new List<List<string>>();
        int numOfActorsInScene = 0;

        for (int i = 0; i < tags.Count; i++) 
        {
            if ( ((i + 1) % numOfDirectionsPerActor) == 0 )
            {
                Directions.Add(new List<string>());
                int temp = (numOfDirectionsPerActor);
                while (temp > 0)
                {
                    Directions[numOfActorsInScene].Add(tags[i - (temp-1)]);
                    temp--;
                }

                numOfActorsInScene++;
            }
        }
        
        foreach(List<string> subList in Directions)
        {
            Acting(subList);
        }

    }

    
    void ResetActors()
    {
        foreach (GameObject actor in actors)
        {
            actor.transform.position = resetPos;
        }
    }
    
    
    
    public void ChooseKnot(string knot)
    {
        story.ChoosePathString(knot);
        lastManuallyChosenKnot = knot;
        UpdateUI();
    }
    
    
    
    
    
    //Leftover functions__________________________________________________________________________
    private void ChooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        
        UpdateUI();
    }
    
}
