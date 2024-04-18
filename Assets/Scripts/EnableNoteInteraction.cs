using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableNoteInteraction : MonoBehaviour
{
    public ShowPlayerText noteInteractionScript;
    
    // Start is called before the first frame update
    void Start()
    {
        noteInteractionScript.enabled = false;
    }

    // Update is called once per frame
    public void enableNoteInteraction()
    {
        noteInteractionScript.enabled = true;

    }
    

}
