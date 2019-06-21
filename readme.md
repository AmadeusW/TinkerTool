# Debug Center
Get real time insight into state of your app. Tweak the behavior of your app in real time.
The Debug Center is built using Blazor and communication is accomplished with SignalR, so that you the diagnostic library is cross platform and language agnostic.

Debug Center facilitates two way communication between a flexible web interface and user's application.
User's application sends diagnostic logs, current value of the variables, etc. The web interface may chart numeric data.
User's application probes the server for up to date value for use. The web interface offers a way for user to modify this data.

## Architecture
| Component | Role |
|---|---|
| **.NET core server:** | |
| User interface | Displaying and modifying data |
| SignalR host | Communication with user app |
|   |   |
| **Client library:** | |
| Network adapter | SignalR communication |
| Cache | Instant access to data from app |
| **Debugged program** | |

# Requirements:
## Server:
dot net core 2.0 SDK and runtime
signalr for asp net core 2 (nuget.config is provided)
## Client:
ability to connect using sockets