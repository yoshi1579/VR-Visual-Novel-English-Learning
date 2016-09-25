using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(UnityChanLipSync))]
public class SpeakManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public int index = 0;
    public TextAsset yukiText;
    public TextAsset playerText;
    public string pInput;

    public string[] yukiLines;
    public string[] playerLines;
    private int currentLine = 0;
    private int pCurrentLine = 0;
    private UnityChanLipSync lipSync_;
    private bool bYukiSpeaking = true;
    private bool bPlayerSpeaking = false;
    private GameObject canvas;
    private Text dialogText;
    private string distinction = "-";
    private bool pInputCorrect = false;
    private bool bAutoPlay = false;

    void Start()
    {
        // get reference to UI text
        canvas = GameObject.FindGameObjectWithTag("UI");
        dialogText = canvas.GetComponentInChildren<Text>();

        // get a reference to lipSync
        lipSync_ = GetComponent<UnityChanLipSync>();

        
        if (yukiText != null)
        {
            
            yukiLines = (yukiText.text.Split('\n'));
        }

        if (playerText != null)
        {
            playerLines = (playerText.text.Split('\n'));
        }
    }

    void Update()
    {
        if (bYukiSpeaking)
        {
            // check if we have to switch the speaker
            if (yukiLines[currentLine] == "-" && !lipSync_.isTalking)
            {
                // switch turn to player
                bYukiSpeaking = false;
                bPlayerSpeaking = true;
                currentLine += 1;
                pInputCorrect = true;
            }

            if (Input.anyKeyDown || bAutoPlay)
            {
                bAutoPlay = false;              
                // Display line to the canvas panel
                dialogText.text = yukiLines[currentLine];
                currentLine += 1;
                yukiLines[currentLine] = yukiLines[currentLine].Substring(0, yukiLines[currentLine].Length - 1);
                // make the unity chan to speak a line
                if (index < 0 || index >= audioClips.Length) index = 0;
                lipSync_.Play(audioClips[index]);
                ++index;
                          
            }
        }
        
        if (bPlayerSpeaking)
        {
            

            if (pInput == playerLines[pCurrentLine])
            {
                pInputCorrect = true;
                pCurrentLine += 1;
            }

            if (playerLines[pCurrentLine].Length > 2)
            {
                
                if (pInputCorrect)
                {
                    pInputCorrect = false;                   
                    dialogText.text = playerLines[pCurrentLine];
                    playerLines[pCurrentLine] = playerLines[pCurrentLine].Substring(0, playerLines[pCurrentLine].Length - 1);
                }
            }
            else
            {
                bYukiSpeaking = true;
                bPlayerSpeaking = false;
                pCurrentLine += 1;
                bAutoPlay = true;
            }
        }
    }
}
