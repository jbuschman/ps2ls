using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;

namespace ps2ls.Graphics.Materials
{
    public class DrawStyle
    {
        public string Name { get; private set; }
        public uint NameHash { get; private set; }
        public string Effect { get; private set; }
        public uint VertexLayoutNameHash { get; private set; }

        private DrawStyle()
        {
            Name = String.Empty;
            NameHash = 0;
            Effect = String.Empty;
            VertexLayoutNameHash = 0;
        }

        public static DrawStyle LoadFromXPathNavigator(XPathNavigator navigator)
        {
            if (navigator == null)
            {
                return null;
            }

            DrawStyle drawStyle = new DrawStyle();

            //name
            drawStyle.Name = navigator.GetAttribute("Name", String.Empty);
            drawStyle.NameHash = Cryptography.JenkinsOneAtATime(drawStyle.Name);

            //effect
            drawStyle.Effect = navigator.GetAttribute("Effect", String.Empty);

            //input layout
            string vertexLayout = navigator.GetAttribute("InputLayout", String.Empty);
            drawStyle.VertexLayoutNameHash = Cryptography.JenkinsOneAtATime(vertexLayout);

            return drawStyle;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
