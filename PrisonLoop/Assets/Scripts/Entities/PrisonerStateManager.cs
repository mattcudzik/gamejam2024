using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    public class PrisonerStateManager : EntityStateManager
    {
        [SerializeField] private NavMeshData _meshData;
        private EntityMovement _entityMovement { get; set; }

        private void Start()
        {
            _entityMovement = GetComponent<EntityMovement>();
        }

        public NavMeshData MeshData => _meshData;

    }
}