using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class MoveAcc : MonoBehaviour
{
    public TextMeshProUGUI outputText;
    public TMP_InputField axInput;
    public TMP_InputField azInput;
    public TMP_InputField bxInput;
    public TMP_InputField bzInput;
    public TMP_InputField t1Input;
    public TMP_InputField t2Input;
    public TMP_InputField t3Input;

    private float _speedX, _speedZ ;
    private string _speedOutputs;

    private float accelerationX , accelerationZ ;
    private float acAcX, acAcZ;
    private float t1, t2, t3;
    private float _timePassed, _distance , _resultSpeed, _resultAcceleration;

    private bool _isParsed,_isOutputed;
    
    private void Start()
    {
        Time.timeScale = 0f;
    }
    private string setOutputText()
    {
        return            
                          $"\nСкорость X:\n {_speedX:f3}" + $"\nСкорость Y:\n {_speedZ:f3}" + $"\nРезультирующая скорость:\n {_resultSpeed:f3}" + 
                          $"\nУскорение X:\n {accelerationX:f3}" + $"\nУскорение Y:\n {accelerationZ:f3}" + 
                          $"\nРезультирующее ускорение:\n {_resultAcceleration:f3}"+
                          $"\nВремя: {_timePassed:f3}" + 
                          $"\nx:\n {transform.position.x:f3}" + $"\nz:\n {transform.position.z:f3}";
    }
    
    private void Update()
    {
        if(!_isOutputed)outputText.text = "Вывод в t3\n \n \n"+
                          $"Прошедшее время: {_timePassed:f3}";
    }
    
    public void ReadInputs()
    {
        accelerationX = axInput.text is "" or "-" ? 0 : float.Parse(axInput.text);
        accelerationZ = azInput.text is "" or "-" ? 0 : float.Parse(azInput.text);
        acAcX = bxInput.text is "" or "-" ? 0 : float.Parse(bxInput.text);
        acAcZ = bzInput.text is "" or "-" ? 0 : float.Parse(bzInput.text);
        t1 = t1Input.text is "" or "-" || float.Parse(t1Input.text) <= 0
            ? 0f : float.Parse(t1Input.text);
        t2 = t2Input.text is "" or "-" || float.Parse(t2Input.text) <= 0
            ? 10f : float.Parse(t2Input.text);
        t3 = t3Input.text is "" or "-" || float.Parse(t3Input.text) <= 0
            ? 5f : float.Parse(t3Input.text);
    }
    private void FixedUpdate()
    {
        _timePassed += Time.fixedDeltaTime;

        
        if (Math.Abs(_timePassed - t3) < Time.fixedDeltaTime / 2)
        {
            outputText.text = setOutputText();
            _isOutputed = true;
            _speedOutputs = outputText.text;
            outputText.text = $"Дистанция:\n {_distance:f3}" + setOutputText();
            
        }

        if (Math.Abs(_timePassed - t2) < Time.fixedDeltaTime / 2)
        {
            outputText.text = $"Дистанция: {_distance:f3}" + _speedOutputs; 
            Time.timeScale = 0f;
        }

        if (_timePassed > t1)
        {
            _speedX += accelerationX * Time.fixedDeltaTime;             
            _speedZ += accelerationZ * Time.fixedDeltaTime;
            accelerationX += acAcX * Time.fixedDeltaTime;
            accelerationZ += acAcZ * Time.fixedDeltaTime;
        }

        _resultSpeed = Mathf.Sqrt(_speedX * _speedX + _speedZ * _speedZ);
        _resultAcceleration = Mathf.Sqrt(accelerationX * accelerationX + accelerationZ * accelerationZ);

        transform.position += new Vector3(_speedX*Time.fixedDeltaTime, 0, _speedZ*Time.fixedDeltaTime);
        
        _distance = _resultSpeed * _timePassed;
    }
}
