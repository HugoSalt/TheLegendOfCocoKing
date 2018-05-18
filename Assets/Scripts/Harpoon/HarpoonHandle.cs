using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonHandle : MonoBehaviour
{

    private Harpoon harpoonScript;
    private bool startedGrabbing;

    // Use this for initialization
    void Start()
    {
        harpoonScript = transform.parent.GetComponent<Harpoon>();
        startedGrabbing = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "HAND_INTERACTOR")
        {

            Vector3 currentGrabPos = other.gameObject.transform.position;
            Quaternion currentGrabRot = other.gameObject.transform.rotation;

            if (other.gameObject.GetComponent<HandInteractor>().IsGrabbing())
            {
                harpoonScript.MoveHarpoon(currentGrabPos, currentGrabRot, other.gameObject);
                startedGrabbing = true;
            }
            else if (startedGrabbing)
            {
                harpoonScript.ReleaseHarpoon(currentGrabPos, currentGrabRot, other.gameObject);
                startedGrabbing = false;
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HAND_INTERACTOR")
        {
            Vector3 currentGrabPos = other.gameObject.transform.position;
            Quaternion currentGrabRot = other.gameObject.transform.rotation;
            harpoonScript.ReleaseHarpoon(currentGrabPos, currentGrabRot, other.gameObject);
            startedGrabbing = false;
        }
    }

}
