# File Transfer Application

A WPF application that allows users to transfer files between devices connected to the same Wi-Fi network. The application consists of two parts:

- **FileTransferServer**: A server application that listens for incoming file transfers and saves the received files to a specified location.
- **FileTransferClient**: A client application that allows users to select files and send them to the server over the network.

---

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
  - [Running the Server Application](#running-the-server-application)
  - [Running the Client Application](#running-the-client-application)
  - [Testing on One Device](#testing-on-one-device)
- [Error Handling](#error-handling)
- [Enhancements](#enhancements)
- [License](#license)
- [Author](#author)
- [GitHub Repository](#github-repository)

---

## Features

- **File Transfer over Wi-Fi**: Transfer files between devices on the same Wi-Fi network.
- **Custom Save Location**: Server can specify where to save received files.
- **Enhanced Error Handling**: Provides detailed error messages for troubleshooting.
- **Large File Support**: Handles large files efficiently by reading and writing data in chunks.

---

## Prerequisites

- **Operating System**: Windows 7 or later.
- **Development Environment**: Visual Studio 2019 or later.
- **.NET Framework**: Version 4.7.2 or later.
- **Basic Knowledge**: Familiarity with C# and WPF is helpful but not required.

---

## Installation

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/alphadevking/file-transfer-application.git
   ```

2. **Open the Solution**:

   - Navigate to the cloned repository.
   - Open the `FileTransferApplication.sln` solution file in Visual Studio.

3. **Restore NuGet Packages**:

   - Visual Studio should automatically restore any required NuGet packages.
   - If not, go to **Tools** > **NuGet Package Manager** > **Manage NuGet Packages for Solution** and restore them manually.

---

## Usage

### Running the Server Application

1. **Build the Server Project**:

   - In Visual Studio, right-click on the `FileTransferServer` project and select **Build**.

2. **Run the Server Application**:

   - Start the `FileTransferServer` application.
   - The server will display the default save location (Desktop).
   - Use the **Browse** button to select a different save location if desired.
   - The server starts listening on port `5000`.

3. **Firewall Settings**:

   - Ensure that your firewall allows the server application to accept incoming connections on port `5000`.
   - Add an exception in your firewall settings if necessary.

### Running the Client Application

1. **Build the Client Project**:

   - In Visual Studio, right-click on the `FileTransferClient` project and select **Build**.

2. **Run the Client Application**:

   - Start the `FileTransferClient` application.
   - Enter the server's IP address in the **Server IP Address** field.
     - Use `127.0.0.1` or `localhost` if testing on the same machine.
     - For different devices, use the server machine's local IP address (e.g., `192.168.x.x`).
   - Click the **Select File** button to choose a file to send.
   - The status message will indicate whether the file was sent successfully.

3. **Firewall Settings**:

   - Ensure that your firewall allows the client application to make outgoing connections on port `5000`.

### Testing on One Device

- **Running Both Applications**:

  - You can run both the server and client applications on the same machine for testing.
  - Use `127.0.0.1` or `localhost` as the server IP address in the client application.

- **Multiple Startup Projects in Visual Studio**:

  - Set both projects to start simultaneously:
    - Right-click on the **Solution** in Solution Explorer.
    - Select **Properties**.
    - Under **Common Properties**, choose **Startup Project**.
    - Select **Multiple startup projects** and set both to **Start**.

---

## Error Handling

- **Enhanced Exception Handling**:

  - The applications include detailed exception handling to provide informative error messages.
  - Common errors such as network issues, invalid IP addresses, and file I/O errors are handled gracefully.

- **Common Error Messages**:

  - **"No such host is known"**: Indicates that the client cannot resolve the server IP address.
    - **Solution**: Verify the server IP address and ensure the server is running.

  - **"Socket Error"**: Indicates a network communication issue.
    - **Solution**: Check network connectivity and firewall settings.

---

## Enhancements

- **Progress Reporting**:

  - Future updates may include progress bars to show file transfer progress.

- **Security Features**:

  - Implementing authentication and encryption for secure file transfers.

- **User Interface Improvements**:

  - Enhancing the UI for better user experience.

- **Multi-File Transfer**:

  - Adding the ability to select and send multiple files at once.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## Author

### Favour Orukpe

- GitHub: [alphadevking](https://github.com/alphadevking/)
- LinkedIn: [Favour Orukpe](https://www.linkedin.com/in/favour-orukpe/)

---

## GitHub Repository

Find the repository at: [https://github.com/alphadevking/file-transfer-application](https://github.com/alphadevking/file-transfer-application)

---
