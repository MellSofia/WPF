namespace Практика_17._09
{
    public partial class Form1 : Form
    {
        List<Vehicle> auto_park = new List<Vehicle>();
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 forma = new Form2(auto_park);
            forma.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool a = false;
            Vehicle veh = new Vehicle(auto_park.Count + 1, textBox1.Text, Int32.Parse(textBox2.Text), comboBox1.Text, comboBox2.Text);
            foreach (Vehicle v in auto_park)
            {
                if (v.Name == veh.Name)
                {
                    MessageBox.Show("Такая машина уже есть!");
                    a = true;
                }
            }
            if (!a)
            {
                auto_park.Add(veh);
                comboBox3.Items.Add(veh.Name);
            }
        }

        
    }
}
