using Microsoft.Extensions.Logging;
using Prism.Ioc;
using TaskStar.ZvtTest.Contracts;

namespace TaskStar.ZvtTest.StartApp
{
    public class ViewModelBase : Prism.Mvvm.BindableBase
    {
        #region Private Fields

        private IContainerRegistry _ContainerRegistry;

        private ILogger _Logger;

        //Register Logger
        private IZvtTestManager _ZvtTestManager;

        #endregion Private Fields

        #region Public Constructors

        public ViewModelBase()
        {
        }

        public ViewModelBase(IContainerRegistry containerRegistry
            , Microsoft.Extensions.Logging.ILogger logger
            , IZvtTestManager zvtTestManager)
        {
            ContainerRegistry = containerRegistry;
            Logger = logger;
            ZvtTestManager = zvtTestManager;
        }

        #endregion Public Constructors

        #region Public Properties

        public IContainerRegistry ContainerRegistry { get => _ContainerRegistry; set => _ContainerRegistry = value; }
        public ILogger Logger { get => _Logger; set => _Logger = value; }
        public IZvtTestManager ZvtTestManager { get => _ZvtTestManager; set => _ZvtTestManager = value; }

        #endregion Public Properties
    }
}