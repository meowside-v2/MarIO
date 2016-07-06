using Mario.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Data.Objects
{
    
    class Block : ICore
    {

        public int X { get; set; }
        public int Y { get; set; }
        
        public bool IsSecret { get; set; }
        public bool IsBonus { get; set; }
        public bool IsDestroyable { get; set; }

        public Material mesh { get; set; }

        public void Init(int type)
        {
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

                case 4:         // UnderGround - Bonus

                    IsSecret = false;
                    IsBonus = true;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.BlockUnderGround1, ObjectDatabase.path));
                    break;

                case 5:         // UnderGround - ground

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.BlockUnderGround2, ObjectDatabase.path));
                    break;

                case 6:         // UnderGround - Brick 1

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.BlockUnderGround3, ObjectDatabase.path));
                    break;

                case 7:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.BlockUnderGround4, ObjectDatabase.path));
                    break;

                case 8:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky1, ObjectDatabase.path));
                    break;

                case 9:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky2, ObjectDatabase.path));
                    break;

                case 10:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky3, ObjectDatabase.path));
                    break;

                case 11:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky4, ObjectDatabase.path));
                    break;

                case 12:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky5, ObjectDatabase.path));
                    break;

                case 13:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Sky6, ObjectDatabase.path));
                    break;

                case 14:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.UnderGroundBackground1, ObjectDatabase.path));
                    break;

                case 15:         // UnderGround - Brick 2

                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.UnderGroundBackground2, ObjectDatabase.path));
                    break;
            }
        }
        
        public object Copy()
        {
            return this.MemberwiseClone();
        }

        public void AddTo(List<object> destination)
        {
            destination.Add(this);
        }
    }
}
