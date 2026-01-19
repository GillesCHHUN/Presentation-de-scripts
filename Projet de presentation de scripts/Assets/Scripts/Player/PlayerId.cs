using System;
using UnityEngine;

namespace Player.Core
{
    public class PlayerId : MonoBehaviour
    {
        [SerializeField] private PlayerReferenceSO playerRef;
        
        private void OnEnable()
        {
            playerRef.TransformSO = this.transform;
        }

    }
}

