# NeuChessHu User Documentation

## 1. Overview

The main goal of the project 'NeuChessHu' is to deliver the very first Hungarian‑origin chess program to the world, available primarily in Hungarian but also internationally in English.

'NeuChessHu' is a chess platform with two main parts:

- **Website**: used for registration, login, account management, profile pages, player statistics, match history, downloads, and administration.
- **Desktop application**: used to play chess matches against other players or against Stockfish, manage in-game settings, chat during matches, and review match results.

The website and desktop application use the same 'NeuChessHu' account. After registration on the website, the same account can be used to log in from the desktop application.

## 2. Website

### 2.1 Welcome page

The welcome page introduces 'NeuChessHu' and provides the main entry point into the platform.

Available actions:

- **Get Started**: opens the sign-in page.
- **Download**: opens a download menu for the desktop application.
- **Language switch**: changes the website language between English and Hungarian.
- **Theme toggle**: switches between light and dark mode.

On mobile screens, these actions are available from the menu button in the navigation bar.

### 2.2 Downloading the desktop app

The website provides Windows desktop downloads from the **Download** menu.

Available versions:

- **Windows x64**
- **Windows ARM64**

Choose the version that matches the computer where the desktop app will be used. Most standard Windows PCs use the x64 version.

### 2.3 Creating an account

Registration is a three-step process.

#### Step 1: Sign Up

Enter:

- email address
- password
- password confirmation

The website checks that:

- the email address has a valid format
- the password is at least the required length
- the password contains both letters and numbers
- the password and confirmation match

After completing the fields, choose **Next**.

#### Step 2: Personal information

Enter profile information:

- nickname
- first name
- last name
- region/country
- date of birth

The nickname is the public name used on the platform. If a nickname is already taken, the website shows an error during registration.

First name, last name, region, and date of birth may be displayed on the user profile depending on the saved profile data.

#### Step 3: Confirmation

Review the registration details and accept the **Terms and Conditions**.

The account cannot be created until the terms are accepted. After confirmation, the website creates the account, logs the user in, and opens the new profile page.

### 2.4 Signing in

Open the sign-in page and enter:

- email address
- password

If the credentials are correct:

- regular users are redirected to their profile page
- admin users are redirected to the administration page

If the credentials are incorrect, the website displays an invalid email or password message.

### 2.5 Signing out

Use the **Log out** button in the navigation bar.

After logout, the website returns to the sign-in page.

### 2.6 Profile page

Each user has a public profile page.

The profile page can show:

- nickname
- profile picture
- first name
- last name
- region
- date of birth
- registration date
- online/offline status
- personal match statistics
- match history
- chess statistics

The match area lists played games, including opponent, result, game mode, date, and end reason where available.

### 2.7 Editing your profile

When viewing your own profile, use the settings/edit icon on the profile card.

Editable profile data includes:

- nickname
- first name
- last name
- region
- date of birth
- profile picture

Profile pictures can be uploaded by selecting or dropping an image file. The website accepts image files and compresses them before saving. Very large images may be rejected after processing.

Use:

- **Save** to apply profile changes
- **Cancel** to discard changes
- **Delete account** to remove the account after confirmation

### 2.8 Deleting an account

From your profile edit mode, choose **Delete account**.

The website asks for confirmation before deleting the account. After deletion, regular users are signed out and returned to the sign-in page.

### 2.9 Match history and statistics

The profile page includes match-related information.

Match history may show:

- opponent
- result: win, loss, or draw
- game mode: Bullet, Blitz, or Rapid
- match date
- match end reason, such as checkmate, timeout, resignation, stalemate, or mutual agreement

Statistics may include:

- favorite game type
- most played duration
- favorite first move
- average winning time
- win/loss/draw charts
- match timeline

If a user has no matches yet, the profile shows that no matches were found.

### 2.10 Admin page

Admin users are sent to the administration page after signing in.

The admin page lists users and allows administrative profile management, including:

- viewing user profile information
- editing user details
- changing profile pictures
- deleting users

The Stockfish system user is hidden from the visible user list.

## 3. Desktop Application

### 3.1 Purpose of the desktop app

The desktop application is the main place to play chess. It supports:

- player vs. player matchmaking
- games against Stockfish
- Bullet, Blitz, and Rapid time controls
- live board interaction
- move notation
- clocks
- captured pieces
- chat during player matches
- draw offers, resignation, aborting early games, and match result screens
- language, theme, board, piece, sound, and auto-promotion settings

### 3.2 Logging in from the desktop app

Most of the desktop features require login.

When login is needed, the app opens the website login flow in a browser. After successful login, the browser redirects back to the desktop app through the NeuChessHu callback.

If a feature is used without being logged in, the app shows a login notification.

### 3.3 Main menu

The main desktop menu shows the chess board preview and match controls.

Main actions:

- **Start Game**: starts matchmaking against another player.
- **Time selector**: opens the time control selection window.
- **More**: expands additional options.
- **Play against Stockfish**: starts a match against the Stockfish chess engine.
- **Settings**: opens application settings.
- **Profile Settings / Logout / Quit**: available from the menu popup.

### 3.4 Choosing a time control

Open the time selector to choose the match duration.

Available categories include:

- **Bullet**
  - 1 min
- **Blitz**
  - 3 min
  - 5 min
- **Rapid**
  - 10 min
  - 15 min

The selected duration is saved and shown on the main menu button.

