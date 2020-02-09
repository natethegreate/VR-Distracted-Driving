using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class keyboardManager : MonoBehaviour {

    // Use this for initialization
    public GameObject Q;
    public GameObject W;
    public GameObject E;
    public GameObject R;
    public GameObject T;
    public GameObject Y;
    public GameObject U;
    public GameObject I;
    public GameObject O;
    public GameObject P;
    public GameObject A;
    public GameObject S;
    public GameObject D;
    public GameObject F;
    public GameObject G;
    public GameObject H;
    public GameObject J;
    public GameObject K;
    public GameObject L;
    public GameObject Z;
    public GameObject X;
    public GameObject C;
    public GameObject V;
    public GameObject B;
    public GameObject N;
    public GameObject M;
    public GameObject Space;
    public GameObject Enter;
    public GameObject Backspace;

    public Text textBox;
    public Text msgBox;
    public Text multBox;

    //This will also generate strings for player to type, and score them as such.
    public ScoreManager script;
    public String copy;         //string that player needs to text
    public int msgIdx;
    private System.Random rand;
    private String[] messages = {"hello", "on my way", "thanks", "cool", "haha", "what", "where", "how long", "coming", "oh really", "lmao", "nani", "im driving", "almost there", "where you at"
    , "ill be late", "im early", "nice", "haha yes", "no way", "got it", "meet you there", "see you", "see ya later", "driving rn", "wait for me", "hold up", "very cool", "good meme", "what time", "in the car"
            , "driving"};  //32 strings for now
    //private string currEvent;
    void Start () {
        //This function will listen for button presses, using collisions.
        //Presume phone starts in the messenger screen.
        //Copy the following and use it in Update() if there exists other screens.
        //Debug.Log(Q.GetInstanceID().ToString() + "key");
        EventManager.StartListening(Q.GetInstanceID().ToString() + "Press", sendCharacterQ);
        EventManager.StartListening(W.GetInstanceID().ToString() + "Press", sendCharacterW);
        EventManager.StartListening(E.GetInstanceID().ToString() + "Press", sendCharacterE);
        EventManager.StartListening(R.GetInstanceID().ToString() + "Press", sendCharacterR);
        EventManager.StartListening(T.GetInstanceID().ToString() + "Press", sendCharacterT);
        EventManager.StartListening(Y.GetInstanceID().ToString() + "Press", sendCharacterY);
        EventManager.StartListening(U.GetInstanceID().ToString() + "Press", sendCharacterU);
        EventManager.StartListening(I.GetInstanceID().ToString() + "Press", sendCharacterI);
        EventManager.StartListening(O.GetInstanceID().ToString() + "Press", sendCharacterO);
        EventManager.StartListening(P.GetInstanceID().ToString() + "Press", sendCharacterP);
        EventManager.StartListening(A.GetInstanceID().ToString() + "Press", sendCharacterA);
        EventManager.StartListening(S.GetInstanceID().ToString() + "Press", sendCharacterS);
        EventManager.StartListening(D.GetInstanceID().ToString() + "Press", sendCharacterD);
        EventManager.StartListening(F.GetInstanceID().ToString() + "Press", sendCharacterF);
        EventManager.StartListening(G.GetInstanceID().ToString() + "Press", sendCharacterG);
        EventManager.StartListening(H.GetInstanceID().ToString() + "Press", sendCharacterH);
        EventManager.StartListening(J.GetInstanceID().ToString() + "Press", sendCharacterJ);
        EventManager.StartListening(K.GetInstanceID().ToString() + "Press", sendCharacterK);
        EventManager.StartListening(L.GetInstanceID().ToString() + "Press", sendCharacterL);
        EventManager.StartListening(Z.GetInstanceID().ToString() + "Press", sendCharacterZ);
        EventManager.StartListening(X.GetInstanceID().ToString() + "Press", sendCharacterX);
        EventManager.StartListening(C.GetInstanceID().ToString() + "Press", sendCharacterC);
        EventManager.StartListening(V.GetInstanceID().ToString() + "Press", sendCharacterV);
        EventManager.StartListening(B.GetInstanceID().ToString() + "Press", sendCharacterB);
        EventManager.StartListening(N.GetInstanceID().ToString() + "Press", sendCharacterN);
        EventManager.StartListening(M.GetInstanceID().ToString() + "Press", sendCharacterM);
        EventManager.StartListening(Space.GetInstanceID().ToString() + "Press", sendCharacterSpace);
        EventManager.StartListening(Enter.GetInstanceID().ToString() + "Press", sendCharacterEnter);
        EventManager.StartListening(Backspace.GetInstanceID().ToString() + "Press", sendCharacterBack);
        //Do we ever truly stop listening???
        textBox.text = "\n\n\n";

        //Generate copy string
        rand = new System.Random();
        msgIdx = rand.Next(messages.Length);
        copy = messages[msgIdx];
    }
	
	// Update is called once per frame
	void Update ()
    {
        msgBox.text = copy;
        //For now, print multiplier and score next to phone
        multBox.text = script.getMulti().ToString() + "x\n" + script.getScore().ToString();
	}

    //Simply manipulate the Canvas text with each button press.
    private void sendCharacterQ()
    {
        textBox.text = String.Concat(textBox.text, "q");
    }
    private void sendCharacterW()
    {
        textBox.text = String.Concat(textBox.text, "w");
    }
    private void sendCharacterE()
    {
        textBox.text = String.Concat(textBox.text, "e");
    }
    private void sendCharacterR()
    {
        textBox.text = String.Concat(textBox.text, "r");
    }
    private void sendCharacterT()
    {
        textBox.text = String.Concat(textBox.text, "t");
    }
    private void sendCharacterY()
    {
        textBox.text = String.Concat(textBox.text, "y");
    }
    private void sendCharacterU()
    {
        textBox.text = String.Concat(textBox.text, "u");
    }
    private void sendCharacterI()
    {
        textBox.text = String.Concat(textBox.text, "i");
    }
    private void sendCharacterO()
    {
        textBox.text = String.Concat(textBox.text, "o");
    }
    private void sendCharacterP()
    {
        textBox.text = String.Concat(textBox.text, "p");
    }
    private void sendCharacterA()
    {
        textBox.text = String.Concat(textBox.text, "a");
    }
    private void sendCharacterS()
    {
        textBox.text = String.Concat(textBox.text, "s");
    }
    private void sendCharacterD()
    {
        textBox.text = String.Concat(textBox.text, "d");
    }
    private void sendCharacterF()
    {
        textBox.text = String.Concat(textBox.text, "f");
    }
    private void sendCharacterG()
    {
        textBox.text = String.Concat(textBox.text, "g");
    }
    private void sendCharacterH()
    {
        textBox.text = String.Concat(textBox.text, "h");
    }
    private void sendCharacterJ()
    {
        textBox.text = String.Concat(textBox.text, "j");
    }
    private void sendCharacterK()
    {
        textBox.text = String.Concat(textBox.text, "k");
    }
    private void sendCharacterL()
    {
        textBox.text = String.Concat(textBox.text, "l");
    }
    private void sendCharacterZ()
    {
        textBox.text = String.Concat(textBox.text, "z");
    }
    private void sendCharacterX()
    {
        textBox.text = String.Concat(textBox.text, "x");
    }
    private void sendCharacterC()
    {
        textBox.text = String.Concat(textBox.text, "c");
    }
    private void sendCharacterV()
    {
        textBox.text = String.Concat(textBox.text, "v");
    }
    private void sendCharacterB()
    {
        textBox.text = String.Concat(textBox.text, "b");
    }
    private void sendCharacterN()
    {
        textBox.text = String.Concat(textBox.text, "n");
    }
    private void sendCharacterM()
    {
        textBox.text = String.Concat(textBox.text, "m");
    }
    private void sendCharacterSpace()
    {
        textBox.text = String.Concat(textBox.text, " ");
    }
    private void sendCharacterEnter()
    {
        //Call score function
        sendText(textBox.text.Substring(3));   //ignore first 3 newlines. Change this if newlines are changed!
        //Clear text box
        textBox.text = "\n\n\n";
    }
    //backspace removes last character
    private void sendCharacterBack()
    {
        if (textBox.text.Length >= 1)
            textBox.text = textBox.text.Remove(textBox.text.Length - 1);
    }

    private void sendText(String tex)
    {
        Debug.Log("sending text");
        if (tex.Equals(copy))
        {
            Debug.Log("match");
            //strings match! update scoring
            script.correctText(copy.Length);
            //TODO: Play different sound for correct or incorrect
        }
        else
        {
            Debug.Log("Incorrect match baka");
        }
        //Update copy string
        msgIdx = rand.Next(messages.Length);
        copy = messages[msgIdx];
    }

}
