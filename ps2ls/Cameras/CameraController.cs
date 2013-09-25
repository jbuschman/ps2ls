using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ps2ls.Cameras
{
    public abstract class CameraController : ICameraController
    {
        public abstract Camera Camera { get; }

        protected CameraController()
        {
        }

        public abstract void OnMouseDown(object sender, MouseEventArgs e);
        public abstract void OnMouseUp(object sender, MouseEventArgs e);
        public abstract void OnMouseMove(object sender, MouseEventArgs e);
        public abstract void OnKeyDown(object sender, KeyEventArgs e);
        public abstract void OnKeyUp(object sender, KeyEventArgs e);

        public abstract void Update();
    }

    public interface ICameraController : IInputHandler
    {
        Camera Camera { get; }
    }

    public interface IInputHandler
    {
        void OnMouseDown(object sender, MouseEventArgs e);
        void OnMouseUp(object sender, MouseEventArgs e);
        void OnMouseMove(object sender, MouseEventArgs e);
        void OnKeyDown(object sender, KeyEventArgs e);
        void OnKeyUp(object sender, KeyEventArgs e);
    }
}
