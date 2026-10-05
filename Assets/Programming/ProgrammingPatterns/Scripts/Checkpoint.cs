using UnityEngine;

namespace Game
{
    public class Checkpoint : MonoBehaviour
    {
        public int Id => id;

        [SerializeField] private int id;
    }
}
