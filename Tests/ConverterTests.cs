using System.Linq;
using Shouldly;
using ThomasMurray;
using Xunit;

namespace Tests
{
    public class ConverterTests
    {
        [Fact]
        public void ThreeDigitsConvertTest_Succeeded()
        {
            var t = new DigitsConverter();
            t.ConvertOrderMultiplierText("сто двадцать три").ShouldBe((ushort?)123);
            t.ConvertOrderMultiplierText("сто двадцать три тысячи").ShouldBe((ushort?)123);
            t.ConvertOrderMultiplierText("двадцати тремя").ShouldBe((ushort?)23);
            t.ConvertOrderMultiplierText("восемью").ShouldBe((ushort?)8);
            t.ConvertOrderMultiplierText("триллион").ShouldBe((ushort?)1);
            t.ConvertOrderMultiplierText("тысяча").ShouldBe((ushort?)1);

            t.ConvertOrderMultiplierText(string.Empty).ShouldBe(null);
            t.ConvertOrderMultiplierText(null).ShouldBe(null);
            t.ConvertOrderMultiplierText(" ").ShouldBe(null);
        }

        [Fact]
        public void ThreeDigitsConvertTest_Failed()
        {
            var t = new DigitsConverter();
            t.ConvertOrderMultiplierText("двести сто три").ShouldBe(null);
            t.ConvertOrderMultiplierText("пятнадцати двухста").ShouldBe(null);
        }

        [Fact]
        public void FinNumericExpressionsInText_Test()
        {
            var t = new DigitsConverter();
            var exprs = t.FindNumericExpressionsInSentense("триста пятнадцать авып ывапыв выап тридцать миллионов двести пятьдесят два ыаптват дтып тридцать семь тысяч девятнадцать.");
            exprs.Count().ShouldBe(3);
            exprs[0].ShouldBe("триста пятнадцать");
            exprs[1].ShouldBe("тридцать миллионов двести пятьдесят два");
            exprs[2].ShouldBe("тридцать семь тысяч девятнадцать");

            exprs = t.FindNumericExpressionsInSentense(" потом триста сорок четыре и минус два.");
            exprs.Count().ShouldBe(2);
            exprs[0].ShouldBe("триста сорок четыре");
            exprs[1].ShouldBe("два");

            exprs = t.FindNumericExpressionsInSentense("вы;.;пвыажва тлот; dsg344; ;,d, dsf,sdf,. . sfgdsf.");
            exprs.ShouldBeEmpty();
        }

        [Fact]
        public void SplitByOrders_Test()
        {
            var t = new DigitsConverter();
            var exprs = t.SplitByOrders("семнадцать триллионов двести пятьдесят семь миллиардов четыреста тридцать миллионов пятьсот семь тысяч пятьдесят два");
            exprs.Count().ShouldBe(5);
            exprs[0].ShouldBe("семнадцать триллионов");
            exprs[1].ShouldBe("двести пятьдесят семь миллиардов");
            exprs[2].ShouldBe("четыреста тридцать миллионов");
            exprs[3].ShouldBe("пятьсот семь тысяч");
            exprs[4].ShouldBe("пятьдесят два");

            exprs = t.SplitByOrders("двадцать миллионов");
            exprs.Count().ShouldBe(1);
            exprs[0].ShouldBe("двадцать миллионов");

            exprs = t.SplitByOrders("дВадцать СеМЬ");
            exprs.Count().ShouldBe(1);
            exprs[0].ShouldBe("дВадцать СеМЬ");

            t.SplitByOrders(string.Empty).ShouldBeNull();
            t.SplitByOrders(" ").ShouldBeNull();
        }

        [Fact]
        public void CalculateSingleNumericExpression_Test()
        {
            var t = new DigitsConverter();
            t.CalculateSingleNumericExpression("тридцать миллионов двести пятьдесят два").ShouldBe(30000252);
            t.CalculateSingleNumericExpression("сто тридцать триллионов один миллиард восемьдесят шесть миллионов тысяча двести тридцать три").ShouldBe(130001086001233);
        }

        [Fact]
        public void ConvertNumericsInText_Test()
        {
            var t = new DigitsConverter();
            t.ConvertNumericsInText("Он заплатил двадцать миллионов за три таких автомобиля и два айпадика, и какбэ еще миллиард сто.").ShouldBe("Он заплатил 20000000 за 3 таких автомобиля и 2 айпадика, и какбэ еще 1000000100.");
            t.ConvertNumericsInText("двадцать миллионов перед три. потом триста сорок четыре и минус два.").ShouldBe("20000000 перед 3. потом 344 и минус 2.");
        }
    }
}