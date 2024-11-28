using System.Management;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace ConnectedDeviceManager_02._10_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadDevices();
        }

        private void LoadDevices()
        {
            listView1.Items.Clear();

            using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity");

            foreach (ManagementObject obj in searcher.Get())
            {
                var description = obj["Description"]?.ToString() ?? "Неизвестно";
                var name = obj["Name"]?.ToString() ?? "Неизвестно";
                var status = obj["Status"]?.ToString() ?? "Неизвестно";
                var deviceId = obj["DeviceID"]?.ToString() ?? "Неизвестно";
                string model = "Неизвестно";
                try
                {
                    if (obj["Model"] != null)
                    {
                        model = obj["Model"].ToString();
                    }
                }
                catch
                {
                    model = "Неизвестно";  
                }
                var type = obj["Caption"] != null ? obj["Caption"].ToString() : "Неизвестно";

                ListViewItem item = new ListViewItem(new[] { type, name, status, model, deviceId });
                listView1.Items.Add(item);
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDevices();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var deviceId = listView1.SelectedItems[0].SubItems[4].Text;
                Console.WriteLine("Выбранное устройство ID: " + deviceId);  
                DisplayDeviceDetails(deviceId);
            }
        }


        private void DisplayDeviceDetails(string deviceId)
        {
            string query = $"SELECT * FROM Win32_PnPEntity WHERE DeviceID = '{deviceId.Replace("\\", "\\\\")}'";

            using var searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject obj in searcher.Get())
            {
                string name = obj["Name"]?.ToString() ?? "Неизвестно";
                string deviceIdText = obj["DeviceID"]?.ToString() ?? "Неизвестно";
                string status = obj["Status"]?.ToString() ?? "Неизвестно";
                string description = obj["Description"]?.ToString() ?? "Неизвестно";
                string manufacturer = obj["Manufacturer"]?.ToString() ?? "Неизвестно";
                string model = "Неизвестно";
                try
                {
                    if (obj["Model"] != null)
                    {
                        model = obj["Model"].ToString();
                    }
                }
                catch
                {
                    model = "Неизвестно"; 
                }
                string caption = obj["Caption"]?.ToString() ?? "Неизвестно";

                richTextBox1.Text = $"Имя: {name}\nИдентификатор: {deviceIdText}\nСтатус: {status}\nОписание: {description}\n" +
                    $"Производитель: {manufacturer}\nМодель: {model}\nТип устройства: {caption}\n";
            }
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var deviceId = listView1.SelectedItems[0].SubItems[4].Text;
                DisableDevice(deviceId);
            }
        }

        private void DisableDevice(string deviceId)
        {
            var confirmResult = MessageBox.Show("Вы уверены, что хотите отключить это устройство?", "Подтверждение", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using var searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_PnPEntity WHERE DeviceID = '{deviceId.Replace("\\", "\\\\")}'");
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        obj.InvokeMethod("Disable", null); // не для всех устройств работает
                    }
                    MessageBox.Show("Устройство успешно отключено.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка отключения устройства: {ex.Message}");
                }
            }
        }
    }
}
