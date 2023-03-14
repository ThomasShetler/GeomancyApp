using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeomancyApp
{

    public class figure
    {
        public int headLine { get; set; }
        public int neckLine { get; set; }
        public int bodyLine { get; set; }

        public int footLine { get; set; }

        public int HouseNumber { get; set; }

        public string figureName { get; set; }
        
        

    }
    public class HouseChart
    {
        public figure FirstHouse { get; set; }
        public figure SecondHouse { get; set; }
        public figure ThirdHouse { get; set; }
        public figure ForthHouse { get; set; }
        public figure FifthHouse { get; set; }
        public figure SixthHouse { get; set; }
        public figure SeventhHouse { get; set; }

        public figure EightHouse { get; set; }
        public figure NinthHouse { get; set; }

        public figure TenthHouse { get; set; }
        public figure eleventhHouse { get; set; }
        public figure twelvethHouse { get; set; }

        public figure RightWitness { get; set; }
        public figure LeftWittness { get; set; }

        public figure Judge { get; set; }
        public figure fallout { get; set; }

        

     }
}
