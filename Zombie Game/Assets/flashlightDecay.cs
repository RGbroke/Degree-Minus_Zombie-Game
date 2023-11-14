using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

public class flashlightDecay : MonoBehaviour
{
    [SerializeField] private float fovDecayPerSecond = 1f;
    [SerializeField] private FieldOfView fovControl;
    private float originalFOV;
    private bool activeDecay = false;

    private void Start()
    {
        originalFOV = fovControl.currentFOV();
        activeDecay = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!activeDecay)
        {
            return;
        }

        float newFOV = fovControl.currentFOV() - Time.deltaTime * fovDecayPerSecond;
        if (newFOV <= 0)
        {
            activeDecay = false;
            newFOV = 0f;
        }
        fovControl.setFOV(newFOV);
    }

    public void setActiveDecay(bool isActive)
    {
        activeDecay = isActive;
    }

    public async void pauseDecay(float delayInSeconds)
    {
        activeDecay = false;
        int waitTime = (int) delayInSeconds * 1000;
        await Task.Delay(waitTime);
        activeDecay = true;
    }

    public void batteryObtained()
    {
        if (!activeDecay)
            return;

        fovControl.setFOV(originalFOV);
        pauseDecay(60f);
    }
}
