using UnityEngine;
using TMPro;

// This is to show current FPS in the game scipt
public class FPS : MonoBehaviour
{
    // Text for current FPS
    [SerializeField] TextMeshProUGUI FpsText;

    // how long are we checking Frames, using one for each second
    private float pollingTime = 1f;
    // time Checking
    private float time;
    // how many frames have been shown
    private int frameCount;
    // how many frames have been shown in a time
    public int frameRate;

    private void Start()
    {
        // Get how many frames per second we are targeting
        Application.targetFrameRate = PlayerPrefs.GetInt("FPS");
    }
    void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        // how to get the frames per second
        if(time >= pollingTime)
        {
            frameRate = Mathf.RoundToInt(frameCount / time);

            FpsText.text = frameRate +"FPS";

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
