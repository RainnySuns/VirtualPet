using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Votanic.vNet.Database;
using Votanic.vXR.vGear;


public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed = 0.5f;

    public float jumpHeight = 2.0f;
    public float jumpFrequency = 2.0f;

    public float amplitude = 0.5f;
    public float frequency = 1f;

    private Vector3 startPos;
    private float phase = 0.0f;
    

    void Start()
    {
        startPos = transform.localPosition;
        textComponent = transform.GetComponent<TextMeshProUGUI>();
        BounceEffect();
    }

    public void speak(string[] lines)
    {
        StartCoroutine(DisplayLines(lines, 2f)); 
    }

    IEnumerator DisplayLines(string[] lines, float delay)
    {
        foreach (var line in lines)
        {
            //textComponent.text = line;
            StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(delay);
        }
    }

    public void speakline(string line)
    {
        StartCoroutine(TypeLine(line));
    }

    IEnumerator TypeLine(string line)
    {
        textComponent.text = string.Empty;
        foreach(char c in line.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }


    public void ChangeText(string text)
    {
        textComponent.text = text;
        BounceEffect();
    }

    public void BounceEffect()
    {
        phase += jumpFrequency * Time.deltaTime;
        float height = Mathf.Abs(Mathf.Sin(phase)) * jumpHeight;
        transform.localPosition = new Vector3(startPos.x, startPos.y + height, startPos.z);
    }

}
