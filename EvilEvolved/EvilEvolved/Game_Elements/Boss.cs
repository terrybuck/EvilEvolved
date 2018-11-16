using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    public class Boss : Villain
    {
        /// <summary>
        /// Create a Boss for the hero to battle against.
        /// </summary>
        /// <param name="name">The name of the scene if any.</param>
        public Boss(string name)
            : base(name)
        {

            TimeBetweenAttacks = 1000;
            Velocity = (float)(1.0 / 60.0);
            DirectionY = 0;
            DirectionX = 0;
            MaxHealth = 500.0f;
            CurrentHealth = 500.0f;
            Level = 1;
            LastSpawn = DateTime.Now;
            TimeBetweenSpawns = 10000;
            Damage = 100.0f;
            Range = 200;

        }

        public override void Update(TimeSpan dt, GenericInput input)
        {
            base.Update(dt, input);

            SpawnTimer = DateTime.Now - LastSpawn;
            if(SpawnTimer.TotalMilliseconds > TimeBetweenSpawns)
            {
                Message_SpawnMinions spawn = new Message_SpawnMinions(Level);
                MessageManager.AddMessageItem(spawn);
                LastSpawn = DateTime.Now;
            }

        }

        #region -----[Properties]

        public int Level { get; set; }
        private TimeSpan SpawnTimer;
        private DateTime LastSpawn;
        private int TimeBetweenSpawns; //(Terry)TODO: consider making this readonly or decrease spawn time/level instead of increasing # of spawns

        #endregion
    }
}
