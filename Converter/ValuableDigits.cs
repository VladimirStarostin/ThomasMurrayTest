using System;
using System.Collections.Generic;

namespace ThomasMurray
{
    using ValueAndEndings = Tuple<ushort, string[]>;

    public static class ValueableDigitsStore
    {
        private static readonly string[] CommonTensEndings = new[] { "ь", "и", "ью" };
        private static readonly string[] CommonHundredsEndings = new[] { "та", "от" };
        private static readonly string[] CommonOrderEndings = new[] { string.Empty, "а", "ов", "у", "ам" };

        public static Dictionary<string, ValueAndEndings> Digits = new Dictionary<string, ValueAndEndings>
        {
            {"од",          new ValueAndEndings(1, new[]{"ин","ному","ного"})},
            {"дв",          new ValueAndEndings(2, new[]{"а","умя","ух"})},
            {"тр",          new ValueAndEndings(3, new[]{"и", "емя","ёх","ех"})},
            {"четыр",       new ValueAndEndings(4, new[]{"е","мя","ёх","ех"})},
            {"пят",         new ValueAndEndings(5, CommonTensEndings)},
            {"шест",        new ValueAndEndings(6, CommonTensEndings)},
            {"сем",         new ValueAndEndings(7, CommonTensEndings)},
            {"восем",       new ValueAndEndings(8, new[]{"ь","ью"})},
            {"вось",        new ValueAndEndings(8, new[]{"ми"})},
            {"девят",       new ValueAndEndings(9, CommonTensEndings)},

            {"десят",       new ValueAndEndings(10, CommonTensEndings)},
            {"одиннадцат",  new ValueAndEndings(11, CommonTensEndings)},
            {"двенадцат",   new ValueAndEndings(12, CommonTensEndings)},
            {"тринадцат",   new ValueAndEndings(13, CommonTensEndings)},
            {"четырнадцат", new ValueAndEndings(14, CommonTensEndings)},
            {"пятнадцат",   new ValueAndEndings(15, CommonTensEndings)},
            {"шестнадцат",  new ValueAndEndings(16, CommonTensEndings)},
            {"семнадцат",   new ValueAndEndings(17, CommonTensEndings)},
            {"восемнадцат", new ValueAndEndings(18, CommonTensEndings)},
            {"девятнадцат", new ValueAndEndings(19, CommonTensEndings)},
            {"двадцат",     new ValueAndEndings(20, CommonTensEndings)},
            {"тридцат",     new ValueAndEndings(30, CommonTensEndings)},
            {"сорок",       new ValueAndEndings(40, new string[]{string.Empty, "а", "у"})},

            {"пятьдесят",    new ValueAndEndings(50, new string[]{})},
            {"пятидесяти",   new ValueAndEndings(50, new string[]{})},
            {"пятьюдесятью", new ValueAndEndings(50, new string[]{})},


            {"шестдесят",    new ValueAndEndings(60, new string[]{})},
            {"шестиидесяти", new ValueAndEndings(60, new string[]{})},
            {"шестьюдесятью",new ValueAndEndings(60, new string[]{})},


            {"семьдесят",    new ValueAndEndings(70, new string[]{})},
            {"семидесяти",   new ValueAndEndings(70, new string[]{})},
            {"семьюдесятью", new ValueAndEndings(70, new string[]{})},


            {"восемьдесят",    new ValueAndEndings(80, new string[]{})},
            {"восьмидесяти",   new ValueAndEndings(80, new string[]{})},
            {"восемьюдесятью", new ValueAndEndings(80, new string[]{})},


            {"девяност",   new ValueAndEndings(90, new string[]{"о", "а"})},

            {"ст",         new ValueAndEndings(100, new string[]{"о", "а"})},
            {"двести",     new ValueAndEndings(200, new string[]{})},
            {"триста",     new ValueAndEndings(300, new string[]{})},
            {"четыреста",  new ValueAndEndings(400, new string[]{})},
            {"пятьсот",    new ValueAndEndings(500, new string[]{})},
            {"шестьсот",   new ValueAndEndings(600, new string[]{})},
            {"семьсот",    new ValueAndEndings(700, new string[]{})},
            {"восемьсот",  new ValueAndEndings(800, new string[]{})},
            {"девятьсот",  new ValueAndEndings(900, new string[]{})},

            {"двухс",      new ValueAndEndings(200, CommonHundredsEndings)},
            {"трёхс",      new ValueAndEndings(300, CommonHundredsEndings)},
            {"четырёхс",   new ValueAndEndings(400, CommonHundredsEndings)},
            {"трехс",      new ValueAndEndings(300, CommonHundredsEndings)},
            {"четырехс",   new ValueAndEndings(400, CommonHundredsEndings)},
            {"пятис",      new ValueAndEndings(500, CommonHundredsEndings)},
            {"шестис",     new ValueAndEndings(600, CommonHundredsEndings)},
            {"семис",      new ValueAndEndings(700, CommonHundredsEndings)},
            {"восьмис",    new ValueAndEndings(800, CommonHundredsEndings)},
            {"девятис",    new ValueAndEndings(900, CommonHundredsEndings)},
        };

        public static Dictionary<string, ValueAndEndings> OrderWords = new Dictionary<string, ValueAndEndings>
        {
            {"тысяч",       new ValueAndEndings(3,  new[] { string.Empty, "а", "и", "у", "ам" })},
            {"миллион",     new ValueAndEndings(6,  CommonOrderEndings)},
            {"миллиард",    new ValueAndEndings(9,  CommonOrderEndings)},
            {"триллион",    new ValueAndEndings(12, CommonOrderEndings)},
            {"квадриллион", new ValueAndEndings(15, CommonOrderEndings)},
        };
    }
}