using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Attributes;
using ExileCore.Shared.Nodes;

namespace DrawAOE
{
    public class DrawAOESettings : ISettings
    {
        public DrawAOESettings()
        {
            //plugin itself
            Enable = new ToggleNode(false);
            DisplayInTown = new ToggleNode(true);

            CircleEnable = new ToggleNode(false);
            CircleSize = new RangeNode<int>(500, 50, 2000);
            LineWidth = new RangeNode<int>(1, 1, 5);
            LineColor = 0xffffffff;

            CircleEnable2 = new ToggleNode(false);
            CircleSize2 = new RangeNode<int>(500, 50, 2000);
            LineWidth2 = new RangeNode<int>(1, 1, 5);
            LineColor2 = 0xffffffff;



        }

        //Menu




        [Menu("Display in Town", 1)]
        public ToggleNode DisplayInTown { get; set; }

        [Menu("Circle 1", 2)]
        public ToggleNode CircleEnable { get; set; }
        [Menu("Circle Size", 20, 2)]
        public RangeNode<int> CircleSize { get; set; }
        [Menu("Line Width", 21, 2)]
        public RangeNode<int> LineWidth { get; set; }
        [Menu("Line Color", 22, 2)]
        public ColorNode LineColor { get; set; }

        [Menu("Circle 2", 3)]
        public ToggleNode CircleEnable2 { get; set; }
        [Menu("Circle Size", 30, 3)]
        public RangeNode<int> CircleSize2 { get; set; }
        [Menu("Line Width", 31, 3)]
        public RangeNode<int> LineWidth2 { get; set; }
        [Menu("Line Color", 32, 3)]
        public ColorNode LineColor2 { get; set; }

        [Menu("Enable", 4)]
        public ToggleNode Enable { get; set; } = new ToggleNode(true);



    }
}
