using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

namespace HelloWorld
{
    public class HatPicker : NetworkBehaviour
    {

        public GameObject[] hats;
        
        private int hatNum=1;

        public NetworkVariableInt Hat = new NetworkVariableInt(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.ServerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        });

        public override void NetworkStart()
        {
            //foreach(GameObject hat in hats)
          //  {
          //      hat.SetActive(false);
          //  }
           // hatNum = 1;
            //ChangeHat();
        }

        public void ChangeHat()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                Hat.Value = PickNextHat(Hat.Value);
            }
            else
            {
                SubmitHatRequestServerRpc();
            }
        }

        [ServerRpc]
        void SubmitHatRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Hat.Value = PickNextHat(Hat.Value);
        }

        static int PickNextHat(int num)
        {
            return (num + 1) % 11;
        }

        void Update()
        {
            if (hatNum != Hat.Value)
            {
                hats[hatNum].SetActive(false);
                hats[Hat.Value].SetActive(true);
                hatNum = Hat.Value;
            }
        }
    }
}