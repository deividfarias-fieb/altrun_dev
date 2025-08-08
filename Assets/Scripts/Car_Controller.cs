using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    [SerializeField] private SliderJoint2D sliderPlayer; //O [SerializeField] ajuda a otimizar o jogo e cumpre a mesma fun��o que o public
    [SerializeField] private JointMotor2D motorTranslation;
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;  
        motorTranslation = sliderPlayer.motor;
        sliderPlayer.motor = motorTranslation;
    }

    // Update is called once per frame
    void Update()
    {
        if (sliderPlayer.limitState == JointLimitState2D.LowerLimit)
        {
            transform.position = initialPosition;
        }
    }
}
