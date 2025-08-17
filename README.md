# Windows 11 Mobile Shell

A Windows 11 Mobile-like shell experience for Windows 10/11 Desktop.

![Windows 11 Mobile Shell](https://i.imgur.com/placeholder.png)

## Features

- **Windows 11 Mobile UI**: Modern interface inspired by Windows 11 and Windows 10 Mobile
- **Live Tiles**: Dynamic tile system with app icons and information
- **Status Bar**: Shows time and system status
- **Navigation Bar**: Quick access to common actions
- **Responsive Design**: Adapts to different screen sizes

## Getting Started

### Prerequisites

- Windows 10 or Windows 11
- Visual Studio 2019 or newer with UWP development tools
- Developer Mode enabled on your Windows device

### Installation

1. Clone this repository
2. Open `src/MobileShellPlus.sln` in Visual Studio
3. Build and run the project

## Project Structure

- **MobSDes**: The main UWP application with the Windows 11 Mobile UI
- **MobileShellPlus**: Core shell functionality
- **MobileShellExtension**: Extension components
- **MobileCoreServer**: Backend services

## Building from Source

The project includes GitHub Actions workflows that automatically build the solution for both x86 and x64 architectures. You can also build it locally using Visual Studio or MSBuild:

```powershell
# Using MSBuild
msbuild src/MobileShellPlus.sln /p:Configuration=Release /p:Platform=x64
```

## Contributing

Contributions are welcome! Feel free to submit pull requests or open issues for bugs and feature requests.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Inspired by Windows 10 Mobile and Windows 11
- Built with UWP (Universal Windows Platform)
