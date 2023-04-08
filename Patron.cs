using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.R.A.P._Editor
{
    public class Patron
    {
        public bool Enabled { get; set; }
        public double Weight { get; set; }
        public int InitialSpeed { get; set; }
        public int Damage { get; set; }
        public int AmmoAccr { get; set; }
        public int AmmoRec { get; set; }
        public int PenetrationPower { get; set; }
        public int ProjectileCount { get; set; }
        public double MisfireChance { get; set; }
        public int MinFragmentsCount { get; set; }
        public int MaxFragmentsCount { get; set; }
        public double FragmentationChance { get; set; }
        public bool Tracer { get; set; }
        public string TracerColor { get; set; }
        public int ArmorDamage { get; set; }
        public double StaminaBurnPerDamage { get; set; }
        public int HeavyBleedingDelta { get; set; }
        public int LightBleedingDelta { get; set; }
        public double MalfMisfireChance { get; set; }
        public int DurabilityBurnModificator { get; set; }
        public double HeatFactor { get; set; }
        public double MalfFeedChance { get; set; }
        public int Explosive { get; set; }
        public bool Blinding { get; set; }
    }
}
