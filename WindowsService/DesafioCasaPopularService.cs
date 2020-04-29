using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using WindowsService.Service;
using static System.DateTime;

namespace WindowsService
{
    public partial class DesafioCasaPopularService : ServiceBase
    { 
        private Timer timerGerarRegistrosPontuacoesNaoReceberamCasa;
        private int FrequenciaExecucao => Convert.ToInt32(TimeSpan.FromHours(24).TotalMilliseconds); 
        private int ExecutarServicosAposIniciarServico
        {
            get
            {
                DateTime horaMarcada = Parse(ConfigurationManager.AppSettings["HoraMarcadaGerarRegistrosPontuacoesNaoReceberamCasa"]);
                if (Now > horaMarcada)
                {
                    //se já passou do horário que deveria executar, executa no horário setado do próximo dia
                    horaMarcada = horaMarcada.AddDays(1);
                }

                TimeSpan timeSpan = horaMarcada.Subtract(Now);
                return Convert.ToInt32(timeSpan.TotalMilliseconds);
            }
        }

        private PontuacaoCasaPopularService _pontuacaoCasaPopularService;
        private PontuacaoCasaPopularService pontuacaoCasaPopularService => _pontuacaoCasaPopularService ?? (_pontuacaoCasaPopularService = new PontuacaoCasaPopularService());
         
        public DesafioCasaPopularService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            timerGerarRegistrosPontuacoesNaoReceberamCasa_Tick(null);
        }

        protected override void OnStart(string[] args)
        {
            timerGerarRegistrosPontuacoesNaoReceberamCasa = new Timer(timerGerarRegistrosPontuacoesNaoReceberamCasa_Tick,
                                                                      null,
                                                                      ExecutarServicosAposIniciarServico,
                                                                      FrequenciaExecucao);
        }

        protected override void OnStop()
        {
            timerGerarRegistrosPontuacoesNaoReceberamCasa.Dispose();
        }

        private void timerGerarRegistrosPontuacoesNaoReceberamCasa_Tick(object sender)
        {
            pontuacaoCasaPopularService.GerarRegistrosPontuacoesNaoReceberamCasa();
        }
    }
}
