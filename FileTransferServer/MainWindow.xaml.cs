using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace FileTransferServer
{
    public partial class MainWindow : Window
    {
        private const int Port = 5000;
        private string saveFolderPath;

        public MainWindow()
        {
            InitializeComponent();
            saveFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Default path
            SaveLocationTextBox.Text = saveFolderPath;
            StartServer();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = saveFolderPath;
                var result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    saveFolderPath = dialog.SelectedPath;
                    SaveLocationTextBox.Text = saveFolderPath;
                }
            }
        }

        private async void StartServer()
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Any, Port);
                listener.Start();
                StatusTextBlock.Text = $"Server started on port {Port}.";

                while (true)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    _ = Task.Run(() => HandleClient(client));
                }
            }
            catch (SocketException ex)
            {
                Dispatcher.Invoke(() =>
                {
                    StatusTextBlock.Text = $"Socket Error: {ex.Message}";
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    StatusTextBlock.Text = $"Error: {ex.Message}";
                });
            }
            finally
            {
                listener?.Stop();
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            try
            {
                using (NetworkStream networkStream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(networkStream))
                {
                    // Read file name length and file name
                    int fileNameLength = reader.ReadInt32();
                    string fileName = new string(reader.ReadChars(fileNameLength));

                    // Read file length and file content
                    long fileLength = reader.ReadInt64();

                    string filePath = Path.Combine(saveFolderPath, fileName);

                    // Read file content in chunks
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = new byte[8192];
                        long totalBytesRead = 0;
                        int bytesRead;

                        while (totalBytesRead < fileLength &&
                               (bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await fs.WriteAsync(buffer, 0, bytesRead);
                            totalBytesRead += bytesRead;
                        }
                    }

                    Dispatcher.Invoke(() =>
                    {
                        StatusTextBlock.Text = $"Received file: {fileName}";
                    });
                }
            }
            catch (IOException ex)
            {
                Dispatcher.Invoke(() =>
                {
                    StatusTextBlock.Text = $"IO Error: {ex.Message}";
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    StatusTextBlock.Text = $"Error: {ex.Message}";
                });
            }
            finally
            {
                client.Close();
            }
        }
    }
}
