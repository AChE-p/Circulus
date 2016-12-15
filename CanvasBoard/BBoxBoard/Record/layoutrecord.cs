using BBoxBoard.Comp;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BBoxBoard.Record
{
    class LayoutRecord
    {
        public static void Save(ElecCompSet elecCompSet)
        {
            SaveCircDialog saveCircDialog = new SaveCircDialog();
            saveCircDialog.ShowDialog();
            MessageBox.Show("Saving!");
            String SaveingStr = "";
            SaveingStr += elecCompSet.PrintSavingAll();
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(SaveingStr);

            using (FileStream fsWrite = new FileStream(@"D:\1.circ", FileMode.Create))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }
        public static void Read(ElecCompSet elecCompSet, Canvas Mycanvas)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "circ files (*.circ)|*.circ";
            fd.ShowReadOnly = true;
            if (!fd.ShowDialog().Value) return;
            String openingFileName = fd.FileName;
            //MessageBox.Show(openingFileName);
            using (FileStream fsRead = new FileStream(openingFileName, FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                string myStr = Encoding.UTF8.GetString(heByte);
                Console.WriteLine(myStr);
                string[] Strsp = myStr.Split('\n');
                for (int i = 0; i < Strsp.Length - 1; i++)
                {
                    Console.WriteLine("(" + i + "):" + Strsp[i]);
                    String[] singleSp = Strsp[i].Split(' ');
                    if (singleSp.Length != 5)
                    {
                        MessageBox.Show("Record Wrong!");
                    }
                    for (int j = 0; j < singleSp.Length; j++)
                    {
                        Console.WriteLine("    (" + j + "):" + singleSp[j]);
                    }
                    int Comp = int.Parse(singleSp[0]);
                    int RotateState = int.Parse(singleSp[1]);
                    int X = int.Parse(singleSp[2]);
                    int Y = int.Parse(singleSp[3]);
                    //这里需要补充一句attribute
                    ElecComp x = elecCompSet.AddFromRecord(Comp, Mycanvas);
                    x.Move(X, Y);
                    for (int j = 0; j < RotateState; j++)
                    {
                        x.RotateLeft();
                    }
                    x.HandleAttr(singleSp[4]);
                }
            }
        }
    }
}
