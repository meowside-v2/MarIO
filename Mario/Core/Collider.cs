using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class Collider
    {
        public bool CollisionLeft(Core_objects obj, World map, List<Enemy> enemies)
        {
            for (int row = obj.Y; row < obj.Y + obj.mesh.height - 1; row++)
            {
                if (obj.X <= 0) return true;
                else if (map.mesh.bitmapTransparent[row, obj.X - 1] == 255 && obj.X > 0) return true;
            }

            foreach (var item in enemies)
            {

            }

            return false;
        }

        public bool CollisionRight(Core_objects obj, World map, List<Enemy> enemies)
        {
            for (int row = obj.Y; row < obj.Y + obj.mesh.height - 1; row++)
            {
                if (obj.X + obj.mesh.width >= 300) return true;
                else if (map.mesh.bitmapTransparent[row, obj.X + obj.mesh.width] == 255 && obj.X + obj.mesh.width - 1 < 300) return true;
            }

            foreach (var item in enemies)
            {

            }

            return false;
        }

        public bool CollisionTop(Core_objects obj, World map, List<Enemy> enemies)
        {
            for (int column = obj.X; column < obj.X + obj.mesh.width - 1; column++)
            {
                if (obj.Y <= 0) return true;
                else if (map.mesh.bitmapTransparent[obj.Y - 1, column] == 255 && obj.Y > 0) return true;
            }

            foreach (var item in enemies)
            {

            }

            return false;
        }

        public bool CollisionBottom(Core_objects obj, World map, List<Enemy> enemies)
        {
            for (int column = obj.X; column < obj.X + obj.mesh.width - 1; column++)
            {
                if (obj.Y + obj.mesh.height - 1 >= 100) return true;
                else if (map.mesh.bitmapTransparent[obj.Y + obj.mesh.height, column] == 255 && obj.Y + obj.mesh.height - 1 < 100) return true;
            }

            foreach (var item in enemies)
            {

            }

            return false;
        }
    }
}
