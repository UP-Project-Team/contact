using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Contact.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void AppStartup(object sender, StartupEventArgs args)
        {
            ConstructStyles();
            var app = new ClientControll();
        }

        private static void ConstructStyles()
        {
            var type = typeof(GameService.GameState.State);
            const string property = "State";


            foreach (var val in Enum.GetNames(type))
            {
                var st = new Style();
                var dt = new DataTrigger
                {
                    Binding = new Binding
                    {
                        Path = new PropertyPath(property)
                    },
                    Value = Enum.Parse(type, val)
                };

                dt.Setters.Add(new Setter()
                {
                    Property = Control.VisibilityProperty,
                    Value = Visibility.Visible
                });

                st.Setters.Add(new Setter()
                {
                    Property = Control.VisibilityProperty,
                    Value = Visibility.Collapsed
                });
                st.Triggers.Add(dt);

                Application.Current.Resources["ShowOn" + val] = st;
            }
        }
    }
}
