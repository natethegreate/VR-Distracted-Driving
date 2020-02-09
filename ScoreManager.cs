using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    // Use this for initialization
    private int score;
    private volatile int multiplier;
    private volatile float time;
    private volatile float textTime;
    void Start() {
        textTime = 0;
        multiplier = 1;
        score = 0;
        time = 1;
        //Always listen for Power up, in case player is fast texter
        EventManager.StartListening("PowerUp", correctMulti);  //event for temporary boost from correct text
    }

    // Update is called once per frame
    void Update() {
        time -= Time.deltaTime;
        if (textTime >= 0)
            textTime -= Time.deltaTime;
        if (time <= 0)
        {
            time = 1f;
            increaseScore();
        }
        if (textTime < 0)
            EventManager.TriggerEvent("PowerDown");
    }

    public int getScore()
    {
        return score;
    }

    //We will score every second. Multiplier is based off of player speed.
    //Speeds between 0 and some speed get between 0 and 1 points respectively. 
    //Going higer than some speed yields 4x multiplier
    private void increaseScore()
    {
        score += 1 * multiplier;
    }
    //On correctly sending a text, increase multiplier by 2 for time depending on string length
    public void correctText(int length)
    {
        EventManager.TriggerEvent("PowerUp");
        textTime = length;
    }
    public void correctMulti()
    {
        multiplier *= 2;
        EventManager.StartListening("PowerDown", stopMulti);
    }
    public void stopMulti()
    {
        multiplier /= 2;
        EventManager.StopListening("PowerDown", stopMulti); //remove a listener so we dont keep calling this
    }
    public int getMulti()
    {
        return multiplier;
    }
}
