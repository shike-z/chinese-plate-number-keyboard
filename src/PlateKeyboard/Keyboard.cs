using System;
using System.Linq;
using System.Windows.Forms;

namespace PlateKeyboard
{
    public partial class Keyboard : Form
    {
        public event Action<string> TextCompleted;

        public Keyboard(string text)
        {
            InitializeComponent();

            this.tbText.Text = text;
            this.tbText.SelectionStart = 0;
            this.tbText.SelectionLength = 0;

            HookEvents();
        }

        private void HookEvents()
        {
            this.Shown += Keyboard_Shown;

            this.btnOk.Click += BtnOk_Click;
            this.btnClose.Click += BtnClose_Click;

            this.tbText.KeyUp += TbText_KeyUp;
            this.tbText.MouseClick += TbText_MouseClick;

            foreach (var button in this.panel1.Controls.OfType<Button>())
            {
                button.Click += Button_Click;
            }

            foreach (var button in this.panel2.Controls.OfType<Button>())
            {
                button.Click += Button_Click;
            }
        }

        private void Keyboard_Shown(object sender, EventArgs e)
        {
            this.tbText.Focus();
            ShowKeys(this.tbText.SelectionStart);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            TextCompleted?.Invoke(this.tbText.Text.Trim());
            this.Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TbText_KeyUp(object sender, KeyEventArgs e)
        {
            ShowKeys(this.tbText.SelectionStart);
        }

        private void TbText_MouseClick(object sender, MouseEventArgs e)
        {
            ShowKeys(this.tbText.SelectionStart);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var key = char.Parse(((Button)sender).Text);

            var selectionStart = this.tbText.SelectionStart + 1;
            var text1 = this.tbText.Text.Substring(0, this.tbText.SelectionStart);
            var text2 = this.tbText.Text.Substring(this.tbText.SelectionStart);
            this.tbText.Text = text1 + key + text2;

            this.tbText.SelectionStart = selectionStart;

            ShowKeys(this.tbText.SelectionStart);

            this.tbText.Focus();
        }

        private void ShowKeys(int textIndex)
        {
            panel1.Visible = textIndex == 0;
            panel2.Visible = textIndex > 0;
        }
    }
}
