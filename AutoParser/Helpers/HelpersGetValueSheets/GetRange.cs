﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParser.Helpers.HelpersGetValueSheets
{
    public class GetRange
    {
        public static string GetRangeLetter(int columnIndex)
        {
            const int LETTERS_IN_ALPHABET = 26;
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //int dividend = columnIndex + 1;
            int dividend = columnIndex;
            string range = "";
            
            while (dividend > 0)
            {
                int modulo = (dividend - 1) % LETTERS_IN_ALPHABET;
                range = letters[modulo] + range;
                dividend = (int)((dividend - modulo) / LETTERS_IN_ALPHABET);
            }

            return range;
        }
    }
}
