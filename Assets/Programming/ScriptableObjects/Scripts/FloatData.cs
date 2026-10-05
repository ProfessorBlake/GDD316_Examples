using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName ="FloatData")]
    public class FloatData : ScriptableObject
    {
        public Action<float> onValueChange;

        private float value;

        public FloatData(float value)
        {
            this.value = value;
        }

        public void SetFloat(float newValue)
        {
            value = newValue;
        }
    }
}
