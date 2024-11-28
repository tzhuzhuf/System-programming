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
                var description = obj["Description"]?.ToString() ?? "����������";
                var name = obj["Name"]?.ToString() ?? "����������";
                var status = obj["Status"]?.ToString() ?? "����������";
                var deviceId = obj["DeviceID"]?.ToString() ?? "����������";
                string model = "����������";
                try
                {
                    if (obj["Model"] != null)
                    {
                        model = obj["Model"].ToString();
                    }
                }
                catch
                {
                    model = "����������";  
                }
                var type = obj["Caption"] != null ? obj["Caption"].ToString() : "����������";

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
                Console.WriteLine("��������� ���������� ID: " + deviceId);  
                DisplayDeviceDetails(deviceId);
            }
        }


        private void DisplayDeviceDetails(string deviceId)
        {
            string query = $"SELECT * FROM Win32_PnPEntity WHERE DeviceID = '{deviceId.Replace("\\", "\\\\")}'";

            using var searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject obj in searcher.Get())
            {
                string name = obj["Name"]?.ToString() ?? "����������";
                string deviceIdText = obj["DeviceID"]?.ToString() ?? "����������";
                string status = obj["Status"]?.ToString() ?? "����������";
                string description = obj["Description"]?.ToString() ?? "����������";
                string manufacturer = obj["Manufacturer"]?.ToString() ?? "����������";
                string model = "����������";
                try
                {
                    if (obj["Model"] != null)
                    {
                        model = obj["Model"].ToString();
                    }
                }
                catch
                {
                    model = "����������"; 
                }
                string caption = obj["Caption"]?.ToString() ?? "����������";

                richTextBox1.Text = $"���: {name}\n�������������: {deviceIdText}\n������: {status}\n��������: {description}\n" +
                    $"�������������: {manufacturer}\n������: {model}\n��� ����������: {caption}\n";
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
            var confirmResult = MessageBox.Show("�� �������, ��� ������ ��������� ��� ����������?", "�������������", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using var searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_PnPEntity WHERE DeviceID = '{deviceId.Replace("\\", "\\\\")}'");
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        obj.InvokeMethod("Disable", null); // �� ��� ���� ��������� ��������
                    }
                    MessageBox.Show("���������� ������� ���������.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"������ ���������� ����������: {ex.Message}");
                }
            }
        }
    }
}
