using Player.StateManagement;
using System.Collections.Generic;
using UnityEngine;

namespace InputCommands
{

    public class Invoker : MonoBehaviour
    {

        public void Execute(ReusableCommand command, StateController receiver)
        {
            command.Execute(receiver);
        }

        public void Execute(CoupledCommand command, StateController receiver)
        {
            command.Execute(receiver);
        }
    }
}