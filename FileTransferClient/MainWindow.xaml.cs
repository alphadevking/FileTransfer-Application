using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace FileTransferClient
{
    public partial class MainWindow : Window
    {
        private const int Port = 5000;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            string serverIp = ServerIpTextBox.Text.Trim();
            if (string.IsNullOrEmpty(serverIp))
            {
                StatusTextBlock.Text = "Please enter a valid server IP address.";
                return;
            }

            if (!IPAddress.TryParse(serverIp, out IPAddress ipAddress))
            {
                StatusTextBlock.Text = "Invalid IP address format.";
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                await SendFileAsync(serverIp, filePath);
            }
        }

        private async Task SendFileAsync(string serverIp, string filePath)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    await client.ConnectAsync(serverIp, Port);

                    using (NetworkStream networkStream = client.GetStream())
                    using (BinaryWriter writer = new BinaryWriter(networkStream))
                    {
                        string fileName = Path.GetFileName(filePath);
                        byte[] fileNameBytes = System.Text.Encoding.UTF8.GetBytes(fileName);

                        // Send file name length and file name
                        writer.Write(fileNameBytes.Length);
                        writer.Write(fileNameBytes);

                        // Get file length
                        long fileLength = new FileInfo(filePath).Length;
                        writer.Write(fileLength);

                        // Send file content in chunks
                        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            byte[] buffer = new byte[8192];
                            int bytesRead;
                            while ((bytesRead = await fs.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                await networkStream.WriteAsync(buffer, 0, bytesRead);
                            }
                        }

                        StatusTextBlock.Text = $"File sent: {fileName}";
                    }
                }
            }
            catch (SocketException ex)
            {
                StatusTextBlock.Text = $"Socket Error: {ex.Message}";
            }
            catch (IOException ex)
            {
                StatusTextBlock.Text = $"IO Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Error: {ex.Message}";
            }
        }
    }
}
