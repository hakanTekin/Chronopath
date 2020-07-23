using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

namespace Assets.Code.World.Obstacle.Decorator
{
    public enum BlipType
    {
        None = 0,
        Standart = 1,
        Existing = 2,
        Delayed = 3
    }
    class BlipDecorator : ObstacleDecorator
    {
        private int score = 50;
        public override int Score { get => score; set => score = value; }
        protected World world;

        private Color color;
        private SpriteShapeRenderer renderer;

        public BlipType type;

        /// <summary>
        /// Time interval between blipping on and off
        /// </summary>
        protected int blipInterval = int.MaxValue;
        [SerializeField] private int minBlipInterval, maxBlipInterval;
        BoxCollider2D collider;

        public BlipDecorator(IObstacle w) : base(w)
        {
            world = w.GetWorld();
            Score = score + base.Score;
            ConfigureBlip();
            renderer = w.GetGameObject().GetComponent<SpriteShapeRenderer>();
            renderer.color = Color.black;
            collider = this.GetGameObject().GetComponent<BoxCollider2D>();
        }

        /// <summary>
        /// Randomly generates the 
        /// </summary>
        protected virtual void ConfigureBlip()
        {
            this.blipInterval = UnityEngine.Random.Range(10, 50);
        }
        public string GetType()
        {
            throw new NotImplementedException();
        }

        public override void UpdateObstacle()
        {   
            wrappee.UpdateObstacle();

            int result = ((int)(world.worldTime / blipInterval) % 2);
            if (result == 1)//hide
            {
                BlipOff();
            }
            else if (result == 0)//Show
            {
                BlipOn();
            }
        }

        private void BlipOff()
        {
            this.GetGameObject().SetActive(false);
        }
        
        private void BlipOn()
        {
            Collider2D[] results = new Collider2D[6];
            this.GetGameObject().SetActive(true);
            collider.OverlapCollider(new ContactFilter2D(), results);
            foreach(Collider2D c in results)
            {
                if(c !=null)
                    c.gameObject.SendMessage("Death");
            }
        }
    }
}