### 3.5 Starting a match against another player

To start matchmaking:

1. Log in to the desktop app.
2. Choose a time control.
3. Select **Start Game**.
4. Wait in the **Looking for Match** screen.

The looking-for-match screen shows:

- selected match duration
- elapsed search time
- rotating search messages
- an option to stop searching

When an opponent is found, the match starts automatically.

### 3.6 Playing against Stockfish

To play against Stockfish:

1. Open **More** on the main menu.
2. Select **Play against Stockfish**.

Stockfish games use the desktop chess board and match screen. Chat and draw offers are disabled when playing against Stockfish.

### 3.7 Chess board interaction

During a match, the board shows:

- pieces
- board coordinates
- the player orientation, depending on whether the user is White or Black
- current board theme and piece theme

Move pieces by interacting with the board. Illegal moves are rejected by the game logic.

When a pawn reaches the promotion rank, the promotion window appears unless 'Auto-Queen' is enabled.

### 3.8 Pawn promotion

When promotion is required, choose one of:

- queen
- rook
- bishop
- knight

If **Auto-Queen** is enabled in settings, promotion automatically selects a queen.

### 3.9 Match sidebar

The match sidebar shows important live match information:

- player nickname and profile picture
- opponent nickname and profile picture
- player clock
- opponent clock
- captured pieces
- material point difference
- move notation
- chat panel, when available

The notation panel updates as moves are played.

### 3.10 Chat

Chat is available during normal player matches.

To use chat:

1. Open the chat panel from the match sidebar.
2. Type a message.
3. Send the message.

If the chat panel is closed and a new message arrives, the app shows an unread message notification.

Messages are checked for respectful communication. If a message violates the rules, the app shows a warning notification and the message is not accepted.

Chat is not available against Stockfish.

### 3.11 Match options

The match options menu provides match actions.

Depending on the current match state, the main action can be:

- **Abort**: available at the very beginning of a match before the game has properly progressed.
- **Resign**: available after the game has progressed.
- **Quit to menu**: available after the match ends.

Other available actions:

- **Offer Draw**: sends a draw offer to the opponent.
- **Settings**: opens in-game settings.
- **Go back**: closes the options menu.

Draw offers are not shown when playing against Stockfish.

### 3.12 Draw offers

When a draw is offered:

- the opponent receives a draw confirmation prompt
- the opponent can accept or reject the draw
- if accepted, the match ends by mutual agreement

If you offer a draw, the app shows the relevant confirmation state in the match sidebar.

### 3.13 Resigning

Choose **Resign** from the match options menu.

The app asks for confirmation before sending the resignation. If confirmed, the match ends and the opponent wins by resignation.

### 3.14 Match results

At the end of a match, the app shows the result screen.

Possible results include:

- match won
- match lost
- match drawn
- game aborted

Possible end reasons include:

- checkmate
- timeout
- resignation
- mutual agreement
- stalemate
- 50 consecutive moves
- 75 consecutive moves
- insufficient material
- threefold repetition
- fivefold repetition

Available result screen actions:

- **Play again**
- **Quit to menu**
- open player profile
- open opponent profile, except for Stockfish

### 3.15 Application settings

The desktop app settings include:

- **Board Theme**
  - Wooden
  - Modern
  - Royal
- **Piece Theme**
  - Default
- **Language**
  - System
  - English
  - Hungarian
- **Dark Mode**
- **Disable Sounds**
- **Auto-Queen**

Settings are saved automatically and reused the next time the app opens.

### 3.16 Sounds

The desktop app uses sounds for chess events such as:

- match start
- move
- capture
- castle
- check
- promotion
- illegal move
- match end

Use **Disable Sounds** in settings to mute these sounds.

## 4. Terms and Fair Play

NeuChessHu is intended for fair play, practice, learning, and entertainment.

Users must not:

- cheat
- use chess engines during player matches
- use bots or automated help
- receive outside help while playing
- exploit bugs
- manipulate results
- harass or threaten others
- use hate speech, discrimination, spam, or offensive behavior

Chat messages should remain respectful. The app may reject messages that violate communication rules.

## 5. Troubleshooting

### I cannot sign in

Check that:

- the email address is correct
- the password is correct
- the account exists

If the website shows an invalid credentials message, the email or password is not accepted.

### I cannot continue registration

Check the highlighted fields. Common causes:

- invalid email format
- password too short
- password does not contain both letters and numbers
- password confirmation does not match
- missing nickname
- invalid date of birth
- terms and conditions were not accepted

### My nickname is rejected

The nickname may already be taken. Choose a different nickname and try again.

### Profile picture upload does not work

Check that:

- the file is an image
- the image is not too large after processing
- the browser has permission to read the selected file

### I cannot start a desktop match

Check that:

- you are logged in

### Chat is missing

Chat is disabled in matches against Stockfish. It is only available in player vs. player matches.

### Draw offer is missing

Draw offers are disabled against Stockfish. In player matches, use the match options menu.

### The desktop app opens the browser for login

This is expected. The desktop app uses the website login flow and then returns to the app through a callback after successful authentication.

## 6. Quick Start

1. Open the NeuChessHu website.
2. Create an account through the three-step registration flow.
3. Download the Windows desktop application.
4. Open the desktop application.
5. Log in when prompted.
6. Choose a time control.
7. Start matchmaking or choose **Play against Stockfish**.
8. Play the match.
9. Review the result and match statistics on your profile.
