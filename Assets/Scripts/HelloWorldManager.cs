using MLAPI;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldManager : MonoBehaviour
    {
        public Texture btnTexture;

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                GUIStyle myStyle = new GUIStyle();
                myStyle.active.background = (Texture2D)btnTexture;
                
                //if (GUI.Button(new Rect(200, 10, 500, 500), btnTexture, myStyle)) Debug.Log("ButtonPressed");

                StatusLabels();

                SubmitNewPosition();

                SubmitNewSkin();

                SubmitNewHat();
            }

            GUILayout.EndArea();
        }

        static void StartButtons()
        {
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
            if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
            if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
        }

        static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        static void SubmitNewPosition()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
            {
                if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
                    out var networkedClient))
                {
                    var player = networkedClient.PlayerObject.GetComponent<HelloWorldPlayer>();
                    if (player)
                    {
                        player.Move();
                    }
                }
            }
        }


        static void SubmitNewSkin()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Change Skin" : "Request Change Skin"))
            {
                if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
                    out var networkedClient))
                {
                    var player = networkedClient.PlayerObject.GetComponent<SkinPickerScript>();
                    if (player)
                    {
                        player.ChangeSkin();
                    }
                }
            }
        }

        static void SubmitNewHat()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Change Hat" : "Request Change Hat"))
            {
                if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
                    out var networkedClient))
                {
                    var player = networkedClient.PlayerObject.GetComponent<HatPicker>();
                    if (player)
                    {
                        player.ChangeHat();
                    }
                }
            }
        }
    }
}