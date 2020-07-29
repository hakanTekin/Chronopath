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

        [SerializeField] private GameObject electricParticle = Resources.Load<GameObject>("JMO Assets/Cartoon FX/CFX Prefabs/Electric/CFX_ElectricityBall");

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
            collider = this.GetGameObject().GetComponent<BoxCollider2D>();
            if (electricParticle != null) {
                GameObject particleSystemGO = GameObject.Instantiate(electricParticle, this.GetGameObject().transform);
                ParticleSystem particle = particleSystemGO.GetComponent<ParticleSystem>();
                
                var main = particle.main;
                var x = this.collider.bounds.size.x;
                var y = this.collider.bounds.size.y;
                main.startSize = x > y ? x : y;

                particle.gameObject.transform.position = this.GetGameObject().transform.position;
                particle.Play(true);
            }
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
                if (c != null && c.gameObject.CompareTag("Player"))
                    c.gameObject.SendMessage("Death");
            }
        }
    }
}
