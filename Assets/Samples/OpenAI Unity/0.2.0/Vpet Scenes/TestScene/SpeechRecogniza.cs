using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;


public class SpeechRecogniza : MonoBehaviour
{
    public Text hypothesesText;
    public Text recognitionsText;
    public Button startBtn;
    public Button stopBtn;
    public SendTextData send;

    private DictationRecognizer dictationRecognizer;

    void Start()
    {
        dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationResult += OnDictationResult;
        dictationRecognizer.DictationHypothesis += OnDictationHypothesis;
        dictationRecognizer.DictationComplete += OnDictationComplete;
        dictationRecognizer.DictationError += OnDictationError;

        startBtn.onClick.AddListener(DictationStart);
        stopBtn.onClick.AddListener(DictionStop);
    }

    private void OnDestroy()
    {
        dictationRecognizer.Stop();
        dictationRecognizer.Dispose();
    }


    private void OnDictationResult(string text, ConfidenceLevel confidence)
    {
        Debug.LogFormat("识别结果: {0}", text);
        recognitionsText.text = text;
        send.DeliverText(text);

        DictionStop();
    }

    private void OnDictationHypothesis(string text)
    {
        Debug.LogFormat("识别猜想: {0}", text);
        hypothesesText.text = text;
    }

    private void OnDictationComplete(DictationCompletionCause cause)
    {
        if (cause != DictationCompletionCause.Complete)
            Debug.LogErrorFormat("识别失败: {0}.", cause);
    }

    private void OnDictationError(string error, int hresult)
    {
        Debug.LogErrorFormat("识别错误: {0}; HResult = {1}.", error, hresult);
    }

    public void DictationStart()
    {
        dictationRecognizer.Start();
        startBtn.gameObject.SetActive(false);
        stopBtn.gameObject.SetActive(true);
    }

    public void DictionStop()
    {
        dictationRecognizer.Stop();
        startBtn.gameObject.SetActive(true);
        stopBtn.gameObject.SetActive(false);
    }

}
