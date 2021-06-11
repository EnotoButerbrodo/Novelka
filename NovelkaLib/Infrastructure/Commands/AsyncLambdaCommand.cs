using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovelkaCreationTool.Infrastructure.Commands.Base;

namespace NovelkaCreationTool.Infrastructure.Commands
{
    class AsyncLambdaCommand : AsyncCommand
    {
        Action<object> onExecute;
        Action<object> beforeExecute;
        Action<object, Exception> afterExecute;
        Func<object, bool> canExecute;

        public AsyncLambdaCommand(Action<object> onExecute, Func<object, bool> canExecute,
            Action<object> beforeExecute = null, Action<object, Exception> afterExecute = null)
        {
            this.onExecute = onExecute;
            this.canExecute = canExecute;
            this.beforeExecute = beforeExecute;
            this.afterExecute = afterExecute;
        }


        public override bool CanExecute(object parameter)
        {
            return canExecute?.Invoke(parameter) ?? true;
        }
        protected override void BeforeExecute(object parameter)
        {
            beforeExecute?.Invoke(parameter);
        }
        protected override void AfterExecute(object parameter, Exception error)
        {
            afterExecute?.Invoke(parameter, error);
        }
        protected override void OnExecute(object parameter)
        {
            if(!canExecute?.Invoke(parameter) ?? true)
                onExecute.Invoke(parameter);
        }

    }
}
