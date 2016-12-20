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
        Core core;
        public bool IsNowShown;
        TextBlock textBlock1;
        TextBlock textBlock2;
        TextBox textBox1;
        TextBox textBox2;
        ElecComp elecComp;

        public SuspensionWindow(Core core_)
        {
            core = core_;
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
                    core.Mycanvas.Children.Add(textBlock1);
                    textBox1 = new TextBox();
                    textBox1.Width = 70;
                    textBox1.Text = "" + ((Resistance)elecComp).R;
                    Canvas.SetLeft(textBox1, postion.X);
                    Canvas.SetTop(textBox1, postion.Y - 30);
                    textBox1.KeyUp += TextBox1_KeyUp;
                    core.Mycanvas.Children.Add(textBox1);
                    break;
                case ElecComp.Comp_Capacity:
                    textBlock1 = new TextBlock();
                    textBlock1.Width = 30;
                    textBlock1.Text = "F";
                    Canvas.SetLeft(textBlock1, postion.X + 75);
                    Canvas.SetTop(textBlock1, postion.Y - 30);
                    core.Mycanvas.Children.Add(textBlock1);
                    textBox1 = new TextBox();
                    textBox1.Width = 70;
                    textBox1.Text = "" + ((Capacity)elecComp).C;
                    Canvas.SetLeft(textBox1, postion.X);
                    Canvas.SetTop(textBox1, postion.Y - 30);
                    textBox1.KeyUp += TextBox1_KeyUp;
                    core.Mycanvas.Children.Add(textBox1);
                    break;
                case ElecComp.Comp_Inductance:
                    textBlock1 = new TextBlock();
                    textBlock1.Width = 30;
                    textBlock1.Text = "H";
                    Canvas.SetLeft(textBlock1, postion.X + 75);
                    Canvas.SetTop(textBlock1, postion.Y - 30);
                    core.Mycanvas.Children.Add(textBlock1);
                    textBox1 = new TextBox();
                    textBox1.Width = 70;
                    textBox1.Text = "" + ((Inductance)elecComp).L;
                    Canvas.SetLeft(textBox1, postion.X);
                    Canvas.SetTop(textBox1, postion.Y - 30);
                    textBox1.KeyUp += TextBox1_KeyUp;
                    core.Mycanvas.Children.Add(textBox1);
                    break;
                case ElecComp.Comp_Power:
                    textBlock1 = new TextBlock();
                    textBlock1.Width = 30;
                    textBlock1.Text = "V (电压)";
                    Canvas.SetLeft(textBlock1, postion.X + 75);
                    Canvas.SetTop(textBlock1, postion.Y - 30);
                    core.Mycanvas.Children.Add(textBlock1);
                    textBox1 = new TextBox();
                    textBox1.Width = 70;
                    textBox1.Text = "" + ((Power)elecComp).voltage;
                    Canvas.SetLeft(textBox1, postion.X);
                    Canvas.SetTop(textBox1, postion.Y - 30);
                    textBox1.KeyUp += TextBox1_KeyUp;
                    core.Mycanvas.Children.Add(textBox1);
                    break;
                case ElecComp.Comp_ACPower:
                    textBlock1 = new TextBlock();
                    textBlock1.Width = 70;
                    textBlock1.Text = "V (幅值)";
                    Canvas.SetLeft(textBlock1, postion.X + 75);
                    Canvas.SetTop(textBlock1, postion.Y - 30);
                    mainWindow.Mycanvas.Children.Add(textBlock1);
                    textBox1 = new TextBox();
                    textBox1.Width = 70;
                    textBox1.Text = "" + ((ACPower)elecComp).pp_value;
                    Canvas.SetLeft(textBox1, postion.X);
                    Canvas.SetTop(textBox1, postion.Y - 30);
                    textBox1.KeyUp += TextBox1_KeyUp;
                    mainWindow.Mycanvas.Children.Add(textBox1);

                    textBlock2 = new TextBlock();
                    textBlock2.Width = 70;
                    textBlock2.Text = " Hz(频率)";
                    Canvas.SetLeft(textBlock2, postion.X + 75);
                    Canvas.SetTop(textBlock2, postion.Y - 60);
                    mainWindow.Mycanvas.Children.Add(textBlock2);
                    textBox2 = new TextBox();
                    textBox2.Width = 70;
                    textBox2.Text = "" + ((ACPower)elecComp).frequency;
                    Canvas.SetLeft(textBox2, postion.X);
                    Canvas.SetTop(textBox2, postion.Y - 60);
                    textBox2.KeyUp += TextBox2_KeyUp;
                    mainWindow.Mycanvas.Children.Add(textBox2);

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
        private void TextBox2_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
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
                core.Mycanvas.Children.Remove(textBox1);
                textBox1 = null;
            }
            if (textBlock1 != null)
            {
                core.Mycanvas.Children.Remove(textBlock1);
                textBlock1 = null;
            }
            if (textBox2 != null)
            {
                mainWindow.Mycanvas.Children.Remove(textBox2);
                textBox2 = null;
            }
            if (textBlock2 != null)
            {
                mainWindow.Mycanvas.Children.Remove(textBlock2);
                textBlock2 = null;
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
                case ElecComp.Comp_Inductance:
                    double L;
                    if (IsNumeric(textBox1.Text, out L))
                    {
                        Inductance inductance  = (Inductance)elecComp;
                        inductance.L = L;
                        ReleaseChooses();
                    }
                    else
                    {
                        MessageBox.Show("输入不是数字");
                    }
                    break;
                case ElecComp.Comp_Power:
                    double V;
                    if (IsNumeric(textBox1.Text, out V))
                    {
                        Power power= (Power)elecComp;
                        power.voltage=V;
                        ReleaseChooses();
                    }
                    else
                    {
                        MessageBox.Show("输入不是数字");
                    }
                    break;
                case ElecComp.Comp_ACPower:
                    double  Vpp;
                    double fre;
                    if (IsNumeric(textBox1.Text, out Vpp))
                    {
                        ACPower acpower = (ACPower)elecComp;
                        acpower.pp_value = Vpp;
                        
                    }
                    else
                    {
                        MessageBox.Show("输入不是数字");
                    }
                    if (IsNumeric(textBox2.Text, out fre))
                    {
                        ACPower acpower = (ACPower)elecComp;
                        acpower.frequency =fre ;
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
