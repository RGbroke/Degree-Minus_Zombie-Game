using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class flashlightDecay : MonoBehaviour
{
    [SerializeField] private float fovDecayPerSecond = 1f;
    [SerializeField] private float secondsPerPause = 30f;
    [SerializeField] private FieldOfView fovControl;

    private CancellationTokenSource cancellationToken;
    private float originalFOV;
    private float currentFOV;
    private bool[] activeDecay = { false, true };

    private void Start()
    {
        originalFOV = fovControl.currentFOV();
        pauseDecay(secondsPerPause);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!activeDecay[0] || !activeDecay[1])
            return;

        currentFOV = fovControl.currentFOV() - Time.deltaTime * fovDecayPerSecond;
        if (currentFOV <= 0)
        {
            activeDecay[0] = false;
            currentFOV = 0f;
        }
        fovControl.setFOV(currentFOV);
    }

    public void flashlightToggle(bool isOn)
    {
        activeDecay[1] = isOn;
        if (isOn)
            fovControl.setFOV(currentFOV);
        else
            fovControl.setFOV(0);
    }

    public void setActiveDecay(bool isActive)
    {
        activeDecay[1] = isActive;
    }

    public async void pauseDecay(float delayInSeconds)
    {
        activeDecay[0] = false;
        int waitTime = (int)delayInSeconds * 1000;
        await Task.Delay(waitTime);
        activeDecay[0] = true;
    }

    public void batteryObtained()
    {
        fovControl.setFOV(originalFOV);
        pauseDecay(secondsPerPause);
    }
}
