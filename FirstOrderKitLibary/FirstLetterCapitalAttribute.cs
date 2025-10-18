using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstOrderKitModel
{
    public class FirstLetterCapitalAttribute:ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string word = value.ToString();
            char firstLetter = word[0];
            char last = ' ';

             if (firstLetter < 'A' || firstLetter>'Z') 
                return false;
             for (int i = 1; i < word.Length; i++)
            {
                if (last != ' ')
                {
                    if (word[i] != ' ')
                    {
                        if (word[i] < 'a' || word[i] > 'z')
                            return false;
                    }
                    last = word[i];
                }
                else
                {
                    if (word[i] < 'a' || word[i] > 'z')
                        return false;
                }
            }
             return true;
        }
    }
}
