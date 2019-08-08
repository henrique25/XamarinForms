using App01_ConsultarCEP.Servico;
using App01_ConsultarCEP.Servico.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App01_ConsultarCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            Botao.Clicked += BuscarCep;
		}

        private void BuscarCep(object sender, EventArgs args)
        {
            string cep = Cep.Text.Trim();

            if (isValidCep(cep))
            {
                try{
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCep(cep);

                    if(end != null)
                    {
                        Resultado.Text = $"Endereço: {end.logradouro} {end.bairro}, {end.localidade},{end.uf}";
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o Cep informado", "OK");
                    }

                }catch(Exception ex)
                {
                    DisplayAlert("Erro Crítico", ex.Message, "OK");
                }
            }
        }

        private bool isValidCep(string cep)
        {
            bool valido = true;

            if(cep.Length != 8)
            {
                DisplayAlert("Erro", "CEP Inválido! O Cep deve conter 8 caracteres.", "OK");

                valido = false;
            }

            int NovoCEP = 0;

            if(!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("Erro", "CEP Inválido! O Cep deve conter apenas números.", "OK");

                valido = false;
            }


            return valido;
        }
	}
}
