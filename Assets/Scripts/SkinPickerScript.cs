using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

namespace HelloWorld
{
    public class SkinPickerScript : NetworkBehaviour
    {

        public Material[] skins;
        public SkinnedMeshRenderer meshRenderer;


        public NetworkVariableInt Skin = new NetworkVariableInt(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.ServerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        });

        public override void NetworkStart()
        {
           // ChangeSkin();
        }

        public void ChangeSkin()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                Skin.Value = PickNextSkin(Skin.Value);
            }
            else
            {
                SubmitSkinRequestServerRpc();
            }
        }

        [ServerRpc]
        void SubmitSkinRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Skin.Value = PickNextSkin(Skin.Value);
        }

        static int PickNextSkin(int num)
        {
            return (num + 1)%8;
        }

        void Update()
        {
            meshRenderer.material = skins[Skin.Value];   
        }
    }
}