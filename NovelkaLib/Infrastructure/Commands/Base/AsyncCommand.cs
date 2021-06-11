using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovelkaCreationTool.Infrastructure.Commands.Base
{
    /// <summary>
    /// Implementation of <c>ICommand</c> that allows for asynchronous operation.
    /// </summary>
    public class AsyncCommand : ICommand
    {
        /// <summary>
        /// Raises the <c>CanExecuteChanged</c> event for the command.
        /// </summary>
        /// <remarks>This method should be invoked whenever the 
        /// returned value of <c>CanExecute</c> changes.</remarks>
        protected void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null) CanExecuteChanged(this, new EventArgs());
        }

        /// <summary>
        /// When overridden in a derived class, performs operations in the UI thread
        /// before beginning the background operation.
        /// </summary>
        /// <param name="parameter">The parameter passed to the 
        /// <c>Execute</c> method of the command.</param>
        protected virtual void BeforeExecute(object parameter) { }

        /// <summary>
        /// When overridden in a derived class, performs operations in a background
        /// thread when the <c>Execute</c> method is invoked.
        /// </summary>
        /// <param name="parameter">The parameter passed to the 
        /// <c>Execute</c> method of the command.</param>
        protected virtual void OnExecute(object parameter) { }

        /// <summary>
        /// When overridden in a derived class, performs operations when the
        /// background execution has completed.
        /// </summary>
        /// <param name="parameter">The parameter passed to the 
        /// <c>Execute</c> method of the command.</param>
        /// <param name="error">The error object that was thrown 
        /// during the background operation, or null if no error was thrown.</param>
        protected virtual void AfterExecute(object parameter, Exception error) { }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;


        /// <summary>
        /// When overridden in a derived class, defines the method that 
        /// determines whether the command can execute in its
        /// current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        /// <returns>True if this command can be executed; 
        /// otherwise, false.</returns>
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Runs the command method in a background thread.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            BeforeExecute(parameter);

            var bgw = new BackgroundWorker();

            bgw.DoWork += (s, e) =>
            {
                OnExecute(parameter);
            };
            bgw.RunWorkerCompleted += (s, e) =>
            {
                AfterExecute(parameter, e.Error);
            };
            bgw.RunWorkerAsync();
        }
    }
}
