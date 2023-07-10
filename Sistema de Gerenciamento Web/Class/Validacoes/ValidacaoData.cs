using System.Drawing;

namespace Sistema_de_Gerenciamento_Web.Class.Validacoes
{
    public static class ValidacaoData
    {
        public static bool IsDataValida(string _data)
        {
            //if (_textBox.Text.Length == _textBox.Properties.MaxLength && _textBox.Text != "----------")
            //{
            DateTime time;
            if (DateTime.TryParse(_data, out time))
            {
                //_textBox.BackColor = Color.FromArgb(0, 255, 255, 255);

                return true;
            }
            else
            {
                //_textBox.BackColor = Color.LightGray;

                //MessageBox.Show($"Data Digitada Invalida: {_textBox.Text}", "Atencao!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //_textBox.Focus();

                return false;
            }
            //}
            return true;
        }
    }
}