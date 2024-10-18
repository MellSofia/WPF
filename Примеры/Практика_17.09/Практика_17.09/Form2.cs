using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Практика_17._09
{
    public partial class Form2 : Form
    {
        List<Vehicle> auto_park = new List<Vehicle>();
        public Form2(List<Vehicle> auto_parkk)
        {
            InitializeComponent();
            auto_park.AddRange(auto_parkk);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string final_calc = "";
            if(textBox1.Text != "")
            {
                final_calc += Calculate('л');
            }
            if (textBox2.Text != "")
            {
                final_calc += Calculate('н');
            }
            if (textBox3.Text != "")
            {
                final_calc += Calculate('а');
            }
            if (textBox4.Text != "")
            {
                final_calc += Calculate('г');
            }
            if(find_rbutt() == -1)
            {
                MessageBox.Show("Не выбран промежуток времени!");
            }
            else
            {
                MessageBox.Show(final_calc);
            }
        }
        private string Calculate(char command)
        {

            if(command == 'л')
            {
                double count = 0;
                double fuel_cons = Double.Parse(textBox1.Text) / 100;
                double fuel_consumption = 0;
                string purpose_fuel = "";
                Vehicle veh  = new Vehicle();
                foreach(Vehicle v in auto_park)
                {
                    if (v.Purpose == "Легковая")
                    {
                        count++;
                        fuel_consumption = v.FuelConsumption;
                        purpose_fuel = v.PurposeFuel;
                    }
                        
                }
                return $"Потрачено {purpose_fuel}: {count*find_rbutt()*fuel_consumption * fuel_cons}";
            }
            if (command == 'н')
            {
                double count = 0;
                double fuel_cons = Double.Parse(textBox2.Text) / 100;
                double fuel_consumption = 0;
                string purpose_fuel = "";
                Vehicle veh = new Vehicle();
                foreach (Vehicle v in auto_park)
                {
                    if (v.Purpose == "Машина начальства")
                    {
                        count++;
                        fuel_consumption = v.FuelConsumption;
                        purpose_fuel = v.PurposeFuel;
                    }

                }
                return $"Потрачено {purpose_fuel}: {count * find_rbutt() * fuel_consumption * fuel_cons}";
            }
            if (command == 'а')
            {
                double count = 0;
                double fuel_cons = Double.Parse(textBox3.Text) / 100;
                double fuel_consumption = 0;
                string purpose_fuel = "";
                Vehicle veh = new Vehicle();
                foreach (Vehicle v in auto_park)
                {
                    if (v.Purpose == "Автобус")
                    {
                        count++;
                        fuel_consumption = v.FuelConsumption;
                        purpose_fuel = v.PurposeFuel;
                    }

                }
                return $"Потрачено {purpose_fuel}: {count * find_rbutt() * fuel_consumption * fuel_cons}";
            }
            if (command == 'г')
            {
                double count = 0;
                double fuel_cons = Double.Parse(textBox4.Text) / 100;
                double fuel_consumption = 0;
                string purpose_fuel = "";
                Vehicle veh = new Vehicle();
                foreach (Vehicle v in auto_park)
                {
                    if (v.Purpose == "Грузовик")
                    {
                        count++;
                        fuel_consumption = v.FuelConsumption;
                        purpose_fuel = v.PurposeFuel;
                    }

                }
                return $"Потрачено {purpose_fuel}: {count * find_rbutt() * fuel_consumption * fuel_cons}";
            }
            return "";
        }
        private double find_rbutt()
        {
            if(radioButton1.Checked == true) 
            {
                return 1;
            }
            if (radioButton2.Checked == true)
            {
                return 7;
            }
            if (radioButton3.Checked == true)
            {
                return 30;
            }
            if (radioButton4.Checked == true)
            {
                return 90;
            }
            return -1;
        }
        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton4.Checked = false;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }
    }
}
