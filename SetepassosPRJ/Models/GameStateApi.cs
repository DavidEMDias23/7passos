using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public enum Result { NoResult, Success, SuccessVictory, InvalidAction, GameHasEnded }

    public class GameStateApi
    {
        public int GameID { get; set; }
        public int RoundNumber { get; set; }
        public PlayerAction Action { get; set; }
        public Result Result { get; set; }
        public bool FoundEnemy { get; set; }
        public bool FoundItem { get; set; }
        public bool FoundKey { get; set; }
        public bool FoundPotion { get; set; }
        public int GoldFound { get; set; }
        public double EnemyDamageSuffered { get; set; }
        public double EnemyHealthPoints { get; set; }
        public int EnemyAttackPoints { get; set; }
        public int EnemyLuckPoints { get; set; }
        public int ItemHealthEffect { get; set; }
        public int ItemAttackEffect { get; set; }
        public int ItemLuckEffect { get; set; }

        public GameStateApi(int gameID, int roundNumber, PlayerAction action, Result result, bool foundEnemy, bool foundItem, bool foundKey, bool foundPotion, int goldFound, double enemyDamageSuffered, double enemyHealthPoints, int enemyAttackPoints, int enemyLuckPoints, int itemHealthEffect, int itemAttackEffect, int itemLuckEffect)
        {
            GameID = gameID;
            RoundNumber = roundNumber;
            Action = action;
            Result = result;
            FoundEnemy = foundEnemy;
            FoundItem = foundItem;
            FoundKey = foundKey;
            FoundPotion = foundPotion;
            GoldFound = goldFound;
            EnemyDamageSuffered = enemyDamageSuffered;
            EnemyHealthPoints = enemyHealthPoints;
            EnemyAttackPoints = enemyAttackPoints;
            EnemyLuckPoints = enemyLuckPoints;
            ItemHealthEffect = itemHealthEffect;
            ItemAttackEffect = itemAttackEffect;
            ItemLuckEffect = itemLuckEffect;
        }

    }
}
