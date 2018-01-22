using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace stone_paper
{
    class Program
    {
        //body 
        static void Main(string[] args)
        {
            Random rnd = new Random();
            var choice = new[] { "Rock", "Fire", "Scissors", "Sponge", "Paper", "Air", "Water" };
            int len = choice.Length;
            string res = "", again, key = "mykey";
            Boolean repeat;
           
            do
            {
                int turncomp = rnd.Next(len);
                
                Console.WriteLine("Choosing an opponent -- " + Hash(choice[turncomp], key));
                Console.WriteLine("Make your choice: ");
                for (int i = 0; i < len; i++)
                {
                    Console.WriteLine(i + 1 + " -- " + choice[i]);
                }
                Console.WriteLine("0 -- Exit");
                Console.Write("Your choice ");
                int uchoice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                logicGame(turncomp+1, uchoice, res, len);

                Console.WriteLine("Сhoice of the enemy was --  " + choice[turncomp
                    ] + ", key was --  " + key);

                Console.Write("Do you want more? Y/N ");
                again = Console.ReadLine();
                if (again == "Y" || again == "y")
                {
                    repeat = true;
                }
                else
                {
                    repeat = false;
                }

                Console.WriteLine("-------------------------------");

            } while (repeat);


            Console.WriteLine(res);
            Console.ReadLine();
        }

        //function is intended for hashing a key
        private static string Hash(string message, string secretKey)
        {
            var encoding = System.Text.Encoding.UTF8;
            byte[] msgBytes = encoding.GetBytes(message);
            var keyBytes = encoding.GetBytes(secretKey);
            using (HMACSHA512 hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(msgBytes);

                var sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                    sb.Append(hashBytes[i].ToString("x2"));
                return sb.ToString();
            }
        }

        //function with logic game
        static void logicGame(int turncomp, int uchoice, string res, int len)
        {
            int i = 1, j = 0, k=0;
            double q = len;
          
            do
            {
                i += 2;
                //exception for the central element
                if (turncomp == Math.Ceiling(q / 2) && uchoice == len)
                {
                    res = "Lose :(";
                    break;
                }
                else if (uchoice == Math.Ceiling(q / 2) && turncomp == len)
                {
                    res = "Win!";
                    break;
                }

                if (turncomp == len - j && uchoice == len - j)
                    {
                        turncomp = 0;
                        uchoice = 0;
                        j = 0;
                    }
                    else if (uchoice == len - j)
                    {
                        uchoice = 0;
                        if (turncomp == 0)
                        {
                            turncomp += j;
                            k = j-1;
                        }
                        j = 0;
                    }
                    else if (turncomp == len - j)
                    {
                        turncomp = 0;
                        if (uchoice == 0)
                        {
                            uchoice += j;
                            k = j-1;
                    }
                        j = 0;
                    }
                j += 1;            
                if (turncomp == uchoice + j+k)
                {
                    res = "Win!";
                    break;
                }
                else if (turncomp == uchoice)
                {
                    res = "Draw";
                    break;
                }
                else
                {
                    res = "l=Lose :(";
                }
                
            } while (i != len);

            Console.WriteLine(res);
        }
    }
}