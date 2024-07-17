# TDP

## System Requirements
    
- [Docker](https://docs.docker.com/get-docker/)
- Internet connection.

## Installation

1. Clone the repo: `git clone https://github.com/lucasneuwit/TDP.git`
2. From root directory (TDP/) open a command prompt.
3. Run `install.sh` script from a **git bash**. This will deploy a `sqlserver` instance and the actual application in separated containers. Deploy configuration can be found under `Source/` in the `docker-compose.yml` file.
4. Navigate to [localhost:8080](localhost:8080/) where the application should be running.

## First steps

1. You will be prompted with a registration screen for a new user. First user is always registered with administration privileges.
2. Sign in with your credentials (username and password).
3. From the navigation bar on top, the following sections are available:
    - ***Home*** redirects to the main search screen.
    - ***Watchlist*** lets you check and manage your followed movies.
    - ***Profile*** allows to edit user preferences, including your profile picture.
    - ***Members*** redirects to the members management section where the administrator user can visualize, add and remove members.
    - ***LogOut*** button ends the current session.

## Support

Need help with anything? Get in touch with TDP support:

- lucas.neuwit42@gmail.com
- matiasquaroni98@gmail.com
- santiagoo98p@gmail.com


