using UnityEngine;
using System.Collections;

public class NetworkCamera : MonoBehaviour {

	void Update ()
    {
        if (GetComponent<NetworkView>().isMine)
            GetComponent<Camera>().enabled = true;
    }
}
