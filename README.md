
# VoidENV PATH Manager 

## Overview
![image](https://github.com/user-attachments/assets/b0a23042-5cdc-47f4-881f-6a6acdf9c45e)

VoidENV PATH Manager is a simple yet powerful console application written in C#. It allows users to manage both User and System PATH environment variables on Windows. With an easy-to-use interface, users can add, edit, check, or remove directories from their PATH.

## Features

- **Manage User and System PATH**: Effortlessly navigate and modify PATH entries for both user and system.
- **Check Directory Existence**: Verify if a specific directory exists in the current PATH.
- **Add New Directories**: Add new directories to the PATH with simple prompts.
- **User-Friendly Interface**: Navigate using arrow keys and interact with options seamlessly.

## Prerequisites

- Windows Operating System
- .NET SDK
  
## Installation

1. **Clone the repository** or download the source code.
   
   ```bash
   git clone https://github.com/xptea/VoidENV
   ```

2. **Build the project** using Visual Studio or the .NET CLI.

   ```bash
   dotnet build
   ```

3. **Run the application** from the command line or terminal.

   ```bash
   dotnet run


## Usage

1. Launch the application in the console.
2. Use the arrow keys to navigate the menu and select an option by pressing Enter.
3. Follow the prompts to manage your PATH entries.

### Menu Options

- **Manage Current User PATH**: Navigate and modify the User PATH.
- **Manage System PATH**: Navigate and modify the System PATH (requires administrator privileges).
- **Check if a directory exists in PATH**: Enter a directory path to check its existence in the current PATH.
- **Add new directory to PATH**: Input a directory to add to the User or System PATH.
- **Exit**: Exit the application.

## Important Notes

- **Administrator Privileges**: To modify the System PATH, run the application as an administrator. If you attempt to modify the System PATH without the necessary permissions, you will receive a warning.
- **Backup**: It is recommended to back up your PATH variable before making changes to prevent any unintended issues.

## Contributing

Contributions are welcome! If you have suggestions for improvements or additional features, feel free to fork the repository and submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).


This README file will help users understand how to use your application effectively. If you have any more requests or need further adjustments, let me know!
