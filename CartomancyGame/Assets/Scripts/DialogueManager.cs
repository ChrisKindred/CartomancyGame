using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkJSON;
    public Text questionBox;
    public GameObject [] choiceButtons;
    public bool isTalking;
    public string currentKnot = "Default";

    private static Story story;
    //private Text nametag;
    //private Text message;
    private List<string> tags = new List<string>();
    private static Choice choiceSelected;

    void Awake()
    {
        story = new Story(inkJSON.text);
        ChooseKnot("Default");

        
        UpdateUI();

        /*
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            Debug.Log(story.currentChoices[i].text);
        }
        */
        
        //story.ChooseChoiceIndex(0);
        //Debug.Log(LoadStoryChunk());
        //ChooseKnot("Response1");
        //Debug.Log(LoadStoryChunk());
        
        //choiceSelected = null;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyUp(KeyCode.Space))
        {
            AdvanceDialogue();
            
            //any choices?
            if (story.currentChoices.Count != 0)
            {
                ShowChoices();
            }
        }
        else
        {
            FinishDialogue();
        }
        */
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
        
        //questionBox.text = LoadStoryChunk();
        //List<Choice> choices = story.currentChoices;
        
        Debug.Log(story.currentChoices.Count);
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i <= (story.currentChoices.Count-1))
            {
                Debug.Log(i);
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
            choiceButtons[choice.index].GetComponent<Button>().onClick.AddListener(delegate
            {
                ChooseStoryChoice(choice);
            });
        }
    }

    

    private void UpdateChoiceUI(Choice choice)
    {
        choiceButtons[choice.index].GetComponentInChildren<Text>().text = choice.text;
        choiceButtons[choice.index].GetComponent<Button>().onClick.AddListener(delegate
        {
            ChooseStoryChoice(choice);
        });
    }
    
    private void ChooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        UpdateUI();
    }
    
    
    private void FinishDialogue()
    {
        Debug.Log("End");
    }

    void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        //ParseTags();
        StopAllCoroutines();
    }

    public void ChooseKnot(string knot)
    {
        story.ChoosePathString(knot);
        currentKnot = knot;
    }

    /*
    void ParseTags()
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            //
        }
    }
    */
}
