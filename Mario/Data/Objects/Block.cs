using Mario.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockGround1, ObjectDatabase.path_block));
                    break;

                case 1:         // Ground - Ground

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockGround2, ObjectDatabase.path_block));
                    break;

                case 2:         // Ground - Brick 1

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockGround3, ObjectDatabase.path_block));
                    break;

                case 3:         // Ground - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = true;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockGround4, ObjectDatabase.path_block));
                    break;

                case 4:         // Ground - Sky 1

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky1, ObjectDatabase.path_block));
                    break;

                case 5:         // Ground - Sky 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky2, ObjectDatabase.path_block));
                    break;

                case 6:         // Ground - Sky 3

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky3, ObjectDatabase.path_block));
                    break;

                case 7:         // Ground - Sky 4

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky4, ObjectDatabase.path_block));
                    break;

                case 8:         // Ground - Sky 5

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky5, ObjectDatabase.path_block));
                    break;

                case 9:         // Ground - Sky 6

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky6, ObjectDatabase.path_block));
                    break;

                case 10:         // Ground - Sky 7

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky7, ObjectDatabase.path_block));
                    break;

                case 11:         // UnderGround - Bonus

                    IsSecret = false;
                    IsBonus = true;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockUnderGround1, ObjectDatabase.path_block));
                    break;

                case 12:         // UnderGround - ground

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockUnderGround2, ObjectDatabase.path_block));
                    break;

                case 13:         // UnderGround - Brick 1

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockUnderGround3, ObjectDatabase.path_block));
                    break;

                case 14:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockUnderGround4, ObjectDatabase.path_block));
                    break;

                

                case 15:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.UnderGroundBackground1, ObjectDatabase.path_block));
                    break;

                case 16:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.UnderGroundBackground2, ObjectDatabase.path_block));
                    break;

                case 17:         // Pipe 1

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Pipe1, ObjectDatabase.path_block));
                    break;

                case 18:         // Pipe 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Pipe2, ObjectDatabase.path_block));
                    break;

                case 19:         // Pipe 3

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Pipe3, ObjectDatabase.path_block));
                    break;

                case 20:         // Pipe 4

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Pipe4, ObjectDatabase.path_block));
                    break;

                case 21:         // Pipe 5

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Pipe5, ObjectDatabase.path_block));
                    break;

                case 22:         // Border

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Blocks.Border, ObjectDatabase.path_block));
                    break;

                default:
                    MessageBox.Show("No such block in database", "Out of Range", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(-1);
                    break;
            }
        }
    }
}
