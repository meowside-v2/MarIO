using Mario.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Data.Objects
{
    
    class Block : Core_objects
    {
        
        public bool IsSecret { get; set; }
        public bool IsBonus { get; set; }
        public bool IsDestroyable { get; set; }
        public int Type { get; set; }
        public Block(int? _X = null, int? _Y = null, int? _Type = null)
        {
            if (_X != null) this.X = (int)_X;
            if (_Y != null) this.Y = (int)_Y;
            if (_Type != null) this.Init((int)_Type);
        }

        public void Init(int type)
        {
            Type = type;

            switch (type)
            {
                case 0:         // Ground - Bonus

                    IsSecret = false;
                    IsBonus = true;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.BlockGround1, ObjectDatabase.path));
                    break;

                case 1:         // Ground - Ground

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.BlockGround2, ObjectDatabase.path));
                    break;

                case 2:         // Ground - Brick 1

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.BlockGround3, ObjectDatabase.path));
                    break;

                case 3:         // Ground - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = true;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.BlockGround4, ObjectDatabase.path));
                    break;

                case 4:         // Ground - Sky 1

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky1, ObjectDatabase.path));
                    break;

                case 5:         // Ground - Sky 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky2, ObjectDatabase.path));
                    break;

                case 6:         // Ground - Sky 3

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky3, ObjectDatabase.path));
                    break;

                case 7:         // Ground - Sky 4

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky4, ObjectDatabase.path));
                    break;

                case 8:         // Ground - Sky 5

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky5, ObjectDatabase.path));
                    break;

                case 9:         // Ground - Sky 6

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky6, ObjectDatabase.path));
                    break;

                case 10:         // Ground - Sky 7

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky7, ObjectDatabase.path));
                    break;

                case 11:         // UnderGround - Bonus

                    IsSecret = false;
                    IsBonus = true;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.BlockUnderGround1, ObjectDatabase.path));
                    break;

                case 12:         // UnderGround - ground

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.BlockUnderGround2, ObjectDatabase.path));
                    break;

                case 13:         // UnderGround - Brick 1

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.BlockUnderGround3, ObjectDatabase.path));
                    break;

                case 14:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.BlockUnderGround4, ObjectDatabase.path));
                    break;

                

                case 15:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.UnderGroundBackground1, ObjectDatabase.path));
                    break;

                case 16:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.UnderGroundBackground2, ObjectDatabase.path));
                    break;

                case 17:         // Pipe 1

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Pipe1, ObjectDatabase.path));
                    break;

                case 18:         // Pipe 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Pipe2, ObjectDatabase.path));
                    break;

                case 19:         // Pipe 3

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Pipe3, ObjectDatabase.path));
                    break;

                case 20:         // Pipe 4

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Pipe4, ObjectDatabase.path));
                    break;

                case 21:         // Pipe 5

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Pipe5, ObjectDatabase.path));
                    break;
            }
        }
    }
}
