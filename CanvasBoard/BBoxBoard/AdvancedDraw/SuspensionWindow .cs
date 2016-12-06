using BBoxBoard.BasicDraw;
using BBoxBoard.Comp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BBoxBoard.AdvancedDraw
{
    public class SuspensionWindow
    {
        MainWindow mainWindow;
        public bool IsNowShown;
        TextBlock textBlock1;
        TextBox textBox1;
        ElecComp elecComp;

        public SuspensionWindow(MainWindow mainWindow_)
        {
            mainWindow = mainWindow_;
            IsNowShown = false;
        }

        public void ShowChooses(ElecComp elecComp_, IntPoint postion)
        {
            IsNowShown = true;
            elecComp = elecComp_;
            //MessageBox.Show("Choose" + elecComp.Comp);
            switch (elecComp.Comp)
            {
                case ElecComp.Comp_Resistance:
                    textBlock1 = new TextBlock();
                    textBlock1.Width = 30;
                    textBlock1.Text = "Ω";
                    Canvas.SetLeft(textBlock1, postion.X + 75);
                    Canvas.SetTop(textBlock1, postion.Y - 30);
                    mainWindow.Mycanvas.Children.Add(textBlock1);
                    textBox1 = new TextBox();
                    textBox1.Width = 70;
                    textBox1.Text = "" + ((Resistance)elecComp).R;
                    Canvas.SetLeft(textBox1, postion.X);
                    Canvas.SetTop(textBox1, postion.Y - 30);
                    textBox1.KeyUp += TextBox1_KeyUp;
                    mainWindow.Mycanvas.Children.Add(textBox1);
                    break;
                case ElecComp.Comp_Capacity:
                    textBlock1 = new TextBlock();
                    textBlock1.Width = 30;
                    textBlock1.Text = "F";
                    Canvas.SetLeft(textBlock1, postion.X + 75);
                    Canvas.SetTop(textBlock1, postion.Y - 30);
                    mainWindow.Mycanvas.Children.Add(textBlock1);
                    textBox1 = new TextBox();
                    textBox1.Width = 70;
                    textBox1.Text = "" + ((Capacity)elecComp).C;
                    Canvas.SetLeft(textBox1, postion.X);
                    Canvas.SetTop(textBox1, postion.Y - 30);
                    textBox1.KeyUp += TextBox1_KeyUp;
                    mainWindow.Mycanvas.Children.Add(textBox1);
                    break;
                default:
                    IsNowShown = false;
                    elecComp = null;
                    break;
            }
        }

        private void TextBox1_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EndInput();
            }
        }

        public void ReleaseChooses()
        {
            IsNowShown = false;
            if (textBox1 != null)
            {
                mainWindow.Mycanvas.Children.Remove(textBox1);
                textBox1 = null;
            }
            if (textBlock1 != null)
            {
                mainWindow.Mycanvas.Children.Remove(textBlock1);
                textBlock1 = null;
            }
        }

        private void EndInput()
        {
            //MessageBox.Show(textBox1.Text);
            switch (elecComp.Comp)
            {
                case ElecComp.Comp_Resistance:
                    double R;
                    if (IsNumeric(textBox1.Text, out R))
                    {
                        Resistance resistance = (Resistance)elecComp;
                        resistance.R = R;
                        ReleaseChooses();
                    }
                    else
                    {
                        MessageBox.Show("输入不是数字");
                    }
                    break;
                case ElecComp.Comp_Capacity:
                    double C;
                    if (IsNumeric(textBox1.Text, out C))
                    {
                        Capacity capacity = (Capacity)elecComp;
                        capacity.C = C;
                        ReleaseChooses();
                    }
                    else
                    {
                        MessageBox.Show("输入不是数字");
                    }
                    break;
                default:
                    break;
            }
        }

        private bool IsNumeric(String str, out double Result)
        {
            Result = -1;
            try
            {
                Result = Convert.ToDouble(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
