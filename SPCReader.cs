namespace SPCReader
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
    using System.Windows;
    public class SPCReader
    {
        float[,] data;
        public float[,] read(String path)
        {
            byte b = 0; // 1 byte
            char ch = 'a'; // 2 byte
            int integer = 0; //4 byts
            double d = 0; // 8 byte
            float f = 0; // 16 byte
            string pathSave = path.Replace(".spc", ".txt");
            pathSave = pathSave.Replace(@"\", @"//");
            try
            {
                using (FileStream fstream = File.OpenRead(path))
                {

                    byte[] array = new byte[fstream.Length];

                    fstream.Read(array, 0, array.Length);

                    if (array[1] == (byte)75)
                    {
                        //MessageBox.Show("1");

                    }
                    else if (array[1] == (byte)77)
                    {
                        //MessageBox.Show("2");
                    }

                    int v4 = BitConverter.ToInt32(array, 4); //Number of points

                    double v5 = BitConverter.ToDouble(array, 8); // First X coordinate

                    double v6 = BitConverter.ToDouble(array, 16); // Last X coordinate

                    long v7 = BitConverter.ToInt32(array, 24) + 1; // Number of subfiles

                    long v12 = BitConverter.ToInt32(array, 32); //Compressed Date

                    string v13 = BitConverter.ToString(array, 36, 43); //Resolution description text

                    string v14 = BitConverter.ToString(array, 44, 52); //Source instrument description text

                    string v15 = BitConverter.ToString(array, 53, 54); //Peak point number for interferograms

                    for (int i = 55; i <= 86; i += 4)
                    {
                        float v16 = BitConverter.ToSingle(array, i); //Number of points
                    }
                    float v311 = BitConverter.ToSingle(array, 311);


                    this.data = new float[v7, v4];
                    int j = 544;
                    int jMax = j + v4 * 4;

                    for (int i = 0; i < v7; i++)
                    {

                        if (i == v7 - 1)
                        {
                           
                            jMax = jMax - 32;
                        }
                        for (int i2 = 0; j < jMax; j += 4, i2++)
                        {
                            data[i, i2] = BitConverter.ToSingle(array, j);
                        }
                        j = jMax + 32;
                        jMax = j + v4 * 4;
                       

                    }

                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }

            return this.data;
        }
    }
}

