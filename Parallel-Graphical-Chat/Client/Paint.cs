using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Client
{
    [Serializable]
    public class Paint
    {
        public Color color { get; set; }
        public Point coord { get; set; }

        public Paint()
        {
            color = Color.Black;
            coord = new Point(10, 10);
        }
    }
}
