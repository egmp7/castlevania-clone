using Player.StateManagement;
using System.Collections.Generic;
using UnityEngine;

namespace InputCommands
{

    public class Invoker : MonoBehaviour
    {

        public void Execute(MoveCommand command, StateMachine receiver)
        {
            command.Execute(receiver);
        }

        public void Execute(ButtonCommand command, StateMachine receiver)
        {
            command.Execute(receiver);
        }

        public void Execute(SensorCommand command, StateMachine receiver)
        {
            command.Execute(receiver);
        }
    }
}