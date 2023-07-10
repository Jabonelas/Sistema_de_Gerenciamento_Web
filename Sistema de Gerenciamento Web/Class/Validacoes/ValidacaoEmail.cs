using System.Text.RegularExpressions;

namespace Sistema_de_Gerenciamento_Web.Class.Validacoes
{
    public static class ValidacaoEmail
    {
        public static bool IsEmailValido(string _email)
        {
            Regex validaEmail = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

            if (!validaEmail.IsMatch(_email))
            {
                return false;
            }
            return true;
        }
    }
}