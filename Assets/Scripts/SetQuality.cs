using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class SetQuality : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown qualityDropdown;
    // Find another Script on what current FPS is
    private FPS currentFPS;

    // Add all the render Pipelines that you are using in the build;
    [SerializeField] List<UniversalRenderPipelineAsset> _myPipeline;
    // Slider that the Render Scale is set by
    [SerializeField] private Slider renderScaleSlider;
    //Text for render Scale
    [SerializeField] private TextMeshProUGUI renderScaleText;
    // Text for if Auto Graphics is set or not
    [SerializeField] private TextMeshProUGUI autoGraphics;
    // Value for when the RenderScale should be decreased. Example if you are targeting 30 FPS and set this number to 5 it will lower RenderScale with FPS is 25 or lower
    [SerializeField] private int decreaseRenderScaleFPS = 6;

    //if the game is using autoGraphics or users choice. = true should be using
    [SerializeField] bool autoGraphicsOn;


    // Guessing lots to clean up here
    private void Start()
    {
       
        // How the autoGrapics text should look based on start value of true or false
        if(autoGraphicsOn)
        {
            // Set text if Auto Grapics is ON
            autoGraphics.text = "Auto Graphics ON";
        }
        else
        {
            // Set text if Auto Grapics is OFF
            autoGraphics.text = "Auto Graphics OFF";
        }
        
        // Find the Object to use FPS variables
        currentFPS = FindObjectOfType<FPS>();

        // Starting Quality level based on last time played
        qualityDropdown.value = PlayerPrefs.GetInt("QualityLevel");
        // Think this part will not work need to figure out
        QualitySettings.SetQualityLevel(qualityDropdown.value);

        // changes all the render piplines you have set up to new renderscale
        for (int i = 0; i < _myPipeline.Count; i++)
        {
            renderScaleSlider.value = PlayerPrefs.GetFloat("Pipeline");
            _myPipeline[i].renderScale = renderScaleSlider.value;         
        }
        renderScaleText.text = "Screen Scale: " + renderScaleSlider.value.ToString("F2");

        // has the SetGraphicsRenderByFPS() run every 1/10 of a second. Could probably be more than 1/10
        InvokeRepeating("SetGraphicsRenderByFPS", 0f, 0.1f);

    }
 
    // if button is pressed should auto graphics be set or disabled
    public void SetAutoGraphicsButton()
    {
        if(autoGraphicsOn)
        {
            autoGraphics.text = "Auto Graphics OFF";
            autoGraphicsOn = false;
        }
        else
        {
            autoGraphics.text = "Auto Graphics ON";
            autoGraphicsOn = true;
        }
        
    }

    // the code to use auto graphics or user choice
    private void SetGraphicsRenderByFPS()
    {
        // auto graphics should change render scale to correct FPS on what was choosen 
        if(autoGraphicsOn)
        {
            // decrease the fps until the renderScaleSlider.value is less than .2
            if (currentFPS.frameRate + decreaseRenderScaleFPS <= PlayerPrefs.GetInt("FPS") && renderScaleSlider.value > 0.2f)
            {
                Debug.Log("Decrease Scale Resolution");
                for (int i = 0; i < _myPipeline.Count; i++)
                {
                    // decreases render scale by .003 each time
                    renderScaleSlider.value -= .003f;
                    // all pipelines are = to new renderScaleSlider.value
                    _myPipeline[i].renderScale = renderScaleSlider.value;
                }
            }

            // increase the fps until the enderScaleSlider.value is less or = to 2, 
            if (currentFPS.frameRate + 3 >= PlayerPrefs.GetInt("FPS") && renderScaleSlider.value < 2)
            {
                Debug.Log("Increase Scale Resolution");
                for (int i = 0; i < _myPipeline.Count; i++)
                {  

                    // increases render scale by .003 each time    
                    renderScaleSlider.value += .003f;
                    // all pipelines are = to new renderScaleSlider.value
                    _myPipeline[i].renderScale = renderScaleSlider.value;                                   
                }
            }          
        }                   
    }

    // us a drop down menu to choose the Quaility. Quality is mostly the way lighting works. It is what type of render will be used in URP
    public void SetQualityLevelDropdown(int index)
    {     
        index = qualityDropdown.value;
        QualitySettings.SetQualityLevel(index, false);
        PlayerPrefs.SetInt("QualityLevel", index);   
    }

    // This is how many pixes will be Used. Best way I have found to inrease FPS. 
    public void SetRenderScaleLevel()
    {
        // Using all pipelines
        for (int i = 0; i < _myPipeline.Count; i++)
        {
            // each pipeline will be = to the renderScaleSlider.value
            _myPipeline[i].renderScale = renderScaleSlider.value;
            // will save the renderScale data
            PlayerPrefs.SetFloat("Pipeline", renderScaleSlider.value);
        }
            // will set the UI text to the correct value while rounding to two decimal places 
            renderScaleText.text = "Screen Scale: " + renderScaleSlider.value.ToString("F2");
    }
}
