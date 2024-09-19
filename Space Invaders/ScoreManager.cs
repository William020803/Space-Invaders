using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    public class ScoreManager
    {
        public int score = 0;
        private int enemyScore = 10;
        private int eliteEnemyScore = 30;
        private int bossEnemyScore = 50;
        
        public void Update()
        {
            score = score + enemyScore;
        }

        //public void Display(){}
        

        
    }
}
