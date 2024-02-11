using Microsoft.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using Serilog.Extensions.Logging;
using System.Windows;
using TaskStar.ZvtTest.Contracts;
using TaskStar.ZvtTest.ZvtManager;

namespace TaskStar.ZvtTest.StartApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        #region Private Fields

        private Microsoft.Extensions.Logging.ILogger _Logger;

        #endregion Private Fields

        #region Private Methods

        private void InitLogger()
        {
            Serilog.ILogger logger = ZvtManager.LoggerFactory.CreateLoggerSerilog(null, "serilogconfig.json", "Serilog");
            _Logger = new SerilogLoggerFactory(logger).CreateLogger<App>();
            _Logger.LogInformation($"logger created");
        }

        private void InitApp()
        {
            InitLogger();
        }

        #endregion Private Methods

        #region Protected Methods

        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            InitApp();

            //Register container
            containerRegistry.RegisterInstance<IContainerRegistry>(containerRegistry);

            //Register Logger
            containerRegistry.RegisterInstance(_Logger);

            //Register Logger
            containerRegistry.RegisterInstance<IZvtTestManager>(new ZvtTestManager());
        }

        #endregion Protected Methods
    }
}