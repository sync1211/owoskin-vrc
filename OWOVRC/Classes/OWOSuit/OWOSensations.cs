using OWOGame;

namespace OWOVRC.Classes.OWOSuit
{
    public static class OWOSensations
    {
        //public readonly BakedSensation Ball = BakedSensation.Parse("0~Ball~100,1,100,0,0,0,Impact|5%100~impact-0~");
        public static readonly BakedSensation FallDmg = BakedSensation.Parse("9~Fall Damage~50,1,100,0,0,0,Hit|0%100,1%100,2%100,3%100,4%100,5%100~environment-5~Impacts");
        public static readonly BakedSensation Wind = BakedSensation.Parse("0~Wind~100,25,25,0,0,0,Lift|3%100,2%100,4%100,5%100,9%100,8%100,0%100,1%100,6%100,7%100~environment-1~");
    }
}