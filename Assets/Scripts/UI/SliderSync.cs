using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderSync : MonoBehaviour
{
    private float value;
    public bool isInt;
    InputField text;
    Slider slider;

    private void Awake()
    {
        text = transform.Find("Input").GetComponent<InputField>();
        slider = transform.Find("Slider").GetComponent<Slider>();
        slider.value = value;
        text.text = value.ToString();
    }
    void Start()
    {

    }

    void Update()
    {
        
    }

    private float InputCheck()
    {
        if (text.text.Contains(".")) { text.text.Replace('.', ','); }
        float input = float.Parse(text.text);
        float val;
        if (isInt)
            val = Mathf.Round(input);
        else
            val = Mathf.Round(input * 10) / 10f;
        if (val > slider.maxValue)
            val = slider.maxValue;
        else if (val < slider.minValue)
            val = slider.minValue;
        return val;
    }

    private float SliderCheck()
    {
        float val;
        if (isInt)
            val = Mathf.Round(slider.value);
        else
            val = Mathf.Round(slider.value * 10) / 10f;
        if (val > slider.maxValue)
            val = slider.maxValue;
        else if (val < slider.minValue)
            val = slider.minValue;
        return val;
    }

    public float GetValue()
    {
        return value;
    }

    public void SetValue(float val)
    {
        text.text = val.ToString();
        OnTextValueChanged();
    }

    public void OnSliderValueChanged() 
    {
        float val = SliderCheck();
        slider.value = val;
        text.text = val.ToString();
        value = val;
    }

    public void OnTextValueChanged() 
    {
        float val = InputCheck();
        slider.value = val;
        text.text = val.ToString();
        value = val;
    }
}
