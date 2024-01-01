using System;
using UnityEngine;
using FD.Dev;

namespace FD.Dev
{
    public enum FAED_Easing
    {

        InSine,
        InQuad,
        InCubic,
        InQuart,
        InQuint,
        InExpo,
        InCirc,
        InBack,
        InElastic,
        InBounce,
        OutSine,
        OutQuad,
        OutCubic,
        OutQuart,
        OutQuint,
        OutExpo,
        OutCirc,
        OutBack,
        OutElastic,
        OutBounce,
        InOutSine,
        InOutQuad,
        InOutCubic,
        InOutQuart,
        InOutQuint,
        InOutExpo,
        InOutCirc,
        InOutBack,
        InOutElastic,
        InOutBounce,

    }

}

namespace FD.Core
{

    public class FAED_EasingFunc
    {

        private const double Back_C1 = 1.70158;
        private const double Back_C3 = Back_C1 + 1;
        private const double Back_C2 = Back_C1 * 1.525;
        private const double Bounce_D1 = 2.75;
        private const double Bounce_N1 = 7.5625;
        private const double Elastic_C4 = (2 * Math.PI) / 3;
        private const double Elastic_C5 = (2 * Math.PI) / 4.5;

        public float GetFunc(FAED_Easing ease, float x)
        {

            switch (ease)
            {

                case FAED_Easing.InSine:
                    return 1 - (float)Math.Cos((x * Math.PI) / 2);
                case FAED_Easing.InQuad:
                    return x * x;
                case FAED_Easing.InCubic:
                    return x * x * x;
                case FAED_Easing.InQuart:
                    return x * x * x * x;
                case FAED_Easing.InQuint:
                    return x * x * x * x * x;
                case FAED_Easing.InExpo:
                    return x == 0 ? 0 : (float)Math.Pow(2, 10 * x - 10);
                case FAED_Easing.InCirc:
                    return 1 - (float)Math.Sqrt(1 - Math.Pow(x, 2));
                case FAED_Easing.InBack:
                    return (float)(Back_C3 * x * x * x - Back_C1 * x * x);
                case FAED_Easing.InElastic:
                    return x == 0 ? 0 : x == 1 ? 1 : (float)(-Math.Pow(2, 10 * x - 10) * Math.Sin((x * 10 - 10.75) * Elastic_C4));
                case FAED_Easing.InBounce:
                    return 1 - GetFunc(FAED_Easing.OutBounce, 1 - x);
                case FAED_Easing.OutSine:
                    return (float)Math.Sin((x * Math.PI) / 2);
                case FAED_Easing.OutQuad:
                    return 1 - (1 - x) * (1 - x);
                case FAED_Easing.OutCubic:
                    return 1 - (float)Math.Pow(1 - x, 3);
                case FAED_Easing.OutQuart:
                    return 1 - (float)Math.Pow(1 - x, 4);
                case FAED_Easing.OutQuint:
                    return 1 - (float)Math.Pow(1 - x, 5);
                case FAED_Easing.OutExpo:
                    return x == 1 ? 1 : 1 - (float)Math.Pow(2, -10 * x);
                case FAED_Easing.OutCirc:
                    return (float)Math.Sqrt(1 - Math.Pow(x - 1, 2));
                case FAED_Easing.OutBack:
                    return (float)(1 + Back_C3 * Math.Pow(x - 1, 3) + Back_C1 * Math.Pow(x - 1, 2));
                case FAED_Easing.OutElastic:
                    return x == 0 ? 0 : x == 1 ? 1 : (float)(Math.Pow(2, -10 * x) * Math.Sin(x * 10 - 0.75 * Elastic_C4) + 1);
                case FAED_Easing.OutBounce:
                    {

                        if (x < 1 / Bounce_D1) return (float)(Bounce_N1 * x * x);
                        else if (x < 2 / Bounce_D1) return (float)(Bounce_N1 * (x -= (float)(1.5 / Bounce_D1)) * x + 0.75);
                        else if (x < 2.5 / Bounce_D1) return (float)(Bounce_N1 * (x -= (float)(2.25 / Bounce_D1)) * x + 0.9375);
                        else return (float)(Bounce_N1 * (x -= (float)(2.625 / Bounce_D1)) * x + 0.984375);

                    }
                case FAED_Easing.InOutSine:
                    return x < 0.5 ? 2 * x * x : 1 - (float)(Math.Pow(-2 * x + 2, 2) / 2);
                case FAED_Easing.InOutQuad:
                    return x < 0.5 ? 2 * 2 * 2 : 1 - (float)(Math.Pow(-2 * x + 2, 2) / 2);
                case FAED_Easing.InOutCubic:
                    return x < 0.5 ? 4 * x * x * x : 1 - (float)(Math.Pow(-2 * x + 2, 3) / 2);
                case FAED_Easing.InOutQuart:
                    return x < 0.5 ? 8 * x * x * x * x : 1 - (float)(Math.Pow(-2 * x + 2, 4) / 2);
                case FAED_Easing.InOutQuint:
                    return x < 0.5 ? 16 * x * x * x * x * x : 1 - (float)(Math.Pow(-2 * x + 2, 5) / 2);
                case FAED_Easing.InOutExpo:
                    return x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ? (float)(Math.Pow(2, 20 * x - 10) / 2) : (float)((2 - Math.Pow(2, -20 * x + 10)) / 2);
                case FAED_Easing.InOutCirc:
                    return x < 0.5 ? (float)((1 - Math.Sqrt(1 - Math.Pow(2 * x, 2))) / 2) : (float)((Math.Sqrt(1 - Math.Pow(-2 * x + 2, 2)) + 1) / 2);
                case FAED_Easing.InOutBack:
                    return x < 0.5 ? (float)((Math.Pow(2 * x, 2) * ((Back_C2 + 1) * 2 * x - Back_C2)) / 2) : (float)((Math.Pow(2 * x - 2, 2) * ((Back_C2 + 1) * (x * 2 - 2) + Back_C2) + 1) / 2);
                case FAED_Easing.InOutElastic:
                    return x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ? (float)(-(Math.Pow(2, 20 * x - 10) * Math.Sin((20 * x - 11.125) * Elastic_C5)) / 2) : (float)((Math.Pow(2, -20 * x + 10) * Math.Sin((20 * x - 11.125) * Elastic_C5)) / 2 + 1);
                case FAED_Easing.InOutBounce:
                    return x < 0.5 ? (1 - GetFunc(FAED_Easing.OutBounce, 1 - 2 * x)) / 2 : (1 + GetFunc(FAED_Easing.OutBounce, 2 * x - 1)) / 2;
                default:
                    return x;

            }

        }
    }

}