using System;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private TouchButton m_LeftFlipperButton;
    [SerializeField]
    private TouchButton m_RightFlipperButton;
    [SerializeField]
    private Button m_PlungerButton;
    [SerializeField]
    private Slider m_PlungerPowerSlider;

    public Action OnLeftFlipperBtnDown;
    public Action OnLeftFlipperBtnUp;

    public Action OnRightFlipperBtnDown;
    public Action OnRightFlipperBtnUp;

    public Action<float> OnPlungerBtnPressed;

    private float m_DefaultPlungerPower;
    private bool m_AreInputsEnabled;

    public void EnableInputs()
    {
        m_LeftFlipperButton.OnButtonDown += OnLeftFlipperButtonDown;
        m_LeftFlipperButton.OnButtonUp += OnLeftFlipperButtonUp;

        m_RightFlipperButton.OnButtonDown += OnRightFlipperButtonDown;
        m_RightFlipperButton.OnButtonUp += OnRightFlipperButtonUp;

        m_PlungerButton.onClick.AddListener(OnPlungerButtonPressed);

        //Initialize values for plunger power slider
        m_PlungerPowerSlider.minValue = 0.0f;
        m_PlungerPowerSlider.maxValue = 1.0f;
        m_PlungerPowerSlider.value = 0.5f;
        m_DefaultPlungerPower = m_PlungerPowerSlider.value;

        m_PlungerPowerSlider.onValueChanged.AddListener(OnPlungerSliderValueChanged);

        m_AreInputsEnabled = true;
    }
    public void DisableInputs()
    {
        m_AreInputsEnabled = false;

        m_LeftFlipperButton.OnButtonDown -= OnLeftFlipperButtonDown;
        m_LeftFlipperButton.OnButtonUp -= OnLeftFlipperButtonUp;

        m_RightFlipperButton.OnButtonDown -= OnRightFlipperButtonDown;
        m_RightFlipperButton.OnButtonUp -= OnRightFlipperButtonUp;

        m_PlungerButton.onClick.RemoveListener(OnPlungerButtonPressed);

        m_PlungerPowerSlider.onValueChanged.RemoveListener(OnPlungerSliderValueChanged);
    }

    private void OnLeftFlipperButtonDown()
    {
        if (OnLeftFlipperBtnDown != null)
            OnLeftFlipperBtnDown();
    }
    private void OnRightFlipperButtonDown()
    {
        if (OnRightFlipperBtnDown != null)
            OnRightFlipperBtnDown();
    }
    private void OnLeftFlipperButtonUp()
    {
        if (OnLeftFlipperBtnUp != null)
            OnLeftFlipperBtnUp();
    }
    private void OnRightFlipperButtonUp()
    {
        if (OnRightFlipperBtnUp != null)
            OnRightFlipperBtnUp();
    }
    private void OnPlungerSliderValueChanged(float Value)
    {
        m_DefaultPlungerPower = Value;
    }

    private void OnPlungerButtonPressed()
    {
        if (OnPlungerBtnPressed != null)
            OnPlungerBtnPressed(m_DefaultPlungerPower);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (!m_AreInputsEnabled)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
            OnLeftFlipperButtonDown();
        else if (Input.GetKeyUp(KeyCode.Q))
            OnLeftFlipperButtonUp();

        if (Input.GetKeyDown(KeyCode.D))
            OnRightFlipperButtonDown();
        else if (Input.GetKeyUp(KeyCode.D))
            OnRightFlipperButtonUp();

        if (Input.GetKeyDown(KeyCode.Space))
            OnPlungerButtonPressed();
#endif
    }
}
