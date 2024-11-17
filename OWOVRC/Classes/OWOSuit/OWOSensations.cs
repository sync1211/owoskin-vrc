using OWOGame;

namespace OWOVRC.Classes.OWOSuit
{
    public class OWOSensations
    {
        //public readonly BakedSensation Ball = BakedSensation.Parse("0~Ball~100,1,100,0,0,0,Impact|5%100~impact-0~");
        public readonly BakedSensation FallDmg = BakedSensation.Parse("9~Fall Damage~50,1,100,0,0,0,Hit|0%100,1%100,2%100,3%100,4%100,5%100~environment-5~Impacts");
        public readonly BakedSensation Wind = BakedSensation.Parse("0~Wind~100,3,25,0,0,0,Wind|0%100,1%100,2%100,3%100,4%100,5%100~environment-1~");
    }
}