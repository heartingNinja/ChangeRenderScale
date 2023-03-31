using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseFPS : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI framesPerSecond;
  
    public void Start()
    {
        framesPerSecond.text = "Frames Per Second: " + PlayerPrefs.GetInt("FPS").ToString();
    }
        
    public void SetFPS30()
    {
        Application.targetFrameRate = 30;
        framesPerSecond.text = "Frames Per Second: " + Application.targetFrameRate.ToString();
        // the PlayerPrefs will keep this choice on restart
        PlayerPrefs.SetInt("FPS", Application.targetFrameRate);
    }

    public void SetFPS60()
    {
        Application.targetFrameRate = 60;
        framesPerSecond.text = "Frames Per Second: " + Application.targetFrameRate.ToString();
        PlayerPrefs.SetInt("FPS", Application.targetFrameRate);
    }

    public void ResetAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
