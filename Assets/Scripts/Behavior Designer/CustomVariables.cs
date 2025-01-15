using BehaviorDesigner.Runtime;
using egmp7.Game.Sensors;
using UnityEngine;

namespace egmp7.BehaviorDesigner
{
    public class CustomVariables : MonoBehaviour
    {
        [System.Serializable]
        public class SharedCollisionSensor : SharedVariable<CollisionSensor>
        {
            public static implicit operator SharedCollisionSensor(CollisionSensor value)
            { 
                return new SharedCollisionSensor { Value = value }; 
            }
        }
    }
}

