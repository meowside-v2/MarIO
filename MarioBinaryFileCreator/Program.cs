using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarioBinaryFileCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            //MusicToFile();
            //FontToFile();
            BlocksToFile();
        }

        public static void BlocksToFile()
        {
            List<byte[]> converted = new List<byte[]>();
            
            string[] files = Directory.GetFiles(Environment.CurrentDirectory + @"\Data\Sprites\Ground", "*.png");
            
            foreach (string s in files)
            {
                using (FileStream fs = new FileStream(s, FileMode.Open, FileAccess.Read))
                {
                    byte[] temp = new byte[fs.Length];

                    fs.Read(temp, 0, (int)fs.Length);
                    fs.Close();

                    converted.Add(temp);
                }
            }

            files = Directory.GetFiles(Environment.CurrentDirectory + @"\Data\Sprites\Underground", "*.png");

            foreach (string s in files)
            {
                using (FileStream fs = new FileStream(s, FileMode.Open, FileAccess.Read))
                {
                    byte[] temp = new byte[fs.Length];

                    fs.Read(temp, 0, (int)fs.Length);
                    fs.Close();

                    converted.Add(temp);
                }
            }

            files = Directory.GetFiles(Environment.CurrentDirectory + @"\Data\Sprites\Pipes", "*.png");

            foreach (string s in files)
            {
                using (FileStream fs = new FileStream(s, FileMode.Open, FileAccess.Read))
                {
                    byte[] temp = new byte[fs.Length];

                    fs.Read(temp, 0, (int)fs.Length);
                    fs.Close();

                    converted.Add(temp);
                }
            }

            using (FileStream fs = new FileStream(Environment.CurrentDirectory + @"\Data\Sprites\border.png", FileMode.Open, FileAccess.Read))
            {
                byte[] temp = new byte[fs.Length];

                fs.Read(temp, 0, (int)fs.Length);
                fs.Close();

                converted.Add(temp);
            }

            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream(Environment.CurrentDirectory + @"\Data\Sprites\MarioBlockSpriteFile.MEX", FileMode.Create));
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot create file.");
                return;
            }

            try
            {
                bw.Write(converted.Count);

                for (int index = 0; index < converted.Count; index++)
                {
                    bw.Write(converted[index].Length);
                    bw.Write(converted[index], 0, converted[index].Length);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot write to file.");
                return;
            }

            bw.Close();
        }

        public static void FontToFile()
        {
            string[] files = Directory.GetFiles(Environment.CurrentDirectory + @"\Data\Font", "*.png");

            List<byte[]> converted = new List<byte[]>();

            foreach (string s in files)
            {
                FileStream fs = new FileStream(s, FileMode.Open, FileAccess.Read);

                byte[] temp = new byte[fs.Length];

                fs.Read(temp, 0, (int)fs.Length);
                fs.Close();

                converted.Add(temp);
            }

            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream(Environment.CurrentDirectory + @"\Data\Font\MarioFontFile.MEX", FileMode.Create));
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot create file.");
                return;
            }

            try
            {
                bw.Write(converted.Count);

                for (int index = 0; index < converted.Count; index++)
                {
                    bw.Write(converted[index].Length);
                    bw.Write(converted[index], 0, converted[index].Length);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot write to file.");
                return;
            }

            bw.Close();
        }

        /*public static void MusicToFile()
        {
            string[] files = Directory.GetFiles(Environment.CurrentDirectory + @"\Data\Music", "*.wav");

            List<byte[]> converted = new List<byte[]>();

            foreach (string s in files)
            {
                FileStream fs = new FileStream(s, FileMode.Open, FileAccess.Read);

                byte[] temp = new byte[fs.Length];

                fs.Read(temp, 0, (int)fs.Length);
                fs.Close();

                converted.Add(temp);
            }

            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream("Data\\Music\\MarioMusicFile.MEX", FileMode.Create));
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot create file.");
                return;
            }

            try
            {
                bw.Write(converted.Count);

                for (int index = 0; index < converted.Count; index++)
                {
                    bw.Write(converted[index].Length);
                    bw.Write(converted[index], 0, converted[index].Length);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot write to file.");
                return;
            }

            bw.Close();
        }*/
    }
}

/*

            converted.Clear();

            BinaryReader br;

            try
            {
                br = new BinaryReader(new FileStream("Music\\MarioMusicFile.MEX", FileMode.Open));
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot create file.");
                return;
            }

            try
            {
                int lenght = br.ReadInt32();

                for (int index = 0; index < lenght; index++)
                {
                    int temp = br.ReadInt32();
                    converted.Add(br.ReadBytes(temp));
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot write to file.");
                return;
            }*/
