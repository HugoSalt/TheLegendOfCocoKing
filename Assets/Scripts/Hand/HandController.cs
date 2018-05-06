using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    private SteamVR_TrackedController device;
    private HandInteractor handInteractorScript;
    public HandInteractor handInteractorObject;

	// Use this for initialization
	void Start () {
        device = GetComponent<SteamVR_TrackedController>();
        device.TriggerClicked += TriggerClick;
        device.TriggerUnclicked += TriggerUnclick;
        handInteractorScript = handInteractorObject.GetComponent<HandInteractor>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TriggerClick(object sender, ClickedEventArgs e) {
        handInteractorScript.setGrab(true);
    }

    void TriggerUnclick(object sender, ClickedEventArgs e)
    {
        handInteractorScript.setGrab(false);
    }
}
