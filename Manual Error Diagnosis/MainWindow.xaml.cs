using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft;
using Newtonsoft.Json;

namespace Manual_Error_Diagnosis {
    public partial class MainWindow : Window {

        struct Device {
            public Device(string name, List<string> errors) {
                Name = name;
                Errors = errors;    
            }
            public string Name;
            public List<string> Errors;
        }
        
        List<Device> deserializedProduct;
        
        public MainWindow() {

            InitializeComponent();
            CB1.SelectedIndex = 0;
            CB2.SelectedIndex = 0;
            List<Device> list = new List<Device>();

            list.Add(new Device("SAR", new List<string> { "SARError1", "SARError2" }));
            list.Add(new Device("SAHR", new List<string> { "SAHRError1", "SAHRError2" }));
            string fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Deviсes.json");
            string output = JsonConvert.SerializeObject(list);
            File.WriteAllText(fileName, output);

            
            deserializedProduct = JsonConvert.DeserializeObject<List<Device>>(File.ReadAllText(fileName)) ?? new List<Device>();

            foreach (var item in deserializedProduct) {
                CB1.Items.Add(item.Name);
            }
        }

        private void CB1_SelectionChanged(object sender, SelectionChangedEventArgs e) {
                CB2.Items.Clear();
            var selectedDevice = deserializedProduct.Single(p => p.Name == (string)CB1.SelectedItem);
            foreach (var error in selectedDevice.Errors) {
                CB2.Items.Add(error);
            }
        }
    }

}

