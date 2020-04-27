using App_CEP.Servico;
using App_CEP.Servico.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_CEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }
        private void BuscarCEP(object sender, EventArgs args)
        {
            //TODO logica do programa

            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {

                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {2} de {3} {0}, {1}, ", end.localidade, end.uf, end.logradouro, end.bairro);

                    }
                    else
                    {
                        DisplayAlert("Erro", "O endereço não foi encontrado para o CEP informado:" + cep, "Ok");
                    }

                }
                catch (Exception e)
                {
                    DisplayAlert("Erro Crítico", e.Message, "Ok");
                }
            }
        }

        private bool isValidCEP(string cep)
        {

            bool valido = true;

            if (cep.Length != 8)
            {
                //erro
                DisplayAlert("Erro", "CEP inválido! O CEP deve conter 8 caracteres", "Ok");

                valido = false;
            }
            int NovoCEP = 0;
            if (!int.TryParse(cep, out NovoCEP))
            {
                //erro

                DisplayAlert("Erro", "CEP inválido! O CEP deve ser composto apenas por números", "Ok");

                valido = false;
            }

            return valido;
        }
    }
}
