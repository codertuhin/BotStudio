using System;


using System.Threading;

using System.Windows;


namespace SmartifyBotStudio
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();

            SetLanguageDictionary();

          

        }


        private void SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();



            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en-US":
                    dict.Source = new Uri("..\\Resources\\en-US.xaml", UriKind.Relative);
                    break;
                case "fr-CA":
                    dict.Source = new Uri("..\\Resources\\fr-FR.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\en-US.xaml", UriKind.Relative);
                    break;
            }


            this.Resources.MergedDictionaries.Add(dict);

        }
    }
}
