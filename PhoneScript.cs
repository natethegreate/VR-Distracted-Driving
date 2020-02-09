using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PhoneScript : MonoBehaviour {

    // Use this for initialization
    public GameObject phone;
    public GameObject phoneScreen;
    public GameObject screenText;
    public GameObject cursor;
    public GameObject Hando;

    //for now just declare screens as materials.
    public int screen;
    public RenderTexture cam;
    public Material[] screens;
    public Material off;    //text screen
    public Material onTest; //call screen

    public float cursorDamp = 100000.01f;     //cursor speed is still not working OwO
    private CapsuleCollider cursed;

    public float pressSpeed = .0111111f;  //change this for longer presses

    public Text textBox;
    public volatile String backupText;   //lazy code UwU

    private int keyboardIdx = 3;    //change this if children to the Phablet object are added before the keyboard

	void Start () {
        screen = 0;
        //phone.GetComponent<Renderer>().material = off;
        screens = new Material[2];
        screens[0] = off;
        screens[1] = onTest;

        //initialize cursor with no collision
        cursed = cursor.GetComponent<CapsuleCollider>();
        cursed.enabled = false;
        backupText = textBox.text;
	}
	
	// Update is called once per frame
	void Update () {
        //on right trigger, hide/unhide phone.
        //TODO: Consider StopListening on all buttons when phone is hidden. This prevents player from 'storing' keypresses if phone is hidden.
		if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            //This hides and unhides phone and keyboard buttons.
            if (phone.GetComponent<Renderer>().enabled == false)
            {
                phone.GetComponent<Renderer>().enabled = true;
                phoneScreen.GetComponent<Renderer>().enabled = true;
                screenText.SetActive(true);
                cursor.GetComponent<Renderer>().enabled = true;
                Transform keyboard = transform.GetChild(keyboardIdx);
                keyboard.gameObject.SetActive(true);
                //Hide hand that is holding phone
                Hando.SetActive(false);
            }
            else
            {
                phone.GetComponent<Renderer>().enabled = false;
                phoneScreen.GetComponent<Renderer>().enabled = false;
                screenText.SetActive(false);
                cursor.GetComponent<Renderer>().enabled = false;
                Transform keyboard = transform.GetChild(keyboardIdx);
                keyboard.gameObject.SetActive(false);
                Hando.SetActive(true);
            }
        }

        //Press A. For now, switch screens.
        if(OVRInput.GetDown(OVRInput.Button.One))
        {
            //screens = phoneScreen.GetComponent<Renderer>().materials;
            //screens[0] = cam;
            if (screen == 0)
            {
                //disable keyboard buttons, save current string in text box
                textBox.enabled = false;
                backupText = textBox.text;
                Transform keyboard = transform.GetChild(keyboardIdx);
                keyboard.gameObject.SetActive(false);

                phoneScreen.GetComponent<Renderer>().material = screens[1];
                screen = 1; 
            }
            else
            {
                //enable text screen and keyboard buttons, restore text box progress
                textBox.enabled = true;
                textBox.text = backupText;
                Transform keyboard = transform.GetChild(keyboardIdx);
                keyboard.gameObject.SetActive(true);

                phoneScreen.GetComponent<Renderer>().material = screens[0];
                screen = 0;
            }
        }

        //Cursed cursor code                                                    *****OWO ***********TODO************
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        input.x *= Time.deltaTime;    //time independence makes it super slow and cant get faster...
        input.y *= Time.deltaTime;
        Debug.Log(input.x*cursorDamp);
        Debug.Log(input.y*cursorDamp);
        //current cursor position
        Vector2 curr = cursor.transform.localPosition;
        //cursor boundaries, only add components that wont go past boundaries
        if (curr.y+input.y < .475f && curr.y+input.y > -.4786f)
            cursor.transform.localPosition += new Vector3(0, input.y, 0);
        if (curr.x+input.x < .4554f && curr.x+input.x > -.456f)
            cursor.transform.localPosition += new Vector3(input.x, 0, 0);
        //cursor.transform.localPosition = new Vector3(curr.x + (input.x * cursorDamp), curr.y + (input.y*cursorDamp), -0.68f); //current z position is constant
        //cursor.transform.localPosition += new Vector3(input.x, input.y,0);

        //Cursed code - Disable cursor collider ASAP. Placing this line here makes us wait for the next frame
        //if (cursed.enabled == true)
            //cursed.enabled = false;

        //Clicking joystick is a click on the cursor position, using collisions!
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            //Debug.Log("thumb press uwu");
            //enable cursor collision
            cursed.enabled = true;

            //Invoke is rarted and doesnt work at high speeds.
            EventManager.StartListening("sent", disableCollider);
            //However, there is the case in which the player presses a non button. We cannot poll events forever, so invoke a canceller function.
            Invoke("disableCollider", pressSpeed);
        }
    }

    void disableCollider()
    {
        cursed.enabled = false;
        EventManager.StopListening("sent", disableCollider);
    }
}
