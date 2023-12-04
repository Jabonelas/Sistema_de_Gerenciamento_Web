using Microsoft.AspNetCore.Mvc;
using Sistema_de_Gerenciamento_Web.Class.Validacoes;
using Sistema_de_Gerenciamento_Web.Context;

namespace Sistema_de_Gerenciamento_Web.Controllers
{
    public class ValidacoesController : Controller
    {
        private bool IsCpfExiste(string _cpf)
        {
            try
            {
                if (_cpf != null)
                {
                    using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                    {
                        var isCpfExiste = context.tb_registro.Where(x => x.rg_cpf.Equals(_cpf)).Any();

                        if (isCpfExiste == true)
                        {
                            string mensagem = "CPF já Cadastrado!";
                            TempData["mensagemFalha"] = mensagem;
                        }

                        return isCpfExiste;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao buscar CPF!";

                return true;
            }
        }

        private bool IsCnpjExiste(string _cnpj)
        {
            try
            {
                if (_cnpj != null)
                {
                    using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                    {
                        var isCnpjExiste = context.tb_registro.Where(x => x.rg_cnpj.Equals(_cnpj)).Any();

                        if (isCnpjExiste == true)
                        {
                            string mensagem = "CNPJ já Cadastrado!";
                            TempData["mensagemFalha"] = mensagem;
                        }

                        return isCnpjExiste;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao buscar CNPJ!";

                return true;
            }
        }

        [HttpPost]
        public JsonResult ValidarCPF(string _cpf)
        {
            bool isCpfValido = true;

            if (_cpf != null)
            {
                isCpfValido = ValidacaoCPF.IsCpfValido(_cpf);
            }

            return Json(isCpfValido);
        }

        [HttpPost]
        public JsonResult VerificacaoExistenciaCPF(string _cpf)
        {
            bool isCpfExiste = false;

            if (_cpf != null)
            {
                isCpfExiste = IsCpfExiste(_cpf);
            }

            return Json(isCpfExiste);
        }

        [HttpPost]
        public JsonResult ValidarRG(string _rg)
        {
            bool isRgValido = true;

            if (_rg != null)
            {
                isRgValido = ValidacaoRG.IsRGValido(_rg);
            }

            return Json(isRgValido);
        }

        [HttpPost]
        public JsonResult ValidarEmail(string _email)
        {
            bool isEmailValido = true;

            if (_email != null)
            {
                isEmailValido = ValidacaoEmail.IsEmailValido(_email);
            }

            return Json(isEmailValido);
        }

        [HttpPost]
        public JsonResult ValidarCNPJ(string _cnpj)
        {
            bool isCnpjValido = true;

            if (_cnpj != null)
            {
                isCnpjValido = ValidacaoCNPJ.IsCnpjValido(_cnpj);
            }

            return Json(isCnpjValido);
        }

        [HttpPost]
        public JsonResult VerificacaoExistenciaCNPJ(string _cnpj)
        {
            bool isCnpjExiste = false;

            if (_cnpj != null)
            {
                isCnpjExiste = IsCnpjExiste(_cnpj);
            }

            return Json(isCnpjExiste);
        }
    }
}