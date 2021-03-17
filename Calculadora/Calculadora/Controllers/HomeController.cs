using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Calculadora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Visor = 0;
            ViewBag.PrimeiroOperador = "nao";
            return View();
        }

        [HttpPost]
        public IActionResult Index(string botao, string visor, string primeiroOperador)
        {

            //avaliar o valor associado à variável 'botao'
            switch (botao)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    //atribuir ao 'visor' o valor do 'botão'
                    if (visor == "0") visor = botao;
                    else visor = visor + botao;
                    break;

                case "+/-":
                    //faz a inversão do valor do visor
                    if (visor.StartsWith('-')) visor = visor.Substring(1);
                    else visor = "-" + visor;
                    break;

                case ",":
                    if (!visor.Contains(',')) visor = visor + ",";
                    break;


                case "+":
                case "-":
                case "x":
                case "/":
                    string primeiroOperando = "";
                    string operador = "";//marcar o visor como sendo necessário o seu reinício
                    if (primeiroOperador == "sim") {
                        //armezenar os valores atuais para cálculos futuros
                        // Visor atual, que passa a '1º oparando'
                        primeiroOperando = visor;
                        //guardar o valor do operador
                        operador = botao;
                        //assinalar que já se escolheu um operador
                        primeiroOperador = "nao";
                    }
                    else {
                        //esta é a 2ª vez(ou mais) que se selecionou um 'operador'
                        //efetuar a operação com o operador antigo, e os valores dos operandos
                        double operando1 = Convert.ToDouble(primeiroOperando);
                        double operando2 = Convert.ToDouble(visor);
                        //efetuar a operação aritmética
                        switch (operador)
                        {
                            case "+":
                                visor = operando1 + operando2 + "";
                                break;
                            case "-":
                                visor = operando1 - operando2 + "";
                                break;
                            case "x":
                                visor = operando1 + operando2 + "";
                                break;
                            case "/":
                                visor = operando1 / operando2 + "";
                                break;
                        }
                    }

                    break;

            }//fim do switch

            //enviar o valor do 'visor' para a view
            ViewBag.Visor = visor;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}