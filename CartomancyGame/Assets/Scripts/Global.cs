using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    // Start is called before the first frame update

    public static Global g;

    public enum State
    {
        test,
        other
    }

    public State state;

    // Update is called once per frame
    void Awake()
    {
        g = this;
        state = State.other; //just testing
    }
}
