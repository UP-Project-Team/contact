using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Contact.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //TODO: move this in XAML
            Closed += MainWindow_Closed;
            Loaded += MainWindow_Loaded;

            //TODO: this not supposed to be here
            // "Login"
            ClientControll.Login();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            ClientControll.Logoff();
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.StartGame();
        }

        private void btnSubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.GiveCurrentWordVariant(txtAnswer.Text);
        }


        #region States Activate Actions 
        private readonly List<Tuple<string, Func<bool>>> StateActivateActionsList = new List<Tuple<string, Func<bool>>>
            {
                new Tuple<string, Func<bool>>("btnStartGame", () => ClientControll.gameState.State==GameService.GameState.State.NotStarted),
                new Tuple<string, Func<bool>>("HaveCurrentWord", () => ClientControll.gameState.State==GameService.GameState.State.HaveCurrentWord),
                new Tuple<string, Func<bool>>("HaveCurrentWordVariant", () => ClientControll.gameState.State==GameService.GameState.State.HaveCurrentWordVariant)
            };
        private readonly List<UIElement> StateControlsList = new List<UIElement>();

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FillStateActions();
        }

        public void gameState_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName=="State")
                UpdateStatesVisibility();
        }

        void UpdateStatesVisibility()
        {
            for (var i = 0; i < StateControlsList.Count; ++i)
            {
                if (StateActivateActionsList[i].Item2())
                    StateControlsList[i].Visibility = Visibility.Visible;
                else
                    StateControlsList[i].Visibility = Visibility.Collapsed;
            }
        }

        private void FillStateActions()
        {
            foreach (var action in StateActivateActionsList)
            {
                var c = FindChild<UIElement>(this, action.Item1);
                if(c==null) throw new Exception("Failed to init State show actions");
                StateControlsList.Add(c);
            }

            UpdateStatesVisibility();
        }

        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                var frameworkElement = child as FrameworkElement;
                if (frameworkElement != null && frameworkElement.Name == childName)
                {
                    // if the child's name is of the request name
                    foundChild = (T) child;
                    break;
                }
                else
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
            }

            return foundChild;
        }

        #endregion
    }
}
