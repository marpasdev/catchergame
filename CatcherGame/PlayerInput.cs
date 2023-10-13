using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CatcherGame
{    
     public class PlayerInput
    {
        public List<Keys> Right;
        public List<Keys> Left;

        public PlayerInput(List<Keys> right, List<Keys> left)
        {
            Right = right;
            Left = left;
        }
    }

}
