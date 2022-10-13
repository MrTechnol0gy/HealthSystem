using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HealthSystem
{
    internal class Program
    {
        static int health;
        static int shield;
        static int lives;
        static int enemydamage;
        static int enemyhealth;
        static int enemyxpvalue;
        static int experience;
        static int playerlevel;
        static int levelboost;
        static bool gameover;
        static string currentweapon;

        const int healthmax = 999;
        const int healthmin = -1;
        const int shieldmax = 50;
        const int shieldmin = -1;
        const int livesmax = 99;
        const int livesmin = -1;


        static void Main(string[] args)
        {
            BaseStats(); //method to initialize all variables to base levels

            while (gameover == false)
            {
                ShowHud();
                PlayerChoice();
                TakeDamage();
                DeathCheck();
                LivesCheck();
                ShieldRegen();
                Console.WriteLine();
                Thread.Sleep(3000);
                Console.Clear();
            }

            Console.ReadKey();
        }
        static void ShowHud()
        {
            Console.WriteLine("-------------------");
            Console.WriteLine("Ancient Combat.");
            Console.WriteLine("By: Sword of Creation Studios");
            Console.WriteLine("-------------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Shield: " + shield + " Health: " + health);
            Console.WriteLine("Lives: " + lives);
            Console.WriteLine("Current Weapon: " + currentweapon);
            Console.WriteLine("EXP: " + experience + " Level: " + playerlevel);
            Console.WriteLine();
        }

        static void PlayerChoice()
        {
            Console.WriteLine("Press 'F' to shoot, 'T' to switch weapons, 'H' to attempt to heal, or 'Y' to enter TESTMODE");
            if (Console.ReadKey().Key == ConsoleKey.H)
            {
                Console.WriteLine();
                Console.WriteLine("You attempt to heal!");
                PlayerHeal();
            }
            else if (Console.ReadKey().Key == ConsoleKey.T)
            {
                Console.WriteLine();
                Console.WriteLine("You grab for another weapon!");
                WeaponSwitch();
            }
            else if (Console.ReadKey().Key == ConsoleKey.F)
            {
                Console.WriteLine();
                Console.WriteLine("You squeeze off a shot!");
                PlayerShoot();
            }
            else if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                Console.WriteLine("Now entering TESTMODE");
                TestMode();
            }
        }
        static void TakeDamage()
        {
            Random rnd = new Random();
            enemydamage = rnd.Next(3, 21);

            Console.WriteLine();
            Console.WriteLine("You're about to take " + enemydamage + " damage!");
            SpillOver(enemydamage);
        }
        static void ShieldRegen() //regens shields and keeps them from going past maximum (complete)
        {
            if (shield != shieldmax && shield < 45)
            {
                Random rnd = new Random();
                int shieldregen = rnd.Next(1, 6);
                Console.WriteLine("Your shield regenerates for " + shieldregen + " points.");
                shield = shield + shieldregen;
            }
            else if (shield != shieldmax && shield >= 45 && shield < 50)
            {
                Console.WriteLine("Your shield regenerates to full!");
                shield = 50;
            }
            else
            {
                Console.WriteLine("Shields are at full!");
            }

        }
        static void PlayerHeal()
        {
            Random rnd = new Random();
            int heal = rnd.Next(0, 6);

            if (heal == 0)
            {
                Console.WriteLine("The enemy lunges and you're forced to dodge. No healing this turn!");
            }
            else if (heal > 0 && heal < 5)
            {
                int healamount;
                healamount = rnd.Next(1, 20);
                Console.WriteLine("You're about to heal for " + healamount + ".");
                health = health + healamount;
            }
            else
            {
                int healamount;
                healamount = rnd.Next(5, 16) * 2;
                Console.WriteLine("A critical heal!");
                Console.WriteLine("You're about to heal for " + healamount + ".");
                health = health + healamount;
            }
        }
        static void WeaponSwitch()
        {
            Console.WriteLine("Press 'P' to choose your Pistol, or 'J' to choose your Infinity Cannon");
            if (Console.ReadKey().Key == ConsoleKey.P)
            {
                Console.WriteLine();
                Console.WriteLine("You grab your pistol!");
                currentweapon = "Pistol";
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You haul out your Infinity Cannon!");
                currentweapon = "Infinity Cannon";
            }

        }

        static void PlayerShoot()
        {
            Random rnd = new Random();
            int playershot = rnd.Next(5, 21) + levelboost;

            Console.WriteLine();
            Console.WriteLine("You shoot the enemy for " + playershot + " damage!");
            enemyhealth = enemyhealth - playershot;
            EnemyDeathCheck();
            Console.WriteLine();

        }
        static void BaseStats()
        {
            health = 150 + (levelboost * 2);
            shield = 50;
            lives = 3;
            enemydamage = 0;
            enemyhealth = 25;
            experience = 0;
            playerlevel = 1;
            levelboost = playerlevel * 2;
            enemyxpvalue = 25;
            currentweapon = "Pistol";
            gameover = false;
        }
        static void DeathCheck()
        { 
        if (health <= 0)
            {
                lives--;
                health = 150;
                shield = 50;
            }
        }
        static void LivesCheck()
        {
            if (lives <= 0)
            {
                gameover = true;
            }
        }
        static void EnemyDeathCheck()
        {
            if (enemyhealth <= 0)
            {
                enemyhealth = 25 + (levelboost * 2);
                Console.WriteLine();
                Console.WriteLine("You've killed the enemy!");
                experience = experience + enemyxpvalue;
                LevelUpCheck();
                enemyxpvalue = enemyxpvalue * 2;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("The enemy still stands with " + enemyhealth + " health remaining!");
            }
        }
        static void LevelUpCheck()
        {
            if (experience >= ((playerlevel * levelboost) + 100))
            {
                Console.WriteLine();
                Console.WriteLine("You gained " + enemyxpvalue + " experience!");
                Console.WriteLine("Congratulations, you've gained a level!");
                playerlevel++;                
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You gained " + enemyxpvalue + " experience!");
            }
        }
        static void SpillOver(int enemydamage)
        {
            int spillovercheck;
            int healthspill;
            spillovercheck = enemydamage;

            if (shield >= spillovercheck)
            {
                shield = shield - spillovercheck;
                Console.WriteLine("Shields took " + spillovercheck + " damage!");
            }
            else if (shield < spillovercheck)
            {
                shield = shield - spillovercheck;
                healthspill = shield;
                Console.WriteLine("Your shields are gone!");
                shield = 0;
                Console.WriteLine("You're about to lose " + healthspill + " health!");
                health = health + healthspill;
                Console.WriteLine();
            }
        }
        static void TestMode()
        {
            bool TestModeCheck = true;
            Console.WriteLine("Welcome to TESTMODE");

            while (TestModeCheck == true)
            {

            }

        }
    }
}