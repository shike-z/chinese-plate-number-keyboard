using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlateKeyboard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            HookEvents();
        }

        private void HookEvents()
        {
            this.textBox1.DoubleClick += TextBox1_DoubleClick;
        }

        private void TextBox1_DoubleClick(object sender, EventArgs e)
        {
            var keyboard = new Keyboard(this.textBox1.Text.Trim())
            {
                StartPosition = FormStartPosition.Manual,
                Location = this.PointToScreen(new Point(this.textBox1.Location.X, this.textBox1.Location.Y + this.textBox1.Height + 6))
            };

            keyboard.TextCompleted += (text) =>
            {
                this.textBox1.Text = text;
            };

            keyboard.ShowDialog();
        }
    }
}
