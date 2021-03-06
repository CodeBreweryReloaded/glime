using CodeBrewery.Glime.Battle.Potions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace CodeBrewery.Glime.Battle
{
    /// <summary>
    /// Represents an encounter.
    /// </summary>
    public class EncounterManager : MonoBehaviour
    {
        /// <summary>
        /// The enemies which might spawn during the encounter.
        /// </summary>
        [SerializeField]
        private GameObject[] enemyCandidates;

        /// <summary>
        /// The transformation of the game-object.
        /// </summary>
        public Transform TargetTransform;

        /// <summary>
        /// Gets or sets the instance of the player.
        /// </summary>
        public Participant Player { get; set; }

        /// <summary>
        /// Gets the enemies participating in the encounter.
        /// </summary>
        public GameObject[] EnemyCandidates => enemyCandidates;

        /// <summary>
        /// The enemies which are pending to finish their turn.
        /// </summary>
        private List<Enemy> enemiesCurrentlyInTurn = new List<Enemy>();

        /// <summary>
        /// Gets a value indicating whether the battle is ongoing.
        /// </summary>
        public bool BattleOngoing { get; private set; }

        /// <summary>
        /// Gets or sets the target of the enemies.
        /// </summary>
        public Vector3 EnemyTarget => TargetTransform.position;


        /// <summary>
        /// Gets or sets the number of enemies.
        /// </summary>
        public int EnemyCount { get; set; }

        /// <summary>
        /// Occurs when a turn stopped.
        /// </summary>
        public UnityEvent OnTurnStoppedEvent;

        /// <summary>
        /// Gets the time which passed during the battle.
        /// </summary>
        public TimeSpan BattleTime { get; private set; }

        public UnityEvent<int, List<Potion>> OnTurnStartEvent;

        /// <summary>
        /// Gets the number of minutes which passed during the battle.
        /// </summary>
        public int BattleTimeMinutes => Mathf.FloorToInt(BattleTime.Minutes);

        /// <summary>
        /// Gets the number of seconds in the current minute which passed during the battle.
        /// </summary>
        public int BattleTimeSeconds => Mathf.FloorToInt(BattleTime.Seconds);

        /// <summary>
        /// The number of turns which were played so far.
        /// </summary>
        public int TurnCount { get; private set; } = 1;

        /// <summary>
        /// Handles the initialization.
        /// </summary>
        public void Start()
        { }

        /// <summary>
        /// Handles updates prior to each frame.
        /// </summary>
        void Update()
        {
            if (BattleOngoing)
            {
                BattleTime += TimeSpan.FromSeconds(Time.deltaTime);
            }
        }

        public void StartTurn(List<Potion> potions)
        {

            
            var rand = new System.Random();
            int enemyCount = Mathf.Min(1 + ((TurnCount ^ 2) / 10), 100);
            Vector3 location = transform.position;
            enemiesCurrentlyInTurn.Clear();
            BattleOngoing = true;

            for (int i = 0; i < enemyCount; i++)
            {
                Func<float, float, float> nextFloat = (float min, float max) =>
                {
                    System.Random random = new System.Random();
                    double val = (random.NextDouble() * (max - min) + min);
                    return (float)val;
                };

                float vX = nextFloat(-15f, 15f);
                float vY = nextFloat(0f, 15f);

                Enemy enemy = Instantiate(
                        EnemyCandidates[rand.Next(EnemyCandidates.Length)],
                        new Vector3(x: location.x + vX, y: location.y + vY, z: location.z),
                        transform.rotation,
                        transform).GetComponent<Enemy>();

                enemy.TurnStarts(this);
                enemiesCurrentlyInTurn.Add(enemy);
            }

            OnTurnStartEvent.Invoke(TurnCount, potions);
        }

        public void StopTurn(Enemy enemy)
        {
            Debug.Log("removed enemy: " + enemiesCurrentlyInTurn.Remove(enemy));
            Debug.Log("missing end turns from " + enemiesCurrentlyInTurn.Count);

            if (enemiesCurrentlyInTurn.Count == 0)
            {
                TurnCount++;
                BattleOngoing = false;
                OnTurnStoppedEvent.Invoke();
            }
        }
    }
}
