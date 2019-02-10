using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 1000f;
    public float backForce = -1000f;
    public float leftForce = -500f;
    public float rightForce = 500f;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("forward", Forward);
        actions.Add("left", Left);
        actions.Add("right", Right);
        actions.Add("back", Back);
        actions.Add("faster", Faster);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Forward()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);
    }
    private void Left()
    {
        rb.AddForce(leftForce * Time.deltaTime, 0, 0);
    }
    private void Right()
    {
        rb.AddForce(rightForce * Time.deltaTime, 0, 0);
    }
    private void Back()
    {
        rb.AddForce(0, 0, backForce * Time.deltaTime);
    }
    private void Faster()
    {
        forwardForce += 200f;
        backForce -= 200f;
        leftForce -= 200f;
        rightForce += 200f;
    }
}
