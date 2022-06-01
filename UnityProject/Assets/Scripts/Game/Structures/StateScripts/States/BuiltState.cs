using Game.Resources;
using UnityEngine;

namespace Game.Structures.StateScripts.States
{
    public class BuiltState : State
    {
        [SerializeField] private Costs costs;
	
        public Costs Costs
        {
            get { return costs; }
        }
    }
}
