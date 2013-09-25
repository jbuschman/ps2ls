using OpenTK;
using ps2ls.Assets.Dme;

namespace ps2ls.Graphics.Models
{
    public class ModelInstance
    {
        public ModelInstance(Model model)
        {
            Model = model;
        }

        public Model Model { get; private set; }
        public Matrix4 Transform { get; set; }
    }
}
