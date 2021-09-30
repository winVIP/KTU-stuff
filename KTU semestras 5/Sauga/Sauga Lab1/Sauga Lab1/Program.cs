using System;
using System.Collections.Generic;

namespace Sauga_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] Tekstograma = "WELCOMETOKAUNASUNIVERSITYOFTECHNOLOGY".ToLower().ToCharArray();
            char[] Kriptograma = "OPLRHMDHRSSMYAHNNHJHZKAEYDYTDQKVGDZGN".ToLower().ToCharArray();
            for(int i = 0; i < Tekstograma.Length; i++)
            {
                Console.Write(Distance(Tekstograma[i], Kriptograma[i]) + " ");
            }


            //string kriptograma = "OPLRHMDHRSSMYAHNNHJHZKAEYDYTDQKVGDZGN";
            //kriptograma = kriptograma.ToLower();
            //char[] cKriptograma = kriptograma.ToCharArray();
            //List<int> dalikliai = new List<int>();
            //for(int i = 1; i < cKriptograma.Length; i++)
            //{
            //    if(cKriptograma.Length % i == 0)
            //    {
            //        dalikliai.Add(i);
            //    }
            //}
            //foreach(int daliklis in dalikliai)
            //{
            //    char[][] kategorijos = new char[dalikliai][];
            //    foreach(char[] kategorija in kategorijos)
            //    {

            //    }
            //}
        }

        static int Distance(char letter1, char letter2)
        {
            int num1 = 0;
            int num2 = 0;
            int rezult = 0;
            char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
            for(int i = 0; i < alphabet.Length; i++)
            {
                if(alphabet[i] == letter1)
                {
                    num1 = i;
                }
                if(alphabet[i] == letter2)
                {
                    num2 = i;
                }
            }
            if(num1 < num2)
            {
                rezult = num2 - num1;
            }
            else if(num1 > num2)
            {
                rezult = (alphabet.Length - num1) + num2;
            }
            else if(num1 == num2)
            {
                rezult = 0;
            }
            return rezult;
        }
    }
}
