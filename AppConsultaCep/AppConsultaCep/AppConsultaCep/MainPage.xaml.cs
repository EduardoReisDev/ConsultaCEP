using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppConsultaCep.Servico.Modelo;
using AppConsultaCep.Servico;

namespace AppConsultaCep
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP (object sender, EventArgs args)
        {
            String cep = CEP.Text.Trim();

            if (validandoCEP(cep))
            {
                try
                {
                    Endereco end = ViaCepServico.BuscarEnderecoViaCep(cep);
                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {0}, {1}, {2}, {3}", end.localidade, end.uf, end.logradouro, end.bairro);
                    }

                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " +cep, "OK");
                    }
                }

                catch(Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }

        private bool validandoCEP(string cep)
        {
            bool valido = true;

            if(cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres.", "OK");
                valido = false;
            }
            int NovoCEP = 0;

            if(!int.TryParse(cep,  out NovoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP dever ser composto apenas por números.", "OK");
                valido = false;
            }

            return valido;
        }
    }
}
