using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

    public GameObject playerPrefab;
    public Transform spawnObject;
	string gameName = "Tibinirim";
	HostData[] hostData;

	bool refreshing;

	float buttonX;
	float buttonY;
	float buttonW;
	float buttonH;


	// Use this for initialization
	void Start () {
		MasterServer.ipAddress = "192.168.0.2"; //24.36.54.199
		MasterServer.port = 23466;
		Network.natFacilitatorIP = "192.168.0.2";
		Network.natFacilitatorPort = 50005;

		buttonX = Screen.width * 0.05f;
		buttonY = Screen.height * 0.05f;
		buttonW = Screen.width * 0.1f;
		buttonH = Screen.width * 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (refreshing) {
			if (MasterServer.PollHostList ().Length > 0) {
				refreshing = false;
				Debug.Log (MasterServer.PollHostList ().Length);
				hostData = MasterServer.PollHostList ();
			}
		}
	}

	void startServer() {
		Network.InitializeServer (32, 25001, !Network.HavePublicAddress());
		MasterServer.RegisterHost (gameName, "Temp Game Name", "This is going to be amazing!");
	}

	void refreshHostList() {
		MasterServer.RequestHostList (gameName);
		refreshing = true;
		Debug.Log(MasterServer.PollHostList().Length);
	}

	void OnServerInitialized() {
		Debug.Log ("server initialized");
        spawnPlayer();
	}

    void OnConnectedToServer()
    {
        spawnPlayer();
    }

	void OnMasterServerEvent(MasterServerEvent mse) {
		if (mse == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log ("registered server");
		}
	}

    void spawnPlayer()
    {
        Network.Instantiate(playerPrefab, spawnObject.position, Quaternion.identity, 0);
        //Load their stuff from database here
    }

	void OnGUI() {
		if (!Network.isClient && !Network.isServer) {
			if (GUI.Button (new Rect (buttonX, buttonY, buttonW, buttonH), "Start Server")) {
				Debug.Log ("starting server");
				startServer ();

			}
			if (GUI.Button (new Rect (buttonX, buttonY * 1.2f + buttonH, buttonW, buttonH), "Refresh Hosts")) {
				Debug.Log ("Refreshing hosts");
				refreshHostList ();
			}

			if (hostData != null) {
				for (int i = 0; i < hostData.Length; i++) {
					if (GUI.Button (new Rect (buttonX * 1.5f + buttonW, buttonY * 1.2f + (buttonH * i) + buttonH, buttonW * 3.0f, buttonH * 0.5f), hostData [i].gameName)) {
						Network.Connect (hostData [i]);
					}
					
				}
			}
		}
	}

}
