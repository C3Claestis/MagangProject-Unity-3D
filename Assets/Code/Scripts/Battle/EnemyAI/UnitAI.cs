
namespace Nivandria.Battle.AI
{
    using Nivandria.Battle.Action;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;
    public abstract class UnitAI : MonoBehaviour
    {
        protected Unit unit;
        protected BaseAction selectedAction;
        
        public abstract void HandleEnemyTurn();

        private void Start() {
            unit = GetComponent<Unit>();
        }
    }
}