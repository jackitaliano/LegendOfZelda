using JalPals.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using JalPals.Rooms;
using System;
using JalPals.Projectiles;

namespace JalPals.Enemies
{
    public class EnemyManager
    {
        public List<ISprite> roomEnemies = new List<ISprite>();
        public int numEnemies;
        private ContentLoader contentLoader;
        private RoomManager roomManager;
        private Random rnd = new Random(Guid.NewGuid().GetHashCode());

        public EnemyManager(RoomManager roomManager, ContentLoader contentLoader)
        {
            this.roomManager = roomManager;
            this.contentLoader = contentLoader;
            numEnemies = 0;
        }

        public void Remove(ISprite enemy)
        {
            roomEnemies.Remove(enemy);
            numEnemies--;

        }

        public void Add(ISprite enemy)
        {
            roomEnemies.Add(enemy);
            numEnemies++;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ISprite enemy in roomEnemies)
            {
                enemy.Draw(spriteBatch);
            }

        }
        public void Update()
        {
            foreach (ISprite enemy in roomEnemies)
            {
                enemy.Update();
            }
            for (int i = 0; i < roomEnemies.Count; i++)
            {
                if (roomEnemies[i].enemyHealth <= 0)
                {
                    SpawnEnemyDrops(roomEnemies[i]);
                    roomEnemies.RemoveAt(i);
                }

                

            }
        }

        private void SpawnEnemyDrops(ISprite enemy)
        {
            int numDrops = rnd.Next(0, 3);
            Console.WriteLine("Spawning " + numDrops + " enemy drops.");

            Vector2 position = enemy.Position;
            for (int i = 0; i < numDrops; i++)
            {
                int dropType = rnd.Next(0, 2);
                roomManager.SpawnItemDrops(position, dropType);
	        }
	    }

        public void AddEnemyByID(int id, Vector2 position, IProjectileManager projectileManager)
        {
            ISprite enemy = null;
            //Checking what kind of enemy the current Item really is.
            switch (id)
            {
                case 0:
                    enemy = new Keese(contentLoader.EnemyTexture, position);
                    break;
                case 1:
                    enemy = new Gel(contentLoader.EnemyTexture, position);
                    break;
                case 2:
                    enemy = new Goriya(contentLoader.EnemyTexture, position, projectileManager);
                    break;
                case 3:
                    enemy = new OldMan(contentLoader.EnemyTexture, position);
                    break;
                case 4:
                    enemy = new Stalfos(contentLoader.EnemyTexture, position);
                    break;
                case 5:
                    enemy = new Aquamentus(contentLoader.BossTexture, position, projectileManager);
                    break;
                case 6:
                    enemy = new Wallmaster(contentLoader.EnemyTexture, position);
                    break;
                case 7:
                    enemy = new Blade(roomManager.link, contentLoader.EnemyTexture, position);
                    break;
                case 8:
                    enemy = new Fire(contentLoader.FireTexture, position);
                    break;
            }

            //adding it to the list of enemies for this room.
            if (enemy != null)
                Add(enemy);
        }
    }
}

