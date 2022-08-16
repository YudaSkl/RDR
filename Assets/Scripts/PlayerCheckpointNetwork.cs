using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerCheckpointNetwork : NetworkBehaviour
{
    private Dictionary<NetworkVariable<int>, NetworkVariable<float>> checkpoints;
    //[SerializeField] private MeshRenderer _renderer;

    private void Awake()
    {
        // Subscribing to a change event. This is how the owner will change its color.
        // Could also be used for future color changes
        checkpoints = new Dictionary<NetworkVariable<int>, NetworkVariable<float>>();
    }

    public override void OnDestroy()
    {

    }

    private void OnValueChanged(Color prev, Color next)
    {
        _renderer.material.color = next;
    }

    public override void OnNetworkSpawn()
    {
        // Take note, RPCs are queued up to run.
        // If we tried to immediately set our color locally after calling this RPC it wouldn't have propagated
        if (IsOwner)
        {
            _index = (int)OwnerClientId;
            CommitNetworkColorServerRpc(GetNextColor());
        }
        else
        {
            _renderer.material.color = _netColor.Value;
        }   
    }

    [ServerRpc]
    private void CommitNetworkColorServerRpc(Color color)
    {
        _netColor.Value = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;
        CommitNetworkColorServerRpc(GetNextColor());
    }

    private void AddCheckpoint(float time, int index)
    {
        _netCheckpoints.Value.i += _colors[_index++ % _colors.Length];
    }

    private struct PlayerCheckpoints : INetworkSerializable
    {
        public float time;
        public float count;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref time);
            foreach (int i in indexes){
                serializer.SerializeValue(ref i);
            }
        }
    }
}
