﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ThomasMurray
{
    public class ThreeDigitsConverter
    {
        private const string ERROR_MSG = "* Ошибка *";
        private const string NON_ALPHANUM_REGEX_PATTERN = @"[^\w\s\-]*";

        private static Dictionary<string, ushort> _allDigits = new Dictionary<string, ushort>();
        private static char[] _sentenseDelimeters = new[] { ';', '.' };
        private static Dictionary<string, ushort> _allOrderDelimitingWords = new Dictionary<string, ushort>();

        static ThreeDigitsConverter()
        {
            ConvertCollection(_allDigits, ValueableDigitsStore.Digits);
            ConvertCollection(_allOrderDelimitingWords, ValueableDigitsStore.OrderWords);
        }

        private static void ConvertCollection(Dictionary<string, ushort> collection, Dictionary<string, Tuple<ushort, string[]>> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                if (kvp.Value.Item2.Any())
                {
                    foreach (var ending in kvp.Value.Item2)
                        collection.Add(kvp.Key + ending.Trim(), kvp.Value.Item1);
                }
                else
                    collection.Add(kvp.Key, kvp.Value.Item1);
            }
        }

        public string Convert(string stringItem)
        {
            return null;
        }

        internal long? CalculateSingleNumericExpression(string inputNumericExpression)
        {
            if (string.IsNullOrWhiteSpace(inputNumericExpression))
                return null;
            long result = 0;
            ushort? currentOrderMultiplier = 0;
            ushort currentOrderLogPower = 0;
            foreach (var orderExpression in SplitByOrders(inputNumericExpression))
            {
                currentOrderMultiplier = ConvertOrderMultiplierText(orderExpression);
                var lastWord = orderExpression.Split(' ').Last();
                if(_allOrderDelimitingWords.TryGetValue(lastWord, out currentOrderLogPower) == false)
                {
                    currentOrderLogPower = 0;
                }
                result += System.Convert.ToInt64(currentOrderMultiplier * Math.Pow(10, currentOrderLogPower));
            }
            return result;
        }
        
        internal string[] SplitByOrders(string numericExpressionText)
        {
            if (string.IsNullOrWhiteSpace(numericExpressionText))
                return null;

            var splittedWords = numericExpressionText.Split(' ');
            var orderDelimiterWordsIdxs = new List<int>(new[] { 0 });
            var wordIdx = 1;
            foreach (var word in splittedWords)
            {
                if (_allOrderDelimitingWords.ContainsKey(word))
                    orderDelimiterWordsIdxs.Add(wordIdx);

                wordIdx ++;                
            }

            if(orderDelimiterWordsIdxs.Any() == false)
                return null;

            orderDelimiterWordsIdxs.Add(splittedWords.Length);
            var result = new string[orderDelimiterWordsIdxs.Count - 1];
            for (int i = 0; i < orderDelimiterWordsIdxs.Count - 1; i++)
            {
                result[i] = string.Join(' ', splittedWords.Skip(orderDelimiterWordsIdxs[i]).Take(orderDelimiterWordsIdxs[i + 1] - orderDelimiterWordsIdxs[i]));
            }
            return result;
        }
        
        internal string[] FindNumericExpressionsInSentense(string sentense)
        {
            if (string.IsNullOrWhiteSpace(sentense))
                return null;

            var sequences = new List<List<string>>();
            bool expressionFound = false;

            foreach (var word in sentense.Split(' '))
            {
                var trimmedWord = Regex.Replace(word.ToLowerInvariant(), NON_ALPHANUM_REGEX_PATTERN, string.Empty);
                bool match = _allDigits.ContainsKey(trimmedWord) || _allOrderDelimitingWords.ContainsKey(trimmedWord);

                if (match == true && expressionFound == false)
                {
                    expressionFound = true;
                    sequences.Add(new List<string>(new[] { trimmedWord }));
                }
                else if (match == true && expressionFound == true)
                {
                    sequences.Last().Add(trimmedWord);
                }
                else if (match == false)
                {
                    expressionFound = false;
                }
            }

            var idx = 0;
            var result = new string[sequences.Count];
            foreach (var numericWordsSequence in sequences)
            {
                result[idx++] = string.Join(' ', numericWordsSequence);
            }

            return result;
        }

        internal ushort? ConvertOrderMultiplierText(string inputThreeOrdersText)
        {
            if (string.IsNullOrWhiteSpace(inputThreeOrdersText))
                return null;            //  пока пусть так

            ushort result = 0;
            byte lastDidgitOrder = 3;
            foreach (var split in inputThreeOrdersText.Split(' '))
            {
                if (_allDigits.TryGetValue(split.ToLowerInvariant().Trim(), out var digitizedStr))
                {
                    byte currentDigitOrder = (byte)Math.Truncate(Math.Log10(digitizedStr));
                    if (currentDigitOrder >= lastDidgitOrder)
                        return null;   //  написана ерунда в порядках

                    lastDidgitOrder = currentDigitOrder;
                    result += digitizedStr;
                }
                else if(_allOrderDelimitingWords.TryGetValue(split.ToLowerInvariant().Trim(), out _) && result == 0)
                {
                    return 1;
                }
                else if(result == 0)
                {
                    return null;       //  не нашлось цифр
                }
            }

            if (result > 999)
                return null;           //  каким-то образом переполнился трехзначный порядок

            return result;
        }
    }
}