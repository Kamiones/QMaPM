
using System;

public class Transition 
{
    public Func<bool> IsValid { private set; get; }
    public Func<PlayerState> GetNextState { private set; get; }

    public Transition(Func<bool> isValid, Func<PlayerState> getNextState)
    {
        IsValid = isValid;
        GetNextState = getNextState;
    }
}