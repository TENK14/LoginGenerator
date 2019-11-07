using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Generator
{
    class Program
    {
        //abeceda
        static char[] abc = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                    '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};

        static char[] lastLogin;

        private struct NextValue
        {
            public char value;
            public bool isReset;
        };

        private static List<string> logins = new List<string>();

        static void Main(string[] args)
        {
            char[] login = new char[5];
            lastLogin = GetLastLogin(login.Length);

            Console.WriteLine("-== ZAČÁTEK ==-");

            // init
            Init(login);
            logins.Add(new String(login));

            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (!login.SequenceEqual(lastLogin))
            {
                Next(login);
            }

            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            
            // Vypis vsech loginu
            logins.ForEach(l => Console.WriteLine(l));

            Console.WriteLine("-== KONEC ==-");
            Console.ReadLine();
        }

        private static char[] GetLastLogin(int length)
        {
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = abc[abc.Length - 1];
            }

            return result;
        }

        private static void Init(char[] login)
        {
            for (int i = 0; i < login.Length; i++)
            {
                login[i] = abc[0];
            }
        }

        private static void Next(char[] login)
        {
            //login[position] = abc[GetNextPosition]

            if (login == lastLogin)
            {
                throw new ArgumentException($"Další login už neexistuje. Toto je poslední: '{login.ToString()}'");
            }

            IncrementLogin(login, (login.Length - 1));
        }

        private static void IncrementLogin(char[] login, int position)
        {
            if (position < 0
                || position >= login.Length)
            {
                return;
            }

            NextValue result = IncrementValue(login[position]);

            login[position] = result.value;
            if (result.isReset)
            {
                IncrementLogin(login, position - 1);
            }
            else
            {
                logins.Add(new String(login));
            }
        }

        private static NextValue IncrementValue(char value)
        {
            NextValue result;

            int position = GetPosition(value);

            if (position == (abc.Length - 1))
            {
                result.value = abc[0];
                result.isReset = true;
            }
            else
            {
                result.value = abc[position + 1];
                result.isReset = false;
            }

            return result;
        }

        private static int GetPosition(char value)
        {
            for (int i = 0; i < abc.Length; i++)
            {
                if (abc[i] == value)
                {
                    return i;
                }
            }

            throw new ArgumentException($"Nepovolený symbol: '{value}'");
        }
    }
}
