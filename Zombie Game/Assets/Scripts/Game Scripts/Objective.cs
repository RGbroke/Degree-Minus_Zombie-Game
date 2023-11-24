using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective
{
    private string objectiveDescription;
    private int progressMade = 0;
    private int progressToComplete;
    private bool isComplete;
    private float timeTillHidden = 5f;

    public Objective(string description, int stepsToComplete = 1)
    {
        this.progressToComplete = stepsToComplete;
        setDescription(description);
    }
    public void progressObjective()
    {
        this.progressMade++;
        if(progressToComplete > 1)
            setDescription(this.objectiveDescription.Substring(0, objectiveDescription.IndexOf('(')-1));

        if(this.progressMade >= this.progressToComplete)
        {
            completeObjective();
        }
    }
    public void completeObjective()
    {
        this.isComplete = true;
    }
    public void setDescription(string newDescription)
    {
        this.objectiveDescription = newDescription;

        if (progressToComplete <= 1)
            return;

        this.objectiveDescription += " (" + progressMade.ToString() + "/" + progressToComplete.ToString() + ")";
    }
    public string getDescription()
    {
        return this.objectiveDescription;
    }
    public int getProgress()
    {
        return this.progressMade;
    }
    public bool isObjectiveComplete()
    {
        return this.isComplete;
    }
    public float reduceTime(float time)
    {
        this.timeTillHidden-=time;
        return this.timeTillHidden;
    }
}
