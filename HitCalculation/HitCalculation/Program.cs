using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HitCalculation
{
    class Program
    {
        public static Random RAND = new Random();

        private int hits;
        private int misses;
        private List<string> debugText = new List<string>();


        static void Main(string[] args)
        {
            Program program = new Program();
            program.Monte(program.hits, program.misses, program.debugText, true);
        }

        public void Monte(int hits, int misses, List<string> debugText, bool write)
        {
            for(int i=0; i<13; i++)
            {
                //MLS Max accuracy == 112
                //Melee defense cap == 125
                if (TestHit(112, 125, true, true, true, hits, misses, debugText, false))
                    hits++;
                else
                    misses++;
            }

            Console.WriteLine("Total attacks: " + (hits + misses) + "\nTotal misses: " + misses + "\nTotal hits: " + hits + "\n Average Hit chance: " + TotalAccuracy(hits, misses) + "%");

            if (write)
                Output.OutputText(debugText);


        }


        public static bool TestHit(int attackerAccuracy, int targetDefense, bool citros, bool blind, bool pikatta, int hits, int misses, List<string> debugText, bool write)
        {
            string outputText = "";

            if (citros)
                attackerAccuracy += 40;

            if (blind)
                attackerAccuracy -= 60;

            if (pikatta)
                targetDefense += 53;

            double attackerRoll = RAND.NextDouble() * 249.0 + 1.0;
            double defenderRoll = RAND.NextDouble() * 150 + 25.0;


            double accTotal = HitChanceEquation(attackerAccuracy, attackerRoll, targetDefense, defenderRoll);

            //MISS
            if (RAND.NextDouble() * 100.0 > accTotal)
            {
                outputText = "MISS with " + Math.Round(accTotal, 2) + " attackerAccuracy" + " vs targetDefense " + targetDefense;
                Console.WriteLine(outputText);                
                debugText.Add(outputText);
                return false;
            }
            //HIT
            else
            {
                outputText = "HIT with " + Math.Round(accTotal, 2) + " accuraattackerAccuracycy" + " vs targetDefense " + targetDefense;
                Console.WriteLine(outputText);
                debugText.Add(outputText);
                return true;
            }
        }

        public static double HitChanceEquation(double attackerAccuracy, double attackerRoll, double targetDefense,
            double defenderRoll)
        {
            double roll = (attackerRoll - defenderRoll) / 50;
            int rollSign = Math.Sign(roll);


            double accTotal = 75.0 + roll;


            for (int i = 1; i <= 4; i++)
            {
                if (roll * rollSign > i)
                {
                    accTotal += (float)rollSign * 25.0f;
                    roll -= rollSign * i;
                }
                else
                {
                    accTotal += roll / ((float)i) * 25.0f;
                    break;
                }
            }


            accTotal += attackerAccuracy - targetDefense;

            return accTotal;
        }

        private double TotalAccuracy(int hits, int misses)
        {
            double iterations = (double)hits + (double)misses;
            return Math.Round(hits / iterations, 3)*100;
        }
    }
}