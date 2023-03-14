using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GeomancyApp
{
    public class FigureData
    {
        public string Name { get; set; }
        public string OtherNames { get; set; }
        public string Quailty { get; set; }
        public string Keyword { get; set; }
        public string Imagery { get; set; }
        public string StrongHouse { get; set; }
        public string WeakHouse { get; set; }

        public string Planet { get; set; }
        public string Sign { get; set; }
        public string InnerEl { get; set; }
        public string OutterEl { get; set; }
        public string DivinaryMeaning { get; set; }

        public FigureData ReturnFigureData(string name)
        {
            if (name == "Puer")
            {
                Name = "Puer (boy)";
                OtherNames = "Beardless, yellow, warrior, man, sword";
                Quailty = "Mobile";
                Keyword = "Energy";
                Imagery = "A sword; a male figure with exaggerated testicles";
                StrongHouse = "First";
                WeakHouse = "Second";
                Planet = "Mars";
                Sign = "Aries";
                InnerEl = "Air";
                OutterEl = "Fire";
                DivinaryMeaning = "passionate energy, Force, seeking, and sudden change. When ever Energy, Enthusiasm, courage, and change are desirable, Puer can be favorable, unfavorable if in matters where stability, prudence, and maturity are advantanges ";
                return this;
            }
            else if (name == "Amissio")
            {
                Name = "Amissio (loss)";
                OtherNames = "Grasping externally, outter wealth, something lost ";
                Quailty = "Mobile";
                Keyword = "Loss";
                Imagery = "a bag held mouth downward, letter the contents fall out";
                StrongHouse = "Second";
                WeakHouse = "Eighth";
                Planet = "Venus";
                Sign = "Taurus";
                InnerEl = "Fire";
                OutterEl = "Air";
                DivinaryMeaning = "Loss in every sense both negative and positive relevent to both money and love in the sense of loss. Represents something outside of ones grasp, favorable in instances where loss is desired, (sicknesss recovery, losing fear) , unfavorable when hope of gaining something";
                return this;
            }
            else if (name == "Albus")
            {
                Name = "Albus (white)";
                OtherNames = "Old Man, Wise Elder";
                Quailty = "Stable";
                Keyword = "Peace";
                Imagery = "A goblet set  upright";
                StrongHouse = "Third";
                WeakHouse = "Ninth";
                Planet = "Merury";
                Sign = "Gemini";
                InnerEl = "Water";
                OutterEl = "Air";
                DivinaryMeaning = "Peace, wisdom, purity, often favorable but weak, favors quiet progress and the use of intellegence. Favorable in most business and fiance questions, but unfavorable in situations that require energy and courage or involve conflict or disruptive change";
                return this;
            }
            else
            {
                Name = "NA";
                OtherNames = "NA";
                Quailty = "NA";
                Keyword = "NA";
                Imagery = "NA";
                StrongHouse = "NA";
                WeakHouse = "NA";
                Planet = "NA";
                Sign = "NA";
                InnerEl = "NA";
                OutterEl = "NA";
                DivinaryMeaning = "NA";
                return this;
            }
        }


    }


}
